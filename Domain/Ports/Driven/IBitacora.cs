using Domain.Entities.Vistas;

namespace Domain.Ports.Driven
{
    public interface IBitacoraCommand
    {
        Task AgregaBitacoraEstructuraAsync(int idReporte, int idEstado, int idUsuario, string descripcion);
        Task AgregaBitacoraInstalacionAsync(int idReporte, int idEstado, int idUsuario, string descripcion);
    }

    public interface IBitacoraQuery
    {
        Task<List<BitacoraSeguimientoVista>> ObtenerBitacoraInstalacionPorReporteAsync(int idReporte);
        Task<List<BitacoraSeguimientoVista>> ObtenerBitacoraEstructuraPorReporteAsync(int idReporte);
    }
}
