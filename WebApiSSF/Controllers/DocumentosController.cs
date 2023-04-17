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
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IEnumerable<DocumentoDto>>> Get([Required] string opcion, [Required] string criterio, [Required] int anio, [Required] int mes, [Required] string usuario)
        {
            try
            {
                var evidencias = await _pp.ObtenerDocumentosAsync(opcion, criterio, anio, mes, usuario);

                if (evidencias == null || evidencias.Count() == 0)
                {
                    return NotFound();
                }

                return Ok(evidencias);
            }
            catch (Exception ex)
            {
                _log.LogInformation($"error al obtener documentos de patrullaje para la opción: {opcion}, criterio: {criterio}, del Año: {anio} y Mes: {mes} para el usuario: {usuario}", ex);
                return StatusCode(500, "Ocurrió un problema mientras se procesaba la petición");
            }
        }
    }
}
