namespace Domain.Ports.Driven.Repositories
{
    public interface ICatalogosConsultaRepo: IComandanciasQuery, ITipoPatrullajeQuery, ITipoVehiculoQuery, IClasificacionIncidenciaQuery, 
        INivelesQuery, IConceptoAfectacionQuery, IRegionEnRutaQuery, IEstadoPaisQuery, IProcesoResponsableQuery, ITipoDocumentoQuery, 
        IMunicipioQuery, IGerenciaDivisionQuery, IResultadoPatrullajeQuery, IEstadoPatrullajeQuery, IApoyoPatrullajeQuery, IInstalacionesQuery,
        INivelRiesgoQuery, IHallazgoQuery, ILocalidadQuery
    {
    }
}