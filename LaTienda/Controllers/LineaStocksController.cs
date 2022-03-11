using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using LaTienda.Models;
using Microsoft.AspNetCore.Authorization;

namespace LaTienda.Controllers
{
    [Authorize]
    public class LineaStocksController : Controller
    {
        private readonly Context _context;

        public LineaStocksController(Context context)
        {
            _context = context;
        }

        // GET: LineaStocks
        public async Task<IActionResult> Index()
        {
            var context = _context.LineasStock.Include(l => l.Color).Include(l => l.Producto).Include(l => l.Sucursal).Include(l => l.Talle);
            return View(await context.ToListAsync());
        }

        // GET: LineaStocks/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var lineaStock = await _context.LineasStock
                .Include(l => l.Color)
                .Include(l => l.Producto)
                .Include(l => l.Sucursal)
                .Include(l => l.Talle)
                .FirstOrDefaultAsync(m => m.Codigo == id);
            if (lineaStock == null)
            {
                return NotFound();
            }

            return View(lineaStock);
        }

        // GET: LineaStocks/Create
        public IActionResult Create()
        {
            ViewData["IdColor"] = new SelectList(_context.Colores, "Codigo", "Descripcion");
            ViewData["IdProducto"] = new SelectList(_context.Productos, "Codigo", "Descripcion");
            ViewData["IdSucursal"] = new SelectList(_context.Sucursales, "Codigo", "Codigo");
            ViewData["IdTalle"] = new SelectList(_context.Talles, "Codigo", "Descripcion");
            return View();
        }

        // POST: LineaStocks/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Codigo,IdProducto,IdColor,IdTalle,CodigoSucursal,Stock")] LineaStock lineaStock)
        {
            if (ModelState.IsValid)
            {
                lineaStock.Codigo = Guid.NewGuid();
                _context.Add(lineaStock);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdColor"] = new SelectList(_context.Colores, "Codigo", "Descripcion", lineaStock.IdColor);
            ViewData["IdProducto"] = new SelectList(_context.Productos, "Codigo", "Descripcion", lineaStock.IdProducto);
            ViewData["IdSucursal"] = new SelectList(_context.Sucursales, "Codigo", "Codigo", lineaStock.CodigoSucursal);
            ViewData["IdTalle"] = new SelectList(_context.Talles, "Codigo", "Descripcion", lineaStock.IdTalle);
            return View(lineaStock);
        }

        // GET: LineaStocks/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var lineaStock = await _context.LineasStock.FindAsync(id);
            if (lineaStock == null)
            {
                return NotFound();
            }
            ViewData["IdColor"] = new SelectList(_context.Colores, "Codigo", "Descripcion", lineaStock.IdColor);
            ViewData["IdProducto"] = new SelectList(_context.Productos, "Codigo", "Descripcion", lineaStock.IdProducto);
            ViewData["IdSucursal"] = new SelectList(_context.Sucursales, "Codigo", "Codigo", lineaStock.CodigoSucursal);
            ViewData["IdTalle"] = new SelectList(_context.Talles, "Codigo", "Descripcion", lineaStock.IdTalle);
            return View(lineaStock);
        }

        // POST: LineaStocks/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Codigo,IdProducto,IdColor,IdTalle,CodigoSucursal,Stock")] LineaStock lineaStock)
        {
            if (id != lineaStock.Codigo)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(lineaStock);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LineaStockExists(lineaStock.Codigo))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdColor"] = new SelectList(_context.Colores, "Codigo", "Descripcion", lineaStock.IdColor);
            ViewData["IdProducto"] = new SelectList(_context.Productos, "Codigo", "Descripcion", lineaStock.IdProducto);
            ViewData["IdSucursal"] = new SelectList(_context.Sucursales, "Codigo", "Codigo", lineaStock.CodigoSucursal);
            ViewData["IdTalle"] = new SelectList(_context.Talles, "Codigo", "Descripcion", lineaStock.IdTalle);
            return View(lineaStock);
        }

        // GET: LineaStocks/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var lineaStock = await _context.LineasStock
                .Include(l => l.Color)
                .Include(l => l.Producto)
                .Include(l => l.Sucursal)
                .Include(l => l.Talle)
                .FirstOrDefaultAsync(m => m.Codigo == id);
            if (lineaStock == null)
            {
                return NotFound();
            }

            return View(lineaStock);
        }

        // POST: LineaStocks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var lineaStock = await _context.LineasStock.FindAsync(id);
            _context.LineasStock.Remove(lineaStock);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool LineaStockExists(Guid id)
        {
            return _context.LineasStock.Any(e => e.Codigo == id);
        }
    }
}
