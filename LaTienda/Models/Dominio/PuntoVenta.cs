using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace LaTienda.Models
{
    public class PuntoVenta
    {
        [Key]
        public int Id { get; set; }
        public Usuario UsuarioAcutal { get; set; }
    }
}
