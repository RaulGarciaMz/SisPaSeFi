using Domain.Entities;
using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Ports.Driving
{
    public interface IPuntosPatrullaje
    {
        
        void Agrega(PuntoPatrullaje pp);
        void Update(PuntoPatrullaje pp);
        void Delete(int id);

        List<PuntoPatrullaje> ObtenerPorEstado(int id_estado);
        List<PuntoPatrullaje> ObtenerPorUbicacion(string ubicacion);
        int ObtenerItinerariosPorPunto(int id);
    }
}
