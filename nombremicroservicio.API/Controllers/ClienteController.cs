using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using nombremicroservicio.Domain.Interfaces.Services;
using nombremicroservicio.Entities.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace nombremicroservicio.API.Controllers
{
    [ApiVersion("1")]
    [ApiController]
    [Route("clientes")]
    public class ClienteController : ControllerBase
    {
        private readonly ILogger _iLogger;
        private readonly ICliente _clienteServicio;

        public ClienteController(ILogger<ClienteController> iLogger, ICliente clienteServicio)
        {
            _iLogger = iLogger;
            _clienteServicio = clienteServicio;
        }

        /// <summary>
        /// Endpoint para obtener todos los clientes
        /// </summary>
        /// <response code="200">Retorna todos los clientes</response>
        /// <response code="500">si ocurre un error</response>   
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> ListarClientes()
        {
            var result = await _clienteServicio.ObtenerClientesAsync();

            return Ok(result);
        }

        /// <summary>
        /// Endpoint para obtener un cliente en especifico
        /// </summary>
        /// <param name="identificacion"></param> 
        /// <response code="200">Retorna el cliente</response>
        /// <response code="404">si no existe el cliente</response>   
        /// <response code="500">si ocurre un error</response>   
        [HttpGet]
        [Route("{identificacion}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> ObtenerCliente(string identificacion)
        {
            var result = await _clienteServicio.ObtenerClienteAsync(identificacion);
            if (result is null)
                return NotFound($"No se encontro el cliente con identificacion: {identificacion}");
            return Ok(result);
        }

        /// <summary>
        /// Endpoint para agregar un cliente
        /// </summary>
        /// <response code="201">Retorna el cliente creado</response>
        /// <response code="400">Bad Request</response>   
        /// <response code="500">si ocurre un error</response>  
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> AgregarCliente(ClienteAddDto cliente)
        {
            var resultado = await _clienteServicio.GuardarClienteAsync(cliente);
            if (!resultado)
                return BadRequest($"Cliente {cliente.Identificacion} ya existe");

            return Created("clientes", cliente);
        }

        /// <summary>
        /// Endpoint para modificar un cliente
        /// </summary>
        /// <response code="200">Cliente actualizado con exito</response>
        /// <response code="404">No existe el cliente a modificar</response>
        /// <response code="400">Bad Request</response>   
        /// <response code="500">si ocurre un error</response>  
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> ModificarCliente(ClienteDto cliente)
        {
            var resultado = await _clienteServicio.ActualizarClienteAsync(cliente);
            if (resultado == 2)
            {
                return BadRequest($"No se puede modificar el cliente, ya existe cliente con identificacion {cliente.Identificacion}");
            }
            else if (resultado == 0)
                return NotFound($"No existe Cliente con ID: {cliente.ClienteId}");

            return Ok();
        }

        /// <summary>
        /// Endpoint para eliminar un cliente
        /// </summary>
        /// <response code="200">Cliente eliminado con exito</response>
        /// <response code="404">No existe el cliente a eliminar</response> 
        /// <response code="400">Bad Request</response>  
        /// <response code="500">si ocurre un error</response>  
        [HttpDelete]
        [Route("{clienteid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> EliminarCliente(int clienteid)
        {
            var resultado = await _clienteServicio.EliminarClienteAsync(clienteid);
            if (resultado == 0)
                return NotFound($"No existe el Cliente con el id: {clienteid}");
            if (resultado == 2)
                return BadRequest($"El Cliente {clienteid} contiene información asociada, no se puede eliminar");
            return Ok();
        }
    }
}
