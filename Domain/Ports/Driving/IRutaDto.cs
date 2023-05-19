using Domain.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Ports.Driving
{
    public interface IRutaDtoCommand
    {
        Task AgregaAsync(RutaDto pp, string usuario);
        Task UpdateAsync(RutaDto pp, string usuario);
        Task DeleteAsync(int id, string usuario);
    }

    public interface IRutaDtoQuery
    {
        Task<List<RutaDto>> ObtenerPorFiltroAsync(string usuario, int opcion, string tipo, string criterio, string actividad);
    }
}
