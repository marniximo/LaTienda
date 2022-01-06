using LaTienda.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LaTienda.Repository.Interfaces
{
    public interface ILoginTicketRepository
    {
        List<TicketAutenticacion> GetAll();

        TicketAutenticacion Get(Guid id);

        TicketAutenticacion GetLastTicket();

        void Create(TicketAutenticacion ticket);

        void Update(TicketAutenticacion ticket);

        void Delete(Guid id);

        bool SaveChanges();
    }
}
