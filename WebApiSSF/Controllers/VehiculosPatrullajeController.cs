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
    public class VehiculosPatrullajeController : ControllerBase
    {
        private readonly IVehiculoService _pp;
        private readonly ILogger<VehiculosPatrullajeController> _log;

        public VehiculosPatrullajeController(IVehiculoService p, ILogger<VehiculosPatrullajeController> log)
        {
            _pp = p ?? throw new ArgumentNullException(nameof(p));
            _log = log ?? throw new ArgumentNullException(nameof(log));
        }

        /// <summary>
        /// Obtiene la lista de vehiculos para patrullaje acorde a los parámetros indicados
        /// </summary>
        /// <param name="opcion">Descripción del tipo de patrullaje al que se asigna al vehículo ("TODOS", "AEREO", "TERRESTRE", "VEHICULOSPATRULLAJEEXTRAORDINARIO-XX", "AEREOHABILITADOS", "TERRESTREHABILITADOS"). Cuando la opción es VEHICULOSPATRULLAJEEXTRAORDINARIO-XX, XX debe contener la descripción del tipo de patrullaje</param>
        /// <param name="regionSSF">Identificador de la comandancia (región) a la que está asignado el vehículo. Este parámetro es obligatorio para las opciones: "TODOS", "AEREO" y "TERRESTRE"</param>
        /// <param name="criterio">Patrón de texto para buscar en matrícula o en número económico excepto si la opción es VEHICULOSPATRULLAJEEXTRAORDINARIO, en cuyo caso criterio es el identificador de la propuesta</param>
        /// <param name="usuario">Nombre del usuario (alias o usuario_nom) que realiza la consulta</param>
        /// <returns></returns>
        [HttpGet]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IEnumerable<VehiculoDto>>> ObtenerVehiculosPorOpcion([Required] string opcion, int regionSSF,  [Required] string usuario, string? criterio )
        {
            try
            {
                var v = await _pp.ObtenerVehiculosPorOpcionAsync(opcion, regionSSF, criterio, usuario);

                if (v == null)
                {
                    return NotFound(new List<VehiculoDto>());
                }

                if (v.Count == 0)
                {
                    return NotFound(new List<VehiculoDto>());
                }

                return Ok(v);
            }
            catch (Exception ex)
            {
                _log.LogError($"error al obtener la lista de vehículos para opción: {opcion}, región: {regionSSF}, criterio: {criterio}, usuario: {usuario} ", ex);
                return StatusCode(500, "Ocurrió un problema mientras se procesaba la petición");
            }            
        }

        /// <summary>
        /// Agrega un vehículo para patrullaje
        /// </summary>
        /// <param name="vehiculo">Vehículo a dar de alta</param>
        /// <returns></returns>
        [HttpPost]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> Agrega([FromBody] VehiculoDtoForCreate vehiculo)
        {
            try
            {
                await _pp.AgregaAsync(vehiculo);
                return Ok();
            }
            catch (Exception ex)
            {
                _log.LogError($"error al crear un vehículo para el usuario: {vehiculo.strUsuario} ", ex);
                return StatusCode(500, "Ocurrió un problema mientras se procesaba la petición");
            }
        }

        /// <summary>
        /// Actualiza un vehículo
        /// </summary>
        /// <param name="vehiculo">Vehículo a actualizar</param>
        /// <returns></returns>
        [HttpPut]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> Actualiza([FromBody] VehiculoDtoForUpdate vehiculo)
        {
            try
            {
                await _pp.ActualizaAsync(vehiculo);
                return Ok();
            }
            catch (Exception ex)
            {
                _log.LogError($"error al actualizar un vehículo para el usuario: {vehiculo.strUsuario} ", ex);
                return StatusCode(500, "Ocurrió un problema mientras se procesaba la petición");
            }
        }

        /// <summary>
        /// Borra un uso de vehículo acorde a las opciones indicadas
        /// </summary>
        /// <param name="opcion">Descripción de la opción de borrado ("EliminarVehiculoDePrograma")</param>
        /// <param name="dato">Cadena de texto que indica separado por "-" el identificador del program y el identificador del vehículo. por ejemplo: "324-89"</param>
        /// <param name="usuario">Nombre del usuario (alias o usuario_nom) que realiza la operación</param>
        /// <returns></returns>
        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> BorraPorOpcion([Required] string opcion, string dato, [Required] string usuario )
        {
            try
            {
                await _pp.BorraPorOpcionAsync(opcion,  dato,  usuario);
                return Ok();
            }
            catch (Exception ex)
            {
                _log.LogError($"error al borrar un vehículo para la opcion: {opcion}, dato: {dato}, usuario: {usuario} ", ex);
                return StatusCode(500, "Ocurrió un problema mientras se procesaba la petición");
            }
        }
    }
}
