using Domain.DTOs;
using Domain.Entities;
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
    public class PermisoEdicionProcesoConduccionController : ControllerBase
    {
        private readonly IPermisoEdicionConduccionService _pp;
        private readonly ILogger<PermisoEdicionProcesoConduccionController> _log;

        public PermisoEdicionProcesoConduccionController(IPermisoEdicionConduccionService p, ILogger<PermisoEdicionProcesoConduccionController> log)
        {
            _pp = p ?? throw new ArgumentNullException(nameof(p));
            _log = log ?? throw new ArgumentNullException(nameof(log));
        }

        /// <summary>
        /// Obtiene la lista de todos los permisos de edición
        /// </summary>
        /// <param name="usuario">Nombre del usuario (usuario_nom) que realiza la operación</param>
        /// <returns></returns>
        [HttpGet]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IEnumerable<Permisosedicionprocesoconduccion>>> ObtenerPermisos([Required]string usuario)
        {
            try
            {
                var coms = await _pp.ObtenerPermisosAsync(usuario);

                if (coms.Count <= 0)
                {
                    return NotFound();
                }

                return Ok(coms);
            }
            catch (Exception ex)
            {
                _log.LogInformation($"error al obtener los permisos de edición del proceso de conducción para el usuario: {usuario}", ex);
                var m = "Ocurrió un problema mientras se procesaba la petición" + ex.Message;
                return StatusCode(500, m);
            }
        }

        /// <summary>
        /// Obtiene el permiso de edición correspondiente con las opciones indicadas
        /// </summary>
        /// <param name="region">Identificador de la región SSF</param>
        /// <param name="anio">Año dl permiso</param>
        /// <param name="mes">Mes del permiso</param>
        /// <param name="usuario">Nombre del usuario (usuario_nom) que realiza la operación</param>
        /// <returns></returns>
        [Route("especifico")]
        [HttpGet()]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<Permisosedicionprocesoconduccion>> ObtenerPermisosPorOpcion([Required] int region, [Required] int anio, [Required] int mes, [Required] string usuario)
        {
            try
            {
                var coms = await _pp.ObtenerPermisosPorOpcionAsync(region, anio, mes, usuario);

                if (coms == null)
                {
                    return NotFound();
                }

                return Ok(coms);
            }
            catch (Exception ex)
            {
                _log.LogInformation($"error al obtener el permiso de edición del proceso de conducción para el usuario: {usuario} region: {region}, anio: {anio}, mes: {mes}", ex);
                var m = "Ocurrió un problema mientras se procesaba la petición" + ex.Message;
                return StatusCode(500, m);
            }
        }

        /// <summary>
        /// Agrega un permiso de edición
        /// </summary>
        /// <param name="p">Permiso de edición a agregar</param>
        /// <param name="usuario">Nombre del usuario (usuario_nom) que realiza la operación</param>
        /// <returns></returns>
        [HttpPost]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> Agregar([FromBody] PermisoEdicionConduccionDto p, [Required] string usuario)
        {
            try
            {
                 await _pp.AgregarPorOpcionAsync(p.Region, p.Anio, p.Mes, usuario);
  
                return Ok();
            }
            catch (Exception ex)
            {
                _log.LogInformation($"error al agregar el permiso de edición del proceso de conducción para el usuario: {usuario} region: {p.Region}, anio: {p.Anio}, mes: {p.Mes}", ex);
                var m = "Ocurrió un problema mientras se procesaba la petición" + ex.Message;
                return StatusCode(500, m);
            }
        }

        /// <summary>
        /// Borra un permiso de edición
        /// </summary>
        /// <param name="region">Identificador de la región ssf</param>
        /// <param name="anio">Año del permiso</param>
        /// <param name="mes">Mes del permiso</param>
        /// <param name="usuario">Nombre del usuario (usuario_nom) que realiza la operación</param>
        /// <returns></returns>
        [HttpDelete()]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> Delete([Required] int region, [Required] int anio, [Required] int mes, [Required] string usuario)
        {
            try
            {
                await _pp.BorraPorOpcionAsync(region, anio, mes, usuario);

                return Ok();
            }
            catch (Exception ex)
            {
                _log.LogInformation($"error al borrar el permiso de edición del proceso de conducción para el usuario: {usuario} region: {region}, anio: {anio}, mes: {mes}", ex);
                var m = "Ocurrió un problema mientras se procesaba la petición" + ex.Message;
                return StatusCode(500, m);
            }
        }
    }
}
