using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        public int IdRuta;
        /// <summary>
        /// Clave de la ruta de patrullaje
        /// </summary>
        public string Clave;
        /// <summary>
        /// Región militar de SDN de la ruta de patrullaje
        /// </summary>
        public string RegionMilitarSDN;
        /// <summary>
        /// Región SSF de la ruta de patrullaje
        /// </summary>
        public string RegionSSF;
        /// <summary>
        /// Identificador del tipo de patrullaje
        /// </summary>
        public int IdTipoPatrullaje;
        /// <summary>
        /// Indicador del estado (bloqueado o desbloqueado) del registro de la ruta
        /// </summary>
        public int Bloqueado;
        /// <summary>
        /// Indicador de la zona miklitar de la ruta de patrullaje
        /// </summary>
        public int ZonaMilitarSDN;
        /// <summary>
        /// Observaciones de la ruta de patrullaje
        /// </summary>
        public string Observaciones;
        /// <summary>
        /// Consecutivo de la región militar SDN de la ruta de patrullaje
        /// </summary>
        public int ConsecutivoRegionMilitarSDN;
        /// <summary>
        /// Cantidad de rutas en la región militar SDN de la ruta de patrullaje
        /// </summary>
        public int TotalRutasRegionMilitarSDN;
        /// <summary>
        /// Fecha de la última actualización del registro de la ruta de patrullaje
        /// </summary>
        public string UltimaActualizacion;
        /// <summary>
        /// Indicador del estado (habilitado o deshabilitado) del registro de la ruta de patrullaje
        /// </summary>
        public int Habilitado;
        /// <summary>
        /// Descripción del itinerario de la ruta de patrullaje
        /// </summary>
        public string Itinerario;
        /// <summary>
        /// Lista de recorridos de la ruta de patrullaje
        /// </summary>
        public List<RecorridoDto> Recorridos;
    }

    /// <summary>
    /// Recorrido de patrullaje
    /// </summary>
    public class RecorridoDto
    {
        /// <summary>
        /// Identificador del itinerario al que pertenece el recorrido
        /// </summary>
        public int IdItinerario;
        /// <summary>
        /// Identificador del punto de patrullaje incluido en el recorrido
        /// </summary>
        public int IdPunto;
        /// <summary>
        /// Indicador del orden o secuencia del recorrido en una ruta
        /// </summary>
        public int Posicion;
        /// <summary>
        /// Descripción de la ubicación del recorrido
        /// </summary>
        public string Ubicacion;
        /// <summary>
        /// Coordenadas X, Y del recorrido
        /// </summary>
        public string Coordenadas;
    }
}
