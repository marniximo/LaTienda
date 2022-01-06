using LaTienda.Models;
using LaTienda.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LaTienda.Repository
{
    public class LoginTicketRepository : ILoginTicketRepository
    {
        private Context _context;

        public LoginTicketRepository(Context context) {
            _context = context;
        }

        public void Create(TicketAutenticacion ticket)
        {
            _context.Tickets.Add(ticket);
            SaveChanges();
        }

        public void Delete(Guid id)
        {
            var ticket = Get(id);
            _context.Tickets.Remove(ticket);
            SaveChanges();
        }

        public TicketAutenticacion Get(Guid id)
        {
            return _context.Tickets.Find(id);
        }

        public List<TicketAutenticacion> GetAll()
        {
            return _context.Tickets.ToList();
        }

        public TicketAutenticacion GetLastTicket()
        {
            return _context.Tickets.OrderByDescending(t => t.ExpirationTime).FirstOrDefault();
        }

        public bool SaveChanges()
        {
            return _context.SaveChanges() >= 0;
        }

        public void Update(TicketAutenticacion ticket)
        {
            if (ticket == null)
                throw new ArgumentNullException(nameof(ticket));
            var entry = _context.Tickets.Find(ticket.Id);
            entry.ExpirationTime = ticket.ExpirationTime;
            entry.GenerationTime = ticket.GenerationTime;
            entry.Service = ticket.Service;
            entry.Sign = ticket.Sign;
            entry.Token = ticket.Token;
            SaveChanges();
        }
    }
}
