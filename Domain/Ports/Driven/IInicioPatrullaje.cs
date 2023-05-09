using Domain.DTOs;
using Domain.Entities;
using Domain.Entities.Vistas;

namespace Domain.Ports.Driven
{
    public interface IInicioPatrullajeCommand
    {
        void AgregaProgramaPatrullajeEnMemoria(int idRuta, DateTime fechaPatrullaje, int idUsuario, int idPuntoResponsable, int idRutaOriginal);
        void ActualizaProgramaPatrullajeEnMemoria(int idPrograma, TimeSpan inicio, int idUsuario, int riesgo);
        void AgregaTarjetaInformativaEnMemoria(int idPrograma, int idUsuario, TimeSpan inicio, int comandantesInstalacionSsf, int sedenaOficial, DateTime fechaPatrullaje, int tropaSdn, int linieros, int comandantesTurnos, int oficialSsf, DateTime fechaTermino);
        Task ActualizaTarjetaInformativaEnMemoria(int idPrograma, int idUsuario, TimeSpan inicio, int comandantesInstalacionSsf, int sedenaOficial, DateTime fechaPatrullaje, int tropaSdn, int linieros, int comandantesTurnos, int oficialSsf, DateTime fechaTermino);
        Task CreaOrActualizaUsosVehiculoEnMemoria(List<InicioPatrullajeVehiculoDto> usosVehiculo, int idPrograma, int idUsuario);
        Task SaveTransactionAsync();
    }

    public interface IInicioPatrullajeQuery
    {
        Task<List<InicioPatrullajeProgramaVista>> ObtenerProgramaPorRutaAndFechaAsync(int idRuta, string fecha);
        Task<List<InicioPatrullajePuntosVista>> ObtenerPuntosEnRutaDelItinerarioAsync(int idRuta);
        Task<List<TarjetaInformativa>> ObtenerTarjetaInformativaPorProgramaAsync(int idPrograma);
        Task<List<int>> ObtenerUsoVehiculoPorProgramaAndIdVehiculoAsync(int idPrograma, int idVehiculo);
    }
}
