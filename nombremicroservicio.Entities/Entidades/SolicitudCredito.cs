using System;
using System.Collections.Generic;

namespace nombremicroservicio.Entities.Entidades
{
    public partial class SolicitudCredito
    {
        public int SolicitudId { get; set; }
        public int ClienteId { get; set; }
        public int PatioId { get; set; }
        public int VehiculoId { get; set; }
        public int EjecutivoId { get; set; }
        public DateTime FechaSolicitud { get; set; }
        public int MesesPlazo { get; set; }
        public decimal Cuotas { get; set; }
        public decimal Entrada { get; set; }
        public string EstadoSolicitud { get; set; }
        public string Observacion { get; set; }
    }
}
