using Domain.DTOs;
using Domain.Entities.Vistas;

namespace Domain.Ports.Driving
{
    public interface IEstadisticasDtoQuery
    {
        Task<List<EstadisticasSistemaDto>> ObtenerEstadisticasDeSistemaAsync(string usuario);
    }
}
