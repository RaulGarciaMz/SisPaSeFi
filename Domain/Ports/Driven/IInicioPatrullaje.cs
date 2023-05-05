using Domain.Entities;
using Domain.Entities.Vistas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Ports.Driven
{
    public interface IInicioPatrullajeCommand
    {
        Task AgregaProgramaPatrullajeAsync(int idRuta, DateTime fechaPatrullaje, int idUsuario, int idPuntoResponsable, int idRutaOriginal);
        Task ActualizaProgramaPatrullajeAsync(int idPrograma, TimeSpan inicio, int idUsuario, int riesgo);
        Task AgregaTarjetaInformativaAsync(int idPrograma, int idUsuario, TimeSpan inicio, int comandantesInstalacionSsf, int sedenaOficial, DateTime fechaPatrullaje, int tropaSdn, int linieros, int comandantesTurnos, int oficialSsf, DateTime fechaTermino);
        Task ActualizaTarjetaInformativaAsync(int idPrograma, int idUsuario, TimeSpan inicio, int comandantesInstalacionSsf, int sedenaOficial, DateTime fechaPatrullaje, int tropaSdn, int linieros, int comandantesTurnos, int oficialSsf, DateTime fechaTermino);
        Task AgregaUsoVehiculoAsync(int idPrograma, int idVehiculo, int kminicio, int idUsuarioVehiculo, string estadoVehiculo);
        Task ActualizaUsoVehiculoAsync(int idPrograma, int idVehiculo, int kminicio, int idUsuarioVehiculo, string estadoVehiculo);
    }

    public interface IInicioPatrullajeQuery
    {
        Task<List<InicioPatrullajeProgramaVista>> ObtenerProgramaPorRutaAndFechaAsync(int idRuta, string fecha);
        Task<List<InicioPatrullajePuntosVista>> ObtenerPuntosEnRutaDelItinerarioAsync(int idRuta);
        Task<List<TarjetaInformativa>> ObtenerTarjetaInformativaPorProgramaAsync(int idPrograma);
        Task<List<int>> ObtenerUsoVehiculoPorProgramaAndIdVehiculoAsync(int idPrograma, int idVehiculo);
    }
}
