using Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        public int id_punto { get; set; }

        /// <summary>
        /// descripción de la ubicación del punto de patrullaje
        /// </summary>
        [Required]
        [StringLength(50)]
        public string ubicacion { get; set; }

        /// <summary>
        /// Coordenadas X, y del punto de patrullaje
        /// </summary>
        [Required]
        [StringLength(50)]
        public string coordenadas { get; set; }

        /// <summary>
        /// Indicador para conocer si el punto de patrulaje es una instalación física
        /// </summary>
        [Required]
        public int esInstalacion { get; set; }

        /// <summary>
        /// Identificador del nivel de riesgo del punto de patrullaje
        /// </summary>
        public int? id_nivelRiesgo { get; set; }
        /// <summary>
        /// Identificador de la comandancia a la que pertenece el punto de patrullaje
        /// </summary>
        public int? id_comandancia { get; set; }
        /// <summary>
        /// Identificador del proceso responsable del punto de patrullaje
        /// </summary>
        [Required]
        public int id_ProcesoResponsable { get; set; }
        /// <summary>
        /// Identificador de la gerencia y división encargada del punto de patrullaje
        /// </summary>
        [Required]
        public int id_GerenciaDivision { get; set; }    
        /// <summary>
        /// Indicador del estado (Bloqueado o desbloqueado) del registro del punto de patrullaje
        /// </summary>
        [Required]
        public int bloqueado { get; set; }
        /// <summary>
        /// Identificador del municipio al que pertenece el punto de patrullaje
        /// </summary>
        [Required]
        public int id_municipio { get; set; }
        /// <summary>
        /// Nombre del municipio al que pertenece el punto de patrullaje
        /// </summary>
        public string municipio { get; set; }
        /// <summary>
        /// Identificador del estado de la república al que pertenece el punto de patrullaje
        /// </summary>
        public int id_estado { get; set; }
        /// <summary>
        /// Nombre de estado de la república al que pertenece el punto de patrullaje
        /// </summary>
        public string estado { get; set; }
        /// <summary>
        /// Identificador del usuario que registra el punto de patrullaje
        /// </summary>
        public int id_usuario { get; set; }
    }
}
