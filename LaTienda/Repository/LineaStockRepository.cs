using LaTienda.Models;
using LaTienda.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
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

        public LineaStock GeyByDescripcion(int codigoProducto, Guid talle, Guid color)
        {
            return _context.LineasStock.FirstOrDefault(l => l.IdProducto == codigoProducto && l.IdColor == color && l.IdTalle == talle);
        }

        public List<LineaStock> GetAll()
        {
            return _context.LineasStock.Include(s=>s.Color).Include(s=>s.Talle).ToList();
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
            entry.CodigoSucursal = lineaStock.CodigoSucursal;
            entry.IdTalle = lineaStock.IdTalle;
            entry.Producto = lineaStock.Producto;
            entry.Stock = lineaStock.Stock;
            entry.Sucursal = lineaStock.Sucursal;
            entry.Talle = lineaStock.Talle;
            SaveChanges();
        }

        public List<LineaStock> GetByProducto(int codigoProducto)
        {
            return _context.LineasStock.Where(l => l.IdProducto == codigoProducto).ToList();
        }
    }
}
