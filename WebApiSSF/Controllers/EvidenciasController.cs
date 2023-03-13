using Domain.DTOs;
using Domain.Entities;
using Domain.Entities.Vistas;
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
    public class EvidenciasController : ControllerBase
    {
        private readonly IEvidenciasService _pp;
        private readonly ILogger<EvidenciasController> _log;

        public EvidenciasController(IEvidenciasService ps, ILogger<EvidenciasController> log)
        {
            _pp = ps ?? throw new ArgumentNullException(nameof(ps));
            _log = log;
        }


        /// <summary>
        /// Obtiene las evidencias de acuerdo a los parámetros indicados
        /// </summary>
        /// <param name="idReporte">Identificador del reporte</param>
        /// <param name="tipo">Tipo de incidencia ("INSTALACION" o "ESTRUCTURA")</param>
        /// <param name="usuario">Usuario (alias o usuario_nom) que realiza la operación</param>
        /// <returns></returns>
        [HttpGet]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IEnumerable<EvidenciaVista>>> Get([Required]int idReporte, [Required] string tipo, [Required] string usuario)
        {
            try
            {
                var evidencias = await _pp.ObtenerEvidenciasPorTipo(idReporte, tipo, usuario);

                if (evidencias == null || evidencias.Count() == 0) 
                {
                    return NotFound();
                }

                return Ok(evidencias);
            }
            catch (Exception ex)
            {
                _log.LogInformation($"error al obtener evidencias del tipo: {tipo}, para el reporte: {idReporte}, usuario: {usuario}", ex);
                return StatusCode(500, "Ocurrió un problema mientras se procesaba la petición");
            }
        }

        /// <summary>
        /// Agrega una evidencia de incidencia acorde al tipo indicado en la estructura
        /// </summary>
        /// <param name="evidencia">Evidencia a dar de alta</param>
        [HttpPost]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> PostEvidencia([FromBody] EvidenciaDto evidencia)
        {
            try
            {
                await _pp.AgregarEvidenciaPorTipo(evidencia);
                return StatusCode(201, "Ok");
            }
            catch (Exception ex)
            {
                _log.LogInformation($"error al registrar una evidencia de tipo: {evidencia.TipoIncidencia} para el usuario: {evidencia.Usuario}", ex);
                return StatusCode(500, "Ocurrió un problema mientras se procesaba la petición");
            }
        }

        /// <summary>
        /// Elimina evidencias acorde a los parámetros indicados
        /// </summary>
        /// <param name="id">Identificador de la evidencia</param>
        /// <param name="tipo">Tipo de la evidencia ("INSTALACION", "ESTRUCTURA")</param>
        /// <param name="usuario">Nombre del usuario (usuario_nom) que realiza la operación</param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> Delete(int id, [Required] string tipo, [Required] string usuario)
        {
            try
            {
                await _pp.BorrarEvidenciaPorTipo(id, tipo, usuario);
                return Ok();
            }
            catch (Exception ex)
            {
                _log.LogInformation($"error al eliminar la evidencia: {id}, de tipo: {tipo} para el usuario: {usuario}", ex);
                return StatusCode(500, "Ocurrió un problema mientras se procesaba la petición");
            }
        }
    }
}
