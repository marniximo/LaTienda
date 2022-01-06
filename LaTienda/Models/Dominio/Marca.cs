using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace LaTienda.Models
{
    public class Marca
    {
        [Key]
        public Guid Codigo { get; set; }
        public string Descripcion { get; set; }
        public IEnumerable<Producto> Productos { get; set; }
    }
}
