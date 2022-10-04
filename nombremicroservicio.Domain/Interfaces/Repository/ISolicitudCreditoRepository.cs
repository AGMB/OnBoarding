using nombremicroservicio.Entities.Entidades;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace nombremicroservicio.Domain.Interfaces.Repository
{
    public interface ISolicitudCreditoRepository: IBaseRepository<SolicitudCredito>
    {
        public Task<bool> ClienteEstaAsociadoAsync(int clienteId);
        public Task<bool> VehiculoEstaAsociadoAsync(int vehiculoId);
        public Task<bool> PatioEstaAsociadoAsync(int patioId);
    }
}
