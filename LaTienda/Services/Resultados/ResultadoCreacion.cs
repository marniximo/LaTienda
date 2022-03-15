using LaTienda.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LaTienda.Services.Resultados
{
    public class ResultadoCreacion
    {
        public Venta Venta { get; set; } = null;
        public int CodigoError { get; set; } = 0;
    }
}
