using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace LaTienda.Models
{
    public class LineaStock
    {
        [Key]
        public Guid Codigo { get; set; }
        public Guid IdProducto { get; set; }
        public Producto Producto { get; set; }
        public Guid IdColor {get; set;}
        public Color Color { get; set; }
        public Guid IdTalle { get; set; }
        public Talle Talle { get; set; }
        public Guid IdSucursal { get; set; }
        public Sucursal Sucursal { get; set; }
        public int Stock { get; set; }
    }
}
