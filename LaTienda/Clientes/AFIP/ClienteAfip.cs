using LaTienda.Models;
using LaTienda.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Wsfe;

namespace LaTienda.Clientes.AFIP
{
    public class ClienteAfip : IClienteAfip
    {
        TicketAutenticacion _ticket;
        ILoginTicketRepository _ticketRepository;
        public ClienteAfip(ILoginTicketRepository loginTicketRepository) {
            _ticketRepository = loginTicketRepository;
        }

        public async Task<TicketAutenticacion> GetLoginTicket() 
        {
            var dbTicket = _ticketRepository.GetLastTicket();
            if (dbTicket == null || DateTime.Now > dbTicket.ExpirationTime)
            {
                var objTicketRespuesta = new LoginTicket();
                var response = await objTicketRespuesta.ObtenerLoginTicketResponse();
                var newTicket = new TicketAutenticacion
                {
                    ExpirationTime = objTicketRespuesta.ExpirationTime,
                    GenerationTime = objTicketRespuesta.GenerationTime,
                    Id = Guid.NewGuid(),
                    Service = objTicketRespuesta.Service,
                    Sign = objTicketRespuesta.Sign,
                    Token = objTicketRespuesta.Token
                };
                _ticket = newTicket;
                _ticketRepository.Create(newTicket);
                return newTicket;
            }
            else
            {
                _ticket = dbTicket;
                return dbTicket;
            }
        }

        public async Task<FECAESolicitarResponse> EnviarFactura(Venta venta) 
        {
            if (_ticket == null || DateTime.Now > _ticket.ExpirationTime) {
                await GetLoginTicket();
            }
            var response = await FacturasAfip.EnviarFactura(_ticket, venta);
            return response;
        }

        public async Task<FECAESolicitarResponse> GetUltimaFactura(Venta venta)
        {
            if (_ticket == null || DateTime.Now > _ticket.ExpirationTime)
            {
                await GetLoginTicket();
            }
            var response = await FacturasAfip.EnviarFactura(_ticket, venta);
            return response;
        }

        public async Task<List<TipoFactura>> GetTiposFactura() {
            if (_ticket == null || DateTime.Now > _ticket.ExpirationTime)
            {
                await GetLoginTicket();
            }
            return await FacturasAfip.GetTiposFactura(_ticket);
        }

        public async Task<List<PuntoVenta>> GetPuntosVenta()
        {
            if (_ticket == null || DateTime.Now > _ticket.ExpirationTime)
            {
                await GetLoginTicket();
            }
            return await FacturasAfip.GetPuntosVenta(_ticket);
        }
    }
}
