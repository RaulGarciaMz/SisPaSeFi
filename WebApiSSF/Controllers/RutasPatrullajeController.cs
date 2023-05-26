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
    public class RutasPatrullajeController : ControllerBase
    {
        private readonly IRutaService _rp;
        private readonly ILogger<RutasPatrullajeController> _log;

        public RutasPatrullajeController(IRutaService r, ILogger<RutasPatrullajeController> log)
        {
            //_rp = new RutaService(new SqlServerAdapter.RutaRepository(new RutaContext()));
            _rp = r ?? throw new ArgumentNullException(nameof(r));
            _log = log;
        }

        /// <summary>
        /// Obtiene las rutas de patrullaje acorde a los parámetros indicados
        /// </summary>
        /// <param name="usuario">Nombre del usuario</param>
        /// <param name="opcion">Indicador del filtro que se incluirá (0 - por observaciones , 1 - Por región militar, 2 - Por región SSF)</param>
        /// <param name="tipo">Tipo de patrullaje (TERRESTRE o AEREO)</param>
        /// <param name="criterio">Descripción del criterio de búsqueda en una ruta. Si el parámetro opción es 2, entonces lleva el valor del identificador de la región SSF </param>
        /// <param name="actividad">Descripción del tipo de actividad a la que pertenece la ruta ("Propuestas", "Programas", "Rutas") </param>
        /// <returns></returns>
        [HttpGet]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IEnumerable<RutaDto>>> GetValues([Required] string usuario, [Required] int opcion, [Required] string tipo, string criterio, string actividad)
        {
            try
            {
                var rutas = await _rp.ObtenerPorFiltroAsync(usuario, opcion, tipo, criterio, actividad);

                if (rutas != null && rutas.Count > 0) 
                {
                    return Ok(rutas);
                }
                
                return NotFound(new List<RutaDto>());
            }
            catch (Exception ex)
            {
                _log.LogError($"error al obtener rutas para la opcion: {opcion}, tipo: {tipo}, criterio: {criterio}, usuario: {usuario}, actividad {actividad} - ", ex);
                return StatusCode(500, "Ocurrió un problema mientras se procesaba la petición - "+  ex.Message);
            }    
        }

        /// <summary>
        /// Obtiene las rutas disponibles en una región para realizar cambio de ruta
        /// </summary>
        /// <param name="region">Identificador de la región</param>
        /// <param name="fecha">Fecha del patrullaje</param>
        /// <returns></returns>
        [HttpGet]
        [Route("cambioruta")]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IEnumerable<RutaDisponibleDto>>> ObtenerRutasDisponiblesParaCambioDeRutaGetValues([Required] string region, [Required] string fecha)
        {
            try
            {
                var f = DateTime.Parse(fecha);

                var rutas = await _rp.ObtenerRutasDisponiblesParaCambioDeRutaAsync(region,f);

                if (rutas != null && rutas.Count > 0)
                {
                    return Ok(rutas);
                }

                return NotFound(new List<RutaDisponibleDto>());
            }
            catch (Exception ex)
            {
                _log.LogError($"error al obtener rutas para la region: {region}, fecha: {fecha} - ", ex);
                return StatusCode(500, "Ocurrió un problema mientras se procesaba la petición - " + ex.Message);
            }
        }

        /// <summary>
        /// Registra una ruta de patrullaje
        /// </summary>
        /// <param name="usuario">Nombre del usuario que registra la ruta de patrullaje</param>
        /// <param name="objRutaPatrullaje">Ruta de patrullaje</param>
        /// <returns></returns>
        [HttpPost]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> PostValue([Required] string usuario,[FromBody] RutaDto objRutaPatrullaje)
        {
            try
            {
                await _rp.AgregaAsync(objRutaPatrullaje, usuario);
                return StatusCode(201, "Ok");
            }
            catch (Exception ex)
            {
                _log.LogError($"error al agregar rutas para el usuario: {usuario}", ex);
                return StatusCode(500, "Ocurrió un problema mientras se procesaba la petición - " + ex.Message);
            }
        }

        /// <summary>
        /// Actualiza una ruta de patrullaje
        /// </summary>
        /// <param name="usuario">Nombre del usuario que realiza la actualización de la ruta de patrullaje</param>
        /// <param name="strDatos">Ruta de patrullaje con los datos a actualizar</param>
        /// <returns></returns>
        [HttpPut]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult> PutValue([Required] string usuario, [FromBody] RutaDto strDatos)
        {
            try
            {
                await _rp.UpdateAsync(strDatos, usuario);
                return Ok();
            }
            catch (Exception ex)
            {
                _log.LogError($"error al actualizar la ruta con id: {strDatos.intIdRuta} para el usuario: {usuario} - ", ex);
                return StatusCode(500, "Ocurrió un problema mientras se procesaba la petición - " + ex.Message);
            }            
        }

        /// <summary>
        /// Reinicia las regiones militares
        /// </summary>
        /// <param name="opcion">descripción de la opción de cambio ("ReiniciarClavesRegionMilitar")</param>
        /// <param name="regionMilitar">Identificador de la región militar</param>
        /// <param name="tipoPatrullaje">Descripción del tipo de patrullaje</param>
        /// <param name="usuario">Nombre del usuario que realiza la operación</param>
        /// <returns></returns>
        [HttpPut]
        [Route("reiniciaregion")]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult> ReiniciaRegionMilitar([Required] string opcion, [Required] string regionMilitar, [Required] string tipoPatrullaje, [Required] string usuario)
        {
            try
            {
                await _rp.ReiniciaRegionMilitarAsync(opcion, regionMilitar, tipoPatrullaje, usuario);
                return Ok();
            }
            catch (Exception ex)
            {
                _log.LogError($"error al reiniciar las regiones militares para region Militar: {regionMilitar}, tipo de patrullaje: {tipoPatrullaje} para el usuario: {usuario} - ", ex);
                return StatusCode(500, "Ocurrió un problema mientras se procesaba la petición - " + ex.Message);
            }
        }

        /// <summary>
        /// Elimina una ruta de patrullaje, siempre y cuando no esté bloqueado el registro
        /// </summary>
        /// <param name="id">Identificador de la ruta de patrullaje</param>
        /// <param name="usuario">Nombre del usuario que elimina la ruta de patrullaje</param>
        /// <returns></returns>
        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> DeleteValue([Required] int id, [Required] string usuario)
        {
            try
            {
                await _rp.DeleteAsync(id, usuario);
                return Ok();
            }
            catch (Exception ex)
            {
                _log.LogError($"error al eliminar la ruta con id: {id} para el usuario: {usuario}", ex);
                return StatusCode(500, "Ocurrió un problema mientras se procesaba la petición - " + ex.Message);
            }
        }
    }
}
