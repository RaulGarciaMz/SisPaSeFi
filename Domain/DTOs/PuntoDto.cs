using System.ComponentModel.DataAnnotations;

namespace Domain.DTOs
{
    /// <summary>
    /// Punto de patrullaje
    /// </summary>
    public class PuntoDto
    {
        /// <summary>
        /// Identificador del punto de patrullaje
        /// </summary>
        public int intIdPunto { get; set; }

        /// <summary>
        /// descripción de la ubicación del punto de patrullaje
        /// </summary>
        [Required]
        [StringLength(50)]
        public string strUbicacion { get; set; }

        /// <summary>
        /// Coordenadas X, y del punto de patrullaje
        /// </summary>
        [Required]
        [StringLength(50)]
        public string strCoordenadas { get; set; }

        /// <summary>
        /// Indicador para conocer si el punto de patrulaje es una instalación física
        /// </summary>
        [Required]
        public int intEsInstalacion { get; set; }

        /// <summary>
        /// Identificador del nivel de riesgo del punto de patrullaje
        /// </summary>
        public int? intIdNivelRiesgo { get; set; }
        /// <summary>
        /// Identificador de la comandancia a la que pertenece el punto de patrullaje
        /// </summary>
        public int? intIdComandancia { get; set; }
        /// <summary>
        /// Identificador del proceso responsable del punto de patrullaje
        /// </summary>
        [Required]
        public int intIdProcesoResponsable { get; set; }
        /// <summary>
        /// Identificador de la gerencia y división encargada del punto de patrullaje
        /// </summary>
        [Required]
        public int intIdGerenciaDivision { get; set; }    
        /// <summary>
        /// Indicador del estado (Bloqueado o desbloqueado) del registro del punto de patrullaje
        /// </summary>
        [Required]
        public int intBloqueado { get; set; }
        /// <summary>
        /// Identificador del municipio al que pertenece el punto de patrullaje
        /// </summary>
        [Required]
        public int intIdMunicipio { get; set; }
        /// <summary>
        /// Nombre del municipio al que pertenece el punto de patrullaje
        /// </summary>
        public string strNombreMunicipio { get; set; }
        /// <summary>
        /// Identificador del estado de la república al que pertenece el punto de patrullaje
        /// </summary>
        public int intIdEstado { get; set; }
        /// <summary>
        /// Nombre de estado de la república al que pertenece el punto de patrullaje
        /// </summary>
        public string strNombreEstado { get; set; }
        /// <summary>
        /// Identificador del usuario que registra el punto de patrullaje
        /// </summary>
        public int intIdUsuario { get; set; }
    }
}
