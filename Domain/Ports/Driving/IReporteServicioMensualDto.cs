using Domain.DTOs;

namespace Domain.Ports.Driving
{
    public interface IReporteServicioMensualDtoCommand
    {
        Task<ReporteServicioMensualDto> ObtenerReporteDeServicioMensualPorOpcionAsync(int anio, int mes, string tipo, string usuario);
    }
}
