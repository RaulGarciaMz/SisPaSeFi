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
    public class IncidenciaController : ControllerBase
    {
        private readonly IIncidenciasService _rp;
        private readonly ILogger<IncidenciaController> _log;

        public IncidenciaController(IIncidenciasService r, ILogger<IncidenciaController> log)
        {
            _rp = r ?? throw new ArgumentNullException(nameof(r));
            _log = log;
        }

        /// <summary>
        /// Obtiene la lista de incidencias acorde a los valores de los parámetros
        /// </summary>
        /// <param name="opcion">Tipo de incidencias a obtener ("IncidenciaAbiertaEnINSTALACION",  "IncidenciaAbiertaEnESTRUCTURA" ó "IncidenciaSinAtenderPorVariosDiasEnESTRUCTURAS")</param>
        /// <param name="idActivo">Identificador del activo (id de la estructura o id del punto en caso de instalación) </param>
        /// <param name="usuario">Nombre del usuario (alias o usuario_nom) que realiza la operación</param>
        /// <param name="dias">Cantidad de días anteriores a la fecha actual de la petición que se tomarán en cuenta para buscar incidencias sin atender</param>
        /// <returns></returns>
        [HttpGet]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IEnumerable<IncidenciasDto>>> ObtenerIncidenciasPorOpcion([Required]string opcion, int idActivo, string criterio, [Required] string usuario, int dias=0)
        {//ByVal opcion As String, ByVal IdActivo As Integer, ByVal criterio As String, ByVal usuario As String
            try
            {
                var l = await _rp.ObtenerIncidenciasPorOpcionAsync(opcion, idActivo, criterio, dias, usuario);

                if (l == null) 
                {
                    return NotFound();
                }

                if (l.Count == 0)
                {
                    return NotFound();
                }

                return Ok(l);
            }
            catch (Exception ex)
            {
                _log.LogError($"error al obtener incidencias para la opcion: {opcion}, idActivo: {idActivo}, dias: {dias}, usuario: {usuario}", ex);
                return StatusCode(500, "Ocurrió un problema mientras se procesaba la petición");
            }
        }

        /// <summary>
        /// Agrega una incidencia en el reporte
        /// </summary>
        /// <param name="v">Incidencia a agregar</param>
        /// <returns></returns>
        [HttpPost]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> AgregaIncidencia([FromBody] IncidenciasDtoForCreate v)
        {
            try
            {
                await _rp.AgregaIncidenciaAsync(v);
                return StatusCode(201, "Ok");
            }
            catch (Exception ex)
            {
                _log.LogError($"error al agregar incidencia para el reporte/punto: {v.Id}, tipo: {v.TipoIncidencia}, usuario: {v.Usuario}", ex);
                return StatusCode(500, "Ocurrió un problema mientras se procesaba la petición");
            }
        }

        /// <summary>
        /// Actualiza una incidencia
        /// </summary>
        /// <param name="id">Identificador del reporte de incidencia</param>
        /// <param name="v">Incidencia a actualizar</param>
        /// <returns></returns>
        [HttpPut("{id}")]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult> Put(int id, [FromBody] IncidenciasDtoForUpdate v)
        {
            try
            {
                await _rp.ActualizaIncidenciaAsync(v);
                return Ok();
            }
            catch (Exception ex)
            {
                _log.LogError($"error al actualizar incidencia de tipoo: {v.TipoIncidencia}, reporte/punto: {v.IdReporte}, usuario: {v.Usuario}", ex);
                return StatusCode(500, "Ocurrió un problema mientras se procesaba la petición");
            }
        }

    }
}
