using Domain.Entities.Vistas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Ports.Driven
{
    public interface ILineasCommand
    {
        Task AgregarAsync(string clave, string descripcion, int idPuntoInicio, int idPuntoFin);
        Task ActualizaAsync(int idLinea, string clave, string descripcion, int idPuntoInicio, int idPuntoFin, int idUsuario, int bloqueado);
        Task BorraAsync(int idLinea);
    }

    public interface ILineasQuery
    {
        Task<List<LineaVista>> ObtenerLineaPorClaveAsync(string clave);
        Task<List<LineaVista>> ObtenerLineaPorUbicacionDePuntoInicioAsync(string ubicacion);
        Task<List<LineaVista>> ObtenerLineaPorUbicacionDePuntoFinalAsync(string ubicacion);
        Task<List<LineaVista>> ObtenerLineaDentroDeRecorridoDeRutaAsync(string recorrido);
        Task<List<LineaVista>> ObtenerLineasDentroDeRadio5KmDeUnPuntoAsync(int idPunto);
        Task<List<LineaVista>> ObtenerLineasDentroDeRadio5KmDeUnasCoordenadasAsync(string latitud, string longitud);
    }
}
