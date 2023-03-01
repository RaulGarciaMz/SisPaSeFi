using Domain.DTOs;
using Domain.Ports.Driving;
using Microsoft.AspNetCore.Mvc;
using SqlServerAdapter.Data;
using DomainServices.DomServ;
using System.Drawing;
using Microsoft.Extensions.Options;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApiSSF.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RutasPatrullajeController : ControllerBase
    {

        private readonly IRutaService _rp;
        private readonly ILogger<RutasPatrullajeController> _log;

        public RutasPatrullajeController(IRutaService r, ILogger<RutasPatrullajeController> log)
        {
            //_rp = new RutaService(new SqlServerAdapter.RutaRepository(new RutaContext()));
            _rp = r ?? throw new ArgumentNullException(nameof(r));
            _log = log;
        }

        // GET: api/<RutasPatrullajeController>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<RutaDto>>> GetValues(string usuario, int opcion, string tipo, string criterio, string actividad)
        {
            try
            {
                return Ok(await _rp.ObtenerPorFiltro(usuario, opcion, tipo, criterio, actividad));
            }
            catch (Exception ex)
            {
                _log.LogInformation($"error al obtener rutas para la opcion: {opcion}, tipo: {tipo}, criterio: {criterio}, usuario: {usuario}, actividad {actividad}", ex);
                return StatusCode(500, "Ocurrió un problema mientras se procesaba la petición");
            }    
        }

        // POST api/<RutasPatrullajeController>
        [HttpPost]
        public async Task<ActionResult> PostValue(string usuario,[FromBody] RutaDto r)
        {
            try
            {
                await _rp.Agrega(r, usuario);
                return StatusCode(201, "Ok");
            }
            catch (Exception ex)
            {
                _log.LogInformation($"error al agregar rutas para el usuario: {usuario}", ex);
                return StatusCode(500, "Ocurrió un problema mientras se procesaba la petición");
            }
        }

        // PUT api/<RutasPatrullajeController>/5
        [HttpPut("{id}")]
        public async Task<ActionResult> PutValue(int id, string usuario, [FromBody] RutaDto r)
        {
            try
            {
                await _rp.Update(r, usuario);
                return Ok();
            }
            catch (Exception ex)
            {
                _log.LogInformation($"error al actualizar la ruta con id: {id} para el usuario: {usuario}", ex);
                return StatusCode(500, "Ocurrió un problema mientras se procesaba la petición");
            }            
        }

        // DELETE api/<RutasPatrullajeController>/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteValue(int id, string usuario)
        {
            try
            {
                await _rp.Delete(id, usuario);
                return Ok();
            }
            catch (Exception ex)
            {
                _log.LogInformation($"error al eliminar la ruta con id: {id} para el usuario: {usuario}", ex);
                return StatusCode(500, "Ocurrió un problema mientras se procesaba la petición");
            }
        }
    }
}
