using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace nombremicroservicio.Entities.DTO
{
    public class ClienteAddDto
    {
        [Required]
        [StringLength(13, MinimumLength = 10)]
        public string Identificacion { get; set; }
        [Required]
        [StringLength(50, MinimumLength = 5)]
        public string Nombres { get; set; }
        [Required]
        [StringLength(50, MinimumLength = 5)]
        public string Apellidos { get; set; }
        [Required]
        [Range(12, int.MaxValue)]
        public int Edad { get; set; }
        [Required]
        public DateTime FechaNacimiento { get; set; }
        [Required]
        [StringLength(50, MinimumLength = 5)]
        public string Direccion { get; set; }
        [Required]
        [StringLength(50, MinimumLength = 5)]
        public string Telefono { get; set; }
        [Required]
        [StringLength(50, MinimumLength = 5)]
        public string EstadoCivil { get; set; }
        [Required]
        [StringLength(13, MinimumLength = 10)]
        public string IdentificacionConyuge { get; set; }
        [Required]
        [StringLength(50, MinimumLength = 5)]
        public string NombreConyuge { get; set; }
        [Required]
        public bool SujetoCredito { get; set; }
    }
}
