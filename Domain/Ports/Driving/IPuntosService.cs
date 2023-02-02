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
        void Agrega(PuntoDto pp);
        void Update(PuntoDto pp);
        void Delete(int id);
        List<PuntoDto> ObtenerPorOpcion(FiltroPunto opcion, string valor);
    }
}
