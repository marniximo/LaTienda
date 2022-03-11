using LaTienda.Models;
using LaTienda.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LaTienda.Repository
{
    public class SucursalRepository : ISucursalRepository
    {
        private Context _context;

        public SucursalRepository(Context context)
        {
            _context = context;
        }

        public void Create(Sucursal sucursal)
        {
            _context.Sucursales.Add(sucursal);
            SaveChanges();
        }

        public void Delete(Guid id)
        {
            var sucursal = Get(id);
            _context.Sucursales.Remove(sucursal);
            SaveChanges();
        }

        public Sucursal Get(Guid id)
        {
            return _context.Sucursales.Find(id);
        }

        public List<Sucursal> GetAll()
        {
            return _context.Sucursales.ToList();
        }

        public bool SaveChanges()
        {
            return _context.SaveChanges() >= 0;
        }

        public void Update(Sucursal sucursal)
        {
            if (sucursal == null)
                throw new ArgumentNullException(nameof(sucursal));
            var entry = _context.Sucursales.Find(sucursal.Codigo);
            entry.Descripcion = sucursal.Descripcion;
            SaveChanges();
        }
    }
}
