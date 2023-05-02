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
    public class EstructurasController : ControllerBase
    {
        private readonly IEstructurasService _pp;
        private readonly ILogger<EstructurasController> _log;

        public EstructurasController(IEstructurasService p, ILogger<EstructurasController> log)
        {
            _pp = p ?? throw new ArgumentNullException(nameof(p));
            _log = log ?? throw new ArgumentNullException(nameof(log));
        }

        /// <summary>
        /// Obtiene la lista de estructuras para una línea indicada
        /// </summary>
        /// <param name="idLinea">Identificador de la línea</param>
        /// <param name="usuario">Usuario (alias - usuario_nom) que realiza la consulta</param>
        /// <param name="opcion">Identificador de la opción a ejecutar (1 - Estructuras de una línea, 2 - Estructuras de una linea en una ruta, 3 - Estructuras alrededor de unas coordenadas)</param>
        /// <param name="criterio">Identificador de la ruta (para el caso de la opción 2)</param>
        /// <returns></returns>
        [HttpGet]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IEnumerable<EstructuraDto>>> ObtenerEstructuraPorOpcion([Required] int idLinea, [Required] string usuario, int opcion = 0, string criterio ="")
        {
            try
            {
                if (opcion <= 0 || opcion > 3) 
                {
                    return BadRequest("opción no válida. Debe ser 1,2 o 3");
                }

                if (opcion == 2) 
                {                    
                    var esEntero = Int32.TryParse(criterio, out int val);
                    if (!esEntero)
                        return BadRequest("error en criterio, para la opción 2, debe ser un entero");
                }

                if (opcion == 3)
                {
                    var coord = criterio.Split(",");
                    var latOk = float.TryParse(coord[0], out float x);
                    var longOk = float.TryParse(coord[1], out float y);
                    
                    if (!latOk)
                        return BadRequest("coordenadas erróneas en x (latitud)");
                    if (!longOk)
                        return BadRequest("coordenadas erróneas en y (longitud)");
                }

                var coms = await _pp.ObtenerEstructuraPorOpcionAsync(opcion,idLinea,criterio, usuario);

                if (coms.Count <= 0)
                {
                    return NotFound();
                }

                return Ok(coms);
            }
            catch (Exception ex)
            {
                _log.LogError($"error al obtener las estructuras por identificador de línea", ex);
                return StatusCode(500, "Ocurrió un problema mientras se procesaba la petición");
            }
        }

        
        /// <summary>
        /// Obtiene la estructura correspondiente al identificador indicado
        /// </summary>
        /// <param name="idEstructura">Identificador de la estructura</param>
        /// <param name="usuario">Usuario (alias - usuario_nom) que realiza la consulta</param>
        /// <returns></returns>
        [Route("id")]
        [HttpGet]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<EstructuraDto>> ObtenerEstructuraPorId([Required]int idEstructura, [Required] string usuario)
        {
            try
            {
                var coms = await _pp.ObtenerEstructuraPorIdAsync(idEstructura, usuario);
       
                return Ok(coms);
            }
            catch (Exception ex)
            {
                _log.LogError($"error al obtener la estructura por identificador ", ex);
                return StatusCode(500, "Ocurrió un problema mientras se procesaba la petición");
            }
        }
         

        /// <summary>
        /// Agrega una estructura
        /// </summary>
        /// <param name="estructura">Datos de la estructura a agregar</param>
        /// <returns></returns>
        [HttpPost]
        [Consumes(MediaTypeNames.Application.Json)]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> Post([FromBody] EstructuraDtoForCreate estructura)
        {
            try
            {
                if (estructura.Latitud == null || estructura.Longitud == null) 
                {
                    if (estructura.strCoordenadas == null || estructura.strCoordenadas == "") 
                    { 
                        return BadRequest("faltan coordenadas para la estructura");
                    }

                    var coord = estructura.strCoordenadas.Split(",");

                    estructura.Latitud = coord[0].Trim();
                    estructura.Longitud = coord[1].Trim();
                }

                await _pp.AgregaAsync(estructura.intIdLinea, estructura.strNombre, estructura.intIdMunicipio, estructura.Latitud, estructura.Longitud, estructura.strUsuario);
  
                return Ok();
            }
            catch (Exception ex)
            {
                _log.LogError($"error al obtener las estructuras por identificador de línea para una ruta dada", ex);
                return StatusCode(500, "Ocurrió un problema mientras se procesaba la petición");
            }
        }

        /// <summary>
        /// Actualiza la ubicación de una estructura
        /// </summary>
        /// <param name="id">Identificador de la estructura</param>
        /// <param name="estructura">Valores a modificar de la estructura</param>
        /// <returns></returns>
        [HttpPut("{id}")]
        [Consumes(MediaTypeNames.Application.Json)]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> Put(int id, [FromBody] EstructuraDtoForUbicacionUpdate estructura)
        {
            try
            {

                if (estructura.Latitud == null || estructura.Latitud == "" || estructura.Longitud == null || estructura.Longitud == "")
                {
                    if (estructura.strCoordenadas == null || estructura.strCoordenadas == "")
                    {
                        return BadRequest("faltan coordenadas para la estructura");
                    }

                    var coord = estructura.strCoordenadas.Split(",");

                    estructura.Latitud = coord[0].Trim();
                    estructura.Longitud = coord[1].Trim();
                }

                await _pp.ActualizaUbicacionAsync(estructura.intIdEstructura, estructura.strNombre, estructura.intIdMunicipio, estructura.Latitud, estructura.Longitud, estructura.strUsuario);

                return Ok();
            }
            catch (Exception ex)
            {
                _log.LogError($"error al obtener las estructuras por identificador de línea para una ruta dada", ex);
                return StatusCode(500, "Ocurrió un problema mientras se procesaba la petición");
            }
        }
    }
}
