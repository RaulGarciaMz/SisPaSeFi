using Domain.Entities;
using Domain.Entities.Vistas;

namespace Domain.Ports.Driven
{
    public interface IMonitoreoQuery
    {
        Task<List<MonitoreoVista>> ObtenerProgramasEnEstadoPreConcluidoPorTipoAsync(string tipo, int idUsuario);
        Task<List<MonitoreoVista>> ObtenerProgramasConcluidosPorTipoAsync(string tipo, int idUsuario);
        Task<List<MonitoreoVista>> ObtenerProgramasEnEstadoPostConcluidoPorTipoAsync(string tipo, int idUsuario);
        Task<List<PuntoEnRutaVista>> ObtenerPuntosEnRutaAsync(int idRuta);
        Task<List<TarjetaInformativa>> ObtenerTarjetasInformativasPorProgramaAsync(int idPrograma);
        Task<List<IncidenciaTarjetaVista>> ObtenerIncidenciasEnEstructuraPorTarjetaAsync(int idTarjeta);
        Task<List<IncidenciaTarjetaVista>> ObtenerIncidenciasEnInstalacionPorTarjetaAsync(int idTarjeta);
        Task<List<UsoVehiculoMonitoreoVista>> ObtenerUsoVehiculoPorProgramaAsync(int idPrograma);
    }
}
