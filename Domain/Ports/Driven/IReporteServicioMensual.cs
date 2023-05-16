using Domain.Entities.Vistas;

namespace Domain.Ports.Driven
{
    public interface IReporteServicioMensualQuery
    {
        Task<List<ReporteServicioMensualVista>> ObtenerReporteAereoPorOpcionAsync(int Anio, int Mes, string tipo);
        Task<DetalleReporteServicioMensualVista> ObtenerResumenParaReporteAereoPorOpcionAsync(int Anio, int Mes, string tipo);
        Task<List<ReporteServicioMensualVista>> ObtenerReporteTerrestreSedenaPorAnioAnMesAsync(int Anio, int Mes);
        Task<DetalleReporteServicioMensualVista> ObtenerResumenParaReporteTerrestreSedenaPorAnioAnMesAsync(int Anio, int Mes);
        Task<List<ReporteServicioMensualVista>> ObtenerReporteTerrestreRegionPorAnioAnMesAsync(int Anio, int Mes);
        Task<DetalleReporteServicioMensualVista> ObtenerResumenReporteTerrestreRegionPorAnioAnMesAsync(int Anio, int Mes);
    }
}
