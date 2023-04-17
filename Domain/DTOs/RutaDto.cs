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
        public int IdRuta { get; set; }
        /// <summary>
        /// Clave de la ruta de patrullaje
        /// </summary>
        public string Clave { get; set; }
        /// <summary>
        /// Región militar de SDN de la ruta de patrullaje
        /// </summary>
        public string RegionMilitarSDN { get; set; }
        /// <summary>
        /// Región SSF de la ruta de patrullaje
        /// </summary>
        public string RegionSSF { get; set; }
        /// <summary>
        /// Identificador del tipo de patrullaje
        /// </summary>
        public int IdTipoPatrullaje { get; set; }
        /// <summary>
        /// Indicador del estado (bloqueado o desbloqueado) del registro de la ruta
        /// </summary>
        public int Bloqueado { get; set; }
        /// <summary>
        /// Indicador de la zona miklitar de la ruta de patrullaje
        /// </summary>
        public int ZonaMilitarSDN { get; set; }
        /// <summary>
        /// Observaciones de la ruta de patrullaje
        /// </summary>
        public string Observaciones { get; set; }
        /// <summary>
        /// Consecutivo de la región militar SDN de la ruta de patrullaje
        /// </summary>
        public int ConsecutivoRegionMilitarSDN { get; set; }
        /// <summary>
        /// Cantidad de rutas en la región militar SDN de la ruta de patrullaje
        /// </summary>
        public int TotalRutasRegionMilitarSDN { get; set; }
        /// <summary>
        /// Fecha de la última actualización del registro de la ruta de patrullaje
        /// </summary>
        public string UltimaActualizacion { get; set; }
        /// <summary>
        /// Indicador del estado (habilitado o deshabilitado) del registro de la ruta de patrullaje
        /// </summary>
        public int Habilitado { get; set; }
        /// <summary>
        /// Descripción del itinerario de la ruta de patrullaje
        /// </summary>
        public string Itinerario { get; set; }
        /// <summary>
        /// Lista de recorridos de la ruta de patrullaje
        /// </summary>
        public List<RecorridoDto> Recorridos { get; set; }
    }

    /// <summary>
    /// Recorrido de patrullaje
    /// </summary>
    public class RecorridoDto
    {
        /// <summary>
        /// Identificador del itinerario al que pertenece el recorrido
        /// </summary>
        public int IdItinerario { get; set; }
        /// <summary>
        /// Identificador del punto de patrullaje incluido en el recorrido
        /// </summary>
        public int IdPunto { get; set; }
        /// <summary>
        /// Indicador del orden o secuencia del recorrido en una ruta
        /// </summary>
        public int Posicion { get; set; }
        /// <summary>
        /// Descripción de la ubicación del recorrido
        /// </summary>
        public string Ubicacion { get; set; }
        /// <summary>
        /// Coordenadas X, Y del recorrido
        /// </summary>
        public string Coordenadas { get; set; }
    }
}
