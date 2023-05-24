using Domain.DTOs.catalogos;
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
    public class CatalogosParaSeleccionController : ControllerBase
    {

        private readonly ICatalogosConsultaService _pp;
        private readonly ILogger<CatalogosParaSeleccionController> _log;

        public CatalogosParaSeleccionController(ICatalogosConsultaService p, ILogger<CatalogosParaSeleccionController> log)
        {
            _pp = p ?? throw new ArgumentNullException(nameof(p));
            _log = log ?? throw new ArgumentNullException(nameof(log));
        }

        /// <summary>
        /// Obtiene la lista de registros de un catálogo correspondiente a la opción indicada 
        /// </summary>
        /// <param name="opcion">Nombre de la opción para catálogo ("RSF", "TipoPatrullaje", "TipoVehiculo", "ClasificacionIncidencia", "Niveles", "ConceptosAfectacion", "RegionesSDN", "ResultadoPatrullaje", "EstadosDelPais", "MunicipiosEstado-XX", "ProcesosResponsables", "GerenciaDivision-XX", "TipoDocumento", "EstadosPatrullaje", "TodoEstadosPatrullaje", "ApoyoPatrullaje", "InstalacionesDeComandancia-XX", "NivelRiesgo", "Hallazgo", "LocalidadMunicipio-XX", "EstadosIncidencia", "ComandanciasDeUnUsuario-XX", "GrupoCorreoDeUnUsuario-XX", "RolesDeUnUsuario-XX", "GruposCorreo", "MenusDeRol-XX"  ). XX es información adicional</param>
        /// <param name="usuario">Nombre del usuario (usuario_nom) que realiza la operación</param>
        /// <returns></returns>
        [HttpGet]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IEnumerable<CatalogoGenerico>>> ObtenerCatalogoPorOpcion([Required] string opcion, [Required] string usuario)
        {
            try
            {
                var coms = await _pp.ObtenerCatalogoPorOpcionAsync(opcion, usuario);

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
                _log.LogError($"error al obtener el catálogo por opción", ex);
                return StatusCode(500, "Ocurrió un problema mientras se procesaba la petición");
            }
        }

        /*
                /// <summary>
                /// Obtiene la lista de comandancias para un usuario indicado
                /// </summary>
                /// <param name="usuario">Nombre del usuario (usuario_nom) que realiza la operación</param>
                /// <returns></returns>
                [Route("comandancias")]
                [HttpGet]
                [Produces(MediaTypeNames.Application.Json)]
                [ProducesResponseType(StatusCodes.Status200OK)]
                [ProducesResponseType(StatusCodes.Status404NotFound)]
                [ProducesResponseType(StatusCodes.Status500InternalServerError)]
                public async Task<ActionResult<IEnumerable<CatalogoGenerico>>> ObtenerComandanciaPorUsuario([Required] string usuario)
                {
                    try
                    {
                        var coms = await _pp.ObtenerComandanciaPorIdUsuarioAsync(usuario);

                        if (coms == null) { }

                        if (coms.Count <= 0)
                        {
                            return NotFound();
                        }

                        return Ok(coms);
                    }
                    catch (Exception ex)
                    {
                        _log.LogError($"error al obtener comandancias por usuario del catálogo", ex);
                        return StatusCode(500, "Ocurrió un problema mientras se procesaba la petición");
                    }
                }

                /// <summary>
                /// Obtiene la lista de tipos de patrullaje del catálogo
                /// </summary>
                /// <param name="usuario">Nombre del usuario (usuario_nom) que realiza la operación</param>
                /// <returns></returns>
                [Route("tipopatrullaje")]
                [HttpGet]
                [Produces(MediaTypeNames.Application.Json)]
                [ProducesResponseType(StatusCodes.Status200OK)]
                [ProducesResponseType(StatusCodes.Status404NotFound)]
                [ProducesResponseType(StatusCodes.Status500InternalServerError)]
                public async Task<ActionResult<IEnumerable<CatalogoGenerico>>> ObtenerTiposPatrullaje([Required] string usuario)
                {
                    try
                    {
                        var coms = await _pp.ObtenerTiposPatrullajeAsync(usuario);

                        if (coms == null) { }

                        if (coms.Count <= 0)
                        {
                            return NotFound();
                        }

                        return Ok(coms);
                    }
                    catch (Exception ex)
                    {
                        _log.LogError($"error al obtener tipos de patrullaje del catálogo", ex);
                        return StatusCode(500, "Ocurrió un problema mientras se procesaba la petición");
                    }
                }

                /// <summary>
                /// Obtiene la lista de tipos de vehículos del catálogo
                /// </summary>
                /// <param name="usuario">Nombre del usuario (usuario_nom) que realiza la operación</param>
                /// <returns></returns>
                [Route("tipovehiculo")]
                [HttpGet]
                [Produces(MediaTypeNames.Application.Json)]
                [ProducesResponseType(StatusCodes.Status200OK)]
                [ProducesResponseType(StatusCodes.Status404NotFound)]
                [ProducesResponseType(StatusCodes.Status500InternalServerError)]
                public async Task<ActionResult<IEnumerable<CatalogoGenerico>>> ObtenerTiposVehiculo([Required] string usuario)
                {
                    try
                    {
                        var coms = await _pp.ObtenerTiposVehiculoAsync(usuario);

                        if (coms == null) { }

                        if (coms.Count <= 0)
                        {
                            return NotFound();
                        }

                        return Ok(coms);
                    }
                    catch (Exception ex)
                    {
                        _log.LogError($"error al obtener tipos de vehículo del catálogo", ex);
                        return StatusCode(500, "Ocurrió un problema mientras se procesaba la petición");
                    }
                }

                /// <summary>
                /// Obtiene la lista de clasificaciones de incidencias del catálogo
                /// </summary>
                /// <param name="usuario">Nombre del usuario (usuario_nom) que realiza la operación</param>
                /// <returns></returns>
                [Route("clasificacionincidencias")]
                [HttpGet]
                [Produces(MediaTypeNames.Application.Json)]
                [ProducesResponseType(StatusCodes.Status200OK)]
                [ProducesResponseType(StatusCodes.Status404NotFound)]
                [ProducesResponseType(StatusCodes.Status500InternalServerError)]
                public async Task<ActionResult<IEnumerable<CatalogoGenerico>>> ObtenerClasificacionesIncidencia([Required] string usuario)
                {
                    try
                    {
                        var coms = await _pp.ObtenerClasificacionesIncidenciaAsync(usuario);

                        if (coms == null) { }

                        if (coms.Count <= 0)
                        {
                            return NotFound();
                        }

                        return Ok(coms);
                    }
                    catch (Exception ex)
                    {
                        _log.LogError($"error al obtener clasificaciones de incidencia del catálogo", ex);
                        return StatusCode(500, "Ocurrió un problema mientras se procesaba la petición");
                    }
                }

                /// <summary>
                /// Obtiene la lista de niveles del catálogo
                /// </summary>
                /// <param name="usuario">Nombre del usuario (usuario_nom) que realiza la operación</param>
                /// <returns></returns>
                [Route("niveles")]
                [HttpGet]
                [Produces(MediaTypeNames.Application.Json)]
                [ProducesResponseType(StatusCodes.Status200OK)]
                [ProducesResponseType(StatusCodes.Status404NotFound)]
                [ProducesResponseType(StatusCodes.Status500InternalServerError)]
                public async Task<ActionResult<IEnumerable<CatalogoGenerico>>> ObtenerNiveles([Required] string usuario)
                {
                    try
                    {
                        var coms = await _pp.ObtenerNivelesAsync(usuario);

                        if (coms == null) { }

                        if (coms.Count <= 0)
                        {
                            return NotFound();
                        }

                        return Ok(coms);
                    }
                    catch (Exception ex)
                    {
                        _log.LogError($"error al obtener niveles del catálogo", ex);
                        return StatusCode(500, "Ocurrió un problema mientras se procesaba la petición");
                    }
                }

                /// <summary>
                /// Obtiene la lista de conceptos de afectación del catálogo
                /// </summary>
                /// <param name="usuario">Nombre del usuario (usuario_nom) que realiza la operación</param>
                /// <returns></returns>
                [Route("conceptosafectacion")]
                [HttpGet]
                [Produces(MediaTypeNames.Application.Json)]
                [ProducesResponseType(StatusCodes.Status200OK)]
                [ProducesResponseType(StatusCodes.Status404NotFound)]
                [ProducesResponseType(StatusCodes.Status500InternalServerError)]
                public async Task<ActionResult<IEnumerable<CatalogoGenerico>>> ObtenerConceptosAfectacion([Required] string usuario)
                {
                    try
                    {
                        var coms = await _pp.ObtenerConceptosAfectacionAsync(usuario);

                        if (coms == null) { }

                        if (coms.Count <= 0)
                        {
                            return NotFound();
                        }

                        return Ok(coms);
                    }
                    catch (Exception ex)
                    {
                        _log.LogError($"error al obtener conceptos de afectación del catálogo", ex);
                        return StatusCode(500, "Ocurrió un problema mientras se procesaba la petición");
                    }
                }

                /// <summary>
                /// Obtiene la lista de identificadores de las regiones militares que son usador en las rutas
                /// </summary>
                /// <param name="usuario">Nombre del usuario (usuario_nom) que realiza la operación</param>
                /// <returns></returns>
                [Route("regionesenrutas")]
                [HttpGet]
                [Produces(MediaTypeNames.Application.Json)]
                [ProducesResponseType(StatusCodes.Status200OK)]
                [ProducesResponseType(StatusCodes.Status404NotFound)]
                [ProducesResponseType(StatusCodes.Status500InternalServerError)]
                public async Task<ActionResult<IEnumerable<int>>> ObtenerRegionesMilitaresEnRutasConDescVacia([Required] string usuario)
                {
                    try
                    {
                        var coms = await _pp.ObtenerRegionesMilitaresEnRutasConDescVaciaAsync(usuario);

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
                        _log.LogError($"error al obtener regiones militares en rutas del catálogo de rutas", ex);
                        return StatusCode(500, "Ocurrió un problema mientras se procesaba la petición");
                    }
                }

                /// <summary>
                /// Obtiene la lista de resultados de patrullaje
                /// </summary>
                /// <param name="usuario">Nombre del usuario (usuario_nom) que realiza la operación</param>
                /// <returns></returns>
                [Route("resultadospatrullaje")]
                [HttpGet]
                [Produces(MediaTypeNames.Application.Json)]
                [ProducesResponseType(StatusCodes.Status200OK)]
                [ProducesResponseType(StatusCodes.Status404NotFound)]
                [ProducesResponseType(StatusCodes.Status500InternalServerError)]
                public async Task<ActionResult<IEnumerable<CatalogoGenerico>>> ObtenerResultadosPatrullajeAsync([Required] string usuario)
                {
                    try
                    {
                        var coms = await _pp.ObtenerResultadosPatrullajeAsync(usuario);

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
                        _log.LogError($"error al obtener estados del país del catálogo", ex);
                        return StatusCode(500, "Ocurrió un problema mientras se procesaba la petición");
                    }
                }

                /// <summary>
                /// Obtiene la lista de Estados del país del catálogo
                /// </summary>
                /// <param name="usuario">Nombre del usuario (usuario_nom) que realiza la operación</param>
                /// <returns></returns>
                [Route("estadospais")]
                [HttpGet]
                [Produces(MediaTypeNames.Application.Json)]
                [ProducesResponseType(StatusCodes.Status200OK)]
                [ProducesResponseType(StatusCodes.Status404NotFound)]
                [ProducesResponseType(StatusCodes.Status500InternalServerError)]
                public async Task<ActionResult<IEnumerable<CatalogoGenerico>>> ObtenerEstadosDelPais([Required] string usuario)
                {
                    try
                    {
                        var coms = await _pp.ObtenerEstadosPaisAsync(usuario);

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
                        _log.LogError($"error al obtener estados del país del catálogo", ex);
                        return StatusCode(500, "Ocurrió un problema mientras se procesaba la petición");
                    }
                }

                /// <summary>
                /// Obtiene la lista de Municipios pertenecientes al estado indicado en el parámetro
                /// </summary>
                /// <param name="id">Identificador del estado</param>
                /// <param name="usuario">Nombre del usuario (usuario_nom) que realiza la operación</param>
                /// <returns></returns>
                [Route("municipios")]
                [HttpGet]
                [Produces(MediaTypeNames.Application.Json)]
                [ProducesResponseType(StatusCodes.Status200OK)]
                [ProducesResponseType(StatusCodes.Status404NotFound)]
                [ProducesResponseType(StatusCodes.Status500InternalServerError)]
                public async Task<ActionResult<IEnumerable<CatalogoGenerico>>> ObtenerMunicipiosPorEstado([Required]int id, [Required] string usuario)
                {
                    try
                    {
                        var coms = await _pp.ObtenerMunicipiosPorEstadoAsync(id, usuario);

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
                        _log.LogError($"error al obtener Municipios para el estado {id}", ex);
                        return StatusCode(500, "Ocurrió un problema mientras se procesaba la petición");
                    }
                }

                /// <summary>
                /// Obtiene la lista de Procesos responsables del catálogo
                /// </summary>
                /// <param name="usuario">Nombre del usuario (usuario_nom) que realiza la operación</param>
                /// <returns></returns>
                [Route("procesosresponsables")]
                [HttpGet]
                [Produces(MediaTypeNames.Application.Json)]
                [ProducesResponseType(StatusCodes.Status200OK)]
                [ProducesResponseType(StatusCodes.Status404NotFound)]
                [ProducesResponseType(StatusCodes.Status500InternalServerError)]
                public async Task<ActionResult<IEnumerable<CatalogoGenerico>>> ObtenerProcesosResponsables([Required] string usuario)
                {
                    try
                    {
                        var coms = await _pp.ObtenerProcesosResponsablesAsync(usuario);

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
                        _log.LogError($"error al obtener los procesos responsables del catálogo", ex);
                        return StatusCode(500, "Ocurrió un problema mientras se procesaba la petición");
                    }
                }

                /// <summary>
                /// Obtiene la lista de las tablas del catálogo de gerencia división acorde al identificador del proceso responsable indicado
                /// </summary>
                /// <param name="idProceso">Identificador del proceso responsable</param>
                /// <param name="usuario">Nombre del usuario (usuario_nom) que realiza la operación</param>
                /// <returns></returns>
                [Route("gerenciadivision")]
                [HttpGet]
                [Produces(MediaTypeNames.Application.Json)]
                [ProducesResponseType(StatusCodes.Status200OK)]
                [ProducesResponseType(StatusCodes.Status404NotFound)]
                [ProducesResponseType(StatusCodes.Status500InternalServerError)]
                public async Task<ActionResult<IEnumerable<CatalogoGenerico>>> ObtenerGerenciaDivision([Required] int idProceso, [Required] string usuario)
                {
                    try
                    {
                        var coms = await _pp.ObtenerGerenciaDivisionAsync(idProceso,usuario);

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
                        _log.LogError($"error al obtener la lista de gerencia división del catálogo", ex);
                        return StatusCode(500, "Ocurrió un problema mientras se procesaba la petición");
                    }
                }

                /// <summary>
                /// Obtiene la lista de tipos de documentos del catálogo
                /// </summary>
                /// <param name="usuario">Nombre del usuario (usuario_nom) que realiza la operación</param>
                /// <returns></returns>
                [Route("tiposdocumentos")]
                [HttpGet]
                [Produces(MediaTypeNames.Application.Json)]
                [ProducesResponseType(StatusCodes.Status200OK)]
                [ProducesResponseType(StatusCodes.Status404NotFound)]
                [ProducesResponseType(StatusCodes.Status500InternalServerError)]
                public async Task<ActionResult<IEnumerable<CatalogoGenerico>>> ObtenerTiposDocumentos([Required] string usuario)
                {
                    try
                    {
                        var coms = await _pp.ObtenerTiposDocumentosAsync(usuario);

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
                        _log.LogError($"error al obtener los tipos de documentos del catálogo", ex);
                        return StatusCode(500, "Ocurrió un problema mientras se procesaba la petición");
                    }
                }
        */


    }
}


