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
    public class RegistroController : ControllerBase
    {
        private readonly IRegistroService _pp;
        private readonly ILogger<RegistroController> _log;

        public RegistroController(IRegistroService p, ILogger<RegistroController> log)
        {
            _pp = p ?? throw new ArgumentNullException(nameof(p));
            _log = log ?? throw new ArgumentNullException(nameof(log));
        }

        /// <summary>
        /// Obtiene los datos de un usuario registrado
        /// </summary>
        /// <param name="u">Usuario a confirmar registro</param>
        /// <param name="pathLdap">Ruta del directorio activo</param>
        /// <returns></returns>
        [HttpPost]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<UsuarioRegistradoDto>> ObtenerUsuarioRegistrado(UsuarioDtoForGet u, [Required] string pathLdap)
        {
            try
            {
                var us = await _pp.ObtenerUsuarioRegistradoAsync(u, pathLdap);

                return Ok(us);
            }
            catch (Exception ex)
            {
                _log.LogError($"error al obtener incidencias para el usuario: {u.strNombreDeUsuario} ", ex);
                return StatusCode(500, "Ocurrió un problema mientras se procesaba la petición - " + ex.Message );
            }
        }

        /// <summary>
        /// Actualiza el registro de usuario y sus tablas asociadas acorde a la opción indicada
        /// </summary>
        /// <param name="u">Usuario a actualizar</param>
        /// <param name="opcion">Opción de actualización ("RegistrarIntentoFallido", "RegistrarAcceso", "RegistrarEvento", "RegistrarFinSesion", "RevisarAvisoLegal", "VerificarCorreoElectronico")</param>
        /// <returns></returns>
        [HttpPut]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<string>> ActualizaPorOpcion(UsuarioForPostDto u, [Required] string opcion)
        {
            try
            {
                var res = await _pp.ActualizaRegistroPorOpcion(opcion, u);
                return Ok(res);
            }
            catch (Exception ex)
            {
                _log.LogError($"error al obtener incidencias para el usuario: {u.strNombreDeUsuario} ", ex);
                return StatusCode(500, "Ocurrió un problema mientras se procesaba la petición - " + ex.Message);
            }
        }
    }
}
