using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.Vistas
{
    [Keyless]
    public class EvidenciaVista
    {
        public int IdEvidencia { get; set; }
        public int IdReporte { get; set; }
        public string RutaArchivo { get; set; }
        public string NombreArchivo { get; set; }
        public string? Descripcion { get; set; }
        public string TipoReporte { get; set; }
    }
}
