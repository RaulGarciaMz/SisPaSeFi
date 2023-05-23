using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.Vistas
{
    public class RutaVista
    {
        [Key]
        public int id_ruta { get; set; }
        public string clave { get; set; }
        public string regionMilitarSDN { get; set; }
        public string regionSSF { get; set; }
        public int zonaMilitarSDN { get; set; }
        public string observaciones { get; set; }
        public int consecutivoRegionMilitarSDN { get; set; }
        public int totalRutasRegionMilitarSDN { get; set; }
        public int bloqueado { get; set; } 
        public int habilitado { get; set; }
        public string itinerarioruta { get; set; }
        public string itinerariorutapatrullaje { get; set; }
        public int id_tipopatrullaje { get; set; }
        
    }

    [Keyless]
    public class RutaDisponibleVista
    {
        public int id_ruta { get; set; }
        public string clave { get; set; }
        public string regionMilitarSDN { get; set; }
        public string regionSSF { get; set; }
        public int zonaMilitarSDN { get; set; }
        public string observaciones { get; set; }
        public string itinerario { get; set; }
    }
}
