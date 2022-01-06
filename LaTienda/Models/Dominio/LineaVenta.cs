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
        public Guid IdProducto { get; set; }
        public Producto Producto { get; set; }
        public float PrecioUnitario { get; set; }
        public int Cantidad { get; set; }
        public float Subtotal { get; set; }
        public Guid IdVenta { get; set; }
        public Venta Venta { get; set; }
    }
}
