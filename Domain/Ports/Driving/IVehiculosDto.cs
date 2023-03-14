using Domain.DTOs;
using Domain.Entities;
using Domain.Entities.Vistas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Ports.Driving
{
    public interface IVehiculosDtoCommand
    {
        Task AgregaAsync(VehiculoDtoForCreate vehiculo);
        Task ActualizaAsync(VehiculoDtoForUpdate vehiculo);
    }

    public interface IVehiculosDtoQuery
    {
        Task<List<VehiculoPatrullajeVista>> ObtenerVehiculosPorOpcionAsync(string opcion, int region, string criterio, string usuario);        
    }
}
