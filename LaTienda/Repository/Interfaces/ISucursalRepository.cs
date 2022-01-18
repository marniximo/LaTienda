using LaTienda.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LaTienda.Repository.Interfaces
{
    public interface ISucursalRepository
    {
        List<Sucursal> GetAll();

        Sucursal Get(Guid id);

        void Create(Sucursal sucursal);

        void Update(Sucursal sucursal);

        void Delete(Guid id);

        bool SaveChanges();
    }
}
