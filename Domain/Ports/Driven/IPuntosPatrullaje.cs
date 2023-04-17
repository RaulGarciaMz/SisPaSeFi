using Domain.Entities;

namespace Domain.Ports.Driven
{
    public interface IPuntosPatrullaje
    {
        Task Agrega(PuntoPatrullaje pp);
        Task Update(PuntoPatrullaje pp);
        Task Delete(int id);

        Task<List<PuntoPatrullaje>> ObtenerPorEstadoAsync(int id_estado);
        Task<List<PuntoPatrullaje>> ObtenerPorUbicacionAsync(string ubicacion);
        Task<List<PuntoPatrullaje>> ObtenerPorRutaAsync(int ruta);
        Task<List<PuntoPatrullaje>> ObtenerPorRegionAsync(int region, int nivel);
        Task<int> ObtenerItinerariosPorPuntoAsync(int id);
        Task<int> ObtenerIdUsuarioConfiguradorAsync(string usuario_nom);
    }
}
