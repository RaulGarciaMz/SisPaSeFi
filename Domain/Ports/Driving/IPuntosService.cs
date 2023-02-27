using Domain.DTOs;
using Domain.Entities;
using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Ports.Driving
{
    public interface IPuntosService
    {
        Task Agrega(PuntoDto pp, string usuario);
        Task Update(PuntoDto pp,string usuario);
        Task Delete(int id, string usuario);
        Task<List<PuntoDto>> ObtenerPorOpcionAsync(FiltroPunto opcion, string valor, string usuario);
    }
}
