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
        /// <param name="idPrograma">Identificador del programa de patrullaje</param>
        /// <param name="usuario">Nombre del usuario (alias o usuario_nom) que realiza la operación</param>
        /// <returns></returns>
        [HttpGet]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IEnumerable<UsoVehiculoDtoGet>>> ObtenerUsoVehiculosPorPrograma([Required] int idPrograma, [Required] string usuario)
        {
            try
            {
                var usos = await _pp.ObtenerUsoVehiculosPorProgramaAsync(idPrograma, usuario);

                if (usos == null || usos.Count() == 0)
                {
                    return NotFound(new List<UsoVehiculoDtoGet>());
                }

                var lsta = ConvierteListaUsoVehiculaVistaToListaDto(usos);
                return Ok(lsta);
            }
            catch (Exception ex)
            {
                _log.LogError($"error al obtener usos de vehículos para el programa: {idPrograma}, usuario: {usuario}", ex);
                return StatusCode(500, "Ocurrió un problema mientras se procesaba la petición - " + ex.Message);
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
        public async Task<ActionResult> Agrega([FromBody] UsoVehiculoDtoForCreateOrUpdate uso)
        {
            try
            {
                await _pp.AgregaAsync(uso);
                return Ok();
            }
            catch (Exception ex)
            {
                _log.LogError($"error al agregar un uso de vehículo para el programa: {uso.IdPrograma}, usuario: {uso.Usuario}", ex);
                return StatusCode(500, "Ocurrió un problema mientras se procesaba la petición - " + ex.Message);
            }
        }

        /// <summary>
        /// Actualiza el uso de un vehículo para un programa de patrullaje
        /// </summary>
        /// <param name="uso">Uso de vehículo a actualizar</param>
        /// <returns></returns>
        [HttpPut]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> Actualiza([FromBody] UsoVehiculoDtoForCreateOrUpdate uso)
        {
            try
            {
                await _pp.ActualizaAsync(uso);
                return Ok();
            }
            catch (Exception ex)
            {
                _log.LogError($"error al actualizar un uso de vehículo para el programa: {uso.IdPrograma}, usuario: {uso.Usuario}", ex);
                return StatusCode(500, "Ocurrió un problema mientras se procesaba la petición - " + ex.Message);
            }
        }

        /// <summary>
        /// Borra un uso de vehículo para un progrma de patrullaje
        /// </summary>
        /// <param name="idPrograma">Identificador del programa de patrullaje</param>
        /// <param name="idVehiculo">Identificador del vehículo a borrar</param>
        /// <param name="usuario">Nombre del usuario (alias o usuario_nom) que realiza la operación</param>
        /// <returns></returns>
        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> Delete([Required] int idPrograma, [Required] int idVehiculo, [Required] string usuario)
        {
            try
            {
                await _pp.BorraAsync(idPrograma,idVehiculo,usuario);
                return Ok();
            }
            catch (Exception ex)
            {
                _log.LogError($"error al eliminar un uso de vehículo para el programa: {idPrograma}, usuario: {usuario}", ex);
                return StatusCode(500, "Ocurrió un problema mientras se procesaba la petición - " + ex.Message);
            }
        }

        private UsoVehiculoDtoGet ConvierteUsoVehiculoVistaToDto(UsoVehiculoVista v)
        {
            return new UsoVehiculoDtoGet() 
            {
                intConsumoCombustible = v.consumoCombustible,
                intIdPrograma = v.id_programa,
                intIdUsoVehiculo = v.id_usoVehiculo,
                intIdUsuarioVehiculo = v.id_usuarioVehiculo,
                intIdVehiculo = v.id_vehiculo,
                intKmFin = v.kmFin,
                intKmInicio = v.kmInicio,
                strEstadoVehiculo = v.estadoVehiculo,
                strMatricula = v.matricula,
                strNumeroEconomico = v.numeroEconomico,               
            };
        }

        private List<UsoVehiculoDtoGet> ConvierteListaUsoVehiculaVistaToListaDto(List<UsoVehiculoVista> cv)
        { 
            var lsta = new List<UsoVehiculoDtoGet>();

            foreach (var v in cv) 
            {
                var nvo = ConvierteUsoVehiculoVistaToDto(v);

                lsta.Add(nvo);
            }

            return lsta;
        }
    }
}
