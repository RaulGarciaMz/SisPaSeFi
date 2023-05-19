using Domain.DTOs;

namespace Domain.Ports.Driving
{
    public interface IMonitoreoDtoQuery
    {
        Task<MonitoreoMovilDto> ObtenerMonitoreoMovilAsync(string usuario);
    }
}
