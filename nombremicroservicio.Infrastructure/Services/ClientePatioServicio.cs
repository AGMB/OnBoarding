using Microsoft.EntityFrameworkCore;
using nombremicroservicio.Domain.Interfaces.Repository;
using nombremicroservicio.Domain.Interfaces.Services;
using nombremicroservicio.Entities.DTO;
using nombremicroservicio.Entities.Entidades;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace nombremicroservicio.Infrastructure.Services
{
    public class ClientePatioServicio : IClientePatio
    {
        #region Propiedades
        private readonly IClientePatioRepository _clientePatioRepository;
        #endregion

        #region Constructor
        public ClientePatioServicio(IClientePatioRepository clientePatioRepository)
        {
            _clientePatioRepository = clientePatioRepository;
        }
        #endregion

        #region CRUD Operaciones
        public async Task<bool> ActualizarAsignacionAsync(ClientePatioDto asignacion)
        {
            var response = false;
            var asignacionActualizar = await _clientePatioRepository.GetByIdAsync(asignacion.ClientePatioId);
            if (asignacionActualizar != null)
            {
                asignacionActualizar.IdentificacionCliente = asignacion.IdentificacionCliente;
                asignacionActualizar.PatioId = asignacion.PatioId;
                asignacionActualizar.FechaAsignacion = asignacion.FechaAsignacion;

                _clientePatioRepository.Update(asignacionActualizar);
                await _clientePatioRepository.SaveChangesAsync();
                response = true;
            }

            return response;
        }

        public async Task<bool> EliminarAsignacionAsync(int clientePatioId)
        {
            var response = false;
            var asignacionEliminar = await _clientePatioRepository.GetByIdAsync(clientePatioId);
            if (asignacionEliminar != null)
            {
                _clientePatioRepository.Remove(asignacionEliminar);
                await _clientePatioRepository.SaveChangesAsync();
                response = true;
            }
            return response;
        }

        public async Task GuardarAsignacionAsync(ClientePatioAddDto asignacion)
        {
            var asignacionGuardar = new ClientePatio
            {
                IdentificacionCliente = asignacion.IdentificacionCliente,
                PatioId = asignacion.PatioId,
                FechaAsignacion = asignacion.FechaAsignacion
            };

            await _clientePatioRepository.AddAsync(asignacionGuardar);
            await _clientePatioRepository.SaveChangesAsync();
        }

        public async Task<ClientePatioDto> ObtenerAsignacionAsync(int clientePatioId)
        {
            var asignacion = await _clientePatioRepository.GetByIdAsync(clientePatioId);
            if (asignacion is null)
                return null;

            var result = new ClientePatioDto
            {
                ClientePatioId = asignacion.ClientePatioId,
                IdentificacionCliente = asignacion.IdentificacionCliente,
                PatioId = asignacion.PatioId,
                FechaAsignacion = asignacion.FechaAsignacion
            };

            return result;
        }

        public async Task<IList<ClientePatioDto>> ObtenerAsignacionesAsync()
        {
            List<ClientePatioDto> asignaciones = await _clientePatioRepository.GetAllIQuerable()
                                             .Select(x => new ClientePatioDto
                                             {
                                                 ClientePatioId = x.ClientePatioId,
                                                 IdentificacionCliente = x.IdentificacionCliente,
                                                 PatioId = x.PatioId,
                                                 FechaAsignacion = x.FechaAsignacion
                                             }).ToListAsync();

            return asignaciones;
        } 
        #endregion
    }
}
