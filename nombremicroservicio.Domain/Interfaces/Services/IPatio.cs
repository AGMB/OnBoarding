using nombremicroservicio.Entities.DTO;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace nombremicroservicio.Domain.Interfaces.Services
{
    public interface IPatio
    {
        Task<IList<PatioDto>> ObtenerPatiosAsync();
        Task<PatioDto> ObtenerPatioAsync(int patioId);
        Task GuardarPatioAsync(PatioAddDto patio);
        Task<bool> ActualizarPatiopAsync(PatioDto patio);
        Task<int> EliminarPatioAsync(int patioId);
    }
}
