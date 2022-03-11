using LaTienda.Models;
using LaTienda.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LaTienda.Repository
{
    public class ProductoRepository : IProductoRepository
    {
        private Context _context;

        public ProductoRepository(Context context)
        {
            _context = context;
        }

        public void Create(Producto producto)
        {
            _context.Productos.Add(producto);
            SaveChanges();
        }

        public void Delete(Guid id)
        {
            var producto = Get(id);
            _context.Productos.Remove(producto);
            SaveChanges();
        }

        public Producto Get(Guid id)
        {
            return _context.Productos.Find(id);
        }

        public List<Producto> GetAll()
        {
            return _context.Productos.ToList();
        }

        public bool SaveChanges()
        {
            return _context.SaveChanges() >= 0;
        }

        public void Update(Producto producto)
        {
            if (producto == null)
                throw new ArgumentNullException(nameof(producto));
            var entry = _context.Productos.Find(producto.Codigo);
            entry.Costo = producto.Costo;
            entry.Descripcion = producto.Descripcion;
            entry.IdMarca = producto.IdMarca;
            entry.LineasStock = producto.LineasStock;
            entry.LineasVenta = producto.LineasVenta;
            entry.Marca = producto.Marca;
            entry.MargenGanancia = producto.MargenGanancia;
            entry.IVA = producto.IVA;
            entry.Precio = producto.Precio;
            SaveChanges();
        }
    }
}
