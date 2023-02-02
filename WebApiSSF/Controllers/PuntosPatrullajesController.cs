using Domain.DTOs;
using Domain.Entities;
using Domain.Enums;
using Domain.Ports.Driving;
using Microsoft.AspNetCore.Mvc;
using SqlServerAdapter.Data;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApiSSF.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PuntosPatrullajesController : ControllerBase
    {
        private readonly IPuntosPatrullaje _pp;

        public PuntosPatrullajesController()
        {
            _pp = new SqlServerAdapter.PuntoPatrullajeRepository(new PatrullajeContext());
        }

        // GET: api/<PuntosPatrullajesController>
        [HttpGet]
        public IEnumerable<PuntoDto> GetValues(int opcion, string valor)
        {

            FiltroPunto filtro=(FiltroPunto) opcion;
            List<PuntoPatrullaje> l = _pp.ObtenerPorOpcion(filtro, valor).ToList();

            var p = new List<PuntoDto>();
            if (l.Count >0 ) { 
            // covertir de Domai To Dto
            }

            return p;
        }

        // POST api/<PuntosPatrullajesController>
        [HttpPost]
        public void PostValue([FromBody] PuntoPatrullaje pto)
        {
            _pp.Agrega(pto);
        }

        // PUT api/<PuntosPatrullajesController>/5
        [HttpPut("{id}")]
        public void PutValue(int id, [FromBody] PuntoPatrullaje pto)
        {
           _pp.Update(pto);
        }

        // DELETE api/<PuntosPatrullajesController>/5
        [HttpDelete("{id}")]
        public void DeleteValue(int id)
        {
             _pp.Delete(id);
        }
    }
}
