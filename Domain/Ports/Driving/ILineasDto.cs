using Domain.DTOs;

namespace Domain.Ports.Driving
{
    public interface ILineasDtoCommand
    {
        Task AgregarAsync(LineaDtoForCreate linea);
        Task ActualizaAsync(LineaDtoForUpdate linea);
        Task BorraAsync(int idLinea, string usuario);
    }

    public interface ILineasDtoQuery
    {
        Task<List<LineaDto>> ObtenerLineasAsync(int opcion, string criterio, string usuario);
    }
}
