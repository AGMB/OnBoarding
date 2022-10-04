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
    [ApiController]
    [Route("vehiculos")]
    public class VehiculoController : ControllerBase
    {
        private readonly IVehiculo _vehiculoServicio;

        public VehiculoController(IVehiculo vehiculoServicio)
        {
            _vehiculoServicio = vehiculoServicio;
        }

        /// <summary>
        /// Endpoint para obtener todos los vehiculos
        /// </summary>
        /// <response code="200">Retorna todos los vehiculos</response>
        /// <response code="500">si ocurre un error</response>  
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]       
        public async Task<IActionResult> ListarVehiculos()
        {
            var vehiculos = await _vehiculoServicio.ObtenerVehiculosAsync();
            return Ok(vehiculos);
        }

        /// <summary>
        /// Endpoint para obtener un vehiculo en especifico
        /// </summary>
        /// <param name="placa"> placa a consultar</param> 
        /// <response code="200">Retorna el vehiculo</response>
        /// <response code="404">si no existe el vehiculo</response>   
        /// <response code="500">si ocurre un error</response>   
        [HttpGet]
        [Route("{placa}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> ObtenerVehiculo(string placa)
        {
            var result = await _vehiculoServicio.ObtenerVehiculoAsync(placa);
            if (result is null)
                return NotFound($"No se encontro el vehiculo con placa: {placa}");
            return Ok(result);
        }

        /// <summary>
        /// Endpoint para agregar un vehiculo
        /// </summary>
        /// <response code="201">Retorna el vehiculo creado</response>
        /// <response code="400">Bad Request</response>   
        /// <response code="500">si ocurre un error</response>  
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> AgregarVehiculo(VehiculoAddDto vehiculo)
        {
            var resultado = await _vehiculoServicio.GuardarVehiculoAsync(vehiculo);
            if (!resultado)
                return BadRequest($"Vehiculo {vehiculo.Placa} ya existe");

            return Created("vehiculos",vehiculo);
        }

        /// <summary>
        /// Endpoint para modificar un vehiculo
        /// </summary>
        /// <response code="200">vehiculo actualizado con exito</response>
        /// <response code="404">No existe el vehiculo a modificar</response>
        /// <response code="400">Bad Request</response>   
        /// <response code="500">si ocurre un error</response>  
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> ModificarVehiculo(VehiculoDto vehiculo)
        {
            var resultado = await _vehiculoServicio.ActualizarVehiculoAsync(vehiculo);
            if (resultado == 2)
            {
                return BadRequest($"No se puede modificar el vehiculo, posible placa duplicada {vehiculo.Placa}");
            }
            else if (resultado == 0)
                return NotFound($"No existe vehiculo con ID: {vehiculo.VehiculoId}");

            return Ok();
        }

        /// <summary>
        /// Endpoint para eliminar un Vehiculo
        /// </summary>
        /// <response code="200">Vehiculo eliminado con exito</response>
        /// <response code="404">No existe Vehiculo</response>  
        /// <response code="400">Bad Request</response> 
        /// <response code="500">si ocurre un error</response>  
        [HttpDelete]
        [Route("{vehiculoId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> EliminarVehiculo(int vehiculoId)
        {
            var resultado = await _vehiculoServicio.EliminarVehiculoAsync(vehiculoId);
            if (resultado == 0)
                return NotFound($"No existe el Vehiculo con el id: {vehiculoId}");
            if (resultado == 2)
                return BadRequest($"El vehiculoId {vehiculoId} contiene información asociada, no se puede eliminar");
            return Ok();
        }
    }
}
