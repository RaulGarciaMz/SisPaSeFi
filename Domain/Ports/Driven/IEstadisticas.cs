using Domain.Entities.Vistas;

namespace Domain.Ports.Driven
{
    public interface IEstadisticasQuery
    {
        Task<List<EstadisticaSistemaVista>> ObtenerEstadisticasDeSistemaAsync();
    }
}
