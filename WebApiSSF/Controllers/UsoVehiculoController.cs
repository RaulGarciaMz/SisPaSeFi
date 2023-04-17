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
    public class UsoVehiculoController : ControllerBase
    {
        private readonly IUsoVehiculoService _pp;
        private readonly ILogger<UsoVehiculoController> _log;

        public UsoVehiculoController(IUsoVehiculoService ps, ILogger<UsoVehiculoController> log)
        {
            _pp = ps ?? throw new ArgumentNullException(nameof(ps));
            _log = log;
        }

        /// <summary>
        /// Obtiene 
        /// </summary>
        /// <param name="id">Identificador del programa de patrullaje</param>
        /// <param name="usuario">Nombre del usuario (alias o usuario_nom) que realiza la operación</param>
        /// <returns></returns>
        [HttpGet("{id}")]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IEnumerable<UsoVehiculoVista>>> ObtenerUsoVehiculosPorPrograma(int id, [Required] string usuario)
        {
            try
            {
                var usos = await _pp.ObtenerUsoVehiculosPorProgramaAsync(id, usuario);

                if (usos == null || usos.Count() == 0)
                {
                    return NotFound();
                }

                return Ok(usos);
            }
            catch (Exception ex)
            {
                _log.LogInformation($"error al obtener usos de vehículos para el programa: {id}, usuario: {usuario}", ex);
                return StatusCode(500, "Ocurrió un problema mientras se procesaba la petición");
            }
        }

        /// <summary>
        /// Agrega un uso de vehiculo a un programa de patrullaje
        /// </summary>
        /// <param name="uso">Uso de vehículo a agregar</param>
        /// <returns></returns>
        [HttpPost]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> Agrega([FromBody] UsoVehiculoDto uso)
        {
            try
            {
                await _pp.AgregaAsync(uso);
                return Ok();
            }
            catch (Exception ex)
            {
                _log.LogInformation($"error al agregar un uso de vehículo para el programa: {uso.IdPrograma}, usuario: {uso.Usuario}", ex);
                return StatusCode(500, "Ocurrió un problema mientras se procesaba la petición");
            }
        }

        /// <summary>
        /// Actualiza el uso de un vehículo para un programa de patrullaje
        /// </summary>
        /// <param name="id">Identificador del programa de patrullaje</param>
        /// <param name="uso">Uso de vehículo a actualizar</param>
        /// <returns></returns>
        [HttpPut("{id}")]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> Actualiza(int id, [FromBody] UsoVehiculoDto uso)
        {
            try
            {
                await _pp.ActualizaAsync(uso);
                return Ok();
            }
            catch (Exception ex)
            {
                _log.LogInformation($"error al actualizar un uso de vehículo para el programa: {uso.IdPrograma}, usuario: {uso.Usuario}", ex);
                return StatusCode(500, "Ocurrió un problema mientras se procesaba la petición");
            }
        }

        /// <summary>
        /// Borra un uso de vehículo para un progrma de patrullaje
        /// </summary>
        /// <param name="id">Identificador del programa de patrullaje</param>
        /// <param name="idVehiculo">Identificador del vehículo a borrar</param>
        /// <param name="usuario">Nombre del usuario (alias o usuario_nom) que realiza la operación</param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> Delete(int id, int idVehiculo, string usuario)
        {
            try
            {
                await _pp.BorraAsync(id,idVehiculo,usuario);
                return Ok();
            }
            catch (Exception ex)
            {
                _log.LogInformation($"error al eliminar un uso de vehículo para el programa: {id}, usuario: {usuario}", ex);
                return StatusCode(500, "Ocurrió un problema mientras se procesaba la petición");
            }
        }
    }
}
