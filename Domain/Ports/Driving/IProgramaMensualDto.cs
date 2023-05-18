using Domain.DTOs;

namespace Domain.Ports.Driving
{
    public interface IProgramaMensualDtoQuery
    {
        Task<ProgramaPatrullajeMensualDto> ObtenerProgramaMensualAsync(string opcion, int anio, int mes, string region, string tipo, string usuario);
    }
}
