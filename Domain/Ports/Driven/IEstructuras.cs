using Domain.Entities;
using Domain.Entities.Vistas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Ports.Driven
{
    public interface IEstructurasCommand
    {
        Task ActualizaUbicacionAsync(int idEstructura, string nombre, int idMunicipio, string latitud, string longitud);
        Task AgregaAsync(int idLinea, string nombre, int idMunicipio, string latitud, string longitud);
    }

    public interface IEstructurasQuery
    {
        Task<List<EstructurasVista>> ObtenerEstructuraPorLineaAsync(int idLinea);
        Task<List<EstructurasVista>> ObtenerEstructuraPorLineaEnRutaAsync(int idLinea, int idRuta);
    }
}

