using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LaTienda.Controllers.Requests
{
    public class GetLineaStockRequest
    {
        public int CodigoProducto {get; set;}
        public Guid IdTalle { get; set; }
        public Guid IdColor { get; set; }
    }
}
