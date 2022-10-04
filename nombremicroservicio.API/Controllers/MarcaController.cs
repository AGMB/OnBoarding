using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using nombremicroservicio.Domain.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace nombremicroservicio.API.Controllers
{
    [ApiVersion("1")]
    [Route("marcas")]
    [ApiController]
    public class MarcaController : ControllerBase
    {
        private readonly IMarca _marcaServicio;
        public MarcaController(IMarca marcaServicio)
        {
            _marcaServicio = marcaServicio;
        }

        /// <summary>
        /// Obtiene Catalogo de todos las marcas
        /// </summary>
        /// <response code="200">Retorna todos las marcas</response>
        /// <response code="500">si ocurre un error</response>  
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CatalogoMarcas()
        {
            var result = await _marcaServicio.CatalogoMarcasAsync();
            return Ok(result);
        }
    }
}
