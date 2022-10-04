using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace nombremicroservicio.Entities.DTO
{
    public class ClientePatioDto
    {
        [Required]
        public int ClientePatioId { get; set; }
        [Required]
        public string IdentificacionCliente { get; set; }
        [Required]
        public int PatioId { get; set; }
        [Required]
        public DateTime FechaAsignacion { get; set; }
    }
}
