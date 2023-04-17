using Domain.Entities.Vistas;

namespace Domain.Ports.Driving
{
    public interface IEstadisticasDtoQuery
    {
        Task<List<EstadisticaSistemaVista>> ObtenerEstadisticasDeSistemaAsync(string usuario);
    }
}
