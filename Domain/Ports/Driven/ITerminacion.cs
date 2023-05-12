using Domain.DTOs;
using Domain.Entities;

namespace Domain.Ports.Driven
{
    public interface ITerminacionCommand
    {
        Task ActualizaProgramaEnMemoriaAsync(int idPrograma, int idUsuario, TimeSpan termino);
        Task ActualizaTarjetaInformativaEnMemoriaAsync(int idPrograma, int idUsuario, TerminacionPatrullajeDto t);
        Task ActualizaOrAgregaUsosVehiculoEnMemoriaAsync(int idPrograma, int idUsuario, TerminacionPatrullajeDto t);
        Task SaveTransactionAsync();
    }

    public interface ITerminacionQuery
    {
        Task<int> ObtenerIdProgramaPorRutaAndFechaAsync(int idRuta, DateTime fecha);
        Task<IEnumerable<UsoVehiculo>> ObtenerUsoVehiculoPorProgramaAndIdVehiculoAsync(int idPrograma, int idVehiculo);
        
    }
}
