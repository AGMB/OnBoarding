using nombremicroservicio.Entities.DTO;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace nombremicroservicio.Domain.Interfaces.Services
{
    public interface ICliente
    {
        Task<IList<ClienteDto>> ObtenerClientesAsync();
        Task<ClienteDto> ObtenerClienteAsync(string identificacion);
        Task<bool> GuardarClienteAsync(ClienteAddDto cliente);
        Task<int> ActualizarClienteAsync(ClienteDto cliente);
        Task<int> EliminarClienteAsync(int clienteid);
    }
}
