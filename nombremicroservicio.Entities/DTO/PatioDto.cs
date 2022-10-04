using System;
using System.Collections.Generic;
using System.Text;

namespace nombremicroservicio.Entities.DTO
{
    public class PatioDto
    {
        public int PatioId { get; set; }
        public string Nombre { get; set; }
        public string Direccion { get; set; }
        public string Telefono { get; set; }
        public int NumeroPuntoVenta { get; set; }
    }
}
