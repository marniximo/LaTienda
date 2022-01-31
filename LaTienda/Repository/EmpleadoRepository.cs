using LaTienda.Models;
using LaTienda.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LaTienda.Repository
{
    public class EmpleadoRepository : IEmpleadoRepository
    {
        private Context _context;

        public EmpleadoRepository(Context context)
        {
            _context = context;
        }

        public void Create(Empleado empleado)
        {
            _context.Empleados.Add(empleado);
            SaveChanges();
        }

        public void Delete(Guid id)
        {
            var empleado = Get(id);
            _context.Empleados.Remove(empleado);
            SaveChanges();
        }

        public Empleado Get(Guid id)
        {
            return _context.Empleados.Find(id);
        }

        public List<Empleado> GetAll()
        {
            return _context.Empleados.ToList();
        }

        public bool SaveChanges()
        {
            return _context.SaveChanges() >= 0;
        }

        public void Update(Empleado empleado)
        {
            if (empleado == null)
                throw new ArgumentNullException(nameof(empleado));
            var entry = _context.Empleados.Find(empleado.Id);
            SaveChanges();
        }
    }
}
