using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace nombremicroservicio.Entities.DTO
{
    public class ClientePatioAddDto
    {
        [Required]
        [MinLength(10)]
        public string IdentificacionCliente { get; set; }
        [Required]
        [Range(1,int.MaxValue,ErrorMessage =" El ID del patio debe ser mayor a 0")]
        public int PatioId { get; set; }
        [Required]
        public DateTime FechaAsignacion { get; set; }
    }
}
