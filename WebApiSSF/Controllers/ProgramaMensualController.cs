﻿using Domain.DTOs;
using Domain.Entities;
using Domain.Ports.Driving;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Net.Mime;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApiSSF.Controllers
{
    [Route("api/[controller]")]
    //[Authorize]
    [ApiController]
    public class ProgramaMensualController : ControllerBase
    {
        private readonly IProgramaMensualService _rp;
        private readonly ILogger<ProgramaMensualController> _log;

        public ProgramaMensualController(IProgramaMensualService r, ILogger<ProgramaMensualController> log)
        {
            _rp = r ?? throw new ArgumentNullException(nameof(r));
            _log = log;
        }
                
        /// <summary>
        /// Obtiene el programa mensual de patrullaje acorde a los parámetros indicados
        /// </summary>
        /// <param name="anio">Año del programa mensual</param>
        /// <param name="mes">Mes del programa mensual</param>
        /// <param name="region">Identificador de la región para el reporte mensual</param>
        /// <param name="tipo">Tipo de patrullaje ala que se refiere el programa mensual</param>
        /// <param name="usuario">Nombre del usuario (usuario_nom) que realiza la operación</param>
        /// <returns></returns>
        [HttpGet]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IEnumerable<ProgramaPatrullajeMensualDto>>> ObtenerProgramaMensual([Required]int anio, [Required] int mes, [Required] string region, [Required] string tipo, [Required] string usuario)
        {
            try
            {
                var programa = await _rp.ObtenerProgramaMensualAsync( anio,  mes,  region,  tipo,  usuario);

                if (programa == null)
                {
                    return NotFound();
                }

                return Ok(programa);
            }
            catch (Exception ex)
            {
                _log.LogInformation($"error al obtener programa mensual para el año: {anio}, mes: {mes}, región: {region}, tipo: {tipo}, usuario: {usuario}", ex);
                return StatusCode(500, "Ocurrió un problema mientras se procesaba la petición");
            }
        }


    }
}
