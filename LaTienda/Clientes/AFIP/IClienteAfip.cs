using LaTienda.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Wsfe;

namespace LaTienda.Clientes.AFIP
{
    public interface IClienteAfip
    {
        Task<TicketAutenticacion> GetLoginTicket();
        Task<FECAESolicitarResponse> EnviarFactura(Venta venta);
        Task<List<TipoFactura>> GetTiposFactura();
        Task<List<PuntoVenta>> GetPuntosVenta();
    }
}
