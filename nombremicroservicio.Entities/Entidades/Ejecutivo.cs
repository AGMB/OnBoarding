using System;
using System.Collections.Generic;

namespace nombremicroservicio.Entities.Entidades
{
    public partial class Ejecutivo
    {
        public int EjecutivoId { get; set; }
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
