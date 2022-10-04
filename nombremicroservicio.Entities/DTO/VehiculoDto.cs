using System;
using System.Collections.Generic;
using System.Text;

namespace nombremicroservicio.Entities.DTO
{
    public partial class VehiculoDto
    {
        public int VehiculoId { get; set; }
        public string Placa { get; set; }
        public string Modelo { get; set; }
        public string NumeroChasis { get; set; }
        public int MarcaId { get; set; }
        public string Tipo { get; set; }
        public string Cilindraje { get; set; }
        public decimal Avaluo { get; set; }
    }
}
