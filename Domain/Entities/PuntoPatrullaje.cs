using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{

    [Table("puntospatrullaje", Schema = "ssf")]
    public class PuntoPatrullaje
    {
        [Key]
        public int id_punto { get; set; }

        [Required]
        [StringLength(50)]
        public string ubicacion { get; set; }

        [Required]
        [StringLength(50)]
        public string coordenadas { get; set; }

        public int esInstalacion { get; set; }

        public int? id_nivelRiesgo { get; set; }

        public int? id_comandancia { get; set; }

        public int id_ProcesoResponsable { get; set; }

        public int id_GerenciaDivision { get; set; }

        public int? id_usuario { get; set; }

        public DateTime? ultimaActualizacion { get; set; }

        public int bloqueado { get; set; }

        [StringLength(25)]
        public string? latitud { get; set; }

        [StringLength(25)]
        public string? longitud { get; set; }

        public int id_municipio { get; set; }
        [ForeignKey("id_municipio")]
        public Municipio? Municipio { get; set; }
    }
}
