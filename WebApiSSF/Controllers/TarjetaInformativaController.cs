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
    public class TarjetaInformativaController : ControllerBase
    {
        private readonly ITarjetaService _t;
        private readonly ILogger<TarjetaInformativaController> _log;

        public TarjetaInformativaController(ITarjetaService t, ILogger<TarjetaInformativaController> log)
        {
            //_t = new TarjetasService(new SqlServerAdapter.TarjetaInformativaRepository(new TarjetaInformativaContext()));
            _t = t ?? throw new ArgumentNullException(nameof(t));
            _log = log;
        }

        /// <summary>
        /// Obtiene la lista de tarjetas informativas acorde a los parámetros indicados
        /// </summary>
        /// <param name="opcion">Indicador del tipo de tarjeta iformativa a obtener (1 - Tarjetas informativas por región indicada,  2 - Parte de novedades para un día específico, 3 - Monitoreo de un día en específico)</param>
        /// <param name="tipo">Tipo de patrullaje (TERRESTRE o AEREO)</param>
        /// <param name="anio">Año</param>
        /// <param name="mes">Mes</param>
        /// <param name="dia">Día (utilizado para las opciones 2 y 3)</param>
        /// <param name="usuario">Nombre del usuario (usuario_nom) que realiza la operación</param>
        /// <param name="region">Región de SSF (utilizado sólo para la opción 1)</param>
        /// <returns>ActionResult con lista de tarjetas informativas</returns>
        [HttpGet]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<TarjetaDto>>> ObtenerPorOpcion([Required] int opcion, [Required] string tipo, [Required] int anio, [Required] int mes, int dia, [Required] string usuario, string region)
        {
            try
            {
                var tarjetas = await _t.ObtenerPorOpcion(opcion, tipo, region, anio, mes, dia, usuario);

                return Ok(tarjetas);
            }
            catch (Exception ex)
            {
                _log.LogError($"error al obtener tarjetas informativas para la opción: {opcion}, tipo: {tipo}, usuario: {usuario}, año: {anio}, mes {mes}, día: {dia}, región: {region} ", ex);
                return StatusCode(500, "Ocurrió un problema mientras se procesaba la petición");
            }            
        }

        /// <summary>
        /// Obtiene tqarjeta informativa acorde al identificador indicado
        /// </summary>
        /// <param name="IdTarjeta">Identificador de la tarjeta informativa</param>
        /// <param name="usuario">Nombre del usuario</param>
        /// <returns></returns>
        [HttpGet]
        [Route("id")]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<TarjetaDto>> ObtenerPorId([Required] int IdTarjeta, [Required] string usuario)
        {
            try
            {
                var tarjetas = await _t.ObtenerPorId(IdTarjeta, usuario);

                return Ok(tarjetas);
            }
            catch (Exception ex)
            {
                _log.LogError($"error al obtener tarjetas informativas para el id: {IdTarjeta}, usuario: {usuario} ", ex);
                return StatusCode(500, "Ocurrió un problema mientras se procesaba la petición");
            }
        }

        /// <summary>
        /// Registra una tarjeta informativa
        /// </summary>
        /// <param name="usuario">Nombre del usuario que registra la tarjeta</param>
        /// <param name="tarjeta">Tarjeta informativa a registrar</param>
        /// <returns></returns>
        [HttpPost]
        [Consumes(MediaTypeNames.Application.Json)]        
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> Post([Required] string usuario, [FromBody] TarjetaDto tarjeta)
        {
            try
            {
                await _t.Agrega(tarjeta, usuario);              
                return StatusCode(201, "Ok");
            }
            catch (Exception ex)
            {
                _log.LogError($"error al registrar tarjetas informativas para el usuario: {usuario} ", ex);
                return StatusCode(500, "Ocurrió un problema mientras se procesaba la petición");
            }
        }

        /// <summary>
        /// Actualiza una tarjeta informativa indicada
        /// </summary>
        /// <param name="usuario">Nombre del usuario que realiza la actualización</param>
        /// <param name="tarjeta">Tarjeta informativa con datos a actualizar</param>
        /// <returns></returns>
        [HttpPut]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> Put([Required] string usuario, [FromBody] TarjetaDto tarjeta)
        {
            try
            {
                await _t.Update(tarjeta, usuario);
                return Ok();
            }
            catch (Exception ex)
            {
                _log.LogError($"error al actualizar la tarjeta informativa con id de nota: {tarjeta.intIdNota}, para el usuario: {usuario} ", ex);
                return StatusCode(500, "Ocurrió un problema mientras se procesaba la petición");
            }
        }
    }
}
