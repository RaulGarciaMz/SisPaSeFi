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
    public class MenusController : ControllerBase
    {
        private readonly IMenuService _pp;
        private readonly ILogger<MenusController> _log;

        public MenusController(IMenuService p, ILogger<MenusController> log)
        {
            _pp = p ?? throw new ArgumentNullException(nameof(p));
            _log = log ?? throw new ArgumentNullException(nameof(log));
        }

        /// <summary>
        /// Obtiene la lista de menus acorde a las opciones indicadas
        /// </summary>
        /// <param name="opcion">Opción de obtención de menus ("Submenu", "MenuUsuario")</param>
        /// <param name="criterio">Identificador del menú padre</param>
        /// <param name="usuario">Nombre del usuario que realiza la operación</param>
        /// <returns></returns>
        [HttpGet]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IEnumerable<MenuDto>>> ObtenerMenuPorOpcion([Required] string opcion, string criterio, [Required] string usuario)
        {
            try
            {
                var coms = await _pp.ObtenerMenuPorOpcionAsync(opcion, criterio, usuario);

                if (coms.Count <= 0)
                {
                    return NotFound();
                }

                return Ok(coms);
            }
            catch (Exception ex)
            {
                _log.LogError($"error al obtener las menus para la opción: {opcion}, padre: {criterio}, usuario: {usuario} ", ex);
                var m = "Ocurrió un problema mientras se procesaba la petición - " + ex.Message;
                //return StatusCode(500, "Ocurrió un problema mientras se procesaba la petición");
                return StatusCode(500, m);
            }
        }


        /// <summary>
        /// Agrega un menu
        /// </summary>
        /// <param name="menu">Menu a agregar</param>
        /// <param name="usuario">Nombre del usuario que realiza la operación</param>
        /// <returns></returns>
        [HttpPost]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> AgregarMenu([FromBody] MenuDto menu, [Required] string usuario)
        {
            try
            {
                await _pp.AgregaMenuAsync(menu, usuario);
                return StatusCode(201, "Ok");
            }
            catch (Exception ex)
            {
                _log.LogError($"error al agregar el menu para usuario: {usuario} ", ex);
                var m = "Ocurrió un problema mientras se procesaba la petición - " + ex.Message;
                return StatusCode(500, m);
            }
        }

        /// <summary>
        /// Actualiza un menu
        /// </summary>
        /// <param name="menu">Menú a actualizar</param>
        /// <param name="usuario">Nombre del usuario que realiza la operación</param>
        /// <returns></returns>
        [HttpPut]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> Actualiza([FromBody] MenuDto menu, [Required]string usuario)
        {
            try
            {
                await _pp.ActualizaMenuAsync(menu, usuario);

                return Ok();
            }
            catch (Exception ex)
            {
                _log.LogError($"error al actualizar menú: {menu.intIdMenu} para el usuario: {usuario} ", ex);
                return StatusCode(500, "Ocurrió un problema mientras se procesaba la petición - " + ex.Message );
            }
        }

    }
}
