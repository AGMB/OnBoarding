using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace nombremicroservicio.Entities.DTO
{
    public class PatioAddDto
    {
        [Required]
        [StringLength(50,MinimumLength = 5)]
        public string Nombre { get; set; }
        [Required]
        [StringLength(50, MinimumLength = 5)]
        public string Direccion { get; set; }
        [Required]
        [StringLength(50, MinimumLength = 5)]
        public string Telefono { get; set; }
        [Required]
        [Range(1,int.MaxValue)]
        public int NumeroPuntoVenta { get; set; }
    }
}
