using System;
using System.Collections.Generic;


namespace nombremicroservicio.Entities.Entidades
{
    public partial class Patio
    {
        public int PatioId { get; set; }
        public string Nombre { get; set; }
        public string Direccion { get; set; }
        public string Telefono { get; set; }
        public int NumeroPuntoVenta { get; set; }
    }
}
