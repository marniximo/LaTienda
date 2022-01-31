using LaTienda.Models;
using LaTienda.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LaTienda.Repository
{
    public class LineaStockRepository : ILineaStockRepository
    {
        private Context _context;

        public LineaStockRepository(Context context)
        {
            _context = context;
        }

        public void Create(LineaStock lineaStock)
        {
            _context.LineasStock.Add(lineaStock);
            SaveChanges();
        }

        public void Delete(Guid id)
        {
            var lineaStock = Get(id);
            _context.LineasStock.Remove(lineaStock);
            SaveChanges();
        }

        public LineaStock Get(Guid id)
        {
            return _context.LineasStock.Find(id);
        }

        public List<LineaStock> GetAll()
        {
            return _context.LineasStock.ToList();
        }

        public bool SaveChanges()
        {
            return _context.SaveChanges() >= 0;
        }

        public void Update(LineaStock lineaStock)
        {
            if (lineaStock == null)
                throw new ArgumentNullException(nameof(lineaStock));
            var entry = _context.LineasStock.Find(lineaStock.Codigo);
            entry.IdColor = lineaStock.IdColor;
            entry.Color = lineaStock.Color;
            entry.IdProducto = lineaStock.IdProducto;
            entry.IdSucursal = lineaStock.IdSucursal;
            entry.IdTalle = lineaStock.IdTalle;
            entry.Producto = lineaStock.Producto;
            entry.Stock = lineaStock.Stock;
            entry.Sucursal = lineaStock.Sucursal;
            entry.Talle = lineaStock.Talle;
            SaveChanges();
        }
    }
}
