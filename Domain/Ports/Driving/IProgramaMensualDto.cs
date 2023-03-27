using Domain.DTOs;
using Domain.Entities.Vistas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Ports.Driving
{
    public interface IProgramaMensualDtoQuery
    {
        Task<ProgramaPatrullajeMensualDto> ObtenerProgramaMensualAsync(int anio, int mes, string region, string tipo, string usuario);
    }
}
