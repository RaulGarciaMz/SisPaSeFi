using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DTOs
{
    /// <summary>
    /// Evidencia de incidencia
    /// </summary>
    public class EvidenciaDto
    {
        /// <summary>
        /// Nombre del usuario (alias o usuario_nom) q
        /// </summary>
        public string Usuario { get; set; }
        /// <summary>
        /// Identificador del reporte
        /// </summary>
        public int IdReporte { get; set; }
        /// <summary>
        /// Ruta (path) del archivo de la evidencia
        /// </summary>
        public string RutaArchivo { get; set; }
        /// <summary>
        /// Nombre del archivo de evidencia
        /// </summary>
        public string NombreArchivo { get; set; }
        /// <summary>
        /// Descripción de la evidencia
        /// </summary>
        public string Descripcion { get; set; }
        /// <summary>
        /// Descripción del tipo de incidencia a la que se refiere la evidencia
        /// </summary>
        public string TipoIncidencia { get; set; }
    }
}
