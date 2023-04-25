using Domain.Entities;
using Domain.Entities.Vistas;

namespace Domain.Ports.Driven
{
    public interface ITarjetaInformativaCommand
    {
        Task AgregaAsync(TarjetaInformativa tarjeta, int idEstadoPatrullaje, int usuarioId);
        Task UpdateTarjetaAndProgramaAsync(TarjetaInformativa tarjeta, int idEstadoPatrullaje, int usuarioId, int idPuntoResponsable);
    }

    public interface ITarjetaInformativaQuery
    {
        Task<List<TarjetaInformativaVista>> ObtenerTarjetasPorRegionAsync(string tipo, string region, int anio, int mes);
        Task<List<TarjetaInformativaVista>> ObtenerParteNovedadesPorDiaAsync(string tipo, int anio, int mes, int dia);
        Task<List<TarjetaInformativaVista>> ObtenerMonitoreoAsync(string tipo, int idUsuario, int anio, int mes, int dia);
        Task<TarjetaInformativaVista> ObtenerPorIdAsync(int id);
        Task<int> ObtenerIdUsuarioRegistradoAsync(string usuario);
        Task<int> ObtenerIdUsuarioConfiguradorAsync(string usuario);
        Task<TarjetaInformativa?> ObtenerTarjetaPorIdNotaAsync(int idNota);
        Task<int> NumeroDeTarjetasPorProgamaAsync(int idPrograma);
    }
}
