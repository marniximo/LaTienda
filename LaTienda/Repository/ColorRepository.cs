using LaTienda.Models;
using LaTienda.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LaTienda.Repository
{
    public class ColorRepository : IColorRepository
    {
        private Context _context;

        public ColorRepository(Context context)
        {
            _context = context;
        }

        public void Create(Color color)
        {
            _context.Colores.Add(color);
            SaveChanges();
        }

        public void Delete(Guid id)
        {
            var color = Get(id);
            _context.Colores.Remove(color);
            SaveChanges();
        }

        public Color Get(Guid id)
        {
            return _context.Colores.Find(id);
        }

        public List<Color> GetAll()
        {
            return _context.Colores.ToList();
        }

        public bool SaveChanges()
        {
            return _context.SaveChanges() >= 0;
        }

        public void Update(Color color)
        {
            if (color == null)
                throw new ArgumentNullException(nameof(color));
            var entry = _context.Colores.Find(color.Codigo);
            entry.Descripcion = color.Descripcion;
            SaveChanges();
        }
    }
}
