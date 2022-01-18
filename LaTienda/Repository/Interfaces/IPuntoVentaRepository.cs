using LaTienda.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LaTienda.Repository.Interfaces
{
    public interface IPuntoVentaRepository
    {
        List<PuntoVenta> GetAll();

        PuntoVenta Get(Guid id);

        void Create(PuntoVenta pdv);

        void Update(PuntoVenta pdv);

        void Delete(Guid id);

        bool SaveChanges();
    }
}
