using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    [Table("rutas", Schema = "ssf")]
    public class Ruta
    {
        [Key]
        public int id_ruta { get; set; }

        [Required]
        [StringLength(50)]
        public string clave { get; set; }

        [Required]
        [StringLength(3)]
        public string regionMilitarSDN { get; set; }

        [Required]
        [StringLength(3)]
        public string regionSSF { get; set; }

        [Required]
        public int id_tipoPatrullaje { get; set; }

        [Required]
        public int bloqueado { get; set; }

        [Required]
        public int zonaMilitarSDN { get; set; }

        [StringLength(100)]
        public string observaciones { get; set; }

        [Required]
        public int consecutivoRegionMilitarSDN { get; set; }

        [Required]
        public int totalRutasRegionMilitarSDN { get; set; }
        public DateTime ultimaActualizacion { get; set; }

        [Required]
        public int habilitado { get; set; }

    }
    
}


