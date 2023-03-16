using Domain.DTOs;
using Domain.Entities.Vistas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Ports.Driving
{
    public interface IItinerarioDtoCommand
    {
        Task AgregaItinerarioAsync(ItinerarioDtoForCreate it);
        Task ActualizaItinerarioAsync(ItinerarioDtoForCreate it);
        Task BorraItinerarioAsync(int id, string usuario);
    }

    public interface IItinerarioDtoQuery
    {
        Task<List<ItinerarioVista>> ObtenerItinerariosporRutaAsync(int idRuta, string usuario);
    }
}
