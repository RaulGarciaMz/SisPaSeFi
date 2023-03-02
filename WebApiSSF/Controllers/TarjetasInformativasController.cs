﻿using Domain.DTOs;
using Domain.Ports.Driving;
using DomainServices.DomServ;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using SqlServerAdapter.Data;
using System.Net.Mime;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApiSSF.Controllers
{
    [Route("api/[controller]")]
    //[Authorize]
    [ApiController]
    public class TarjetasInformativasController : ControllerBase
    {
        private readonly ITarjetaService _t;
        private readonly ILogger<TarjetasInformativasController> _log;

        public TarjetasInformativasController(ITarjetaService t, ILogger<TarjetasInformativasController> log)
        {
            //_t = new TarjetasService(new SqlServerAdapter.TarjetaInformativaRepository(new TarjetaInformativaContext()));
            _t = t ?? throw new ArgumentNullException(nameof(t));
            _log = log;
        }

        /// <summary>
        /// Obtiene la lista de tarjetas informativas acorde a los parámetros indicados
        /// </summary>
        /// <param name="tipo">Tipo de patrullaje (TERRESTRE o AEREO)</param>
        /// <param name="region">Región de SSF</param>
        /// <param name="anio">Año</param>
        /// <param name="mes">Mes</param>
        /// <param name="usuario">Nombre del usuario que solicita la información</param>
        /// <returns>ActionResult con lista de tajetas informativas</returns>
        [HttpGet]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
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

        /// <summary>
        /// Registra una tarjeta informativa
        /// </summary>
        /// <param name="usuario">Nombre del usuario que registra la tarjeta</param>
        /// <param name="tarjeta">Tarjeta informativa a registrar</param>
        /// <returns></returns>
        [HttpPost]
        [Consumes(MediaTypeNames.Application.Json)]        
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
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

        /// <summary>
        /// Actualiza una tarjeta informativa indicada
        /// </summary>
        /// <param name="id">Identificador de la tarjeta informativa</param>
        /// <param name="usuario">Nombre del usuario que realiza la actualización</param>
        /// <param name="tarjeta">Tarjeta informativa con datos a actualizar</param>
        /// <returns></returns>
        [HttpPut("{id}")]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
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
