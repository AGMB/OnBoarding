using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using nombremicroservicio.Domain.Interfaces.Services;
using nombremicroservicio.Entities.DTO;
using nombremicroservicio.Infrastructure.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace nombremicroservicio.API.Controllers
{
    [ApiVersion("1")]
    [Route("solicitudes")]
    [ApiController]
    public class SolicitudCreditoController : ControllerBase
    {
        private readonly ISolicitudCredito _solicitudCreditoServicio;

        public SolicitudCreditoController(ISolicitudCredito solicitudCreditoServicio)
        {
            _solicitudCreditoServicio = solicitudCreditoServicio;
        }

        [HttpGet]
        public async Task<IActionResult> ObtenerSolicitudes()
        {
            var result = await _solicitudCreditoServicio.ObtenerSolicitudesCreditoAsync();
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> GenerarSolicitud(SolicitudCreditoAddDto solicitud)
        {
            var result = await _solicitudCreditoServicio.GeneraSolicitudAsync(solicitud);
            if (result == 2)
                return BadRequest("El Cliente ya cuenta con una solicitud Activa para el dia de hoy");
            if (result == 3)
                return BadRequest("El Vehiculo solicitado se encuentra en un estado Reservado");
            return Created("solicitud", solicitud);

        }
    }
}
