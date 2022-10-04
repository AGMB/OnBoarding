using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using nombremicroservicio.Domain.Interfaces.Services;
using nombremicroservicio.Entities.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace nombremicroservicio.API.Controllers
{
    [ApiVersion("1")]
    [Route("asignaciones")]
    [ApiController]
    public class ClientePatioController : ControllerBase
    {
        private readonly IClientePatio _clientePatioServicio;

        public ClientePatioController(IClientePatio clientePatioServicio)
        {
            _clientePatioServicio = clientePatioServicio;
        }

        /// <summary>
        /// Endpoint para obtener todas las asignaciones
        /// </summary>
        /// <response code="200">Retorna todos las asignaciones</response>
        /// <response code="500">si ocurre un error</response>  
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> ListarAsignaciones()
        {
            var asignaciones = await _clientePatioServicio.ObtenerAsignacionesAsync();
            return Ok(asignaciones);
        }

        /// <summary>
        /// Endpoint para obtener una asignacion en especifico
        /// </summary>
        /// <param name="asignacionId"> asignacion a consultar</param> 
        /// <response code="200">Retorna la asignacion</response>
        /// <response code="404">si no existe la asignacion</response>   
        /// <response code="500">si ocurre un error</response>   
        [HttpGet]
        [Route("{asignacionId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> ObtenerAsignacion(int asignacionId)
        {
            var result = await _clientePatioServicio.ObtenerAsignacionAsync(asignacionId);
            if (result is null)
                return NotFound($"No se encontro la asignacion: {asignacionId}");
            return Ok(result);
        }

        /// <summary>
        /// Endpoint para crear un asignacion de cliente a un patio
        /// </summary>
        /// <response code="201">Retorna asignacion creada</response>
        /// <response code="400">Bad Request - Model Validation</response> 
        /// <response code="500">si ocurre un error</response>  
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> AgregarAsignacion(ClientePatioAddDto asignacion)
        {
            await _clientePatioServicio.GuardarAsignacionAsync(asignacion);

            return Created("asignaciones", asignacion);
        }

        /// <summary>
        /// Endpoint para modificar una asignacion
        /// </summary>
        /// <response code="200">asignacion actualizada con exito</response>
        /// <response code="400">Bad Request - Model Validation</response>
        /// <response code="404">No existe la asignacion a modificar</response> 
        /// <response code="500">si ocurre un error</response>  
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> ModificarAsignacion(ClientePatioDto asignacion)
        {
            var resultado = await _clientePatioServicio.ActualizarAsignacionAsync(asignacion);

            if (!resultado)
            {
                return NotFound($"No existe la asignacion con ID: {asignacion.ClientePatioId}");
            }

            return Ok();
        }

        /// <summary>
        /// Endpoint para eliminar una asignacion
        /// </summary>
        /// <response code="200">asignacion eliminada con exito</response>
        /// <response code="404">No existe la asignacion</response>   
        /// <response code="500">si ocurre un error</response>  
        [HttpDelete]
        [Route("{asignacionId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> EliminarAsignacion(int asignacionId)
        {
            var resultado = await _clientePatioServicio.EliminarAsignacionAsync(asignacionId);
            if (!resultado)
            {
                return NotFound($"No existe la asignacion: {asignacionId}");
            }
            return Ok();
        }
    }
}
