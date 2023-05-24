using Domain.DTOs;
using Domain.Ports.Driving;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mime;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApiSSF.Controllers
{
    [Route("api/[controller]")]
    //[Authorize]
    [ApiController]
    public class RegistrarIncidenciaController : ControllerBase
    {
        private readonly IRegistroIncidenciaService _pp;
        private readonly ILogger<RegistrarIncidenciaController> _log;

        public RegistrarIncidenciaController(IRegistroIncidenciaService p, ILogger<RegistrarIncidenciaController> log)
        {
            _pp = p ?? throw new ArgumentNullException(nameof(p));
            _log = log ?? throw new ArgumentNullException(nameof(log));
        }

        /// <summary>
        /// Agrega de manera transaccional el registro de incidencias
        /// </summary>
        /// <param name="incidencia">Incidencia con sus evidencias a agregar</param>
        /// <returns></returns>
        [HttpPost]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> AgregaIncidencia([FromBody] RegistrarIncidenciaDto incidencia)
        {
            try
            {
                await _pp.AgregaIncidenciaTransaccionalAsync(incidencia);

                return Ok("OK");
            }
            catch (Exception ex)
            {
                _log.LogError($"error al registrar incidencia para usuario: {incidencia.usuario}, ruta: {incidencia.IdRuta}, Fecha de patrullaje: {incidencia.FechaPatrullaje} - ", ex);
                var m = "Ocurrió un problema mientras se procesaba la petición - " + ex.Message;
                //return StatusCode(500, "Ocurrió un problema mientras se procesaba la petición");
                return StatusCode(500, ex.Message);
            }
        }
    }
}
