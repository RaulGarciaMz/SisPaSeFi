using Domain.DTOs;
using Domain.Entities;
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
    public class RolesController : ControllerBase
    {
        private readonly IRolesService _rp;
        private readonly ILogger<RolesController> _log;

        public RolesController(IRolesService r, ILogger<RolesController> log)
        {
            _rp = r ?? throw new ArgumentNullException(nameof(r));
            _log = log;
        }

        /// <summary>
        /// Obtiene la lista de roles
        /// </summary>
        /// <param name="usuario">Nombre del usuario que realiza la operación</param>
        /// <returns></returns>
        [HttpGet]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult< IEnumerable<RolDto>>> ObtenerRoles([Required] string usuario)
        {
            try
            {
                var roles = await _rp.ObtenerRolesAsync(usuario);

                if (roles != null && roles.Count > 0)
                {
                    return Ok(roles);
                }

                return NotFound(new List<RolDto>());
            }
            catch (Exception ex)
            {
                _log.LogError($"error al obtener roles para el usuario: {usuario} ", ex);
                return StatusCode(500, "Ocurrió un problema mientras se procesaba la petición " + ex.Message);
            }
        }

        /// <summary>
        /// Agrega un rol
        /// </summary>
        /// <param name="usuario">Nombre del usuario que realiza la operación</param>
        /// <param name="rol">Rol a agregar</param>
        /// <returns></returns>
        [HttpPost]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> AgregaRol([Required] string usuario, [FromBody] RolDtoForCreate rol)
        {
            try
            {
                 await _rp.AgregaRolAsync(rol.strNombre, rol.strDescripcion, rol.intIdMenu, usuario);
                return StatusCode(201, "Ok");
            }
            catch (Exception ex)
            {
                _log.LogError($"error al agregar el rol para nombre de rol: {rol.strNombre} , descripción : {rol.strDescripcion}, menu: {rol.intIdMenu} ", ex);
                return StatusCode(500, "Ocurrió un problema mientras se procesaba la petición " + ex.Message);
            }
        }

        /// <summary>
        /// Actualiza un rol
        /// </summary>
        /// <param name="rol">Rol a actualizar</param>
        /// <param name="usuario">Nombre del usuario que realiza la operación</param>
        /// <returns></returns>
        [HttpPut]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> ActualizaRol([FromBody] RolDto rol, [Required] string usuario)
        {
            try
            {
                await _rp.ActualizaRolAsync(rol.intIdRol, rol.strNombre, rol.strDescripcion, rol.intIdMenu, usuario);
                return Ok();
            }
            catch (Exception ex)
            {
                _log.LogError($"error al actualizar el rol para id: {rol.intIdRol},  nombre: {rol.strNombre} , descripción : {rol.strDescripcion}, menu: {rol.intIdMenu} ", ex);
                return StatusCode(500, "Ocurrió un problema mientras se procesaba la petición " + ex.Message);
            }
        }


    }
}
