using LaTienda.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LaTienda.Repository.Interfaces
{
    public interface ILineaVentaRepository
    {
        List<LineaVenta> GetAll();

        LineaVenta Get(Guid id);

        void Create(LineaVenta lineaVenta);

        void Update(LineaVenta lineaVenta);

        void Delete(Guid id);

        bool SaveChanges();
    }
}
