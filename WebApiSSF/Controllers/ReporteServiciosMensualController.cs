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
    public class ReporteServiciosMensualController : ControllerBase
    {
        private readonly IReporteServicioMensualService _pp;
        private readonly ILogger<ReporteServiciosMensualController> _log;

        public ReporteServiciosMensualController(IReporteServicioMensualService p, ILogger<ReporteServiciosMensualController> log)
        {
            _pp = p ?? throw new ArgumentNullException(nameof(p));
            _log = log ?? throw new ArgumentNullException(nameof(log));
        }

        /// <summary>
        /// Obtiene el reporte de servicio mensual
        /// </summary>
        /// <param name="anio">Año de la información a reportar</param>
        /// <param name="mes">Mes de la información a reportar</param>
        /// <param name="usuario">Nombre del usuario que realiza la operación</param>
        /// <param name="tipoPatrullaje">tipo de reporte de servicio mensual ("AEREO", "TERRESTRESDN", "TERRESTRESSF")  </param>
        /// <returns></returns>
        [HttpGet]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<string>> ObtenerReporteServicioMensual([Required]int anio, [Required]int mes, [Required]string usuario, [Required] string tipoPatrullaje)
        {
            try
            {
                var coms = await _pp.ObtenerReporteDeServicioMensualPorOpcionAsync(anio, mes, tipoPatrullaje, usuario);
                return Ok(coms);
            }
            catch (Exception ex)
            {
              
                _log.LogError($"error al obtener el reporte de servicios mensual para el año: {anio}, mes: {mes}, tipo: {tipoPatrullaje}, usuario: {usuario} ", ex);
                var m = "Ocurrió un problema mientras se procesaba la petición" + ex.Message;
                //return StatusCode(500, "Ocurrió un problema mientras se procesaba la petición");
                return StatusCode(500, m);
            }
        } 
    }
}
