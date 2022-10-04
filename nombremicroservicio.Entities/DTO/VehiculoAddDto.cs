using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace nombremicroservicio.Entities.DTO
{
    public class VehiculoAddDto
    {
        [Required]
        public string Placa { get; set; }
        [Required]
        public string Modelo { get; set; }
        [Required]
        public string NumeroChasis { get; set; }
        [Required]
        public int MarcaId { get; set; }
        public string Tipo { get; set; }
        [Required]
        public string Cilindraje { get; set; }
        [Required]
        public decimal Avaluo { get; set; }
    }
}
