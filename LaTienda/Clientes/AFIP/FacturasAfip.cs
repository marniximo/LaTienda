using System;
using System.Collections.Generic;
using System.Globalization;
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
            var ultimoCompAutorizado = (await GetUltimaFactura(ticket, venta.TipoComprobante)).Body.FECompUltimoAutorizadoResult;
            var authRequest = new FEAuthRequest
            {
                Cuit = 20407268920,
                Sign = ticket.Sign,
                Token = ticket.Token
            };
            var cmpRequest = new FECAERequest {
                FeCabReq = new FECAECabRequest {
                    CantReg = 1,
                    PtoVta = 9,
                    CbteTipo = venta.TipoComprobante,
                },
                FeDetReq = new FECAEDetRequest[] {
                    new FECAEDetRequest{
                        Concepto = 1, //Productos
                        DocTipo = 80, //CUIT
                        DocNro = venta.Cliente.CUIT,
                        CbteDesde = ultimoCompAutorizado.CbteNro + 1,
                        CbteHasta = ultimoCompAutorizado.CbteNro + 1,
                        ImpTotal = (double)Math.Round(venta.NetoGravado + venta.IVA, 2), // neto no gravado + importe excento + neto gravado + iva al 21% + importe de tributos
                        ImpTotConc = 0, //neto no gravado
                        ImpNeto = (double)Math.Round(venta.NetoGravado, 2), //neto gravado
                        ImpOpEx = 0,
                        ImpIVA = (double)Math.Round(venta.IVA, 2), //IVA (suma de los ivas)
                        ImpTrib = 0, //Tributos
                        CbteFch = DateTime.Now.ToString("yyyyMMdd"),
                        Iva = new AlicIva[]{
                            new AlicIva{
                                Id = 5,
                                BaseImp = (double)Math.Round(venta.NetoGravado, 2),
                                Importe = (double)Math.Round(venta.IVA, 2)
                            }
                        },
                        MonId = "PES",
                        MonCotiz = 1
                    }
                }
            };
            var response = await cliente.FECAESolicitarAsync(authRequest, cmpRequest);
            return response;
        }

        public static async Task<FECompUltimoAutorizadoResponse> GetUltimaFactura(TicketAutenticacion ticket, int tipoComprobante) {
            var cliente = new ServiceSoapClient(ServiceSoapClient.EndpointConfiguration.ServiceSoap);
            var authRequest = new FEAuthRequest
            {
                Cuit = 20407268920,
                Sign = ticket.Sign,
                Token = ticket.Token
            };
            var response = await cliente.FECompUltimoAutorizadoAsync(authRequest, 9, tipoComprobante);
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

        public static async Task<List<PuntoVenta>> GetPuntosVenta(TicketAutenticacion ticket) {
            var cliente = new ServiceSoapClient(ServiceSoapClient.EndpointConfiguration.ServiceSoap);
            var authRequest = new FEAuthRequest
            {
                Sign = ticket.Sign,
                Token = ticket.Token,
                Cuit = 20407268920
            };
            var response = await cliente.FEParamGetPtosVentaAsync(authRequest);
            var list = new List<PuntoVenta>();
            foreach (var pdv in response.Body.FEParamGetPtosVentaResult.ResultGet)
            {
                list.Add(new PuntoVenta
                {
                    Nro = pdv.Nro,
                    Bloqueado = pdv.Bloqueado,
                    EmisionTipo = pdv.EmisionTipo,
                    FchBaja = DateTime.ParseExact(pdv.FchBaja, "yyyymmdd", CultureInfo.InvariantCulture),
                });
            }
            return list;
        }
    }
}
