using LaTienda.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LaTienda.Controllers.Requests
{
    public class VentaCreateRequest
    {
        public List<ItemCarrito> Carrito { get; set; } = new List<ItemCarrito>();
        public long CUIT { get; set; }
    }
}
