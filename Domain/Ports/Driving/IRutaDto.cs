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
        void Agrega(RutaDto pp);
        void Update(RutaDto pp);
        void Delete(int id);
    }

    public interface IRutaDtoQuery
    {
        List<RutaDto> ObtenerPorFiltro(string usuario, int opcion, string tipo, string criterio, string actividad);
    }
}
