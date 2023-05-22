using Domain.DTOs;
using Domain.Ports.Driving;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Net.Mime;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApiSSF.Controllers
{
    [Route("api/[controller]")]
    //[Authorize]
    [ApiController]
    public class DatosPropuestaPatrullajeExtraordinarioController : ControllerBase
    {
        private readonly IDatosPropuestaExtraordinariaService _rp;
        private readonly ILogger<DatosPropuestaPatrullajeExtraordinarioController> _log;

        public DatosPropuestaPatrullajeExtraordinarioController(IDatosPropuestaExtraordinariaService r, ILogger<DatosPropuestaPatrullajeExtraordinarioController> log)
        {
            _rp = r ?? throw new ArgumentNullException(nameof(r));
            _log = log;
        }

        /// <summary>
        /// Obtiene los datosde propuestas extraordinarias
        /// </summary>
        /// <param name="idPropuesta">Identificador de la propuesta</param>
        /// <param name="usuario">Usuario que realiza la operación</param>
        /// <returns></returns>
        [HttpGet]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<DatosPropuestaExtraordinariaDto>> ObtenerDatosDePropuestasExtraordinarias([Required]int idPropuesta, [Required] string usuario)
        {
            try
            {
                var programa = await _rp.ObtenerDatosPropuestaExtraordinariaAsync(idPropuesta, usuario);

                if (programa == null)
                {
                    return NotFound();
                }

                return Ok(programa);
            }
            catch (Exception ex)
            {
                _log.LogError($"error al obtener datos de propuestas extraordinarias para prpuesta: {idPropuesta}, usuario: {usuario} - ", ex);
                return StatusCode(500, "Ocurrió un problema mientras se procesaba la petición - " + ex.Message);
            }
        }

   
    }
}
