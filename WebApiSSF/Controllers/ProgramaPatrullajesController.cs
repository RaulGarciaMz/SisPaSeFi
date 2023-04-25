using Domain.DTOs;
using Domain.Enums;
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
    public class ProgramaPatrullajesController : ControllerBase
    {
        private readonly IProgramaService _pp;
        private readonly ILogger<ProgramaPatrullajesController> _log;

        public ProgramaPatrullajesController(IProgramaService ps, ILogger<ProgramaPatrullajesController> log)
        {
            _pp = ps ?? throw new ArgumentNullException(nameof(ps));
            _log = log;
        }

        /// <summary>
        /// Obtiene los programas de patrullaje acorde a las opciones indicadas
        /// </summary>
        /// <param name="tipo">Descripción del tipo de patrullaje (TERRESTRE o AEREO)</param>
        /// <param name="region">Región SSF</param>
        /// <param name="usuario">Nombre del usuario que realiza la operación</param>
        /// <param name="clase">Descripción de la clase de patrullaje a incluir</param>
        /// <param name="anio">Año</param>
        /// <param name="mes">Mes</param>
        /// <param name="dia">Día</param>
        /// <param name="opcion">Indicador del tipo de propgrama o propuesta a obtener. (0 - Extraordinarios o programados, 1 - En progreso, 2 - Concluidos, 3 - Cancelados, 4 - Todos, 5 - Propuestas (todas), 6 - Propuestas pendientes, 7 - Propuestas autorizadas, 8 - Propuestas Rechazadas, 9 - Propuestas enviadas)</param>
        /// <param name="periodo">Indicador del tipo de período que se requiere (0 - Un día, 1 - Un mes, 2 - Todos)</param>
        /// <returns></returns>
        [HttpGet]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IEnumerable<PatrullajeDto>>> GetValues([Required] string tipo, [Required] int region, [Required] string usuario, string clase, int anio, int mes, int dia, [Required] int opcion =0, int periodo=1)
        {
            try
            {
                var laOpcion = (FiltroProgramaOpcion)opcion;
                var elPeriodo = (PeriodoOpcion)periodo;

                var programas = await _pp.ObtenerPorFiltro(tipo, region, clase, anio, mes, dia, laOpcion, elPeriodo);
                
                if (programas == null)
                {
                    return NotFound();
                }


                return Ok(programas);
            }
            catch (Exception ex)
            {
                _log.LogError($"error al obtener programas para el tipo: {tipo}, region: {region}, usuario: {usuario}, año: {anio}, mes {mes}, día: {dia}, clase: {clase}, opción: {opcion}, período: {periodo} ", ex);
                return StatusCode(500, "Ocurrió un problema mientras se procesaba la petición");
            }
        }

        /// <summary>
        /// Registra una propuesta o un programa de patrullaje
        /// </summary>
        /// <param name="opcion">Tipo de elemento a registrar ("Propuesta" o "Programa") </param>
        /// <param name="clase">Clase de patrullaje a registrar ("EXTRAORDINARIO" o "") </param>
        /// <param name="usuario">Nombre del usuario que realiza la operación</param>
        /// <param name="p">Programa de patrullaje a registrar</param>
        /// <returns></returns>   
        //[Route("programas")]
        [HttpPost]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> PostProgramaOrPropuesta([Required] string opcion,  [Required] string clase,  [Required] string usuario, [FromBody] ProgramaDtoForCreateWithListas p)
        {
            try
            {
                await _pp.AgregaPrograma(opcion, clase, p);
                return StatusCode(201, "Ok");
            }
            catch (Exception ex)
            {
                _log.LogError($"error al registrar un programa para el usuario: {usuario}, clase: {clase}, opción: {opcion} ", ex);
                return StatusCode(500, "Ocurrió un problema mientras se procesaba la petición");
            }           
        }

        /// <summary>
        /// Registra una propuesta de patrullaje como programa de patrullaje
        /// </summary>
        /// <param name="usuario">Nombre del usuario que realiza la operación</param>
        /// <param name="p">Propuesta de patrullaje a registrar</param>
        /// <returns></returns>
        [Route("propuestas")]
        [HttpPost]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> PostPropuestas([Required] string usuario, [FromBody] List<ProgramaDtoForCreate> p)
        {
            try
            {
                await _pp.AgregaPropuestasComoProgramas(p, usuario);
                return StatusCode(201, "Ok");
            }
            catch (Exception ex)
            {
                _log.LogError($"error al registrar una propuesta para el usuario: {usuario} ", ex);
                return StatusCode(500, "Ocurrió un problema mientras se procesaba la petición");
            }
        }

        /// <summary>
        /// Actualiza programas o propuestas de patrullaje acorde a la opción indicada en sus parámetros
        /// </summary>
        /// <param name="opcion">Descripción de la opción para actualización ("CambioRuta", "InicioPatrullaje", "AutorizaPropuesta", "RegionalApruebaPropuesta", "RegistrarSolicitudOficioComision", "RegistrarSolicitudOficioAutorizacion", "RegistrarOficioComision", "RegistrarOficioAutorizacion")</param>
        /// <param name="usuario">Nombre del usuario que realiza la operación</param>
        /// <param name="p">Programa o propuesta a actualizar acorde a la opción indicada</param>
        /// <returns></returns>
        [HttpPut]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> PutPorOpcion([Required] string opcion, [Required] string usuario, [FromBody] ProgramaDtoForUpdatePorOpcion p)
        {
            try
            {
                await _pp.ActualizaProgramasOrPropuestasPorOpcion(p, opcion, usuario);
                return Ok();
            }
            catch (Exception ex)
            {
                _log.LogError($"error al actualizar un programa o propuesta por opción: {opcion}, usuario: {usuario} ", ex);
                return StatusCode(500, "Ocurrió un problema mientras se procesaba la petición");
            }
        }

  
        /// <summary>
        /// Actualiza el estado de una propuesta o programa de patrullaje
        /// </summary>
        /// <param name="opcion">Tipo de elemento a actualizar ("Propuesta" o "Programa")</param>
        /// <param name="accion">Tipo de acción a realizar 2 - Rechazar propuestas autorizadas, 3 - Cambiar propuestas aprobadas por comandancia a Pendiente de aprobación , 4 - Cambiar de propuesta autorizada a pendiente de autorización por SSF</param>
        /// <param name="usuario">Nombre del usuario que realiza la operación</param>
        /// <param name="p">Propuesta de patrullaje</param>
        /// <returns></returns>        
        [HttpPut("propuestas")]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> PutPropuestasToProgramas([Required] string opcion, [FromQuery] int accion, [Required] string usuario, [FromBody] List<ProgramaDto> p)
        {
            try
            {
                await _pp.ActualizaPropuestasOrProgramasPorOpcionAndAccion(p, opcion, accion, usuario);
                return Ok();
            }
            catch (Exception ex)
            {
                _log.LogError($"error al actualizar una propuesta como programa para el usuario: {usuario}, opcion: {opcion}, acción: {accion} ", ex);
                return StatusCode(500, "Ocurrió un problema mientras se procesaba la petición");
            }
        }

        /// <summary>
        /// Elimina una propuesta de patrullaje
        /// </summary>
        /// <param name="id">Identificador de la propuesta a eliminar</param>
        /// <param name="usuario">Nombre del usuario que realiza la operación</param>
        /// <returns></returns>
        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> Delete([Required] int id, [Required] string usuario)
        {
            try
            {
                await _pp.DeletePropuesta(id, usuario);
                return Ok();
            }
            catch (Exception ex)
            {
                _log.LogError($"error al eliminar la propuesta id: {id} para el usuario: {usuario} ", ex);
                return StatusCode(500, "Ocurrió un problema mientras se procesaba la petición");
            }
        }

        /// <summary>
        /// Actualiza un programa de patrullaje mediante el cambio de ruta del programa
        /// </summary>
        /// <param name="usuario">Nombre del usuario que realiza la operación</param>
        /// <param name="p">Programa de patrullaje</param>
        /// <returns></returns>
        [HttpPut("{usuario}/ruta")]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> PutCambioRuta([Required] string usuario, [FromBody] ProgramaDtoForUpdateRuta p)
        {
            try
            {
                await _pp.ActualizaProgramaPorCambioDeRuta(p, usuario);
                return Ok();
            }
            catch (Exception ex)
            {
                _log.LogError($"error al actualizar un programa por cambio de ruta para el usuario: {usuario} ", ex);
                return StatusCode(500, "Ocurrió un problema mientras se procesaba la petición");
            }
        }

        /// <summary>
        /// Actualiza un programa de patrullaje mediante el cambio de ruta del programa
        /// </summary>
        /// <param name="usuario">Nombre del usuario que realiza la operación</param>
        /// <param name="p">Programa de patrullaje</param>
        /// <returns></returns>
        [HttpPut("{usuario}/iniciopatrullaje")]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> PutInicioPatrullaje([Required] string usuario, [FromBody] ProgramaDtoForUpdateInicio p)
        {
            try
            {
                await _pp.ActualizaProgramasPorInicioPatrullajeAsync(p, usuario);
                return Ok();
            }
            catch (Exception ex)
            {
                _log.LogError($"error al actualizar un programa por inicio de patrullaje para el usuario: {usuario} ", ex);
                return StatusCode(500, "Ocurrió un problema mientras se procesaba la petición");
            }
        }

    }
}
