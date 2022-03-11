using LaTienda.Clientes.AFIP;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Threading.Tasks;

namespace LaTienda.Models
{
    public class Venta
    {
        [Key]
        public Guid Codigo { get; set; }
        public DateTime Fecha { get; set; }
        public decimal IVA { get; set; }
        public decimal NetoGravado { get; set; }
        public Empleado Vendedor { get; set; }
        public Guid IdVendedor { get; set; }
        public long CUITCliente { get; set; }
        public Cliente Cliente { get; set; }
        public int TipoComprobante { get; set; }
        public IEnumerable<LineaVenta> LineasVenta { get; set; }

        public async Task RegistrarAfip(IClienteAfip cliente)
        {
            await cliente.EnviarFactura(this);
        }

        public bool ValidarCliente() {
            TipoComprobante = CUITCliente != 0 && ( Cliente.CondicionTributaria == CondicionTributaria.RI || Cliente.CondicionTributaria == CondicionTributaria.M ) ? 1 : 6;
            return CUITCliente != 0 || IVA + NetoGravado <=10000;
        }

        public void CalcularCarrito(ItemCarrito itemCarrito) {
            NetoGravado += itemCarrito.LineaVenta.Subtotal;
            IVA += itemCarrito.LineaVenta.Subtotal * 0.21M;
        }

        public static bool ValidarCarrito(List<ItemCarrito> carrito) {
            if (carrito.Count == 0) {
                return false;
            }
            return true;
        }
    }
}
