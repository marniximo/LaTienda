using LaTienda.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LaTienda.Repository.Interfaces
{
    public interface IColorRepository
    {
        List<Color> GetAll();

        Color Get(Guid id);

        void Create(Color color);

        void Update(Color color);

        void Delete(Guid id);

        bool SaveChanges();
    }
}
