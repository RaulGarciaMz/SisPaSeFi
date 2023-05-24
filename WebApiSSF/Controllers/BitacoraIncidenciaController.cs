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
    public class BitacoraIncidenciaController : ControllerBase
    {
        private readonly IBitacoraService _rp;
        private readonly ILogger<BitacoraIncidenciaController> _log;

        public BitacoraIncidenciaController(IBitacoraService r, ILogger<BitacoraIncidenciaController> log)
        {
            _rp = r ?? throw new ArgumentNullException(nameof(r));
            _log = log;
        }

        /// <summary>
        /// Obtiene la lista de bitacoras de seguimiento de incidencias
        /// </summary>
        /// <param name="opcion">Indicador de el tipo de bitácora ("INSTALACION" ó "ESTRUCTURA") </param>
        /// <param name="idReporte">Identificador del reporte</param>
        /// <param name="usuario">Nombre del usuario</param>
        /// <returns></returns>
        [HttpGet]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IEnumerable<BitacoraDto>>> ObtenerPorOpcion([Required] string opcion, [Required] int idReporte, [Required]string usuario)
        {
            try
            {
                var bitacora = await _rp.ObtenerBitacoraPorOpcionAsync( opcion, idReporte, usuario);

                if (bitacora != null && bitacora.Count > 0)
                {
                    return Ok(bitacora);
                }

                return NotFound(new List<BitacoraDto>());
            }
            catch (Exception ex)
            {
                _log.LogError($"error al obtener bitácoras para la opcion: {opcion}, idReporte: {idReporte}, usuario: {usuario} ", ex);
                return StatusCode(500, "Ocurrió un problema mientras se procesaba la petición");
            }
        }

        /// <summary>
        /// Agrega un registro de bitácora de seguimiento de incidencias
        /// </summary>
        /// <param name="r">Bitácora de incidencias a agregar</param>
        /// <returns></returns>
        [HttpPost]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> AgregarPorOpcion([FromBody] BitacoraDtoForCreate r)
        {
            try
            {
                await _rp.AgregaBitacoraPorOpcionAsync(r.strTipoIncidencia, r.intIdReporte, r.intIdEstadoIncidencia, r.strDescripcion, r.strUsuario);
                return StatusCode(201, "Ok");
            }
            catch (Exception ex)
            {
                _log.LogError($"error al agregar bitácora de seguimiento para la opción: {r.strTipoIncidencia}, idReporte: {r.intIdReporte}, estado: {r.intIdEstadoIncidencia}, descripcion:{r.strDescripcion} , usuario: {r.strUsuario} ", ex);
                return StatusCode(500, "Ocurrió un problema mientras se procesaba la petición");
            }
        }
    }
}
