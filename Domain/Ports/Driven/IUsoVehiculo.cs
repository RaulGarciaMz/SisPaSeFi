using Domain.Entities.Vistas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Ports.Driven
{
    public interface IUsoVehiculoCommand
    {
        Task AgregaAsync(int idPrograma, int idVehiculo, int idUsuarioVehiculo, int kmInicio, int kmFin, int consumo, string? edoVehiculo);
        Task ActualizaAsync(int idPrograma, int idVehiculo, int idUsuarioVehiculo, int kmInicio, int kmFin, int consumo, string? edoVehiculo);
        Task BorraAsync(int idPrograma, int idVehiculo);
    }

    public interface IUsoVehiculoQuery
    {
        Task<List<UsoVehiculoVista>> ObtenerUsoVehiculosPorProgramaAsync(int idPrograma);
    }
}
