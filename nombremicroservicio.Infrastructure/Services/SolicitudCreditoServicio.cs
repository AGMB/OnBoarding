using Microsoft.EntityFrameworkCore;
using nombremicroservicio.Domain.Interfaces.Repository;
using nombremicroservicio.Domain.Interfaces.Services;
using nombremicroservicio.Entities.DTO;
using nombremicroservicio.Entities.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace nombremicroservicio.Infrastructure.Services
{
    public class SolicitudCreditoServicio : ISolicitudCredito
    {
        #region Propiedades
        private readonly ISolicitudCreditoRepository _solicitudCreditoRepository;
        private readonly IVehiculoRepository _vehiculoRepository;
        private readonly IClienteRepository _clienteRepository;
        private readonly IClientePatio _clientePatioServicio;
        #endregion

        #region Constructor
        public SolicitudCreditoServicio(ISolicitudCreditoRepository solicitudCreditoRepository, IVehiculoRepository vehiculoRepository,
           IClientePatio clientePatioServicio, IClienteRepository clienteRepository)
        {
            _solicitudCreditoRepository = solicitudCreditoRepository;
            _vehiculoRepository = vehiculoRepository;
            _clientePatioServicio = clientePatioServicio;
            _clienteRepository = clienteRepository;
        }
        #endregion

        #region CRUD Operaciones
        /// <summary>
        /// 
        /// </summary>
        /// <param name="solicitud"></param>
        /// <returns> 1: Creada con exito, 2: Ya existe Solicitud Activa, 3: Vehiculo en Reserva</returns>
        public async Task<int> GeneraSolicitudAsync(SolicitudCreditoAddDto solicitud)
        {

            if (await ExisteSolicitudActiva(solicitud.ClienteId, solicitud.FechaSolicitud))
                return 2;
            if (await VehiculoEstaReservado(solicitud.VehiculoId))
                return 3;

            var solicitudCredito = new SolicitudCredito
            {
                ClienteId = solicitud.ClienteId,
                PatioId = solicitud.PatioId,
                VehiculoId = solicitud.VehiculoId,
                EjecutivoId = solicitud.EjecutivoId,
                FechaSolicitud = solicitud.FechaSolicitud,
                MesesPlazo = solicitud.MesesPlazo,
                Cuotas = solicitud.Cuotas,
                Entrada = solicitud.Entrada,
                EstadoSolicitud = solicitud.EstadoSolicitud,
                Observacion = solicitud.Observacion
            };

            await _solicitudCreditoRepository.AddAsync(solicitudCredito);
            await _solicitudCreditoRepository.SaveChangesAsync();

            var clienteIdentificacion = await _clienteRepository.ObtenerClienteIdentificacionAsync(solicitud.ClienteId);

            await _clientePatioServicio.GuardarAsignacionAsync(new ClientePatioAddDto
            {
                IdentificacionCliente = clienteIdentificacion,
                PatioId = solicitud.PatioId,
                FechaAsignacion = solicitud.FechaSolicitud
            });

            return 1;
        }
        public async Task<IList<SolicitudCreditoDto>> ObtenerSolicitudesCreditoAsync()
        {
            var solicitudes = await _solicitudCreditoRepository.GetAllIQuerable()
                              .Select(x => new SolicitudCreditoDto
                              {
                                  SolicitudId = x.SolicitudId,
                                  ClienteId = x.ClienteId,
                                  PatioId = x.PatioId,
                                  VehiculoId = x.VehiculoId,
                                  EjecutivoId = x.EjecutivoId,
                                  FechaSolicitud = x.FechaSolicitud,
                                  MesesPlazo = x.MesesPlazo,
                                  Cuotas = x.Cuotas,
                                  Entrada = x.Entrada,
                                  EstadoSolicitud = x.EstadoSolicitud,
                                  Observacion = x.Observacion
                              }).ToListAsync();

            return solicitudes;
        }
        #endregion

        #region Metodos Privados
        private async Task<bool> ExisteSolicitudActiva(int clienteID, DateTime fechaSolicitud)
        {
            var existeSolicitud = await _solicitudCreditoRepository.GetAllIQuerable()
                          .AnyAsync(x => x.ClienteId == clienteID && x.FechaSolicitud == fechaSolicitud && x.EstadoSolicitud == "Registrada");

            return existeSolicitud;
        }

        private async Task<bool> VehiculoEstaReservado(int vehiculoID)
        {
            var vehiculoReservado = await (from v in _vehiculoRepository.GetAllIQuerable()
                                           join s in _solicitudCreditoRepository.GetAllIQuerable()
                                           on v.VehiculoId equals s.VehiculoId
                                           where s.VehiculoId == vehiculoID && s.EstadoSolicitud.Equals("Registrada")
                                           select s.SolicitudId).AnyAsync();

            return vehiculoReservado;
        } 
        #endregion
    }
}
