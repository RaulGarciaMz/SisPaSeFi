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
    public class ItinerarioPatrullajeController : ControllerBase
    {
        private readonly IItinerariosService _rp;
        private readonly ILogger<ItinerarioPatrullajeController> _log;

        public ItinerarioPatrullajeController(IItinerariosService r, ILogger<ItinerarioPatrullajeController> log)
        {
            _rp = r ?? throw new ArgumentNullException(nameof(r));
            _log = log;
        }

        /// <summary>
        /// Obtiene la lista de itinerarios para una ruta indicada
        /// </summary>
        /// <param name="idRuta">Identificador de la ruta</param>
        /// <param name="usuario">Nombre del usuario (alias o usuaio_nom) que realiza la operación</param>
        /// <returns></returns>
        [HttpGet]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IEnumerable<ItinerarioVista>>> ObtenerItinerariosporRuta([Required]int idRuta, [Required] string usuario)
        {
            try
            {
                var l = await _rp.ObtenerItinerariosporRutaAsync(idRuta, usuario);

                if (l == null)
                {
                    return NotFound(new List<ItinerarioVista>());
                }

                if (l.Count == 0)
                {
                    return NotFound(new List<ItinerarioVista>());
                }

                return Ok(l);
            }
            catch (Exception ex)
            {
                _log.LogError($"error al obtener itinerarios para la ruta: {idRuta}, usuario: {usuario}", ex);
                return StatusCode(500, "Ocurrió un problema mientras se procesaba la petición");
            }
        }

  
        /// <summary>
        /// Agrega un itinerario
        /// </summary>
        /// <param name="v">Itinerario a agregar</param>
        /// <returns></returns>
        [HttpPost]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> AgregaItinerario([FromBody] ItinerarioDtoForCreate v)
        {
            try
            {
                await _rp.AgregaItinerarioAsync(v);
                return StatusCode(201, "Ok");
            }
            catch (Exception ex)
            {
                _log.LogError($"error al agregar itinerario la ruta: {v.intIdRuta}, punto: {v.intIdPunto}, posición: {v.intPosicion}, usuario: {v.strUsuario}", ex);
                return StatusCode(500, "Ocurrió un problema mientras se procesaba la petición");
            }
        }

        /// <summary>
        /// Actualiza itinerarios
        /// </summary>
        /// <param name="v">Itinerario a actualizar</param>
        /// <returns></returns>
        [HttpPut]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> Put( [FromBody] ItinerarioDtoForUpdate v)
        {
            try
            {
                await _rp.ActualizaItinerarioAsync(v);
                return Ok();
            }
            catch (Exception ex)
            {
                _log.LogError($"error al agregar itinerario la ruta: {v.intIdRuta}, punto: {v.intIdPunto}, posición: {v.intPosicion}, usuario: {v.strUsuario}", ex);
                return StatusCode(500, "Ocurrió un problema mientras se procesaba la petición");
            }
        }

        /// <summary>
        /// Borra itinerarios
        /// </summary>
        /// <param name="id">Identificador del itinerario</param>
        /// <param name="usuario">Nombre del usuario (alisa o user_nom) que realiza la operación</param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> Delete(int id, [Required] string usuario)
        {
            try
            {
                await _rp.BorraItinerarioAsync(id, usuario);
                return Ok();
            }
            catch (Exception ex)
            {
                _log.LogError($"error al borrar el itinerario: {id}, usuario: {usuario}", ex);
                return StatusCode(500, "Ocurrió un problema mientras se procesaba la petición");
            }
        }
    }
}
