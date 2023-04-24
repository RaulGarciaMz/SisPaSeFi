using Domain.DTOs;
using Domain.Entities.Vistas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Ports.Driving
{
    public interface IUsoVehiculoDtoCommand
    {
        Task AgregaAsync(UsoVehiculoDtoForCreateOrUpdate uv);
        Task ActualizaAsync(UsoVehiculoDtoForCreateOrUpdate uv);
        Task BorraAsync(int idPrograma, int idVehiculo, string usuario);
    }

    public interface IUsoVehiculoDtoQuery
    {
        Task<List<UsoVehiculoVista>> ObtenerUsoVehiculosPorProgramaAsync(int idPrograma, string usuario);
    }
}
