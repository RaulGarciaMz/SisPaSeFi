﻿using Domain.DTOs;
using Domain.Entities;
using Domain.Enums;
using Domain.Ports.Driving;
using DomainServices.DomServ;
using Microsoft.AspNetCore.Mvc;
using SqlServerAdapter.Data;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApiSSF.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PuntosPatrullajesController : ControllerBase
    {
        private readonly IPuntosService _pp;

        public PuntosPatrullajesController()
        {
            _pp = new PuntosService(new SqlServerAdapter.PuntoPatrullajeRepository(new PatrullajeContext()));
        }

        // GET: api/<PuntosPatrullajesController>
        [HttpGet]
        public IEnumerable<PuntoDto> GetValues(int opcion, string valor)
        {
            FiltroPunto filtro=(FiltroPunto) opcion;
            return _pp.ObtenerPorOpcion(filtro, valor);
        }

        // POST api/<PuntosPatrullajesController>
        [HttpPost]
        public void PostValue([FromBody] PuntoDto pto)
        {
            _pp.Agrega(pto);
        }

        // PUT api/<PuntosPatrullajesController>/5
        [HttpPut("{id}")]
        public void PutValue(int id, [FromBody] PuntoDto pto)
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
