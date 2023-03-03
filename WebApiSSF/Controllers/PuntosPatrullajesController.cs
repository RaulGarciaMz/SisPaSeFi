using Domain.DTOs;
using Domain.Entities;
using Domain.Enums;
using Domain.Ports.Driving;
using DomainServices.DomServ;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using SqlServerAdapter.Data;
using System.ComponentModel.DataAnnotations;
using System.Drawing;
using System.Net.Mime;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApiSSF.Controllers
{
    [Route("api/[controller]")]
    //[Authorize]
    [ApiController]
    public class PuntosPatrullajesController : ControllerBase
    {
        private readonly IPuntosService _pp;
        private readonly ILogger<PuntosPatrullajesController> _log;

        public PuntosPatrullajesController(IPuntosService p, ILogger<PuntosPatrullajesController> log)
        {
            //_pp = new PuntosService(new SqlServerAdapter.PuntoPatrullajeRepository(new PatrullajeContext()));
            _pp = p ?? throw new ArgumentNullException(nameof(p));
            _log = log;
        }

        /// <summary>
        /// Obtiene los puntos de patrullaje acorde a los parámetros indicados, siempre y cuando el usuario sea configurador
        /// </summary>
        /// <param name="opcion">Indicador del tipo de filtro a realizar (0 - Por ubicación del punto, 1 - Por estado de la república) </param>
        /// <param name="valor">Descripción de la ubicación del punto o del estado de la república. Debe ser acorde al parámetro opción</param>
        /// <param name="usuario">Nombre del usuario con privilegios de configurador</param>
        /// <returns></returns>
        [HttpGet]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IEnumerable<PuntoDto>>> GetValues([Required] int opcion, [Required] string valor, [Required] string usuario)
        {
            try
            {
                FiltroPunto filtro = (FiltroPunto)opcion;

                var puntos = await _pp.ObtenerPorOpcionAsync(filtro, valor, usuario);

                if (puntos == null)
                {
                    return NotFound();
                }

                return Ok(puntos);
            }
            catch (Exception ex)
            {
                _log.LogInformation($"error al obtener puntos de patrullaje para la opcion: {opcion}, criterio: {valor}, usuario: {usuario}", ex);
                string error = "Ocurrió un problema mientras se procesaba la petición " + ex.ToString();
                return StatusCode(500, error);
                //return StatusCode(500, "Ocurrió un problema mientras se procesaba la petición");
            } 
        }

        /// <summary>
        /// Registra un punto de patrullaje
        /// </summary>
        /// <param name="usuario">Nombre del usuario que registra el punto de patrullaje</param>
        /// <param name="pto">Punto de patrullaje a registrar</param>
        /// <returns></returns>
        [HttpPost]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> PostValue([Required] string usuario,[FromBody] PuntoDto pto)
        {
            try
            {
                await _pp.Agrega(pto, usuario);
                return StatusCode(201, "Ok");
            }
            catch (Exception ex)
            {
                _log.LogInformation($"error al agregar el punto de patrullaje para el usuario: {usuario}", ex);
                return StatusCode(500, "Ocurrió un problema mientras se procesaba la petición");
            }
        }

        /// <summary>
        /// Actualiza un punto de patrullaje
        /// </summary>
        /// <param name="id">Identificador del punto de patrullaje a actualizar</param>
        /// <param name="usuario">Nombre del usuario que relaiza la actualización</param>
        /// <param name="pto">Punto de patrullaje con datos a actualizar</param>
        /// <returns></returns>
        [HttpPut("{id}")]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> PutValue(int id, [Required] string usuario, [FromBody] PuntoDto pto)
        {
            try
            {
                await _pp.Update(pto, usuario);
                return Ok();
            }
            catch (Exception ex)
            {
                _log.LogInformation($"error al actualizar el punto de patrullaje para el usuario: {usuario}", ex);
                return StatusCode(500, "Ocurrió un problema mientras se procesaba la petición");
            }
        }

        /// <summary>
        /// Elimina un punto de patrullaje
        /// </summary>
        /// <param name="id">Identificador del punto de patrullaje a eliminar</param>
        /// <param name="usuario">Nombre del usuario que elimina el punto de patrullaje</param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> DeleteValue([Required] int id, [Required] string usuario)
        {
            try
            {
                await _pp.Delete(id, usuario);
                return Ok();
            }
            catch (Exception ex)
            {
                _log.LogInformation($"error al eliminar el punto de patrullaje con id: {id} para el usuario: {usuario}", ex);
                return StatusCode(500, "Ocurrió un problema mientras se procesaba la petición");
            }
        }
    }
}
