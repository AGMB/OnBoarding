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
    public class VehiculoServicio : IVehiculo
    {
        #region Propiedades
        private readonly IVehiculoRepository _vehiculoRepository;
        private readonly ISolicitudCreditoRepository _solicitudCreditoRepository;
        #endregion

        #region Constructor
        public VehiculoServicio(IVehiculoRepository vehiculoRepository, ISolicitudCreditoRepository solicitudCreditoRepository)
        {
            _vehiculoRepository = vehiculoRepository;
            _solicitudCreditoRepository = solicitudCreditoRepository;
        }
        #endregion

        #region CRUD Operaciones
        /// <summary>
        /// Actualizar Vehiculo
        /// </summary>
        /// <param name="cliente"></param>
        /// <returns>
        /// 0 No existe Vehiculo
        /// 1 Vehiculo Actualizado
        /// 2 No se puede actualizar el Vehiculo, # Placa duplicado
        /// </returns>
        public async Task<int> ActualizarVehiculoAsync(VehiculoDto vehiculo)
        {
            var response = 0;
            var vehiculoActualizar = await _vehiculoRepository.GetByIdAsync(vehiculo.VehiculoId);

            if (vehiculoActualizar is null)
                return response;

            if (!await ExisteOtroVehiculoPorPlaca(vehiculoActualizar.VehiculoId, vehiculo.Placa))
            {
                vehiculoActualizar.Placa = vehiculo.Placa;
                vehiculoActualizar.Modelo = vehiculo.Modelo;
                vehiculoActualizar.Tipo = vehiculo.Tipo;
                vehiculoActualizar.NumeroChasis = vehiculo.NumeroChasis;
                vehiculoActualizar.MarcaId = vehiculo.MarcaId;
                vehiculoActualizar.Cilindraje = vehiculo.Cilindraje;
                vehiculoActualizar.Avaluo = vehiculo.Avaluo;

                _vehiculoRepository.Update(vehiculoActualizar);
                await _vehiculoRepository.SaveChangesAsync();
                response = 1;
            }
            else
                response = 2;

            return response;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="patioId"></param>
        /// <returns>0: no existe el vehiculo, 1: eliminado con exito, 2: Vehiculo Asociado</returns>
        public async Task<int> EliminarVehiculoAsync(int vehiculoId)
        {
            var vehiculoEliminar = await _vehiculoRepository.GetByIdAsync(vehiculoId);
            if (vehiculoEliminar is null)
                return 0;
            if (await VehiculoEstaAsociado(vehiculoId))
                return 2;

            _vehiculoRepository.Remove(vehiculoEliminar);
            await _vehiculoRepository.SaveChangesAsync();

            return 1;
        }

        public async Task<bool> GuardarVehiculoAsync(VehiculoAddDto vehiculo)
        {
            var response = false;
            if (!await ExisteVehiculoPorPlaca(vehiculo.Placa))
            {
                var vehiculoGuardar = new Vehiculo
                {
                    Placa = vehiculo.Placa,
                    Modelo = vehiculo.Modelo,
                    NumeroChasis = vehiculo.NumeroChasis,
                    MarcaId = vehiculo.MarcaId,
                    Tipo = vehiculo.Tipo,
                    Cilindraje = vehiculo.Cilindraje,
                    Avaluo = vehiculo.Avaluo
                };

                await _vehiculoRepository.AddAsync(vehiculoGuardar);
                await _vehiculoRepository.SaveChangesAsync();
                response = true;
            }

            return response;
        }

        public async Task<VehiculoDto> ObtenerVehiculoAsync(string placa)
        {
            var vehiculo = await _vehiculoRepository.GetAllIQuerable().FirstOrDefaultAsync(x => x.Placa.Equals(placa));
            if (vehiculo is null)
                return null;

            var result = new VehiculoDto
            {
                VehiculoId = vehiculo.VehiculoId,
                Placa = vehiculo.Placa,
                Modelo = vehiculo.Modelo,
                NumeroChasis = vehiculo.NumeroChasis,
                MarcaId = vehiculo.MarcaId,
                Tipo = vehiculo.Tipo,
                Cilindraje = vehiculo.Cilindraje,
                Avaluo = vehiculo.Avaluo
            };

            return result;
        }

        public async Task<IList<VehiculoDto>> ObtenerVehiculosAsync()
        {
            List<VehiculoDto> vehiculos = await _vehiculoRepository.GetAllIQuerable()
                                            .Select(x => new VehiculoDto
                                            {
                                                VehiculoId = x.VehiculoId,
                                                Placa = x.Placa,
                                                Modelo = x.Modelo,
                                                NumeroChasis = x.NumeroChasis,
                                                MarcaId = x.MarcaId,
                                                Tipo = x.Tipo,
                                                Cilindraje = x.Cilindraje,
                                                Avaluo = x.Avaluo
                                            }).ToListAsync();

            return vehiculos;
        } 
        #endregion

        #region Metodos Privados
        private async Task<bool> ExisteVehiculoPorPlaca(string placa)
        {
            return await _vehiculoRepository.GetAllIQuerable().AnyAsync(x => x.Placa.Equals(placa));
        }

        private async Task<bool> ExisteOtroVehiculoPorPlaca(int vehiculoIdActual, string placa)
        {
            return await _vehiculoRepository.GetAllIQuerable()
                         .AnyAsync(x => x.Placa.Equals(placa) && x.VehiculoId != vehiculoIdActual);
        }

        private async Task<bool> VehiculoEstaAsociado(int vehiculoId)
        {
            return await _solicitudCreditoRepository.VehiculoEstaAsociadoAsync(vehiculoId);
        } 
        #endregion
    }
}
