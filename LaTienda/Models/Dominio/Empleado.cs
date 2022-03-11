using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace LaTienda.Models
{
    public class Empleado
    {
        [Key]
        public Guid Id { get; set; }
        public string Legajo { get; set; }
        public string Password { get; set; }
        public int CodigoSucursal { get; set; }
        public Sucursal Sucursal { get; set; }
        public IEnumerable<Venta> Ventas { get; set; }
    }
}
