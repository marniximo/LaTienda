using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace LaTienda.Models
{
    public class Color
    {
        [Key]
        public Guid Codigo { get; set; }
        public string Descripcion { get; set; }
    }
}
