using LaTienda.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LaTienda.Repository.Interfaces
{
    public interface IProductoRepository
    {
        List<Producto> GetAll();

        Producto Get(Guid id);

        void Create(Producto prdocuto);

        void Update(Producto prdocuto);

        void Delete(Guid id);

        bool SaveChanges();
    }
}
