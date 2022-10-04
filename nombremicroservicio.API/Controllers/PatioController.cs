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
    [Route("patios")]
    [ApiController]
    public class PatioController : ControllerBase
    {
        private readonly IPatio _patioServicio;

        public PatioController(IPatio patioServicio)
        {
            _patioServicio = patioServicio;
        }

        /// <summary>
        /// Endpoint para obtener todos los patios
        /// </summary>
        /// <response code="200">Retorna todos los patios</response>
        /// <response code="500">si ocurre un error</response>  
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> ListarPatios()
        {
            var patios = await _patioServicio.ObtenerPatiosAsync();
            return Ok(patios);
        }

        /// <summary>
        /// Endpoint para obtener un patio en especifico
        /// </summary>
        /// <param name="patioId"> patio id a consultar</param> 
        /// <response code="200">Retorna el patio</response>
        /// <response code="404">si no existe el patio</response>   
        /// <response code="500">si ocurre un error</response>   
        [HttpGet]
        [Route("{patioId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> ObtenerPatio(int patioId)
        {
            var result = await _patioServicio.ObtenerPatioAsync(patioId);
            if (result is null)
                return NotFound($"No se encontro el patio: {patioId}");
            return Ok(result);
        }

        /// <summary>
        /// Endpoint para agregar un patio
        /// </summary>
        /// <response code="201">Retorna el patio creado</response>
        /// <response code="400">Bad Request</response>  
        /// <response code="500">si ocurre un error</response>  
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> AgregarPatio(PatioAddDto patio)
        {
            await _patioServicio.GuardarPatioAsync(patio);

            return Created("patios", patio);
        }

        /// <summary>
        /// Endpoint para modificar un patio
        /// </summary>
        /// <response code="200">patio actualizado con exito</response>
        /// <response code="404">No existe el patio a modificar</response> 
        /// <response code="500">si ocurre un error</response>  
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> ModificarPatio(PatioDto patio)
        {
            var resultado = await _patioServicio.ActualizarPatiopAsync(patio);

            if (!resultado)
            {
                return NotFound($"No existe patio con ID: {patio.PatioId}");
            }

            return Ok();
        }

        /// <summary>
        /// Endpoint para eliminar un patio
        /// </summary>
        /// <response code="200">patio eliminado con exito</response>
        /// <response code="404">No existe patio</response> 
        /// <response code="400">Bad Request</response> 
        /// <response code="500">si ocurre un error</response>  
        [HttpDelete]
        [Route("{patioId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> EliminarVehiculo(int patioId)
        {
            var resultado = await _patioServicio.EliminarPatioAsync(patioId);
            if (resultado == 0)
                return NotFound($"No existe el Patio con el id: {patioId}");
            if (resultado == 2)
                return BadRequest($"El Patio {patioId} contiene información asociada, no se puede eliminar");
            return Ok();
        }

    }
}
