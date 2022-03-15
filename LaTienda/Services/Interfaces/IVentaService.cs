using LaTienda.Models;
using LaTienda.Services.Resultados;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LaTienda.Services.Interfaces
{
    public interface IVentaService
    {
        Task<ResultadoCreacion> CrearVenta(string legajoEmpleado, List<ItemCarrito> carrito, long CUIT);
    }
}
