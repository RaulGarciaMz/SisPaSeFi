using Domain.DTOs;
using Domain.Ports.Driving;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.ComponentModel.DataAnnotations;
using System.Net.Mime;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApiSSF.Controllers
{
    [Route("api/[controller]")]
    //[Authorize]
    [ApiController]
    public class MonitoreoMovilController : ControllerBase
    {
        private readonly IMonitoreoService _pp;
        private readonly ILogger<MonitoreoMovilController> _log;

        public MonitoreoMovilController(IMonitoreoService ps, ILogger<MonitoreoMovilController> log)
        {
            _pp = ps ?? throw new ArgumentNullException(nameof(ps));
            _log = log;
        }

        /// <summary>
        /// Obtiene el reporte de monitoreo
        /// </summary>
        /// <param name="usuario">Nombre del usuario que realiza la operación</param>
        /// <returns></returns>
        [HttpGet]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<MonitoreoMovilDto>> ObtenerMonitoreo([Required]string usuario)
        {
            try
            {
                var monitoreo = await _pp.ObtenerMonitoreoMovilAsync(usuario);

                return Ok(monitoreo);
            }
            catch (Exception ex)
            {
                _log.LogError($"error al obtener el monitoreo para el usuario: {usuario} ", ex);
                var m = "Ocurrió un problema mientras se procesaba la petición - " + ex.Message;

                return StatusCode(500, m);
            }
        }


    }
}
