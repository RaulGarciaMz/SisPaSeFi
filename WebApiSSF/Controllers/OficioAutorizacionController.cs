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
    public class OficioAutorizacionController : ControllerBase
    {
        private readonly IOficioAutorizacionService _pp;
        private readonly ILogger<OficioAutorizacionController> _log;

        public OficioAutorizacionController(IOficioAutorizacionService p, ILogger<OficioAutorizacionController> log)
        {
            _pp = p ?? throw new ArgumentNullException(nameof(p));
            _log = log ?? throw new ArgumentNullException(nameof(log));
        }

        /// <summary>
        /// Obtiene la información para el reporte de propuestas con oficio de autorización
        /// </summary>
        /// <param name="o">Datos de usuario y propuesta</param>
        /// <returns></returns>
        [HttpPost]
        [Consumes(MediaTypeNames.Application.Json)]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IEnumerable<string>>> ObtenerInformacionOficioAutorizacion([FromBody] OficioAutorizacionDto o)
        {
            try
            {
                var info = await _pp.ObtenerInformacionParaOficioAutorizacion(o.usuario, o.pass, o.idPropuesta);

                if (info == null) 
                {
                    return NotFound(new List<string>());
                }

                if (info.Count <= 0)
                {
                    return NotFound(new List<string>());
                }

                return Ok(info);
            }
            catch (Exception ex)
            {
                _log.LogError($"error al obtener la información para el oficio de autorización para la propuesta: {o.idPropuesta},  usuario: {o.usuario}, pass: {o.pass}, ", ex);
                var m = "Ocurrió un problema mientras se procesaba la petición . " + ex.Message;
                return StatusCode(500, m);
            }
        }

     
    }
}
