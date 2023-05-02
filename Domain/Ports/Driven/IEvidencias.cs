using Domain.Entities.Vistas;

namespace Domain.Ports.Driven
{
    public interface IEvidenciasCommand
    {
        Task AgregarEvidenciaDeEstructuraAsync(int idReporte, string rutaArchivo, string nombreArchivo, string descripcion);
        Task AgregarEvidenciaDeInstalacionAsync(int idReporte, string rutaArchivo, string nombreArchivo, string descripcion);
        Task AgregarEvidenciaSeguimientoDeInstalacionAsync(int idReporte, string rutaArchivo, string nombreArchivo, string descripcion);
        Task AgregarEvidenciaSeguimientoDeEstructuraAsync(int idReporte, string rutaArchivo, string nombreArchivo, string descripcion);
        Task BorrarEvidenciaDeEstructuraAsync(int idEvidencia);
        Task BorrarEvidenciaDeInstalacionAsync(int idEvidencia);
        Task BorrarEvidenciaSeguimientoDeInstalacionAsync(int idEvidencia);
        Task BorrarEvidenciaSeguimientoDeEstructuraAsync(int idEvidencia);
    }

    public interface IEvidenciasQuery
    {
        Task <List<EvidenciaVista>> ObtenerEvidenciaDeInstalacionAsync(int idReporte);
        Task <List<EvidenciaVista>> ObtenerEvidenciaDeEstructuraAsync(int idReporte);
        Task<List<EvidenciaVista>> ObtenerEvidenciaSeguimientoDeInstalacionAsync(int idReporte);
        Task<List<EvidenciaVista>> ObtenerEvidenciaSeguimientoDeEstructuraAsync(int idReporte);
        
    }
}
