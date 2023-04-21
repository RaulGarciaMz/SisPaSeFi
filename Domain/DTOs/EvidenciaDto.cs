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
        public string strUsuario { get; set; }
        /// <summary>
        /// Identificador del reporte
        /// </summary>
        public int intIdReporte { get; set; }
        /// <summary>
        /// Ruta (path) del archivo de la evidencia
        /// </summary>
        public string strRutaArchivo { get; set; }
        /// <summary>
        /// Nombre del archivo de evidencia
        /// </summary>
        public string strNombreArchivo { get; set; }
        /// <summary>
        /// Descripción de la evidencia
        /// </summary>
        public string strDescripcion { get; set; }
        /// <summary>
        /// Descripción del tipo de incidencia a la que se refiere la evidencia
        /// </summary>
        public string strTipoIncidencia { get; set; }
    }
}
