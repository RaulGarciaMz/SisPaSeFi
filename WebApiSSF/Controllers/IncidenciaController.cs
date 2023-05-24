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
        /// <param name="opcion">Tipo de incidencias. Puede llevar información adicional del estado de la incidencia separada por un guión p. ej. "IncidenciaAbiertaEnINSTALACION-1" lo que sólo se requiere si la opción es: "EnUnEstadoEspecificoPorBusquedaINSTALACION" ó "EnUnEstadoEspecificoPorBusquedaESTRUCTURA". La opción puede tener los siguientes valores: "IncidenciaAbiertaEnINSTALACION",  "IncidenciaAbiertaEnESTRUCTURA" , "IncidenciaSinAtenderPorVariosDiasEnESTRUCTURAS", "IncidenciaReportadaEnProgramaINSTALACION", "IncidenciaReportadaEnProgramaESTRUCTURA", "TodosPorBusquedaINSTALACION", "TodosPorBusquedaESTRUCTURA", "NoConcluidosPorBusquedaINSTALACION", "NoConcluidosPorBusquedaESTRUCTURA", "EnUnEstadoEspecificoPorBusquedaINSTALACION", "EnUnEstadoEspecificoPorBusquedaESTRUCTURA"</param>
        /// <param name="idActivo">Identificador del activo (id de la estructura o id del punto en caso de instalación) </param>
        /// <param name="criterio">Criterio de filtrado acorde a la opción indicada. Sólo se usa si las opciones es alguna de las siguientes: "TodosPorBusquedaINSTALACION", "TodosPorBusquedaESTRUCTURA" , "NoConcluidosPorBusquedaINSTALACION", "NoConcluidosPorBusquedaESTRUCTURA", "EnUnEstadoEspecificoPorBusquedaINSTALACION", "EnUnEstadoEspecificoPorBusquedaESTRUCTURA"   </param>
        /// <param name="usuario">Nombre del usuario (alias o usuario_nom) que realiza la operación</param>
        /// <returns></returns>
        [HttpGet]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IEnumerable<IncidenciaGeneralDto>>> ObtenerIncidenciasPorOpcion([Required]string opcion,  [Required] string usuario,int idActivo, string criterio = "")
        {//ByVal opcion As String, ByVal IdActivo As Integer, ByVal criterio As String, ByVal usuario As String
            try
            {
                var l = await _rp.ObtenerIncidenciasPorOpcionAsync(opcion, idActivo, criterio, usuario);

                if (l == null) 
                {
                    return NotFound();
                }

                if (l.Count == 0)
                {
                    return NotFound(new List<IncidenciaGeneralDto>());
                }

                return Ok(l);
            }
            catch (Exception ex)
            {
                _log.LogError($"error al obtener incidencias para la opcion: {opcion}, idActivo: {idActivo}, usuario: {usuario}", ex);
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
                _log.LogError($"error al agregar incidencia para el reporte/punto: {v.intIdActivo}, tipo: {v.strTipoIncidencia}, usuario: {v.strUsuario}", ex);
                return StatusCode(500, "Ocurrió un problema mientras se procesaba la petición");
            }
        }

        /// <summary>
        /// Actualiza una incidencia
        /// </summary>
        /// <param name="v">Incidencia a actualizar</param>
        /// <returns></returns>
        [HttpPut]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult> Put([FromBody] IncidenciasDtoForCreate v)
        {
            try
            {
                await _rp.ActualizaIncidenciaAsync(v);
                return Ok();
            }
            catch (Exception ex)
            {
                _log.LogError($"error al actualizar incidencia de tipoo: {v.strTipoIncidencia}, reporte/punto: {v.intIdReporte}, usuario: {v.strUsuario}", ex);
                return StatusCode(500, "Ocurrió un problema mientras se procesaba la petición");
            }
        }

    }
}
