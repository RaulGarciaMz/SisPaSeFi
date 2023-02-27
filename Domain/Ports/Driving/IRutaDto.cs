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
        Task Agrega(RutaDto pp, string usuario);
        Task Update(RutaDto pp, string usuario);
        Task Delete(int id, string usuario);
    }

    public interface IRutaDtoQuery
    {
        Task<List<RutaDto>> ObtenerPorFiltro(string usuario, int opcion, string tipo, string criterio, string actividad);
    }
}
