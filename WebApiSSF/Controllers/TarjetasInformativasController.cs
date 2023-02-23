using Domain.DTOs;
using Domain.Ports.Driving;
using DomainServices.DomServ;
using Microsoft.AspNetCore.Mvc;
using SqlServerAdapter.Data;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApiSSF.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TarjetasInformativasController : ControllerBase
    {
        private readonly ITarjetaService _t;

        public TarjetasInformativasController()
        {
            _t = new TarjetasService(new SqlServerAdapter.TarjetaInformativaRepository(new TarjetaInformativaContext()));
        }

        // GET: api/<TarjetasInformativasController>
        [HttpGet]
        public IEnumerable<TarjetaDto> Get(string tipo, string region, int anio, int mes, string usuario)
        {
            return _t.ObtenerPorAnioMes(tipo, region, anio, mes, usuario);
        }

        // POST api/<TarjetasInformativasController>
        [HttpPost]
        public void Post(string usuario, [FromBody] TarjetaDto tarjeta)
        {
            _t.Agrega(tarjeta, usuario);
        }

        // PUT api/<TarjetasInformativasController>/5
        [HttpPut("{id}")]
        public void Put(int id, string usuario, [FromBody] TarjetaDto tarjeta)
        {
            _t.Update(tarjeta, usuario);
        }
    }
}
