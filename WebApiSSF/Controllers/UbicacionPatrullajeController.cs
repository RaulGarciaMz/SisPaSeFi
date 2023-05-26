using Domain.DTOs;
using Domain.Ports.Driving;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mime;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApiSSF.Controllers
{
    [Route("api/[controller]")]
    //[Authorize]
    [ApiController]
    public class UbicacionPatrullajeController : ControllerBase
    {
        private readonly IUbicacionPatrullajeService _pp;
        private readonly ILogger<UbicacionPatrullajeController> _log;

        public UbicacionPatrullajeController(IUbicacionPatrullajeService p, ILogger<UbicacionPatrullajeController> log)
        {
            _pp = p ?? throw new ArgumentNullException(nameof(p));
            _log = log ?? throw new ArgumentNullException(nameof(log));
        }

        /// <summary>
        /// Actualiza la ubicación de un programa de patrullaje
        /// </summary>
        /// <param name="u">Programa y ubicación a actualizar</param>
        /// <returns></returns>
        [HttpPost]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> ActualizaUbicacion([FromBody] UbicacionForUpdateDto u)
        {
            try
            {
                await _pp.ActualizaUbicacionAsync(u);

                return Ok();
            }
            catch (Exception ex)
            {
                _log.LogError($"error al actualizar la ubicación para la ruta: {u.IdRuta}, fecha: {u.FechaPatrullaje} para el usuario: {u.usuario}  - ", ex);
                return StatusCode(500, "Ocurrió un problema mientras se procesaba la petición - " + ex.Message);
            }

        }
    }
}
