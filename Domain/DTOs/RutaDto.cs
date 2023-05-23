namespace Domain.DTOs
{
    /// <summary>
    /// Ruta de patrullaje
    /// </summary>
    public class RutaDto
    {
        /// <summary>
        /// Identificador de la ruta de patrullaje
        /// </summary>
        public int intIdRuta { get; set; }
        /// <summary>
        /// Clave de la ruta de patrullaje
        /// </summary>
        public string strClave { get; set; }
        /// <summary>
        /// Región militar de SDN de la ruta de patrullaje
        /// </summary>
        public string intRegionMilitarSDN { get; set; }
        /// <summary>
        /// Región SSF de la ruta de patrullaje
        /// </summary>
        public string intRegionSSF { get; set; }
        /// <summary>
        /// Identificador del tipo de patrullaje
        /// </summary>
        public int intIdTipoPatrullaje { get; set; }
        /// <summary>
        /// Indicador del estado (bloqueado o desbloqueado) del registro de la ruta
        /// </summary>
        public int intBloqueado { get; set; }
        /// <summary>
        /// Indicador de la zona miklitar de la ruta de patrullaje
        /// </summary>
        public int intZonaMilitarSDN { get; set; }
        /// <summary>
        /// Observaciones de la ruta de patrullaje
        /// </summary>
        public string strObservaciones { get; set; }
        /// <summary>
        /// Consecutivo de la región militar SDN de la ruta de patrullaje
        /// </summary>
        public int intConsecutivoRegionMilitarSDN { get; set; }
        /// <summary>
        /// Cantidad de rutas en la región militar SDN de la ruta de patrullaje
        /// </summary>
        public int intTotalRutasRegionMilitarSDN { get; set; }
        /// <summary>
        /// Fecha de la última actualización del registro de la ruta de patrullaje
        /// </summary>
        public string strUltimaActualizacion { get; set; }
        /// <summary>
        /// Indicador del estado (habilitado o deshabilitado) del registro de la ruta de patrullaje
        /// </summary>
        public int intHabilitado { get; set; }
        /// <summary>
        /// Descripción del itinerario de la ruta de patrullaje
        /// </summary>
        public string strItinerario { get; set; }
        /// <summary>
        /// Lista de recorridos de la ruta de patrullaje
        /// </summary>
        public List<RecorridoDto> objRecorridoRuta { get; set; }
    }

    /// <summary>
    /// Recorrido de patrullaje
    /// </summary>
    public class RecorridoDto
    {
        /// <summary>
        /// Identificador del itinerario al que pertenece el recorrido
        /// </summary>
        public int intIdItinerario { get; set; }
        /// <summary>
        /// Identificador del punto de patrullaje incluido en el recorrido
        /// </summary>
        public int intIdPunto { get; set; }
        /// <summary>
        /// Indicador del orden o secuencia del recorrido en una ruta
        /// </summary>
        public int intPosicion { get; set; }
        /// <summary>
        /// Descripción de la ubicación del recorrido
        /// </summary>
        public string strUbicacion { get; set; }
        /// <summary>
        /// Coordenadas X, Y del recorrido
        /// </summary>
        public string strCoordenadas { get; set; }
    }

    public class RutaDisponibleDto
    {
        /// <summary>
        /// Identificador de la ruta de patrullaje
        /// </summary>
        public int intIdRuta { get; set; }
        /// <summary>
        /// Clave de la ruta de patrullaje
        /// </summary>
        public string strClave { get; set; }
        /// <summary>
        /// Descripción del itinerario de la ruta de patrullaje
        /// </summary>
        public string strItinerario { get; set; }
        /// <summary>
        /// Región militar de SDN de la ruta de patrullaje
        /// </summary>
        public string intRegionMilitarSDN { get; set; }
        /// <summary>
        /// Región SSF de la ruta de patrullaje
        /// </summary>
        public string intRegionSSF { get; set; }
        /// <summary>
        /// Indicador de la zona miklitar de la ruta de patrullaje
        /// </summary>
        public int intZonaMilitarSDN { get; set; }
        /// <summary>
        /// Observaciones de la ruta de patrullaje
        /// </summary>
        public string strObservaciones { get; set; }
    }
}
