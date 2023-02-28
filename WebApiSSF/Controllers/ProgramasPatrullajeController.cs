using Domain.DTOs;
using Domain.Entities;
using Domain.Enums;
using Domain.Ports.Driving;
using DomainServices.DomServ;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using SqlServerAdapter.Data;
using System;
using System.Security.Cryptography;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApiSSF.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProgramasPatrullajeController : ControllerBase
    {
        private readonly IProgramaService _pp;
        private readonly ILogger<ProgramasPatrullajeController> _log;

        public ProgramasPatrullajeController(ILogger<ProgramasPatrullajeController> log)
        {
            _pp = new ProgramasService(new SqlServerAdapter.ProgramaPatrullajeRepository(new ProgramaContext()));
            _log = log;
        }


        // GET: api/<ProgramasPatrullajeController>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PatrullajeDto>>> GetValues(string tipo, int region, string usuario, string clase, int anio, int mes, int dia, int opcion=0, int periodo=1)
        {
            try
            {
                var laOpcion = (FiltroProgramaOpcion)opcion;
                var elPeriodo = (PeriodoOpcion)periodo;

                var programas = await _pp.ObtenerPorFiltro(tipo, region, clase, anio, mes, dia, laOpcion, elPeriodo);
                return Ok();
            }
            catch (Exception ex)
            {
                _log.LogInformation($"error al obtener programas para el tipo: {tipo}, region: {region}, usuario: {usuario}, año: {anio}, mes {mes}, día: {dia}, clase: {clase}, opción: {opcion}, período: {periodo}", ex);
                return StatusCode(500, "Ocurrió un problema mientras se procesaba la petición");
            }
        }
        
        // POST api/<ProgramasPatrullajeController>
        [HttpPost]
        public async Task<ActionResult> PostPrograma(string opcion, string clase, string usuario, [FromBody] ProgramaDto p)
        {
            try
            {
                await _pp.AgregaPrograma(opcion, clase, p, usuario);
                return StatusCode(201, "Ok");
            }
            catch (Exception ex)
            {
                _log.LogInformation($"error al registrar un programa para el usuario: {usuario}, clase: {clase}, opción: {opcion}", ex);
                return StatusCode(500, "Ocurrió un problema mientras se procesaba la petición");
            }           
        }

/*        // POST api/<ProgramasPatrullajeController>
        [HttpPost]
        public async Task<ActionResult> PostPropuestas([FromQuery] string usuario, [FromBody] List<ProgramaDto> p)
        {
            try
            {
                await _pp.AgregaPropuestasComoProgramas(p, usuario);
                return StatusCode(201, "Ok");
            }
            catch (Exception ex)
            {
                _log.LogInformation($"error al registrar una propuesta para el usuario: {usuario}", ex);
                return StatusCode(500, "Ocurrió un problema mientras se procesaba la petición");
            }
        }*/

        // PUT api/<ProgramasPatrullajeController>/5
        [HttpPut("{usuario}")]
        public async Task<ActionResult> PutCambioRuta(string usuario, [FromBody] ProgramaDto p)
        {
            try
            {
                await _pp.ActualizaProgramaPorCambioDeRuta(p, usuario);
                return Ok();
            }
            catch (Exception ex)
            {
                _log.LogInformation($"error al actualizar un programa por cambio de ruta para el usuario: {usuario}", ex);
                return StatusCode(500, "Ocurrió un problema mientras se procesaba la petición");
            }
        }

/*        // PUT api/<ProgramasPatrullajeController>/5
        [HttpPut("{usuario}")]
        public async Task<ActionResult> PutPropuestasToProgramas(string opcion, int accion, string usuario, [FromBody] List<ProgramaDto> p)
        {
            try
            {
                await _pp.ActualizaPropuestasComoProgramasActualizaPropuestas(p, opcion, accion, usuario);
                return Ok();
            }
            catch (Exception ex)
            {
                _log.LogInformation($"error al actualizar una propuesta como programa para el usuario: {usuario}, opcion: {opcion}, acción: {accion}", ex);
                return StatusCode(500, "Ocurrió un problema mientras se procesaba la petición");
            }
        }*/

        // DELETE api/<ProgramasPatrullajeController>/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id, string usuario)
        {
            try
            {
                await _pp.DeletePropuesta(id, usuario);
                return Ok();
            }
            catch (Exception ex)
            {
                _log.LogInformation($"error al eliminar el programa id: {id} para el usuario: {usuario}", ex);
                return StatusCode(500, "Ocurrió un problema mientras se procesaba la petición");
            }
        }
    }
}
