using Domain.DTOs;
using Domain.Ports.Driving;
using DomainServices.DomServ;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using SqlServerAdapter.Data;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApiSSF.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TarjetasInformativasController : ControllerBase
    {
        private readonly ITarjetaService _t;
        private readonly ILogger<TarjetasInformativasController> _log;

        public TarjetasInformativasController(ILogger<TarjetasInformativasController> log)
        {
            _t = new TarjetasService(new SqlServerAdapter.TarjetaInformativaRepository(new TarjetaInformativaContext()));
            _log = log;
        }

        // GET: api/<TarjetasInformativasController>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TarjetaDto>>> Get(string tipo, string region, int anio, int mes, string usuario)
        {
            try
            {
                var tarjetas = await _t.ObtenerPorAnioMes(tipo, region, anio, mes, usuario);

                return Ok(tarjetas);
            }
            catch (Exception ex)
            {
                _log.LogInformation($"error al obtener tarjetas informativas el tipo: {tipo}, región: {region},  año: {anio}, mes {mes}, usuario: {usuario}", ex);
                return StatusCode(500, "Ocurrió un problema mientras se procesaba la petición");
            }            
        }

        // POST api/<TarjetasInformativasController>
        [HttpPost]
        public async Task<ActionResult> Post(string usuario, [FromBody] TarjetaDto tarjeta)
        {
            try
            {
                await _t.Agrega(tarjeta, usuario);              
                return StatusCode(201, "Ok");
            }
            catch (Exception ex)
            {
                _log.LogInformation($"error al registrar tarjetas informativas para el usuario: {usuario}", ex);
                return StatusCode(500, "Ocurrió un problema mientras se procesaba la petición");
            }
        }

        // PUT api/<TarjetasInformativasController>/5
        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, string usuario, [FromBody] TarjetaDto tarjeta)
        {
            try
            {
                await _t.Update(tarjeta, usuario);
                return Ok();
            }
            catch (Exception ex)
            {
                _log.LogInformation($"error al actualizar la tarjeta informativa con id: {id}, para el usuario: {usuario}", ex);
                return StatusCode(500, "Ocurrió un problema mientras se procesaba la petición");
            }
        }
    }
}
