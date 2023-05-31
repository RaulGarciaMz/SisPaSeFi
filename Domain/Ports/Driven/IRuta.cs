using Domain.DTOs;
using Domain.Entities;
using Domain.Entities.Vistas;

namespace Domain.Ports.Driven
{
    public interface IRutaCommand
    {
        Task AgregaTransacionalAsync(Ruta r, List<Itinerario> itin);
        Task UpdateAsync(RutaDtoForUpdate r);
        Task ReiniciaRegionMilitarAsync(string regionMilitar, string tipoPatrullaje);
        Task DeleteAsync(int id);
    }

    public interface IRutaQueries
    {
        Task<int> ObtenerNumeroRutasConMismaClaveAsync(string clave, int idRuta);
        Task<int> ObtenerNumeroItinerariosConfiguradosPorZonasRutaAsync(int tipoPatrullaje, string regionSsf, string regionMilitar, int zonaMilitar, string ruta);
        Task<int> ObtenerNumeroItinerariosConfiguradosEnOtraRutaAsync(int tipoPatrullaje, string regionSsf, string regionMilitar, int zonaMilitar, int ruta, string rutaItinerario);
        Task<int> ObtenerNumeroRutasPorTipoAndRegionMilitarAsync(int tipoPatrullaje, string regionMilitar);
        Task<string> ObtenerDescripcionTipoPatrullajeAsync(int tipoPatrullaje);
        Task<int> ObtenerNumeroProgramasPorRutaAsync(int idRuta);
        Task<int> ObtenerNumeroPropuestasPorRutaAsync(int idRuta);
        Task<Usuario?> ObtenerUsuarioConfiguradorAsync(string usuario);
        Task<List<RutaVista>> ObtenerRutasPorRegionSsfAsync(string tipo, int regionSsf);
        Task<List<RutaVista>> ObtenerRutasPorRegionMilitarAsync(string tipo, string regionMilitar);
        Task<List<RutaVista>> ObtenerRutasPorCombinacionFiltrosAsync(string tipo, string criterio);
        Task<List<RutaVista>> ObtenerRutasTodasPorTipoAndCriterioAsync(string tipo, string criterio);
        Task<List<RutaVista>> ObtenerRutasTodasPorRegionMilitarAsync(string tipo, string regionMilitar);
        Task<List<RutaVista>> ObtenerPropuestasPorRegionMilitarAndRegionSsfAsync(string tipo, string regionMilitar, int regionSsf);
        Task<List<RutaVista>> ObtenerPropuestasPorCombinacionFiltrosConRegionSsfAsync(string tipo, string criterio, int regionSsf);
        Task<List<RutaDisponibleVista>> ObtenerRutasDisponiblesParaCambioDeRutaAsync(string region, DateTime fecha);
    }
}
