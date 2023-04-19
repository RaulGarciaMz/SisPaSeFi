using Domain.DTOs;
using Domain.Entities.Vistas;
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
    public class AfectacionIncidenciasController : ControllerBase
    {
        private readonly IAfectacionesService _pp;
        private readonly ILogger<AfectacionIncidenciasController> _log;

        public AfectacionIncidenciasController(IAfectacionesService p, ILogger<AfectacionIncidenciasController> log)
        {
            _pp = p ?? throw new ArgumentNullException(nameof(p));
            _log = log ?? throw new ArgumentNullException(nameof(log));
        }

        /// <summary>
        /// Obtiene la lista de afectaciones de incidencias acorde a los parámetros indicados
        /// </summary>
        /// <param name="idReporte">Identificador de la incidencia</param>
        /// <param name="opcion">Tipo de incidencia ("INSTALACION" o "ESTRUCTURA")</param>
        /// <param name="usuario">Nombre del usuario (usuario_nom) que realiza la operación</param>
        /// <returns></returns>
        [HttpGet]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IEnumerable<AfectacionIncidenciaVista>>> ObtenerAfectacionIncidenciaPorOpcion([Required]int idReporte, [Required] string opcion, [Required] string usuario)
        {
            try
            {
                var coms = await _pp.ObtenerAfectacionIncidenciaPorOpcionAsync(idReporte, opcion, usuario);

                if (coms.Count <= 0)
                {
                    return NotFound();
                }

                return Ok(coms);
            }
            catch (Exception ex)
            {
                _log.LogError($"error al obtener las afectaciones incidencias para el id de incidencia: {idReporte}, tipo: {opcion}, usuario: {usuario}", ex);
                var m = "Ocurrió un problema mientras se procesaba la petición" + ex.Message;
                //return StatusCode(500, "Ocurrió un problema mientras se procesaba la petición");
                return StatusCode(500, m);
            }
        }

        /// <summary>
        /// Agrega una incidencia
        /// </summary>
        /// <param name="a">Incidencia a agregar</param>
        /// <returns></returns>
        [HttpPost]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> Agrega([FromBody] AfectacionDtoForCreate a)
        {
            try
            {
                await _pp.AgregaAsync(a);

                return Ok();
            }
            catch (Exception ex)
            {
                _log.LogError($"error al agregar afectación para el usuario: {a.Usuario}", ex);
                return StatusCode(500, "Ocurrió un problema mientras se procesaba la petición");
            }
        }

        /// <summary>
        /// Actuliza una incidencia
        /// </summary>
        /// <param name="id">Identificador de la incidencia</param>
        /// <param name="a">Incidencia a actualizar</param>
        /// <returns></returns>
        [HttpPut("{id}")]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> Actualiza(int id, [FromBody] AfectacionDtoForUpdate a)
        {
            try
            {
                await _pp.ActualizaAsync(a);

                return Ok();
            }
            catch (Exception ex)
            {
                _log.LogError($"error al actualizar afectación: {a.IdAfectacionIncidencia} para el usuario: {a.Usuario}", ex);
                return StatusCode(500, "Ocurrió un problema mientras se procesaba la petición");
            }
        }

    }
}
