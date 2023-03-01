using Domain.DTOs;
using Domain.Entities;
using Domain.Enums;
using Domain.Ports.Driving;
using DomainServices.DomServ;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using SqlServerAdapter.Data;
using System.Drawing;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApiSSF.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PuntosPatrullajesController : ControllerBase
    {
        private readonly IPuntosService _pp;
        private readonly ILogger<PuntosPatrullajesController> _log;

        public PuntosPatrullajesController(IPuntosService p, ILogger<PuntosPatrullajesController> log)
        {
            //_pp = new PuntosService(new SqlServerAdapter.PuntoPatrullajeRepository(new PatrullajeContext()));
            _pp = p ?? throw new ArgumentNullException(nameof(p));
            _log = log;
        }

        // GET: api/<PuntosPatrullajesController>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PuntoDto>>> GetValues(int opcion, string valor, string usuario)
        {
            try
            {
                FiltroPunto filtro = (FiltroPunto)opcion;

                var puntos = await _pp.ObtenerPorOpcionAsync(filtro, valor, usuario);

                if (puntos == null)
                {
                    return NotFound();
                }

                return Ok(puntos);
            }
            catch (Exception ex)
            {
                _log.LogInformation($"error al obtener puntos de patrullaje para la opcion: {opcion}, criterio: {valor}, usuario: {usuario}", ex);
                string error = "Ocurrió un problema mientras se procesaba la petición " + ex.ToString();
                return StatusCode(500, error);
                //return StatusCode(500, "Ocurrió un problema mientras se procesaba la petición");
            } 
        }

        // POST api/<PuntosPatrullajesController>
        [HttpPost]
        public async Task<ActionResult> PostValue(string usuario,[FromBody] PuntoDto pto)
        {
            try
            {
                await _pp.Agrega(pto, usuario);
                return StatusCode(201, "Ok");
            }
            catch (Exception ex)
            {
                _log.LogInformation($"error al agregar el punto de patrullaje para el usuario: {usuario}", ex);
                return StatusCode(500, "Ocurrió un problema mientras se procesaba la petición");
            }
        }

        // PUT api/<PuntosPatrullajesController>/5
        [HttpPut("{id}")]
        public async Task<ActionResult> PutValue(int id, string usuario, [FromBody] PuntoDto pto)
        {
            try
            {
                await _pp.Update(pto, usuario);
                return Ok();
            }
            catch (Exception ex)
            {
                _log.LogInformation($"error al actualizar el punto de patrullaje para el usuario: {usuario}", ex);
                return StatusCode(500, "Ocurrió un problema mientras se procesaba la petición");
            }
        }

        // DELETE api/<PuntosPatrullajesController>/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteValue(int id, string usuario)
        {
            try
            {
                await _pp.Delete(id, usuario);
                return Ok();
            }
            catch (Exception ex)
            {
                _log.LogInformation($"error al eliminar el punto de patrullaje con id: {id} para el usuario: {usuario}", ex);
                return StatusCode(500, "Ocurrió un problema mientras se procesaba la petición");
            }
        }
    }
}
