using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace LaTienda.Models
{
    public class Usuario
    {
        [Key]
        public Guid Legajo { get; set; }
        public string Password { get; set; }
        public int Sucursal { get; set; }
    }
}
