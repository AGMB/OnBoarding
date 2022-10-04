using nombremicroservicio.Entities.DTO;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace nombremicroservicio.Domain.Interfaces.Services
{
    public interface IVehiculo
    {
        Task<IList<VehiculoDto>> ObtenerVehiculosAsync();
        Task<VehiculoDto> ObtenerVehiculoAsync(string placa);
        Task<bool> GuardarVehiculoAsync(VehiculoAddDto vehiculo);
        Task<int> ActualizarVehiculoAsync(VehiculoDto vehiculo);
        Task<int> EliminarVehiculoAsync(int vehiculoId);
    }
}
