using LaTienda.Models;
using LaTienda.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LaTienda.Repository
{
    public class PuntoVentaRepository : IPuntoVentaRepository
    {
        private Context _context;

        public PuntoVentaRepository(Context context)
        {
            _context = context;
        }

        public void Create(PuntoVenta puntoVenta)
        {
            _context.PuntosVenta.Add(puntoVenta);
            SaveChanges();
        }

        public void Delete(Guid id)
        {
            var puntoVenta = Get(id);
            _context.PuntosVenta.Remove(puntoVenta);
            SaveChanges();
        }

        public PuntoVenta Get(Guid id)
        {
            return _context.PuntosVenta.Find(id);
        }

        public List<PuntoVenta> GetAll()
        {
            return _context.PuntosVenta.ToList();
        }

        public bool SaveChanges()
        {
            return _context.SaveChanges() >= 0;
        }

        public void Update(PuntoVenta puntoVenta)
        {
            if (puntoVenta == null)
                throw new ArgumentNullException(nameof(puntoVenta));
            var entry = _context.PuntosVenta.Find(puntoVenta.Id);
            SaveChanges();
        }
    }
}
