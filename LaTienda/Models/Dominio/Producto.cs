using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace LaTienda.Models
{
    public class Producto
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Codigo { get; set; }
        public string Descripcion { get; set; }
        public Guid IdMarca { get; set; }
        public Marca Marca { get; set; }
        public decimal Costo { get; set; }
        public decimal IVA { get; set; }
        public decimal Precio { get; set; }
        public decimal MargenGanancia { get; set; }
        public IEnumerable<LineaStock> LineasStock { get; set; }
        public IEnumerable<LineaVenta> LineasVenta{ get; set; }

        public void SetPrecioYNeto(decimal costo, decimal margen) {
            Precio = costo * (1 + margen / 100);
            IVA = Precio * 0.21M;
        }

    }
}
