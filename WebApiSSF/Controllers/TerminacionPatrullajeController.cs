using Domain.DTOs;
using Domain.Ports.Driving;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mime;
using System.Runtime.CompilerServices;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApiSSF.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TerminacionPatrullajeController : ControllerBase
    {
        private readonly ITerminacionPatrullajeService _pp;
        private readonly ILogger<TerminacionPatrullajeController> _log;

        public TerminacionPatrullajeController(ITerminacionPatrullajeService p, ILogger<TerminacionPatrullajeController> log)
        {
            _pp = p ?? throw new ArgumentNullException(nameof(p));
            _log = log ?? throw new ArgumentNullException(nameof(log));
        }

        /// <summary>
        /// Realiza la terminación del patrullaje
        /// </summary>
        /// <param name="t">Estructura del patrullaje a terminar</param>
        /// <returns></returns>
        [HttpPost]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> TerminarPatrullaje([FromBody] TerminacionPatrullajeDto objTerminacionPatrullaje)
        {
            try
            {
                await _pp.RegistraTerminacionAsync(objTerminacionPatrullaje);

                return Ok();
            }
            catch (Exception ex)
            {
                _log.LogError($"error al intentar la terminación del patrullaje para la ruta: {objTerminacionPatrullaje.IdRuta}, fecha de patrullaje: {objTerminacionPatrullaje.FechaPatrullaje} - ", ex);
                return StatusCode(500, "Ocurrió un problema mientras se procesaba la petición - " + ex.Message);
            }
        }

    }
}
