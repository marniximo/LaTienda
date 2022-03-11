using LaTienda.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LaTienda.Repository.Interfaces
{
    public interface ILineaStockRepository
    {
        List<LineaStock> GetAll();

        List<LineaStock> GetByProducto(int codigoProducto);

        LineaStock Get(Guid id);

        LineaStock GeyByDescripcion(int codigoProducto, Guid talle, Guid color);

        void Create(LineaStock lineaStock);

        void Update(LineaStock lineaStock);

        void Delete(Guid id);

        bool SaveChanges();
    }
}
