using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.Vistas
{
    [Keyless]
    public class IncidenciaEstructuraVista
    {
        public int id_reporte { get; set; }
        public int id_nota { get; set; }
        public int id_estructura { get; set; }
        public string incidencia { get; set; }
        public int estadoIncidencia { get; set; }
        public int prioridadIncidencia { get; set; }
        public int id_clasificacionIncidencia { get; set; }
        public DateTime? ultimaActualizacion { get; set; }
        public string? clave { get; set; }
        public string nombre { get; set; }
        public string coordenadas { get; set; }
        public int id_ProcesoResponsable { get; set; }
        public int id_GerenciaDivision { get; set; }
        public string descripcionEstado { get; set; } 
        public string descripcionnivel { get; set; }
        public string tiporeporte { get; set; }
    }
}
