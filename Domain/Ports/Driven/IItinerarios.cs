using Domain.Entities;
using Domain.Entities.Vistas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Ports.Driven
{
    public interface IItinerariosCommand
    {
        Task AgregaItinerarioAsync(int idRuta, int idPunto, int posicion);
        Task ActualizaItinerarioAsync(int idItinerario, int idPunto, int posicion);
        Task BorraItinerarioAsync(int idItinerario);
    }

    public interface IItinerariosQuery
    {
        Task<List<ItinerarioVista>> ObtenerItinerariosporRutaAsync(int idRuta);
        Task<List<Itinerario>> ObtenerItinerariosporRutaAndPosicionAsync(int idRuta, int posicion);
        Task<List<Itinerario>> ObtenerItinerariosporRutaAndPosicionAndPuntoAsync(int idRuta, int posicion, int idPunto);
    }
}
