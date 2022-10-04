using System;
using System.Collections.Generic;

namespace nombremicroservicio.Entities.Entidades
{
    public partial class ClientePatio
    {
        public int ClientePatioId { get; set; }
        public string IdentificacionCliente { get; set; }
        public int PatioId { get; set; }
        public DateTime FechaAsignacion { get; set; }
    }
}
