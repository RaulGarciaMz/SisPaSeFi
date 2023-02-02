using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    [Table("municipios", Schema = "ssf")]
    public class Municipio
    {
        [Key]
        public int id_municipio { get; set; }     

        [Required]
        [StringLength(3)]
        public string clave { get; set; }

        [Required]
        [StringLength(100)]
        public string nombre { get; set; }

        [Required]
        [StringLength(4)]
        public string nombre_corto { get; set; }

        List<PuntoPatrullaje> Puntos { get; set; }

        public int id_estado { get; set; }
 
    }
}
