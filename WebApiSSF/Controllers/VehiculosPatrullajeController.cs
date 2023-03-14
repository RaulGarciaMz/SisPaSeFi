using Domain.DTOs;
using Domain.Entities;
using Domain.Entities.Vistas;
using Domain.Ports.Driving;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
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
        /// <param name="opcion">Descripción del tipo de patrullaje al que se asigna al vehículo ("TODOS", "AEREO", "TERRESTRE", "VEHICULOSPATRULLAJEEXTRAORDINARIO-XX"). Cuando la opción es VEHICULOSPATRULLAJEEXTRAORDINARIO-XX, XX debe contener la descripción del tipo de patrullaje</param>
        /// <param name="region">Identificador de la comandancia (región) a la que está asignado el vehículo. Este parámetro es obligatorio para las opciones: "TODOS", "AEREO" y "TERRESTRE"</param>
        /// <param name="criterio">Patrón de texto para buscar en matrícula o en número económico excepto si la opción es VEHICULOSPATRULLAJEEXTRAORDINARIO, en cuyo caso criterio es el identificador de la propuesta</param>
        /// <param name="usuario">Nombre del usuario (alias o usuario_nom) que realiza la consulta</param>
        /// <returns></returns>
        [HttpGet]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IEnumerable<VehiculoPatrullajeVista>>> ObtenerVehiculosPorOpcion([Required] string opcion, int region,  [Required] string usuario, string? criterio)
        {
            try
            {
                var v = await _pp.ObtenerVehiculosPorOpcionAsync(opcion, region, criterio, usuario);

                if (v == null)
                {
                    return NotFound();
                }

                if (v.Count == 0)
                {
                    return NotFound();
                }

                return Ok(v);
            }
            catch (Exception ex)
            {
                _log.LogInformation($"error al obtener la lista de vehículos para opción: {opcion}, región: {region}, criterio: {criterio}, usuario: {usuario}", ex);
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
                _log.LogInformation($"error al crear un vehículo para el usuario: {vehiculo.Usuario}", ex);
                return StatusCode(500, "Ocurrió un problema mientras se procesaba la petición");
            }
        }

        /// <summary>
        /// Actualiza un vehículo
        /// </summary>
        /// <param name="id">Identificador del vehículo a actualizar</param>
        /// <param name="vehiculo">Vehículo a actualizar</param>
        /// <returns></returns>
        [HttpPut("{id}")]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> Actualiza(int id, [FromBody] VehiculoDtoForUpdate vehiculo)
        {
            try
            {
                await _pp.ActualizaAsync(vehiculo);
                return Ok();
            }
            catch (Exception ex)
            {
                _log.LogInformation($"error al actualizar un vehículo para el usuario: {vehiculo.Usuario}", ex);
                return StatusCode(500, "Ocurrió un problema mientras se procesaba la petición");
            }
        }
    }
}
