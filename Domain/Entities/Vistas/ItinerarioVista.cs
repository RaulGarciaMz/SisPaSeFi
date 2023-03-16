using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
}
