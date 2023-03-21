using Domain.DTOs;
using Domain.Entities;
using Domain.Entities.Vistas;
using Domain.Ports.Driving;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.ComponentModel.DataAnnotations;
using System.Net.Mime;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApiSSF.Controllers
{
    [Route("api/[controller]")]
    //[Authorize]
    [ApiController]
    public class PersonalParticipantePatrullajeController : ControllerBase
    {
        private readonly IPersonalParticipanteService _pp;
        private readonly ILogger<PersonalParticipantePatrullajeController> _log;

        public PersonalParticipantePatrullajeController(IPersonalParticipanteService p, ILogger<PersonalParticipantePatrullajeController> log)
        {
            _pp = p ?? throw new ArgumentNullException(nameof(p));
            _log = log ?? throw new ArgumentNullException(nameof(log));
        }

        /// <summary>
        /// Obtiene el personal participante en un patrullaje, acorde a los parámetros indicados
        /// </summary>
        /// <param name="opcion">Tipo de personal participante en el patrullaje ("PersonalAsignado" o "PersonalNoAsignado")</param>
        /// <param name="idPrograma">Identificador del programa de patrullaje</param>
        /// <param name="usuario">Nombre del usuario (usuario_nom) que realiza la consulta</param>
        /// <param name="region">Identificador de la región SSF a la que pertenece el patrullaje</param>
        /// <returns></returns>
        [HttpGet]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<List<PersonalParticipanteVista>>> ObtenerPersonalParticipantePorOpcion([Required]string opcion, [Required] int idPrograma, [Required] string usuario, int region = 0)
        {
            try
            {
                var user = await _pp.ObtenerPersonalParticipantePorOpcionAsync(opcion, idPrograma, region, usuario);

                if (user == null)
                {
                    return NotFound();
                }

                if (user.Count == 0) 
                { 
                    return NotFound(); 
                }

                return Ok(user);
            }
            catch (Exception ex)
            {
                _log.LogInformation($"error al obtener personal participante en el patrullaje: {idPrograma}, opción: {opcion}, usuario: {usuario}, region {region}", ex);
                return StatusCode(500, "Ocurrió un problema mientras se procesaba la petición");
            }
        }


        /// <summary>
        /// Agrega un usuario participante en un patrullaje
        /// </summary>
        /// <param name="u"></param>
        /// <returns></returns>
        [HttpPost]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> Agregar([FromBody] PersonalParticipanteDto u)
        {
            try
            {
                await _pp.Agregar(u);
                return Ok();
            }
            catch (Exception ex)
            {
                _log.LogInformation($"error al obtener registrar personal participante en el patrullaje: {u.IdPrograma}, id del usuario: {u.IdUsuario}, usuario: {u.Usuario}", ex);
                return StatusCode(500, "Ocurrió un problema mientras se procesaba la petición");
            }
        }


        /// <summary>
        /// Borra personal participante de un programa indicado
        /// </summary>
        /// <param name="id">Identificador del programa</param>
        /// <param name="u">Usuario a eliminar</param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> Delete(int id, [FromBody] PersonalParticipanteDto u)
        {
            try
            {
                await _pp.Borrar(u);
                return Ok();
            }
            catch (Exception ex)
            {
                _log.LogInformation($"error al borrar personal participante en el patrullaje: {u.IdPrograma}, id del usuario: {u.IdUsuario}, usuario: {u.Usuario}", ex);
                return StatusCode(500, "Ocurrió un problema mientras se procesaba la petición");
            }
        }
    }
}
