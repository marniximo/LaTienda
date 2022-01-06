using LaTienda.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LaTienda.Repository.Interfaces
{
    public interface IClienteRepository
    {
        List<Cliente> GetAll();

        Cliente Get(Guid id);

        void Create(Cliente cliente);

        void Update(Cliente cliente);

        void Delete(Guid id);

        bool SaveChanges();
    }
}
