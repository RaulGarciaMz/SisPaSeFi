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
    public class LineasPatrullajeController : ControllerBase
    {
        private readonly ILineasService _pp;
        private readonly ILogger<LineasPatrullajeController> _log;

        public LineasPatrullajeController(ILineasService ps, ILogger<LineasPatrullajeController> log)
        {
            _pp = ps ?? throw new ArgumentNullException(nameof(ps));
            _log = log;
        }
        
        /// <summary>
        /// Obtiene las líneas de patrullaje acorde a las opciones indicadas
        /// </summary>
        /// <param name="opcion">Indicador del tipo de filtro para aplicar en la obtención de líneas (de 0 a 5)</param>
        /// <param name="criterio">Criterio de búsqueda acorde a la opción indicada. si la opción es 4, debe representar el identificador de un punto de patrullaje (un entero)</param>
        /// <param name="usuario">Nombre del usuario (usuario_nom) que realiza la operación</param>
        /// <returns></returns>
        [HttpGet]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IEnumerable<LineaVista>>> ObtenerLineas([Required]int opcion, [Required] string criterio, [Required] string usuario)
        {
            try
            {
                var lineas = await _pp.ObtenerLineasAsync(opcion, criterio, usuario);

                if (lineas == null) 
                {
                    return NotFound();
                }

                if (lineas.Count == 0)
                { 
                    return NotFound();
                }

                return Ok(lineas);
            }
            catch (Exception ex)
            {
                _log.LogError($"error al obtener líneas de patrullaje para la opción: {opcion}, criterio: {criterio} para el usuario: {usuario}", ex);
                return StatusCode(500, "Ocurrió un problema mientras se procesaba la petición");
            }
        }

        /// <summary>
        /// Agrega una línea de patrullaje
        /// </summary>
        /// <param name="l">Línea de patrullaje a agregar</param>
        /// <returns></returns>
        [HttpPost]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> Agregar([FromBody] LineaDtoForCreate l)
        {
            try
            {
                await _pp.AgregarAsync(l);
                return Ok();
            }
            catch (Exception ex)
            {
                _log.LogError($"error al agregar línea de patrullaje para el usuario: {l.strUsuario}", ex);
                return StatusCode(500, "Ocurrió un problema mientras se procesaba la petición");
            }
        }

        /// <summary>
        /// Actualiza líneas de patrullaje
        /// </summary>
        /// <param name="l">Datos de la línea a actualizar</param>
        /// <returns></returns>
        [HttpPut]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> Actualizar([FromBody] LineaDtoForUpdate l)
        {
            try
            {
                await _pp.ActualizaAsync(l);
                return Ok();
            }
            catch (Exception ex)
            {
                _log.LogError($"error al actualizar la línea de patrullaje: {l.intIdLinea} para el usuario: {l.strUsuario}", ex);
                return StatusCode(500, "Ocurrió un problema mientras se procesaba la petición");
            }
        }

        /// <summary>
        /// Elimina líneas de patrullaje
        /// </summary>
        /// <param name="id">Identificador de la línea a eliminar</param>
        /// <param name="usuario">Nombre del usuario (usuario_nom) que realiza la operación</param>
        /// <returns></returns>
        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> Delete([Required] int id, [Required]string usuario)
        {
            try
            {
                await _pp.BorraAsync(id, usuario);
                return Ok();
            }
            catch (Exception ex)
            {
                _log.LogError($"error al eliminar la línea de patrullaje: {id} para el usuario: {usuario}", ex);
                return StatusCode(500, "Ocurrió un problema mientras se procesaba la petición");
            }
        }
    }
}
