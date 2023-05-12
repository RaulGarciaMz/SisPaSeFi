using Domain.DTOs;
using Domain.Entities;
using Domain.Entities.Vistas;

namespace Domain.Ports.Driven
{
    public interface IRegistroIncidenteCommand
    {
        void AgregaReporteEstructurasEnMemoriaAsync(int idNota, int idEstructura, string descripcion, int prioridad, int idClasificacion);
        void ActualizaReporteEstructurasEnMemoriaAsync(int idEstructura, int idClasificacion, int prioridad, string descripcion);
        void ActualizaReporteInstalacionEnMemoriaAsync(int idActivo, int idClasificacion, int prioridad, string descripcion);
        void AgregaReporteInstalacionEnMemoriaAsync(int idNota, int idActivo, string descripcion, int prioridad, int idClasificacion);
        void AgregaEvidenciaIncidenciaEnMemoriaAsync(int idReporte, string ruta, string nombreArchivo, string descripcion);
        void AgregaListaDeAfectacionesIncidenciaEnMemoriaAsync(int idReporte, string tipoIncidencia, List<AfectacionIncidenciaMovilDto> afectaciones);
        Task SaveTransactionAsync();
    }

    public interface IRegistroIncidenteQuery
    {
        Task<ProgramaRegionVista> ObtenerProgramaAndRegionAsync(int idRuta, DateTime fecha);
        Task<int> ObtenerIdTarjetaInformativaPorProgramaAsync(int idPrograma);
        Task<int> ObtenerNumeroDeReportesEstructurasConcluidosPorIdAndClasificacionAsync(int idEstructura, int idClasificacion);
        Task<ReportePunto> ObtenerReporteInstalacionPorPuntoAndClasificacionAsync(int idPunto, int idClasificacion);
    }
}
