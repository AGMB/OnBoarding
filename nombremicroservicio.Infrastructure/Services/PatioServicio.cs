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
    public class PatioServicio : IPatio
    {
        #region Propiedades
        private readonly IPatioRepository _patioRepository;
        private readonly ISolicitudCreditoRepository _solicitudCreditoRepository;
        #endregion

        #region Constructor
        public PatioServicio(IPatioRepository patioRepository, ISolicitudCreditoRepository solicitudCreditoRepository)
        {
            _patioRepository = patioRepository;
            _solicitudCreditoRepository = solicitudCreditoRepository;
        }
        #endregion

        #region CRUD operaciones
        public async Task<bool> ActualizarPatiopAsync(PatioDto patio)
        {
            var response = false;
            var patioActualizar = await _patioRepository.GetByIdAsync(patio.PatioId);
            if (patioActualizar != null)
            {
                patioActualizar.Nombre = patio.Nombre;
                patioActualizar.Direccion = patio.Direccion;
                patioActualizar.Telefono = patio.Telefono;
                patioActualizar.NumeroPuntoVenta = patio.NumeroPuntoVenta;

                _patioRepository.Update(patioActualizar);
                await _patioRepository.SaveChangesAsync();
                response = true;
            }

            return response;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="patioId"></param>
        /// <returns>0: no existe el patio, 1: eliminado con exito, 2: Patio Asociado</returns>
        public async Task<int> EliminarPatioAsync(int patioId)
        {
            var patioEliminar = await _patioRepository.GetByIdAsync(patioId);
            if (patioEliminar is null)
                return 0;
            if (await PatioEstaAsociado(patioId))
                return 2;

            _patioRepository.Remove(patioEliminar);
            await _patioRepository.SaveChangesAsync();

            return 1;
        }

        public async Task GuardarPatioAsync(PatioAddDto patio)
        {
            var patioGuardar = new Patio
            {
                Nombre = patio.Nombre,
                Direccion = patio.Direccion,
                Telefono = patio.Telefono,
                NumeroPuntoVenta = patio.NumeroPuntoVenta
            };

            await _patioRepository.AddAsync(patioGuardar);
            await _patioRepository.SaveChangesAsync();
        }

        public async Task<PatioDto> ObtenerPatioAsync(int patioId)
        {
            var patio = await _patioRepository.GetByIdAsync(patioId);
            if (patio is null)
                return null;

            var result = new PatioDto
            {
                PatioId = patio.PatioId,
                Nombre = patio.Nombre,
                Direccion = patio.Direccion,
                Telefono = patio.Telefono,
                NumeroPuntoVenta = patio.NumeroPuntoVenta
            };

            return result;
        }

        public async Task<IList<PatioDto>> ObtenerPatiosAsync()
        {
            List<PatioDto> patios = await _patioRepository.GetAllIQuerable()
                                            .Select(x => new PatioDto
                                            {
                                                PatioId = x.PatioId,
                                                Nombre = x.Nombre,
                                                Direccion = x.Direccion,
                                                Telefono = x.Telefono,
                                                NumeroPuntoVenta = x.NumeroPuntoVenta
                                            }).ToListAsync();

            return patios;
        }
        #endregion

        #region Metodos Privados
        private async Task<bool> PatioEstaAsociado(int patioId)
        {
            return await _solicitudCreditoRepository.PatioEstaAsociadoAsync(patioId);
        } 
        #endregion
    }
}
