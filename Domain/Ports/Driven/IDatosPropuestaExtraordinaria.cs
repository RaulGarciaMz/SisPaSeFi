using Domain.Entities.Vistas;

namespace Domain.Ports.Driven
{
    public interface IDatosPropuestaExtraordinariaQuery
    {
        Task<List<UbicacionPropuestaExtraVista>> ObtenerUbicacionLineasPorIdPropuestaAsync(int idPropuesta);
        Task<List<VehiculoPropuestaExtraVista>> ObtenerVehiculosPorIdPropuestaAsync(int idPropuesta);
        Task<List<UsuarioPropuestaExtraVista>> ObtenerResponsablesPorIdPropuestaAsync(int idPropuesta);
    }
}
