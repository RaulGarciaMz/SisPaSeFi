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
    public class UsuariosController : ControllerBase
    {
        private readonly IUsuariosService _pp;
        private readonly ILogger<UsuariosController> _log;

        public UsuariosController(IUsuariosService p, ILogger<UsuariosController> log)
        {
            _pp = p ?? throw new ArgumentNullException(nameof(p));
            _log = log ?? throw new ArgumentNullException(nameof(log));
        }

        /// <summary>
        /// Obtiene la lista de usuarios correspondiente a los valores indicados en sus parámetros
        /// </summary>
        /// <param name="opcion">Descripción de tipo de busqueda de usuarios ("BuscarUsuarios", "UsuariosDeDocumento", "UsuariosNoIncluidosEnDocumento"). Los casos "UsuariosDeDocumento" y "UsuariosNoIncluidosEnDocumento" pueden contener información adicional que corresponde al identificador del documento, por ejemplo: "UsuariosDeDocumento-23"  </param>
        /// <param name="criterio">Cadena de texto a buscar en nombre, y apellidos. Requerido para las opciones "BuscarUsuarios" y "UsuariosNoIncluidosEnDocumento"</param>
        /// <param name="usuario">Nombre del usuario que realiza la operación</param>
        /// <returns></returns>
        [HttpGet]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<List<UsuarioDto>>> ObtenerUsuarioPorOpcion([Required] string opcion, string criterio, [Required] string usuario)
        {
            try
            {
                var user = await _pp.ObtenerUsuarioPorOpcionAsync(opcion, criterio, usuario);

                if (user == null)
                {
                    return NotFound();
                }

                return Ok(user);
            }
            catch (Exception ex)
            {
                _log.LogError($"error al obtener los usuarios para opción: {opcion}, criterio: {criterio}, usuario: {usuario} ", ex);
                return StatusCode(500, "Ocurrió un problema mientras se procesaba la petición");
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="opcion"></param>
        /// <param name="dato"></param>
        /// <param name="usuario"></param>

        /// <returns></returns>
        /// 

        /// <summary>
        /// Agrega usuarios acorde a la opción indicada
        /// </summary>
        /// <param name="opcion">Opción de usuario a agregar ("AsignaUsuarioDeDocumento" ó "CrearUsuario")</param>
        /// <param name="dato">Cadena de caracteres indicando el identificador del documento y el identificador de usuario a agregar, por ejemplo: "23-78"</param>
        /// <param name="usuario">Usuario (alias - usuario_nom) que realiza la operación</param>
        /// <param name="userDto">Usuario a agregar. Sólo si la opción es "CrearUsuario"</param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> AgregaPorOpcion([Required] string opcion, [Required] string dato, [Required] string usuario, [FromBody] UsuarioDto userDto)
        {
            try
            {
                await _pp.AgregaPorOpcionAsync(opcion, dato, usuario, userDto);
                return StatusCode(201, "Ok");
            }
            catch (Exception ex)
            {
                _log.LogError($"error al agregar el usuario ", ex);
                return StatusCode(500, "Ocurrió un problema mientras se procesaba la petición");
            }
        }

        /// <summary>
        /// Actualiza una lista de usuarios con respecto a la opción indicada
        /// </summary>
        /// <param name="opcion">Opción de actualización ("Desbloquear", "Bloquear", "ReiniciarClave", "Actualizar", "RegistrarComandancia", "QuitarComandancia", "RegistrarRol", "QuitarRol", "RegistrarGrupoCorreo", "QuitarGrupoCorreo")</param>
        /// <param name="usuario">Usuario (alias - usuario_nom) que realiza la actualización</param>
        /// <returns></returns>
        [HttpPut]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> ActualizaUsuariosPorOpcion([Required] string opcion, string usuario, [FromBody] List<UsuarioDto> users)
        {
            try
            {
                await _pp.ActualizaUsuariosPorOpcionAsync(opcion, usuario, users);
                return Ok();
            }
            catch (Exception ex)
            {
                _log.LogError($"error al actualizar el usuario ", ex);
                return StatusCode(500, "Ocurrió un problema mientras se procesaba la petición");
            }
        }

        /// <summary>
        /// Elimina usuarios acorde a la opción indicada
        /// </summary>
        /// <param name="opcion">Opción de usuario a eliminar ("EliminaUsuarioDeDocumento" ó "EliminaUsuarioDePatrullaje")</param>
        /// <param name="dato">Cadena de caracteres indicando el identificador del documento ó programa y el identificador de usuario a agregar, por ejemplo: "23-78"</param>
        /// <param name="usuario">Usuario (alias - usuario_nom) que realiza la operación</param>
        /// <returns></returns>
        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> BorraPorOpcion([Required] string opcion, [Required] string dato, string usuario)
        {
            try
            {
                await _pp.BorraPorOpcionAsync(opcion, dato, usuario);
                return Ok();
            }
            catch (Exception ex)
            {
                _log.LogError($"error al eliminar usuario para la opcion: {opcion}, dato: {dato}, usuario: {usuario} ", ex);
                return StatusCode(500, "Ocurrió un problema mientras se procesaba la petición");
            }
        }


        /*        /// <summary>
                /// Obtiene al usuario que contenga la cadena del criterio en alguno de los campos nombre, apellidos o user_nom (alias)
                /// </summary>
                /// <param name="criterio">texto de búsqueda en nombre, apellidos o usuario_nom</param>
                /// <returns></returns>
                [HttpGet]
                [Route("criterio")]
                [Produces(MediaTypeNames.Application.Json)]
                [ProducesResponseType(StatusCodes.Status200OK)]
                [ProducesResponseType(StatusCodes.Status404NotFound)]
                [ProducesResponseType(StatusCodes.Status500InternalServerError)]
                public async Task<ActionResult<UsuarioDto?>> ObtenerUsuarioPorCriterio([Required] string criterio)
                {
                    try
                    {
                        var user = await _pp.ObtenerUsuarioPorCriterioAsync(criterio);

                        if (user == null)
                        {
                            return NotFound();
                        }

                        return Ok(user);
                    }
                    catch (Exception ex)
                    {
                        _log.LogError($"error al obtener el usuario del catálogo, que cumpla con el criterio indicado ", ex);
                        return StatusCode(500, "Ocurrió un problema mientras se procesaba la petición");
                    }
                }

        /// <summary>
        /// Obtiene los datos del usuario, siempre y cuando sea configurador
        /// </summary>
        /// <param name="usuario">alias (usuario_nom) del usuario</param>
        /// <returns></returns>
        [HttpGet]
        [Route("configurador/nombre")]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<UsuarioDto?>> ObtenerUsuarioConfiguradorPorNombre([Required] string usuario)
        {
            try
            {
                var user = await _pp.ObtenerUsuarioConfiguradorPorNombreAsync(usuario);

                if (user == null)
                {
                    return NotFound();
                }

                return Ok(user);
            }
            catch (Exception ex)
            {
                _log.LogError($"error al obtener al usuario configurador para el alias de usuario indicado ", ex);
                return StatusCode(500, "Ocurrió un problema mientras se procesaba la petición");
            }
        }
       
        /// <summary>
        /// Obtiene los datos del usuario indicado en el identificador, siempre y cuando sea configurador
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}/configurador")]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<UsuarioDto?>> ObtenerUsuarioConfiguradorPorId([Required] int id)
        {
            try
            {
                var user = await _pp.ObtenerUsuarioConfiguradorPorIdAsync(id);

                if (user == null) 
                {
                    return NotFound();
                }

                return Ok(user);
            }
            catch (Exception ex)
            {
                _log.LogError($"error al obtener al usuario configurador por id ", ex);
                return StatusCode(500, "Ocurrió un problema mientras se procesaba la petición");
            }
        }

        /// <summary>
        /// Bloquea usuarios
        /// </summary>
        /// <param name="usuario">Usuario (alias - usuario_nom) al que se bloqueará</param>
        /// <returns></returns>
        [HttpPut("{usuario}/bloquea")]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> BloqueaUsuario(string usuario)
        {
            try
            {
                await _pp.BloqueaUsuarioAsync(usuario);
                return Ok();
            }
            catch (Exception ex)
            {
                _log.LogError($"error al bloquear usuario ", ex);
                return StatusCode(500, "Ocurrió un problema mientras se procesaba la petición");
            }
        }

        /// <summary>
        /// Desbloquea usuarios
        /// </summary>
        /// <param name="usuario">Usuario (alias - usuario_nom) al que se desbloqueará</param>
        /// <returns></returns>
        [HttpPut("{usuario}/desbloquea")]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> DesbloqueaUsuario(string usuario)
        {
            try
            {
                await _pp.DesbloqueaUsuarioAsync(usuario);
                return Ok();
            }
            catch (Exception ex)
            {
                _log.LogError($"error al desbloquear usuario ", ex);
                return StatusCode(500, "Ocurrió un problema mientras se procesaba la petición");
            }
        }

        /// <summary>
        /// Reinicia la clave de seguriad (password) de usuarios
        /// </summary>
        /// <param name="usuario">Usuario (alias - usuario_nom) al que se reiniciará la clave</param>
        /// <returns></returns>
        [HttpPut("{usuario}/reiniciaclave")]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> ReiniciaClaveUsuario(string usuario)
        {
            try
            {
                await _pp.ReiniciaClaveUsuarioAsync(usuario);
                return Ok();
            }
            catch (Exception ex)
            {
                _log.LogError($"error al reiniciar la clave del usuario ", ex);
                return StatusCode(500, "Ocurrió un problema mientras se procesaba la petición");
            }
        }
        */

    }
}
