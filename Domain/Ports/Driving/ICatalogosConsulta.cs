using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Ports.Driving
{
    public interface IComandanciasQuery
    {
        Task<List<ComandanciaRegional>> ObtenerComandanciaPorIdUsuarioAsync(int idUsuario);
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
}
