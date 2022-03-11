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

        Cliente Get(long CUIT);

        void Create(Cliente cliente);

        void Update(Cliente cliente);

        void Delete(long CUIT);

        bool SaveChanges();
    }
}
