using Microsoft.EntityFrameworkCore;

namespace Domain.Entities.Vistas
{
    [Keyless]
    public class ItinerarioVista
    {
        public int id_itinerario { get; set; }
        public int id_ruta { get; set; }
        public int id_punto { get; set; }
        public int posicion { get; set; }
        public string ubicacion { get; set; }
        public string coordenadas { get; set; }
        public int id_procesoresponsable { get; set; }
        public int id_gerenciadivision { get; set; }
    }

    [Keyless]
    public class ItinerarioRutaVista
    {
        public string itinerarioruta { get; set; }
    }
}
