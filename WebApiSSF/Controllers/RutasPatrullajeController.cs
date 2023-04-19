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
        /// <param name="actividad">Descripción del tipo de actividad a la que pertenece la ruta ("Propuestas", "Programas") </param>
        /// <returns></returns>
        [HttpGet]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IEnumerable<RutaDto>>> GetValues([Required] string usuario, [Required] int opcion, [Required] string tipo, string criterio, string actividad)
        {
            try
            {
                var rutas = await _rp.ObtenerPorFiltro(usuario, opcion, tipo, criterio, actividad);

                if (rutas != null && rutas.Count > 0) 
                {
                    return Ok(rutas);
                }
                
                return NotFound();
            }
            catch (Exception ex)
            {
                _log.LogError($"error al obtener rutas para la opcion: {opcion}, tipo: {tipo}, criterio: {criterio}, usuario: {usuario}, actividad {actividad}", ex);
                return StatusCode(500, "Ocurrió un problema mientras se procesaba la petición");
            }    
        }

        /// <summary>
        /// Registra una ruta de patrullaje
        /// </summary>
        /// <param name="usuario">Nombre del usuario que registra la ruta de patrullaje</param>
        /// <param name="r">Ruta de patrullaje</param>
        /// <returns></returns>
        [HttpPost]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> PostValue([Required] string usuario,[FromBody] RutaDto r)
        {
            try
            {
                await _rp.Agrega(r, usuario);
                return StatusCode(201, "Ok");
            }
            catch (Exception ex)
            {
                _log.LogError($"error al agregar rutas para el usuario: {usuario}", ex);
                return StatusCode(500, "Ocurrió un problema mientras se procesaba la petición");
            }
        }

        /// <summary>
        /// Actualiza una ruta de patrullaje
        /// </summary>
        /// <param name="id">Identificador de la ruta de patrullaje a actualizar</param>
        /// <param name="usuario">Nombre del usuario que realiza la actualización de la ruta de patrullaje</param>
        /// <param name="r">Ruta de patrullaje con los datos a actualizar</param>
        /// <returns></returns>
        [HttpPut("{id}")]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult> PutValue([Required] int id, [Required] string usuario, [FromBody] RutaDto r)
        {
            try
            {
                await _rp.Update(r, usuario);
                return Ok();
            }
            catch (Exception ex)
            {
                _log.LogError($"error al actualizar la ruta con id: {id} para el usuario: {usuario}", ex);
                return StatusCode(500, "Ocurrió un problema mientras se procesaba la petición");
            }            
        }

        /// <summary>
        /// Elimina una ruta de patrullaje, siempre y cuando no esté bloqueado el registro
        /// </summary>
        /// <param name="id">Identificador de la ruta de patrullaje</param>
        /// <param name="usuario">Nombre del usuario que elimina la ruta de patrullaje</param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> DeleteValue([Required] int id, [Required] string usuario)
        {
            try
            {
                await _rp.Delete(id, usuario);
                return Ok();
            }
            catch (Exception ex)
            {
                _log.LogError($"error al eliminar la ruta con id: {id} para el usuario: {usuario}", ex);
                return StatusCode(500, "Ocurrió un problema mientras se procesaba la petición");
            }
        }
    }
}
