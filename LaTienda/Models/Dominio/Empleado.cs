using System;
using System.ComponentModel.DataAnnotations;

namespace LaTienda.Models
{
    public class Empleado
    {
        [Key]
        public Guid Id { get; set; }
    }
}
