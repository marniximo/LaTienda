using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LaTienda.Models;
using Wsfe;

namespace LaTienda.Clientes.AFIP
{
    public static class FacturasAfip
    {
        public static async Task<FECAESolicitarResponse> EnviarFactura(TicketAutenticacion ticket, Venta venta) {
            var cliente = new ServiceSoapClient(ServiceSoapClient.EndpointConfiguration.ServiceSoap);
            var authRequest = new FEAuthRequest
            {
                Cuit = venta.CUITCliente,
                Sign = ticket.Sign,
                Token = ticket.Token
            };
            var cmpRequest = new FECAERequest {
                FeCabReq = new FECAECabRequest {
                    CantReg = 1,
                    PtoVta = 1,
                    CbteTipo = venta.TipoComprobante
                }
            };
            var response = await cliente.FECAESolicitarAsync(authRequest, cmpRequest);
            return response;
        }

        public static async Task<List<TipoFactura>> GetTiposFactura(TicketAutenticacion ticket) {
            var cliente = new ServiceSoapClient(ServiceSoapClient.EndpointConfiguration.ServiceSoap);
            var authRequest = new FEAuthRequest
            {
                Sign = ticket.Sign,
                Token = ticket.Token,
                Cuit = 20407268920
            };
            var response = await cliente.FEParamGetTiposCbteAsync(authRequest);
            var list = new List<TipoFactura>();
            foreach (var tipo in response.Body.FEParamGetTiposCbteResult.ResultGet) {
                list.Add(new TipoFactura { 
                    Descripcion = tipo.Desc,
                    Id = tipo.Id
                });
            }
            return list;
        }
    }
}
