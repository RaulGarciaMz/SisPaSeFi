using Domain.Ports.Driving;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    [Table("propuestaspatrullajes", Schema = "ssf")]
    public class PropuestaPatrullaje
    {
        [Key]
        public int id_propuestaPatrullaje { get; set; }

        [Required]
        public int id_ruta { get; set; }

        [Required]
        public int id_usuario { get; set; }

        public DateTime ultimaActualizacion { get; set; }

        public DateTime? fechaPatrullaje { get; set; }

        [Required]
        [StringLength(50)]
        public string observaciones { get; set; }

        [Required]
        public int riesgoPatrullaje { get; set; }

        [Required]
        public int id_puntoResponsable { get; set; }

        [Required]
        public int id_estadoPropuesta { get; set; }

        [Required]
        public int id_apoyoPatrullaje { get; set; }

        [Required]
        public int id_clasePatrullaje { get; set; }

        [StringLength(50)]
        public string? solicitudOficioAutorizacion { get; set; }

        [StringLength(50)]
        public string? oficioAutorizacion { get; set; }
    }
}
