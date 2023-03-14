using Domain.DTOs.catalogos;
using Domain.Entities;
using Domain.Ports.Driving;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.Extensions.Options;
using System.ComponentModel.DataAnnotations;
using System.Drawing;
using System.Net.Mime;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApiSSF.Controllers
{
    [Route("api/[controller]")]
    //[Authorize]
    [ApiController]
    public class CatalogosConsultasController : ControllerBase
    {

        private readonly ICatalogosConsultaService _pp;
        private readonly ILogger<CatalogosConsultasController> _log;

        public CatalogosConsultasController(ICatalogosConsultaService p, ILogger<CatalogosConsultasController> log)
        {
            _pp = p ?? throw new ArgumentNullException(nameof(p));
            _log = log ?? throw new ArgumentNullException(nameof(log));
        }

        /// <summary>
        /// Obtiene la lista de comandancias para un usuario indicado
        /// </summary>
        /// <param name="idUsuario">Identificador del usuario</param>
        /// <returns></returns>
        [Route("comandancias")]
        [HttpGet]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IEnumerable<CatalogoGenerico>>> ObtenerComandanciaPorIdUsuario([Required] int idUsuario)
        {
            try
            {
                var coms = await _pp.ObtenerComandanciaPorIdUsuarioAsync(idUsuario);

                if (coms == null) { }

                if (coms.Count <= 0)
                {
                    return NotFound();
                }

                return Ok(coms);
            }
            catch (Exception ex)
            {
                _log.LogInformation($"error al obtener comandancias por usuario del catálogo", ex);
                return StatusCode(500, "Ocurrió un problema mientras se procesaba la petición");
            }
        }

        /// <summary>
        /// Obtiene la lista de tipos de patrullaje del catálogo
        /// </summary>
        /// <returns></returns>
        [Route("tipopatrullaje")]
        [HttpGet]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IEnumerable<CatalogoGenerico>>> ObtenerTiposPatrullaje()
        {
            try
            {
                var coms = await _pp.ObtenerTiposPatrullajeAsync();

                if (coms == null) { }

                if (coms.Count <= 0)
                {
                    return NotFound();
                }

                return Ok(coms);
            }
            catch (Exception ex)
            {
                _log.LogInformation($"error al obtener tipos de patrullaje del catálogo", ex);
                return StatusCode(500, "Ocurrió un problema mientras se procesaba la petición");
            }
        }

        /// <summary>
        /// Obtiene la lista de tipos de vehículos del catálogo
        /// </summary>
        /// <returns></returns>
        [Route("tipovehiculo")]
        [HttpGet]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IEnumerable<CatalogoGenerico>>> ObtenerTiposVehiculo()
        {
            try
            {
                var coms = await _pp.ObtenerTiposVehiculoAsync();

                if (coms == null) { }

                if (coms.Count <= 0)
                {
                    return NotFound();
                }

                return Ok(coms);
            }
            catch (Exception ex)
            {
                _log.LogInformation($"error al obtener tipos de vehículo del catálogo", ex);
                return StatusCode(500, "Ocurrió un problema mientras se procesaba la petición");
            }
        }

        /// <summary>
        /// Obtiene la lista de clasificaciones de incidencias del catálogo
        /// </summary>
        /// <returns></returns>
        [Route("clasificacionincidencias")]
        [HttpGet]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IEnumerable<CatalogoGenerico>>> ObtenerClasificacionesIncidencia()
        {
            try
            {
                var coms = await _pp.ObtenerClasificacionesIncidenciaAsync();

                if (coms == null) { }

                if (coms.Count <= 0)
                {
                    return NotFound();
                }

                return Ok(coms);
            }
            catch (Exception ex)
            {
                _log.LogInformation($"error al obtener clasificaciones de incidencia del catálogo", ex);
                return StatusCode(500, "Ocurrió un problema mientras se procesaba la petición");
            }
        }

        /// <summary>
        /// Obtiene la lista de niveles del catálogo
        /// </summary>
        /// <returns></returns>
        [Route("niveles")]
        [HttpGet]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IEnumerable<CatalogoGenerico>>> ObtenerNiveles()
        {
            try
            {
                var coms = await _pp.ObtenerNivelesAsync();

                if (coms == null) { }

                if (coms.Count <= 0)
                {
                    return NotFound();
                }

                return Ok(coms);
            }
            catch (Exception ex)
            {
                _log.LogInformation($"error al obtener niveles del catálogo", ex);
                return StatusCode(500, "Ocurrió un problema mientras se procesaba la petición");
            }
        }

        /// <summary>
        /// Obtiene la lista de conceptos de afectación del catálogo
        /// </summary>
        /// <returns></returns>
        [Route("conceptosafectacion")]
        [HttpGet]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IEnumerable<CatalogoGenerico>>> ObtenerConceptosAfectacion()
        {
            try
            {
                var coms = await _pp.ObtenerConceptosAfectacionAsync();

                if (coms == null) { }

                if (coms.Count <= 0)
                {
                    return NotFound();
                }

                return Ok(coms);
            }
            catch (Exception ex)
            {
                _log.LogInformation($"error al obtener conceptos de afectación del catálogo", ex);
                return StatusCode(500, "Ocurrió un problema mientras se procesaba la petición");
            }
        }

        /// <summary>
        /// Obtiene la lista de identificadores de las regiones militares que son usador en las rutas
        /// </summary>
        /// <returns></returns>
        [Route("regionesenrutas")]
        [HttpGet]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IEnumerable<int>>> ObtenerRegionesMilitaresEnRutas()
        {
            try
            {
                var coms = await _pp.ObtenerRegionesMilitaresEnRutasAsync();

                if (coms == null) 
                {
                    return StatusCode(500, "La consulta trajo una lista nula");
                }

                if (coms.Count <= 0)
                {
                    return NotFound();
                }

                return Ok(coms);
            }
            catch (Exception ex)
            {
                _log.LogInformation($"error al obtener regiones militares en rutas del catálogo de rutas", ex);
                return StatusCode(500, "Ocurrió un problema mientras se procesaba la petición");
            }
        }

        /// <summary>
        /// Obtiene la lista de Estados del país del catálogo
        /// </summary>
        /// <returns></returns>
        [Route("estadospais")]
        [HttpGet]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IEnumerable<CatalogoGenerico>>> ObtenerEstadosDelPais()
        {
            try
            {
                var coms = await _pp.ObtenerEstadosPaisAsync();

                if (coms == null)
                {
                    return StatusCode(500, "La consulta trajo una lista nula");
                }

                if (coms.Count <= 0)
                {
                    return NotFound();
                }

                return Ok(coms);
            }
            catch (Exception ex)
            {
                _log.LogInformation($"error al obtener estados del país del catálogo", ex);
                return StatusCode(500, "Ocurrió un problema mientras se procesaba la petición");
            }
        }

        /// <summary>
        /// Obtiene la lista de Municipios pertenecientes al estado indicado en el parámetro
        /// </summary>
        /// <param name="id">Identificador del estado</param>
        /// <returns></returns>
        [Route("municipios")]
        [HttpGet]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IEnumerable<CatalogoGenerico>>> ObtenerMunicipiosPorEstado([Required]int id)
        {
            try
            {
                var coms = await _pp.ObtenerMunicipiosPorEstadoAsync(id);

                if (coms == null)
                {
                    return StatusCode(500, "La consulta trajo una lista nula");
                }

                if (coms.Count <= 0)
                {
                    return NotFound();
                }

                return Ok(coms);
            }
            catch (Exception ex)
            {
                _log.LogInformation($"error al obtener Municipios para el estado {id}", ex);
                return StatusCode(500, "Ocurrió un problema mientras se procesaba la petición");
            }
        }

        /// <summary>
        /// Obtiene la lista de Procesos responsables del catálogo
        /// </summary>
        /// <returns></returns>
        [Route("procesosresponsables")]
        [HttpGet]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IEnumerable<CatalogoGenerico>>> ObtenerProcesosResponsables()
        {
            try
            {
                var coms = await _pp.ObtenerProcesosResponsablesAsync();

                if (coms == null)
                {
                    return StatusCode(500, "La consulta trajo una lista nula");
                }

                if (coms.Count <= 0)
                {
                    return NotFound();
                }

                return Ok(coms);
            }
            catch (Exception ex)
            {
                _log.LogInformation($"error al obtener los procesos responsables del catálogo", ex);
                return StatusCode(500, "Ocurrió un problema mientras se procesaba la petición");
            }
        }

        /// <summary>
        /// Obtiene la lista de tipos de documentos del catálogo
        /// </summary>
        /// <returns></returns>
        [Route("tiposdocumentos")]
        [HttpGet]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IEnumerable<CatalogoGenerico>>> ObtenerTiposDocumentos()
        {
            try
            {
                var coms = await _pp.ObtenerTiposDocumentosAsync();

                if (coms == null)
                {
                    return StatusCode(500, "La consulta trajo una lista nula");
                }

                if (coms.Count <= 0)
                {
                    return NotFound();
                }

                return Ok(coms);
            }
            catch (Exception ex)
            {
                _log.LogInformation($"error al obtener los tipos de documentos del catálogo", ex);
                return StatusCode(500, "Ocurrió un problema mientras se procesaba la petición");
            }
        }
    }
}


