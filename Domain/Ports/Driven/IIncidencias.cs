using Domain.Entities;
using Domain.Entities.Vistas;

namespace Domain.Ports.Driven
{
    public interface IIncidenciasCommand
    {
        Task<ReportePunto> AgregaReporteInstalacionAsync(int idPunto, int idNota, string incidencia, int edoIncidencia, int prioridad, int clasificacion);
        Task<ReporteEstructura> AgregaReporteEstructuraAsync(int idEstructura, int idNota, string incidencia, int edoIncidencia, int prioridad, int clasificacion);
        Task ActualizaReporteEnInstalacionPorIncidenciaExistenteAsync(int idReporte, string incidencia, int prioridad);
        Task ActualizaReporteEnInstalacionAsync(int idReporte, string incidencia, int prioridad, int clasificacion, int estado);
        Task ActualizaReporteEnEstructuraPorIncidenciaExistenteAsync(int idReporte, string incidencia, int prioridad);
        Task ActualizaReporteEnEstructuraAsync(int idReporte, string incidencia, int prioridad, int clasificacion, int estado);
        Task AgregaTarjetaInformativaReporteAsync(int idTarjeta, int idReporte, string tipoIncidencia);
    }

    public interface IIncidenciasQuery
    {
        Task<List<IncidenciaGeneralVista>> ObtenerIncidenciasAbiertasEnInstalacionAsync(int idActivo);
        Task<List<IncidenciaGeneralVista>> ObtenerIncidenciasAbiertasEnEstructuraAsync(int idEstructura);
        Task<List<IncidenciaGeneralVista>> ObtenerIncidenciasNoAtendidasPorDiasEnEstructurasAsync(int numeroDias);
        Task<List<IncidenciaGeneralVista>> ObtenerIncidenciasReportadasEnProgramaEnInstalacionAsync(int programa);
        Task<List<IncidenciaGeneralVista>> ObtenerIncidenciasInstalacionPorUbicacionOrIncidenciaAsync(string criterio);
        Task<List<IncidenciaGeneralVista>> ObtenerIncidenciasEstructuraPorUbicacionOrIncidenciaAsync(string criterio);
        Task<List<IncidenciaGeneralVista>> ObtenerIncidenciasNoConcluidasInstalacionAsync(string criterio);
        Task<List<IncidenciaGeneralVista>> ObtenerIncidenciasNoConcluidasEstructuraAsync(string criterio);
        Task<List<IncidenciaGeneralVista>> ObtenerIncidenciasInstalacionEstadoEspecificoAsync(string criterio, int complemento);
        Task<List<IncidenciaGeneralVista>> ObtenerIncidenciasEstructuraEstadoEspecificoAsync(string criterio, int complemento);
        
        Task<List<IncidenciaGeneralVista>> ObtenerIncidenciasReportadasEnProgramaEnEstructuraAsync(int programa);
        Task<List<ReporteIncidenciaAbierto>> ObtenerReportesAbiertosPorInstalacionAsync(int idActivo, int claseIncidencia);
        Task<List<ReporteIncidenciaAbierto>> ObtenerReportesAbiertosPorEstructuraAsync(int idEstructura, int claseIncidencia);
    }
}
