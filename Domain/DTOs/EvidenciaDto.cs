using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DTOs
{
    public class EvidenciaDto
    {
        public string Usuario { get; set; }
        public int IdReporte { get; set; }
        public string RutaArchivo { get; set; }
        public string NombreArchivo { get; set; }
        public string Descripcion { get; set; }
        public string TipoIncidencia { get; set; }
    }
}
