using LaTienda.Models;
using LaTienda.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LaTienda.Repository
{
    public class VentaRepository : IVentaRepository
    {
        private Context _context;

        public VentaRepository(Context context)
        {
            _context = context;
        }

        public void Create(Venta venta)
        {
            _context.Ventas.Add(venta);
            SaveChanges();
        }

        public void Delete(Guid id)
        {
            var venta = Get(id);
            _context.Ventas.Remove(venta);
            SaveChanges();
        }

        public Venta Get(Guid id)
        {
            return _context.Ventas.Find(id);
        }

        public List<Venta> GetAll()
        {
            return _context.Ventas.Include(v=>v.Cliente).Include(v=>v.Vendedor).ToList();
        }

        public bool SaveChanges()
        {
            return _context.SaveChanges() >= 0;
        }

        public void Update(Venta venta)
        {
            if (venta == null)
                throw new ArgumentNullException(nameof(venta));
            var entry = _context.Ventas.Find(venta.Codigo);
            SaveChanges();
        }
    }
}
