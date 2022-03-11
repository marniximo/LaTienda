using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace LaTienda.Models
{
    public class LineaVenta
    {
        [Key]
        public Guid Codigo { get; set; }
        public int IdProducto { get; set; }
        public Producto Producto { get; set; }
        public decimal PrecioUnitario { get; set; }
        public int Cantidad { get; set; }
        public decimal Subtotal { get; set; }
        public Guid IdVenta { get; set; }
        public Venta Venta { get; set; }
    }
}
