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
        /// <param name="opcion">Indicador del tipo de filtro para aplicar en la obtención de líneas (0 - Por clave de línea, 1 - Por ubicación del punto de inicio de la línea, 2 - Por ubicación del punto de final de la línea, 3 - Por un cuadrado definido alrededor de la ruta indicada, 4 - Por un radio de 5 Km alrededor del punto indicado, 5 - Por un radio de 5 Km alrededor de las coordenadas indicadas)</param>
        /// <param name="criterio">Criterio de búsqueda acorde a la opción indicada. Si la opción es: 0 - criterio es la clave de la línea (texto), 1 - criterio es la ubicación (o parte del texto) del punto de inicio de la línea (texto), 2 - criterio es la ubicación (o parte del texto) del punto de final de la línea (texto), 3 - criterio es el identificador de la ruta (entero), 4 - criterio es el identificador del punto de patrullaje (entero), 5 - criterio es el valor de las coordenadas x,y (texto)</param>
        /// <param name="usuario">Nombre del usuario (usuario_nom) que realiza la operación</param>
        /// <returns></returns>
        [HttpGet]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IEnumerable<LineaDto>>> ObtenerLineas([Required]int opcion, [Required] string criterio, [Required] string usuario)
        {
            try
            {

/*                int numParametros = HttpContext.Request.Query.Count;

                if (numParametros <= 0 || numParametros >3 )
                    return BadRequest("Número de parámetros erróneo");*/

                if (opcion == 3) 
                {
                    var esNumero = Int32.TryParse(criterio, out int val);
                    if (!esNumero)
                        return BadRequest("criterio debe ser el entero identificador de la ruta");
                }

                var lineas = await _pp.ObtenerLineasAsync(opcion, criterio, usuario);

                if (lineas == null) 
                {
                    return NotFound(new List<LineaDto>());
                }

                if (lineas.Count == 0)
                {
                    return NotFound(new List<LineaDto>());
                }

                return Ok(lineas);
            }
            catch (Exception ex)
            {
                _log.LogError($"error al obtener líneas de patrullaje para la opción: {opcion}, criterio: {criterio} para el usuario: {usuario} ", ex);
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
                _log.LogError($"error al agregar línea de patrullaje para el usuario: {l.strUsuario} ", ex);
                return StatusCode(500, "Ocurrió un problema mientras se procesaba la petición - " +  ex.Message);
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
                _log.LogError($"error al actualizar la línea de patrullaje: {l.intIdLinea} para el usuario: {l.strUsuario} ", ex);
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
                _log.LogError($"error al eliminar la línea de patrullaje: {id} para el usuario: {usuario} ", ex);
                return StatusCode(500, "Ocurrió un problema mientras se procesaba la petición");
            }
        }
    }
}
