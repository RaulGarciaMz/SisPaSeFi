using Domain.DTOs;
using Domain.Entities.Vistas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Ports.Driving
{
    public interface IAfectacionesDtoCommand
    {
        Task AgregaAsync(AfectacionDtoForCreate a);
        Task ActualizaAsync(AfectacionDtoForUpdate a);
    }

    public interface IAfectacionesDtoQuery
    {
        Task<List<AfectacionIncidenciaVista>> ObtenerAfectacionIncidenciaPorOpcionAsync(int idReporte, string tipo, string usuario);
    }
}
