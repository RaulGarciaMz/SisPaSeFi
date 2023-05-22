using Domain.DTOs;

namespace Domain.Ports.Driving
{
    public interface IDatosPropuestaExtraordinariaDtoQuery
    {
        Task<DatosPropuestaExtraordinariaDto> ObtenerDatosPropuestaExtraordinariaAsync(int idPropuesta, string usuario);
    }
}
