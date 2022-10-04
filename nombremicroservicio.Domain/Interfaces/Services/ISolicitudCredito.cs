using nombremicroservicio.Entities.DTO;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace nombremicroservicio.Domain.Interfaces.Services
{
    public interface ISolicitudCredito
    {
        Task<int> GeneraSolicitudAsync(SolicitudCreditoAddDto solicitud);
        Task<IList<SolicitudCreditoDto>> ObtenerSolicitudesCreditoAsync();
    }
}
