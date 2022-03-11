using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace LaTienda.Models
{
    public class Sucursal
    {
        [Key]
        public int Codigo { get; set; }
        public string Descripcion { get; set; }
        public IEnumerable<Empleado> Empleados { get; set; }
    }
}
