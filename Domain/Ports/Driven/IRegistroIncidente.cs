using Domain.DTOs;

namespace Domain.Ports.Driven
{
    public interface IRegistroIncidenteCommand
    {
/*        void AgregaReporteEstructurasEnMemoria(int idNota, int idEstructura, string descripcion, int prioridad, int idClasificacion);
        Task ActualizaReporteEstructurasEnMemoriaAsync(int idEstructura, int idClasificacion, int prioridad, string descripcion);
        Task ActualizaReporteInstalacionEnMemoriaAsync(int idActivo, int idClasificacion, int prioridad, string descripcion);
        void AgregaReporteInstalacionEnMemoria(int idNota, int idActivo, string descripcion, int prioridad, int idClasificacion);
        void AgregaEvidenciaIncidenciaEnMemoriaAsync(int idReporte, string ruta, string nombreArchivo, string descripcion);
        void AgregaListaDeAfectacionesIncidenciaEnMemoriaAsync(int idReporte, string tipoIncidencia, List<AfectacionIncidenciaMovilDto> afectaciones);
        Task SaveTransactionAsync();*/
    }

    public interface IRegistroIncidenteQuery
    {
        Task AgregaIncidenciaTransaccionalAsync(RegistrarIncidenciaDto ri ,int idUsuario);
/*        Task<ProgramaRegionVista?> ObtenerProgramaAndRegionPorRutaAndFechaAsync(int idRuta, DateTime fecha);
        Task<int> ObtenerIdTarjetaInformativaPorProgramaAsync(int idPrograma);
        //Task<int> ObtenerNumeroDeReportesEstructurasConcluidosPorIdAndClasificacionAsync(int idEstructura, int idClasificacion);
        Task<List<ReportePunto>> ObtenerReporteInstalacionPorPuntoAndClasificacionAsync(int idPunto, int idClasificacion);
        Task<List<ReporteEstructura>> ObtenerReporteEstructuraPorEstructuraAndClasificacionAsync(int idEstructura, int idClasificacion);*/
    }
}
