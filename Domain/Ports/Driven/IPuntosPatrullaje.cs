using Domain.DTOs;
using Domain.Entities;
using Domain.Entities.Vistas;

namespace Domain.Ports.Driven
{
    public interface IPuntosPatrullaje
    {
        Task Agrega(PuntoPatrullaje pp);
        Task Update(PuntoDtoForUpdate pp);
        Task Delete(int id);

        Task<List<PuntoPatrullajeVista>> ObtenerPorEstadoAsync(int id_estado);
        Task<List<PuntoPatrullajeVista>> ObtenerPorUbicacionAsync(string ubicacion);
        Task<List<PuntoPatrullajeVista>> ObtenerPorRutaAsync(int ruta);
        Task<List<PuntoPatrullajeVista>> ObtenerPorRegionAsync(int region, int nivel);
        Task<int> ObtenerItinerariosPorPuntoAsync(int id);
        Task<int> ObtenerIdUsuarioConfiguradorAsync(string usuario_nom);
    }
}
