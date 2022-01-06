using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace LaTienda.Models
{
    public class Cliente
    {
        [Key]
        public long CUIT { get; set; }
        public string RazonSocial { get; set; }
        public string Domicilio { get; set; }
        public ICollection<Venta> Ventas { get; set; }
    }
}
