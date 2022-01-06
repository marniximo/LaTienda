using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace LaTienda.Models
{
    public class Producto
    {
        [Key]
        public Guid Codigo { get; set; }
        public string Descripcion { get; set; }
        public Guid IdMarca { get; set; }
        public Marca Marca { get; set; }
        public float Costo { get; set; }
        public float NetoGravado { get; set; }
        public float Precio { get; set; }
        public float MargenGanancia { get; set; }
        public IEnumerable<LineaStock> LineasStock { get; set; }
        public IEnumerable<LineaVenta> LineasVenta{ get; set; }
    }
}
