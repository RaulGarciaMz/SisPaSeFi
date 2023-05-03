namespace Domain.DTOs
{
     public class IncidenciaGeneralDto
    {
        /// <summary>
        /// Identificador del reporte de incidencia
        /// </summary>
        public int intIdReporte { get; set; }
        /// <summary>
        /// Identificador de la nota informativa
        /// </summary>
        public int intIdTarjeta { get; set; }
        /// <summary>
        /// Descripción de línea
        /// </summary>
        public string strLinea { get; set; }
        /// <summary>
        /// descripción de estructura
        /// </summary>
        public string strEstructura { get; set; }
        /// <summary>
        /// Coordenadas del activo
        /// </summary>
        public string strCoordenadas { get; set; }
        /// <summary>
        /// Identificador del proceso responsable del activo
        /// </summary>
        public int intIdProcesoResponsable { get; set; }
        /// <summary>
        /// Identigficador de la gerencia división a donde pertenece el activo
        /// </summary>
        public int intIdGerenciaDivision { get; set; }
        /// <summary>
        /// Descripción de la incidencia
        /// </summary>
        public string strDescripcionIncidencia { get; set; }
        /// <summary>
        /// Estado de la incidencia
        /// </summary>
        public int intIdEstadoIncidencia { get; set; }
        /// <summary>
        /// descripción del estado de la incidencia
        /// </summary>
        public string strEstadoIncidencia { get; set; }
        /// <summary>
        /// Fecha de última actualización de la incidencia
        /// </summary>
        public string strUltimaActualizacion { get; set; }
        /// <summary>
        /// Prioridad de la incidencia
        /// </summary>
        public int intIdPrioridadIncidencia { get; set; }
        /// <summary>
        /// Identificador de la clasificación de la incidencia
        /// </summary>
        public int intIdClasificacionIncidencia { get; set; }
        /// <summary>
        /// Descripción del tipo de incidencia
        /// </summary>
        public string strTipoIncidencia { get; set; }
        /// <summary>
        /// Descripción de la prioridad de la incidencia
        /// </summary>
        public string strDescripcionPrioridadIncidencia { get; set; }
        /// <summary>
        /// Descripción de la clasificación de la incidencia
        /// </summary>
        public string strDescripcionClasificacionIncidencia { get; set; }
    }

    public class IncidenciasDtoForCreate
    {
        public int intIdActivo { get; set; }
        public int intIdReporte { get; set; }        
        public string strDescripcionIncidencia { get; set; }
        public int intIdEstadoIncidencia { get; set; }
        public int intIdPrioridadIncidencia { get; set; }
        public int intIdClasificacionIncidencia { get; set; }
        public string strUsuario { get; set; }
        public string strTipoIncidencia { get;}
        public int intIdTarjeta { get; }
    }

    public class IncidenciasDtoForUpdate
    {
        public int IdReporte { get; set; }
        public string DescripcionIncidencia { get; set; }
        public int EstadoIncidencia { get; set; }
        public int PrioridadIncidencia { get; set; }
        public int IdClasificacionIncidencia { get; set; }
        public string Usuario { get; set; }
        public string TipoIncidencia { get; }
        public int IdTarjeta { get; }
    }
}
