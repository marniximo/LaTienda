using LaTienda.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LaTienda.Repository.Interfaces
{
    public interface ITalleRepository
    {
        List<Talle> GetAll();

        Talle Get(Guid id);

        void Create(Talle talle);

        void Update(Talle talle);

        void Delete(Guid id);

        bool SaveChanges();
    }
}
