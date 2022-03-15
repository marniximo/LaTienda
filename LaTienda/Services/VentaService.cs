using LaTienda.Clientes.AFIP;
using LaTienda.Models;
using LaTienda.Repository.Interfaces;
using LaTienda.Services.Interfaces;
using LaTienda.Services.Resultados;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LaTienda.Services
{
    public class VentaService : IVentaService
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

        public VentaService(
            IVentaRepository ventaRepository,
            IClienteRepository clienteRepository,
            IProductoRepository productoRepository,
            IEmpleadoRepository empleadoRepository,
            IClienteAfip clienteAfip,
            ILoginTicketRepository loginTicketRepository,
            ITalleRepository talleRepository,
            IColorRepository colorRepository,
            ILineaStockRepository lineaStockRepository,
            ILineaVentaRepository lineaVentaRepository
        ) {
            _ventaRepository = ventaRepository;
            _clienteRepository = clienteRepository;
            _productoRepository = productoRepository;
            _empleadoRepository = empleadoRepository;
            _clienteAfip = clienteAfip;
            _loginTicketRepository = loginTicketRepository;
            _talleRepository = talleRepository;
            _colorRepository = colorRepository;
            _lineaStockRepository = lineaStockRepository;
            _lineaVentaRepository = lineaVentaRepository;
        }

        public async Task<ResultadoCreacion> CrearVenta(
            string legajoEmpleado,
            List<ItemCarrito> carrito,
            long CUIT
        )
        {
            var empleado = _empleadoRepository.GetByLegajo(legajoEmpleado);
            if (!Venta.ValidarCarrito(carrito))
            {
                return new ResultadoCreacion { CodigoError = 2 };
            }
            Venta venta = new Venta
            {
                CUITCliente = CUIT,
                Cliente = _clienteRepository.Get(CUIT),
                Codigo = Guid.NewGuid(),
                Fecha = DateTime.Now,
                IdVendedor = empleado.Id,
                NetoGravado = 0,
                IVA = 0,
            };
            carrito.ForEach(c =>
            {
                c.LineaVenta.Codigo = Guid.NewGuid();
                c.LineaVenta.IdVenta = venta.Codigo;
                venta.CalcularCarrito(c);
            });
            if (!venta.ValidarCliente())
            {
                return new ResultadoCreacion { CodigoError = 1 };
            }
            _ventaRepository.Create(venta);
            carrito.ForEach(c => {
                var stock = _lineaStockRepository.Get(c.LineaStock.Codigo);
                stock.Stock -= c.LineaVenta.Cantidad;
                _lineaStockRepository.Update(stock);
                _lineaVentaRepository.Create(c.LineaVenta);
            });
            await venta.RegistrarAfip(_clienteAfip);
            return new ResultadoCreacion { Venta = venta };
        }
    }
}
