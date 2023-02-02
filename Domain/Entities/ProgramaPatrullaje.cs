using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    [Table("programapatrullajes", Schema = "ssf")]
    public class ProgramaPatrullaje
    {
        [Key]
        public int id_programa { get; set; }

        [Required]
        public int id_ruta { get; set; }

        [Required]
        public int id_usuario { get; set; }

        public DateTime ultimaActualizacion { get; set; }

        public DateTime? fechaPatrullaje { get; set; }

        public TimeSpan? inicio { get; set; }

        public TimeSpan? termino { get; set; }

        [Required]
        public int id_estadoPatrullaje { get; set; }

        [StringLength(100)]
        public string? observaciones { get; set; }

        [Required]
        public int riesgoPatrullaje { get; set; }

        [Required]
        public int id_usuarioResponsablePatrullaje { get; set; }

        [Required]
        public int id_propuestaPatrullaje { get; set; }

        [Required]
        public int id_puntoResponsable { get; set; }

        [Required]
        public int id_ruta_original { get; set; }

        [Required]
        public int id_apoyoPatrullaje { get; set; }

        [StringLength(25)]
        public string? latitud { get; set; }

        [StringLength(25)]
        public string? longitud { get; set; }

        [StringLength(50)]
        public string? solicitudOficioComision { get; set; }

        [StringLength(50)]
        public string? oficioComision { get; set; }
    }
}
