using LaTienda.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LaTienda.Repository.Interfaces
{
    public interface IVentaRepository
    {
        List<Venta> GetAll();

        Venta Get(Guid id);

        void Create(Venta venta);

        void Update(Venta venta);

        void Delete(Guid id);

        bool SaveChanges();
    }
}
