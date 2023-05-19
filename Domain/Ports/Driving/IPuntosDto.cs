using Domain.DTOs;
using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Ports.Driving
{
    public interface IPuntosDtoCommand
    {
        Task AgregaAsync(PuntoDto pp, string usuario);
        Task UpdateAsync(PuntoDto pp, string usuario);
        Task DeleteAsync(int id, string usuario);
    }

    public interface IPuntosDtoQuery
    {
        Task<List<PuntoDto>> ObtenerPorOpcionAsync(FiltroPunto opcion, string valor, string usuario);
    }
}
