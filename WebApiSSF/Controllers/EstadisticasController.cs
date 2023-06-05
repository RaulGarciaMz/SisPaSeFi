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
    [ApiController]
    public class EstadisticasController : ControllerBase
    {
        private readonly IEstadisticasService _pp;
        private readonly ILogger<UsuariosController> _log;

        public EstadisticasController(IEstadisticasService p, ILogger<UsuariosController> log)
        {
            _pp = p ?? throw new ArgumentNullException(nameof(p));
            _log = log ?? throw new ArgumentNullException(nameof(log));
        }

        /// <summary>
        /// Obtiene las estadísticas de sistema
        /// </summary>
        /// <param name="usuario">Nombre del usuario que realiza la operación</param>
        /// <returns></returns>
        [HttpGet]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IEnumerable<EstadisticasSistemaDto>>> ObtenerEstadisticasDeSistema([Required] string usuario)
        {
            try
            {
                var stats = await _pp.ObtenerEstadisticasDeSistemaAsync(usuario);

                if (stats == null || stats.Count == 0)
                {
                    return NotFound(new List<EstadisticasSistemaDto>());
                }

                return Ok(stats);
            }
            catch (Exception ex)
            {
                _log.LogError($"error al obtener las estadísticas para usuario: {usuario} ", ex);
                return StatusCode(500, "Ocurrió un problema mientras se procesaba la petición " + ex.Message);
            }
        }

    }
}
