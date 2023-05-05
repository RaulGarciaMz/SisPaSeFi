using Domain.Entities;
using Domain.Entities.Vistas;

namespace Domain.Ports.Driven
{
    public interface IComandanciasQuery
    {
        Task<List<ComandanciaRegional>> ObtenerComandanciaPorIdUsuarioAsync(int idUsuario);
        Task<List<ComandanciaRegional>> ObtenerComandanciasAsync();
    }
    public interface ITipoPatrullajeQuery
    {
        Task<List<TipoPatrullaje>> ObtenerTiposPatrullajeAsync();
    }
    public interface ITipoVehiculoQuery
    {
        Task<List<TipoVehiculo>> ObtenerTiposVehiculoAsync();
    }
    public interface IClasificacionIncidenciaQuery
    {
        Task<List<ClasificacionIncidencia>> ObtenerClasificacionesIncidenciaAsync();
    }
    public interface INivelesQuery
    {
        Task<List<Nivel>> ObtenerNivelesAsync();
    }
    public interface IConceptoAfectacionQuery
    {
        Task<List<ConceptoAfectacion>> ObtenerConceptosAfectacionAsync();
    }

    public interface IRegionEnRutaQuery
    {
        Task<List<int>> ObtenerRegionesMilitaresEnRutanAsync();
    }

    public interface IEstadoPaisQuery
    {
        Task<List<EstadoPais>> ObtenerEstadosPaisAsync();
    }

    public interface IProcesoResponsableQuery
    {
        Task<List<ProcesoResponsable>> ObtenerProcesosResponsablesAsync();
        Task<ProcesoResponsable?> ObtenerProcesosResponsablePorIdAsync(int id);
    }

    public interface ITipoDocumentoQuery
    {
        Task<List<TipoDocumento>> ObtenerTiposDocumentosAsync();
    }

    public interface IMunicipioQuery
    {
        Task<List<Municipio>> ObtenerMunicipiosPorEstadoAsync(int idEstado);
    }

    public interface IGerenciaDivisionQuery
    {
        Task<List<CatalogoVista>> ObtenerCatalogoPorNombreTablaAync(string nombre);
    }

    public interface IResultadoPatrullajeQuery
    {
        Task<List<ResultadoPatrullaje>> ObtenerResultadosPatrullajeAsync();
    }

    public interface IEstadoPatrullajeQuery
    {
        Task<List<EstadoPatrullaje>> ObtenerEstadosPatrullajeAsync();
    }

    public interface IApoyoPatrullajeQuery
    {
        Task<List<ApoyoPatrullaje>> ObtenerApoyosPatrullajeAsync();
    }

    public interface IInstalacionesQuery
    {
        Task<List<PuntoPatrullaje>> ObtenerInstalacionesDeComandanciaAsync(int idComandancia);
    }

    public interface INivelRiesgoQuery
    {
        Task<List<NivelRiesgo>> ObtenerNivelDeRiesgoAsync();
    }

    public interface IHallazgoQuery
    {
        Task<List<CatalogoHallazgo>> ObtenerHallazgosAsync();
    }

    public interface ILocalidadQuery
    {
        Task<List<Localidad>> ObtenerLocalidadesPorMunicipioAsync(int idMunicipio);
    }
}
