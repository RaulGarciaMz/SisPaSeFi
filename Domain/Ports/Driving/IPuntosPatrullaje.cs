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
        IEnumerable<PuntoPatrullaje> ObtenerPorOpcion(FiltroPunto opcion, string valor);
        void Agrega(PuntoPatrullaje pp);
        void Update(PuntoPatrullaje pp);
        void Delete(int id);
        /*
        IEnumerable<puntospatrullaje> Obtener();        
        IEnumerable<puntospatrullaje> ObtenerPorEstado(int id_estado);
        IEnumerable<puntospatrullaje> ObtenerPorUbicacion(string ubicacion);
        */
    }
}
