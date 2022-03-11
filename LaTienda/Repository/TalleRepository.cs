using LaTienda.Models;
using LaTienda.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LaTienda.Repository
{
    public class TalleRepository : ITalleRepository
    {
        private Context _context;

        public TalleRepository(Context context)
        {
            _context = context;
        }

        public void Create(Talle talle)
        {
            _context.Talles.Add(talle);
            SaveChanges();
        }

        public void Delete(Guid id)
        {
            var talle = Get(id);
            _context.Talles.Remove(talle);
            SaveChanges();
        }

        public Talle Get(Guid id)
        {
            return _context.Talles.Find(id);
        }

        public List<Talle> GetAll()
        {
            return _context.Talles.ToList();
        }

        public bool SaveChanges()
        {
            return _context.SaveChanges() >= 0;
        }

        public void Update(Talle talle)
        {
            if (talle == null)
                throw new ArgumentNullException(nameof(talle));
            var entry = _context.Talles.Find(talle.Codigo);
            entry.Descripcion = talle.Descripcion;
            SaveChanges();
        }
    }
}
