using nombremicroservicio.Entities.DTO;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace nombremicroservicio.Domain.Interfaces.Services
{
    public interface IClientePatio
    {
        Task<IList<ClientePatioDto>> ObtenerAsignacionesAsync();
        Task<ClientePatioDto> ObtenerAsignacionAsync(int clientePatioId);
        Task GuardarAsignacionAsync(ClientePatioAddDto asignacion);
        Task<bool> ActualizarAsignacionAsync(ClientePatioDto asignacion);
        Task<bool> EliminarAsignacionAsync(int clientePatioId);
    }
}
