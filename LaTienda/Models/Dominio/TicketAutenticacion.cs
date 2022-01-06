using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LaTienda.Models
{
    public class TicketAutenticacion
    {
        [Key]
        public Guid Id { get; set; }
        public DateTime GenerationTime { get; set; } // Momento en que fue generado el requerimiento
        public DateTime ExpirationTime { get; set; } // Momento en el que expira la solicitud
        public string Service { get; set; } // Identificacion del WSN para el cual se solicita el TA
        public string Sign { get; set; } // Firma de seguridad recibida en la respuesta
        public string Token { get; set; } // Token de seguridad recibido en la respuesta
    }
}
