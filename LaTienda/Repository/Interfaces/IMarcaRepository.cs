using LaTienda.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LaTienda.Repository.Interfaces
{
    public interface IMarcaRepository
    {
        List<Marca> GetAll();

        Marca Get(Guid id);

        void Create(Marca marca);

        void Update(Marca marca);

        void Delete(Guid id);

        bool SaveChanges();
    }
}
