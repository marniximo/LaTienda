using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace LaTienda.Models
{
    public class Cliente
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long CUIT { get; set; }
        public string RazonSocial { get; set; }
        public string Domicilio { get; set; }
        public CondicionTributaria CondicionTributaria { get; set; }
        public ICollection<Venta> Ventas { get; set; }
    }
}
