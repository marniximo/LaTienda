using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LaTienda.Models
{
    public class ItemCarrito
    {
        public LineaVenta LineaVenta { get; set; }
        public LineaStock LineaStock { get; set; }
    }
}
