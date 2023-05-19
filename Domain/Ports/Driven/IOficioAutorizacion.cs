using Domain.Entities.Vistas;

namespace Domain.Ports.Driven
{
    public interface IOficioAutorizacionQuery
    {
        Task<List<PropuestaConResponsableVista>> ObtenerPropuestaConResponsableAsync(int idPropuesta);
        Task<List<LineaEnPropuestaVista>> ObtenerLineasDePropuestaAsync(int idPropuesta);
        Task<List<VehiculoEnPropuestaVista>> ObtenerVehiculosEnPropuestaAsync(int idPropuesta);
    }
}
