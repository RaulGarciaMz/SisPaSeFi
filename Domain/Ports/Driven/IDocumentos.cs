using Domain.Entities.Vistas;

namespace Domain.Ports.Driven
{
    public interface IDocumentosCommand
    {
        Task AgregarAsync(long idReferencia, long idTipoDocumento, int idComandancia, string rutaArchivo, string nombreArchivo, string descripcionArchivo, DateTime fechaReferencia, int idUsuario);
    }

    public interface IDocumentosQuery
    {

        Task<List<DocumentosVista>> ObtenerDocumentosPatrullajeAsync(int idComandancia, int anio, int mes);
        Task<List<DocumentosVista>> ObtenerDocumentosDeUnUsuarioTodosAsync(int idUsuario);
        Task<List<DocumentosVista>> ObtenerDocumentosDeUnUsuarioMesAsync(int idUsuario, int anio, int mes);
        Task<List<DocumentosVista>> ObtenerDocumentosParaUnUsuarioTodosAsync(int idUsuario);
        Task<List<DocumentosVista>> ObtenerDocumentosParaUnUsuarioMesAsync(int idUsuario, int anio, int mes);
    }
}
