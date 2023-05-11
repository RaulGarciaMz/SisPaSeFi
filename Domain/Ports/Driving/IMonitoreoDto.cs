using Domain.DTOs;

namespace Domain.Ports.Driving
{
    public interface IMonitoreoDtoQuery
    {
        Task<MonitoreoMovilDto> ObtenerMonitoreoMovil(string usuario);
    }
}
