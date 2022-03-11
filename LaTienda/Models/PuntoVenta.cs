using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LaTienda.Models
{
    public class PuntoVenta
    {
        public int Nro { get; set; }
        public string EmisionTipo { get; set; }
        public string Bloqueado { get; set; }
        public DateTime FchBaja { get; set; }
    }
}
