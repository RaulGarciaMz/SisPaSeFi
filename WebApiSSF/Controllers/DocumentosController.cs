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
    public class DocumentosController : ControllerBase
    {

        private readonly IDocumentoService _pp;
        private readonly ILogger<DocumentosController> _log;

        public DocumentosController(IDocumentoService ps, ILogger<DocumentosController> log)
        {
            _pp = ps ?? throw new ArgumentNullException(nameof(ps));
            _log = log;
        }

        /// <summary>
        /// Obtiene la lista de documentos de patrullaje del mes, año y comandancia indicados
        /// </summary>
        /// <param name="opcion">Indicador del tipo de documento ("DocumentosPatrullaje", "DocumentosDeUnUsuario", "DocumentosParaUnUsuario" )</param>
        /// <param name="criterio">Indicador de la cantidad de documentos a regresar ("TODO", "MES")</param>
        /// <param name="anio">Año</param>
        /// <param name="mes">Mes</param>
        /// <param name="usuario">Nombre del usuario (usuario_nom)</param>
        /// <returns></returns>
        [HttpGet]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IEnumerable<DocumentoDto>>> Get([Required] string opcion, [Required] string criterio, [Required] int anio, [Required] int mes, [Required] string usuario)
        {
            try
            {
                var opciones = new List<string>()
                                    {
                                        "DocumentosPatrullaje",
                                        "DocumentosDeUnUsuario",
                                        "DocumentosParaUnUsuario"
                                    };

                var criterios = new List<string>()
                                    {
                                        "TODO",
                                        "MES"
                                    };

                if(!opciones.Contains(opcion)) 
                {
                    return BadRequest("opción no válida");
                }

                if (!criterios.Contains(criterio))
                {
                    return BadRequest("criterio no válido");
                }

                var evidencias = await _pp.ObtenerDocumentosAsync(opcion, criterio, anio, mes, usuario);

                if (evidencias == null || evidencias.Count() == 0)
                {
                    return NotFound(new List<DocumentoDto>());
                }

                return Ok(evidencias);
            }
            catch (Exception ex)
            {
                _log.LogError($"error al obtener documentos de patrullaje para la opción: {opcion}, criterio: {criterio}, del Año: {anio} y Mes: {mes} para el usuario: {usuario} ", ex);
                return StatusCode(500, "Ocurrió un problema mientras se procesaba la petición " + ex.Message);
            }
        }

        /// <summary>
        /// Agrega un documento de patrullaje
        /// </summary>
        /// <param name="d">Documento a agregar</param>
        /// <returns></returns>
        [HttpPost]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> Agregar([FromBody] DocumentoDtoForCreate d)
        {
            try
            {
                await _pp.AgregarAsync(d);
  
                return Ok();
            }
            catch (Exception ex)
            {
                _log.LogError($"error al agregar documento de patrullaje para la referencia: {d.intIdReferencia}, tipo de documento: {d.intIdTipoDocumento}, del Año: {d.strNombreArchivo} ", ex);
                return StatusCode(500, "Ocurrió un problema mientras se procesaba la petición " + ex.Message);
            }
        }
    }
}
