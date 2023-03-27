using Microsoft.EntityFrameworkCore;

namespace Domain.Entities.Vistas
{
    [Keyless]
    public class ProgramaItinerarioVista
    {
        public int id_ruta { get; set; }
        public string clave { get; set; }
        public string regionmilitarsdn { get; set; }
        public string regionssf { get; set; }
        public int zonamilitarsdn { get; set; }
        public string itinerarioruta { get; set; }
        public string fechas { get; set; }
        public string observacionesruta { get; set; }
        public DateTime? fecha { get; set; }
        public string itinerariorutapatrullaje { get; set; }
    }
}
