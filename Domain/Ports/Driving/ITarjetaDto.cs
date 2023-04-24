using Domain.DTOs;

namespace Domain.Ports.Driving
{
    public interface ITarjetaDtoCommand
    {
        Task Agrega(TarjetaDto tarjeta, string usuario);
        Task Update(TarjetaDto tarjeta, string usuario);
    }

    public interface ITarjetaDtoQuery
    {
        Task<List<TarjetaDto>> ObtenerPorOpcion(int opcion, string tipo, string region, int anio, int mes, int dia, string usuario);
        Task<TarjetaDto> ObtenerPorId(int idTarjeta, string usuario);
    }
}
