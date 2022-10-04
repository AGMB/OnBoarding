using System;
using System.Collections.Generic;
using System.Text;

namespace nombremicroservicio.Entities.DTO
{
    public class EjecutivoDto
    {
        public string Identificacion { get; set; }
        public string Nombres { get; set; }
        public string Apellidos { get; set; }
        public string Direccion { get; set; }
        public string TelefonoConvencional { get; set; }
        public string Celular { get; set; }
        public int Edad { get; set; }
        public int PatioId { get; set; }
    }
}
