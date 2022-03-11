using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using LaTienda.Models;
using LaTienda.Clientes.AFIP;
using LaTienda.Repository.Interfaces;
using Microsoft.Extensions.Logging;
using System.IO;
using System.Text;
using LaTienda.Controllers.Requests;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.ViewEngines;
using Microsoft.AspNetCore.Mvc.ViewFeatures;

namespace LaTienda.Controllers
{
    [Authorize]
    public class VentasController : Controller
    {
        private IClienteRepository _clienteRepository;
        private IVentaRepository _ventaRepository;
        private IProductoRepository _productoRepository;
        private IClienteAfip _clienteAfip;
        private ILoginTicketRepository _loginTicketRepository;
        private IEmpleadoRepository _empleadoRepository;
        private ITalleRepository _talleRepository;
        private IColorRepository _colorRepository;
        private ILineaStockRepository _lineaStockRepository;
        private ILineaVentaRepository _lineaVentaRepository;
        private readonly ILogger<HomeController> _logger;

        public VentasController(
            IVentaRepository ventaRepository,
            IClienteRepository clienteRepository,
            IProductoRepository productoRepository,
            IEmpleadoRepository empleadoRepository,
            ILogger<HomeController> logger,
            IClienteAfip clienteAfip,
            ILoginTicketRepository loginTicketRepository,
            ITalleRepository talleRepository,
            IColorRepository colorRepository,
            ILineaStockRepository lineaStockRepository,
            ILineaVentaRepository lineaVentaRepository
            )
        {  
            _ventaRepository = ventaRepository;
            _clienteRepository = clienteRepository;
            _productoRepository = productoRepository;
            _empleadoRepository = empleadoRepository;
            _logger = logger;
            _clienteAfip = clienteAfip;
            _loginTicketRepository = loginTicketRepository;
            _talleRepository = talleRepository;
            _colorRepository = colorRepository;
            _lineaStockRepository = lineaStockRepository;
            _lineaVentaRepository = lineaVentaRepository;
        }

        // GET: Ventas
        public IActionResult Index()
        {
            var ventas = _ventaRepository.GetAll();
            return View(ventas);
        }

        // GET: Ventas/Details/5
        public IActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var venta = _ventaRepository.GetAll()
                .FirstOrDefault(m => m.Codigo == id);
            if (venta == null)
            {
                return NotFound();
            }

            return View(venta);
        }

        // GET: Ventas/Create
        public async Task<IActionResult> Create()
        {
            ViewData["Talles"] = new SelectList(_talleRepository.GetAll(), "Codigo", "Descripcion");
            ViewData["Colores"] = new SelectList(_colorRepository.GetAll(), "Codigo", "Descripcion");
            ViewData["Productos"] = _productoRepository.GetAll();
            ViewData["Clientes"] = _clienteRepository.GetAll();
            ViewData["Stock"] = _lineaStockRepository.GetAll().Where(s=>s.CodigoSucursal.ToString() == HttpContext.User.Identity.Name.Split(":")[1]);
            return View();
        }

        // GET: Ventas/Comrpobante/5
        public IActionResult Comprobante(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var venta = _ventaRepository.GetAll()
                .FirstOrDefault(m => m.Codigo == id);
            if (venta == null)
            {
                return NotFound();
            }

            var html = RenderViewAsync("ComprobanteVenta", venta);
            var ironPdfRender = new IronPdf.ChromePdfRenderer();
            var pdfDoc = ironPdfRender.RenderHtmlAsPdf(html.Result);
            return File(pdfDoc.Stream.ToArray(), "application/pdf");
        }

        // POST: Ventas/Create
        [HttpPost]
        public async Task<IActionResult> Create([FromBody]VentaCreateRequest request)
        {
            string legajo = HttpContext.User.Identity.Name.Split(":")[1];
            var empleado = _empleadoRepository.GetByLegajo(legajo);
            if (!Venta.ValidarCarrito(request.Carrito)) {
                return BadRequest(new
                {
                    Code = 2,
                    Message = "Carrito invalido"
                });
            }
            Venta venta = new Venta
            {
                CUITCliente = request.CUIT,
                Cliente  = _clienteRepository.Get(request.CUIT),
                Codigo = Guid.NewGuid(),
                Fecha = DateTime.Now,
                IdVendedor = empleado.Id,
                NetoGravado = 0,
                IVA = 0,
            };
            request.Carrito.ForEach(c =>
            {
                c.LineaVenta.Codigo = Guid.NewGuid();
                c.LineaVenta.IdVenta = venta.Codigo;
                venta.CalcularCarrito(c);
            });
            if (!venta.ValidarCliente())
            {
                return BadRequest(new
                {
                    Code = 1,
                    Redirect = "/Clientes/Create"
                });
            }
            if (ModelState.IsValid)
            {
                _ventaRepository.Create(venta);
                request.Carrito.ForEach(c => {
                    var stock = _lineaStockRepository.Get(c.LineaStock.Codigo);
                    stock.Stock -= c.LineaVenta.Cantidad;
                    _lineaStockRepository.Update(stock);
                    _lineaVentaRepository.Create(c.LineaVenta);
                });
                await venta.RegistrarAfip(_clienteAfip);
                return RedirectToAction(nameof(Index));
            }
            ViewData["TipoComprobante"] = new SelectList(await _clienteAfip.GetTiposFactura(), "Id", "Descripcion");
            ViewData["CUITCliente"] = new SelectList(_clienteRepository.GetAll(), "CUIT", "RazonSocial");
            ViewData["Productos"] = _productoRepository.GetAll();
            return View(venta);
        }

        [NonAction]
        private async Task<string> RenderViewAsync<TModel>(string viewName, TModel model, bool partial = false)
        {
            if (string.IsNullOrEmpty(viewName))
            {
                viewName = this.ControllerContext.ActionDescriptor.ActionName;
            }
            this.ViewData.Model = model;
            using (var writer = new StringWriter())
            {
                IViewEngine viewEngine = this.HttpContext.RequestServices.GetService(typeof(ICompositeViewEngine)) as ICompositeViewEngine;
                ViewEngineResult viewResult = viewEngine.FindView(this.ControllerContext, viewName, !partial);
                if (viewResult.Success == false)
                {
                    return $"A view with the name {viewName} could not be found";
                }
                ViewContext viewContext = new ViewContext(this.ControllerContext, viewResult.View, this.ViewData, this.TempData, writer, new HtmlHelperOptions());
                await viewResult.View.RenderAsync(viewContext);
                return writer.GetStringBuilder().ToString();
            }
        }
    }
}
