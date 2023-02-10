using Domain.DTOs;
using Domain.Ports.Driving;
using Microsoft.AspNetCore.Mvc;
using SqlServerAdapter.Data;
using DomainServices.DomServ;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApiSSF.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RutasPatrullajeController : ControllerBase
    {

        private readonly IRutaService _rp;

        public RutasPatrullajeController()
        {
            _rp = new RutaService(new SqlServerAdapter.RutaRepository(new RutaContext()));
        }

        // GET: api/<RutasPatrullajeController>
        [HttpGet]
        public IEnumerable<RutaDto> GetValues(string usuario, int opcion, string tipo, string criterio, string actividad)
        {
            return _rp.ObtenerPorFiltro(usuario,  opcion,  tipo,  criterio,  actividad);        
        }

        // POST api/<RutasPatrullajeController>
        [HttpPost]
        public void PostValue([FromBody] RutaDto r)
        {
            _rp.Agrega(r);
        }

        // PUT api/<RutasPatrullajeController>/5
        [HttpPut("{id}")]
        public void PutValue(int id, [FromBody] RutaDto r)
        {
            _rp.Update(r);
        }

        // DELETE api/<RutasPatrullajeController>/5
        [HttpDelete("{id}")]
        public void DeleteValue(int id)
        {
            _rp.Delete(id);
        }
    }
}
