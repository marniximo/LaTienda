using LaTienda.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LaTienda.Repository.Interfaces
{
    public interface IEmpleadoRepository
    {
        List<Empleado> GetAll();

        Empleado Get(Guid id);

        Empleado GetByLegajo(string legajo);

        void Create(Empleado empleado);

        void Update(Empleado empleado);

        void Delete(Guid id);

        bool SaveChanges();
    }
}
