using LaTienda.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LaTienda.Repository.Interfaces
{
    public interface IUsuarioRepository
    {
        List<Usuario> GetAll();

        Usuario Get(Guid id);

        void Create(Usuario usuario);

        void Update(Usuario usuario);

        void Delete(Guid id);

        bool SaveChanges();
    }
}
