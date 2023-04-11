using Domain.Entities;
using Domain.Entities.Vistas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        Task<List<IncidenciaInstalacionVista>> ObtenerIncidenciasAbiertasEnInstalacionAsync(int idActivo);
        Task<List<IncidenciaEstructuraVista>> ObtenerIncidenciasAbiertasEnEstructuraAsync(int idEstructura);
        Task<List<IncidenciaEstructuraVista>> ObtenerIncidenciasNoAtendidasPorDiasEnEstructurasAsync(int numeroDias);
        Task<List<IncidenciaEstructuraVista>> ObtenerIncidenciasReportadasEnProgramaEnEstructuraAsync(int programa);
        Task<List<IncidenciaInstalacionVista>> ObtenerIncidenciasReportadasEnProgramaEnInstalacionAsync(int programa);
        Task<List<ReporteIncidenciaAbierto>> ObtenerReportesAbiertosPorInstalacionAsync(int idActivo, int claseIncidencia);
        Task<List<ReporteIncidenciaAbierto>> ObtenerReportesAbiertosPorEstructuraAsync(int idEstructura, int claseIncidencia);
 
    }
}
