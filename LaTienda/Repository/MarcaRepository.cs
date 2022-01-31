using LaTienda.Models;
using LaTienda.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LaTienda.Repository
{
    public class MarcaRepository : IMarcaRepository
    {
        private Context _context;

        public MarcaRepository(Context context)
        {
            _context = context;
        }

        public void Create(Marca marca)
        {
            _context.Marcas.Add(marca);
            SaveChanges();
        }

        public void Delete(Guid id)
        {
            var marca = Get(id);
            _context.Marcas.Remove(marca);
            SaveChanges();
        }

        public Marca Get(Guid id)
        {
            return _context.Marcas.Find(id);
        }

        public List<Marca> GetAll()
        {
            return _context.Marcas.ToList();
        }

        public bool SaveChanges()
        {
            return _context.SaveChanges() >= 0;
        }

        public void Update(Marca marca)
        {
            if (marca == null)
                throw new ArgumentNullException(nameof(marca));
            var entry = _context.Marcas.Find(marca.Codigo);
            entry.Descripcion = marca.Descripcion;
            SaveChanges();
        }
    }
}
