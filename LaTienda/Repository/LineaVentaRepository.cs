using LaTienda.Models;
using LaTienda.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LaTienda.Repository
{
    public class LineaVentaRepository : ILineaVentaRepository
    {
        private Context _context;

        public LineaVentaRepository(Context context)
        {
            _context = context;
        }

        public void Create(LineaVenta lineaVenta)
        {
            _context.LineasVenta.Add(lineaVenta);
            SaveChanges();
        }

        public void Delete(Guid id)
        {
            var lineaVenta = Get(id);
            _context.LineasVenta.Remove(lineaVenta);
            SaveChanges();
        }

        public LineaVenta Get(Guid id)
        {
            return _context.LineasVenta.Find(id);
        }

        public List<LineaVenta> GetAll()
        {
            return _context.LineasVenta.ToList();
        }

        public bool SaveChanges()
        {
            return _context.SaveChanges() >= 0;
        }

        public void Update(LineaVenta lineaVenta)
        {
            if (lineaVenta == null)
                throw new ArgumentNullException(nameof(lineaVenta));
            var entry = _context.LineasVenta.Find(lineaVenta.Codigo);
            entry.Cantidad = lineaVenta.Cantidad;
            entry.IdProducto = lineaVenta.IdProducto;
            entry.IdVenta = lineaVenta.IdVenta;
            entry.PrecioUnitario = lineaVenta.PrecioUnitario;
            entry.Producto = lineaVenta.Producto;
            entry.Subtotal = lineaVenta.Subtotal;
            entry.Venta = lineaVenta.Venta;
            SaveChanges();
        }
    }
}
