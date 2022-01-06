using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace LaTienda.Models
{
    public class Venta
    {
        [Key]
        public Guid Codigo { get; set; }
        public DateTime Fecha { get; set; }
        public float Total { get; set; }
        public int Vendedor { get; set; }
        public long CUITCliente { get; set; }
        public Cliente Cliente { get; set; }
        public int TipoComprobante { get; set; }
        public IEnumerable<LineaVenta> LineasVenta { get; set; }
    }
}
