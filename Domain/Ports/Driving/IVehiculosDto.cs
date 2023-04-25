using Domain.DTOs;

namespace Domain.Ports.Driving
{
    public interface IVehiculosDtoCommand
    {
        Task AgregaAsync(VehiculoDtoForCreate vehiculo);
        Task ActualizaAsync(VehiculoDtoForUpdate vehiculo);
        Task BorraPorOpcionAsync(string opcion, string dato, string usuario);
    }

    public interface IVehiculosDtoQuery
    {
        Task<List<VehiculoDto>> ObtenerVehiculosPorOpcionAsync(string opcion, int region, string criterio, string usuario);        
    }
}
