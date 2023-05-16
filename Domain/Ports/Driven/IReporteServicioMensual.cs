using Domain.Entities.Vistas;

namespace Domain.Ports.Driven
{
    public interface IReporteServicioMensualQuery
    {
        Task<List<ReporteServicioMensualVista>> ObtenerReporteAereoPorOpcionAsync(int anio, int mes, string tipo);
        Task<DetalleReporteServicioMensualVista> ObtenerResumenParaReporteAereoPorOpcionAsync(int anio, int mes, string tipo);
        Task<List<ReporteServicioMensualVista>> ObtenerReporteTerrestreSedenaPorAnioAnMesAsync(int anio, int mes);
        Task<DetalleReporteServicioMensualVista> ObtenerResumenParaReporteTerrestreSedenaPorAnioAnMesAsync(int anio, int mes);
        Task<List<ReporteServicioMensualVista>> ObtenerReporteTerrestreRegionPorAnioAnMesAsync(int anio, int mes);
        Task<DetalleReporteServicioMensualVista> ObtenerResumenParaReporteTerrestreRegionPorAnioAnMesAsync(int anio, int mes);
    }
}
