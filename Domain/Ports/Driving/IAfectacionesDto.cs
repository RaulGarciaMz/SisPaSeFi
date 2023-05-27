using Domain.DTOs;

namespace Domain.Ports.Driving
{
    public interface IAfectacionesDtoCommand
    {
        Task AgregaAsync(AfectacionDtoForCreate a);
        Task ActualizaAsync(AfectacionDtoForUpdate a);
    }

    public interface IAfectacionesDtoQuery
    {
        Task<List<AfectacionDto>> ObtenerAfectacionIncidenciaPorOpcionAsync(int idReporte, string tipo, string usuario);
    }
}
