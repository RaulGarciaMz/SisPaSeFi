using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.Vistas
{
    [Keyless]
    public class ReporteIncidenciaAbierto
    {
        public int id_reporte { get; set; }
        public string incidencia { get; set; }
    }
}
