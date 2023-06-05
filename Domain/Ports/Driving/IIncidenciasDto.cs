using Domain.DTOs;

namespace Domain.Ports.Driving
{
    public interface IIncidenciasDtoComand
    {
        Task AgregaIncidenciaAsync(IncidenciasDtoForCreate i);
        Task ActualizaIncidenciaAsync(IncidenciasDtoForUpdate i);
    }

    public interface IIncidenciasDtoQuery
    {
        Task<List<IncidenciaGeneralDto>> ObtenerIncidenciasPorOpcionAsync(string opcion, int? idActivo, string usuario, string? criterio);
    }
}
