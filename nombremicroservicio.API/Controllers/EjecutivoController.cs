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
    [Route("ejecutivos")]
    [ApiController]
    public class EjecutivoController : ControllerBase
    {
        private readonly IEjecutivo _ejecutivoServicio;

        public EjecutivoController(IEjecutivo ejecutivoServicio)
        {
            _ejecutivoServicio = ejecutivoServicio;
        }

        /// <summary>
        /// Obtiene Catalogo de todos los ejecutivos por patio
        /// </summary>
        /// <param name="patioID">PatioID</param>
        /// <response code="200">Retorna todos las ejecutivos</response>
        /// <response code="500">si ocurre un error</response>  
        [HttpGet]
        [Route("{patioID}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CatalogoEjecutivos(int patioID)
        {
            var result = await _ejecutivoServicio.CatalogoEjecutivosAsync(patioID);
            return Ok(result);
        }
    }
}
