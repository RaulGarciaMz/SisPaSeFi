using Domain.DTOs;
using Domain.Ports.Driving;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;


// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApiSSF.Controllers
{
    [Route("api/[controller]")]
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
        /// Obtiene los datos de un usuario registrado (Nombre, TOKEN y estado)
        /// </summary>
        /// <param name="u">Usuario a confirmar registro</param>
        /// <param name="pathLdap">Ruta del directorio activo</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult<UsuarioRegistradoDto>> ObtenerUsuarioRegistrado(UsuarioDtoForGet u, [Required] string pathLdap)
        {
            try
            {
                return await _pp.ObtenerUsuarioRegistradoAsync(u, pathLdap);
            }
            catch (Exception ex)
            {
                _log.LogError($"error al obtener incidencias para el usuario: {u.strNombreDeUsuario} ", ex);
                return StatusCode(500, "Ocurrió un problema mientras se procesaba la petición");
            }
        }

    }
}
