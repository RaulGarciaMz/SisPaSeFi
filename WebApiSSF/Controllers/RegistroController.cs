using Domain.DTOs;
using Domain.Entities;
using Domain.Ports.Driving;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;


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
        /// <param name="u"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult<UsuarioDtoRegistro>> RegistraUsuario(UsuarioDtoForAutentication u)
        {
            try
            {

                var use = await _pp.ObtenerUsuarioRegistradoAsync(u, ""); // ConfigurationManager.AppSettings(["LDAP_path"]);

                return use;
            }
            catch (Exception ex)
            {
                _log.LogError($"error al obtener incidencias para el usuario: {u.Nombre} ", ex);
                return StatusCode(500, "Ocurrió un problema mientras se procesaba la petición");
            }
        }

    }
}
