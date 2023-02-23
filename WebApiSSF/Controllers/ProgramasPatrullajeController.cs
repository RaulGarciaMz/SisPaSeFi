using Domain.DTOs;
using Domain.Entities;
using Domain.Enums;
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
    public class ProgramasPatrullajeController : ControllerBase
    {
        private readonly IProgramaService _pp;

        public ProgramasPatrullajeController()
        {
            _pp = new ProgramasService(new SqlServerAdapter.ProgramaPatrullajeRepository(new ProgramaContext()));
        }


        // GET: api/<ProgramasPatrullajeController>
        [HttpGet]
        public IEnumerable<PatrullajeDto> GetValues(string tipo, int region, string usuario, string clase, int anio, int mes, int dia, int opcion=0, int periodo=1)
        {
            var laOpcion = (FiltroProgramaOpcion)opcion;
            var elPeriodo = (PeriodoOpcion)periodo;

            return _pp.ObtenerPorFiltro(tipo, region, clase, anio, mes, dia, laOpcion, elPeriodo);
        }
        
        // POST api/<ProgramasPatrullajeController>
        [HttpPost]
        public void PostPrograma(string opcion, string clase, string usuario, [FromBody] ProgramaDto p)
        {
            _pp.AgregaPrograma(opcion, clase, p, usuario);
        }

        // POST api/<ProgramasPatrullajeController>
        [HttpPost]
        public void PostPropuestas(string usuario, [FromBody] List<ProgramaDto> p)
        {
            _pp.AgregaPropuestasComoProgramas(p, usuario);
        }

        // PUT api/<ProgramasPatrullajeController>/5
        [HttpPut("{usuario}")]
        public void PutCambioRuta(string usuario, [FromBody] ProgramaDto p)
        {
            _pp.ActualizaProgramaPorCambioDeRuta(p, usuario);
        }

        // PUT api/<ProgramasPatrullajeController>/5
        [HttpPut("{usuario}")]
        public void PutPropuestasToProgramas(string opcion, int accion, string usuario, [FromBody] List<ProgramaDto> p)
        {
            _pp.ActualizaPropuestasComoProgramasActualizaPropuestas(p,opcion, accion, usuario);
        }

        // DELETE api/<ProgramasPatrullajeController>/5
        [HttpDelete("{id}")]
        public void Delete(int id, string usuario)
        {
            _pp.DeletePropuesta(id, usuario);
        }
    }
}
