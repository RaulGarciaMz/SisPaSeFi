using Domain.DTOs;

namespace Domain.Ports.Driving
{
    public interface IBitacoraSeguimientoDtoCommand
    {
        Task AgregaBitacoraPorOpcionAsync(string opcion, int idReporte, int idEstado, string descripcion, string usuario);
    }

    public interface IBitacoraSeguimientoDtoQuery
    {
        Task<List<BitacoraDto>> ObtenerBitacoraPorOpcionAsync(string opcion, int idReporte, string usuario);
    }

}
