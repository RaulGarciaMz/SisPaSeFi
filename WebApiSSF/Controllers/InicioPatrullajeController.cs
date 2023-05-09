using Domain.DTOs;
using Domain.Entities;
using Domain.Ports.Driving;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using System.Net.Mime;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApiSSF.Controllers
{
    [Route("api/[controller]")]
    //[Authorize]
    [ApiController]
    public class InicioPatrullajeController : ControllerBase
    {
        private readonly IInicioPatrullajeService _pp;
        private readonly ILogger<InicioPatrullajeController> _log;

        public InicioPatrullajeController(IInicioPatrullajeService p, ILogger<InicioPatrullajeController> log)
        {
            _pp = p ?? throw new ArgumentNullException(nameof(p));
            _log = log ?? throw new ArgumentNullException(nameof(log));
        }

        /// <summary>
        /// Agrega un inicio de patrullaje, actualizando o creando sus elementos relacionados
        /// </summary>
        /// <param name="inicioPatrullaje">Inicio del patrullaje a agregar</param>
        /// <returns></returns>
        [HttpPost]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> AgregaInicioPatrullaje([FromBody] InicioPatrullajeDto inicioPatrullaje)
        {
            try
            {
                await _pp.AgregaInicioPatrullajeAsync(inicioPatrullaje);

                return Ok();
            }
            catch (Exception ex)
            {
                _log.LogError($"error al obtener la estructura por identificador ", ex);
                return StatusCode(500, "Ocurrió un problema mientras se procesaba la petición - " + ex.Message);
            }
        }
    }
}
