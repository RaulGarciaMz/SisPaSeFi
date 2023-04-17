using Domain.Entities.Vistas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Ports.Driving
{
    public interface IEstadisticasDtoQuery
    {
        Task<List<EstadisticaSistemaVista>> ObtenerEstadisticasDeSistemaAsync(string usuario);
    }
}
