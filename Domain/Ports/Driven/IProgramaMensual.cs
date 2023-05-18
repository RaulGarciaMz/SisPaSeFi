using Domain.Entities.Vistas;

namespace Domain.Ports.Driven
{
    public interface IProgramaMensualQuery
    {
        Task<List<ResponsableRegionesVista>> ObtenerRegionesMilitaresDeProgramasPorAnioMesRegionTipoAsync(int anio, int mes, string region, string tipo);
        Task<List<ResponsableRegionesVista>> ObtenerRegionesMilitaresDePropuestasPorAnioMesRegionTipoAsync(int anio, int mes, string region, string tipo);
        Task<List<ProgramaItinerarioVista>> ObtenerMensualDeProgramasAsync(int anio, int mes, string region, string tipo);
        Task<List<ProgramaItinerarioVista>> ObtenerMensualDePropuestasAsync(int anio, int mes, string region, string tipo);
    }

}
