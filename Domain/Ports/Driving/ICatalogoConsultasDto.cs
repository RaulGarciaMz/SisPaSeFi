using Domain.DTOs.catalogos;

namespace Domain.Ports.Driving
{
    public interface ICatalogoConsultasDto
    {
        Task<List<CatalogoGenerico>> ObtenerComandanciaPorIdUsuarioAsync(string usuario);
        Task<List<CatalogoGenerico>> ObtenerTiposPatrullajeAsync(string usuario);
        Task<List<CatalogoGenerico>> ObtenerTiposVehiculoAsync(string usuario);
        Task<List<CatalogoGenerico>> ObtenerClasificacionesIncidenciaAsync(string usuario);
        Task<List<CatalogoGenerico>> ObtenerNivelesAsync(string usuario);
        Task<List<CatalogoGenerico>> ObtenerConceptosAfectacionAsync(string usuario);
        Task<List<CatalogoGenerico>> ObtenerRegionesMilitaresEnRutasConDescVaciaAsync(string usuario);
        Task<List<CatalogoGenerico>> ObtenerResultadosPatrullajeAsync(string usuario);
        Task<List<CatalogoGenerico>> ObtenerEstadosPaisAsync(string usuario);
        Task<List<CatalogoGenerico>> ObtenerMunicipiosPorEstadoAsync(int idEstado, string usuario);
        Task<List<CatalogoGenerico>> ObtenerProcesosResponsablesAsync(string usuario);
        Task<List<CatalogoGenerico>> ObtenerGerenciaDivisionAsync(int idProceso, string usuario);
        Task<List<CatalogoGenerico>> ObtenerTiposDocumentosAsync(string usuario);        
        Task<List<CatalogoGenerico>> ObtenerCatalogoPorOpcionAsync(string opcion, string usuario);
        Task<List<CatalogoGenerico>> ObtenerHallazgosAsync(string usuario);
        Task<List<CatalogoGenerico>> ObtenerEstadosIncidenciaAsync(string usuario);
        Task<List<CatalogoGenerico>> ObtenerComandanciasDeUnUsuarioAsync(int idUsuario, string usuario);
        Task<List<CatalogoGenerico>> ObtenerGruposCorreoDeUnUsuarioAsync(int idUsuario, string usuario);
        Task<List<CatalogoGenerico>> ObtenerRolesDeUnUsuarioAsync(int idUsuario, string usuario);
        Task<List<CatalogoGenerico>> ObtenerGruposCorreoAsync(string usuario);
    }
}
