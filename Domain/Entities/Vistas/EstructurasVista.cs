using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.Vistas
{
    [Keyless]
    public class EstructurasVista
    {
        public int id_estructura { get; set; }
        public string nombre { get; set; } = null!;
        public string coordenadas { get; set; } = null!;
        public string? latitud { get; set; }
        public string? longitud { get; set; }
        public int id_municipio { get; set; }
        public int? id_linea { get; set; }
        public int id_ProcesoResponsable { get; set; }
        public int id_GerenciaDivision { get; set; }
        public string? clave { get; set; }
        public string? descripcion { get; set; }
        public string municipio { get; set; } = null!;
        public int id_estado { get; set; }
        public string estado { get; set; } = null!;
    }
}
