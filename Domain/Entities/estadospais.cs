using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    [Table("estadospais", Schema = "ssf")]
    public class EstadosPais
    {
        [Key]
        public int id_estado { get; set; }

        [Required]
        [StringLength(2)]
        public string clave { get; set; }

        [Required]
        [StringLength(50)]
        public string nombre { get; set; }

        [Required]
        [StringLength(16)]
        public string nombre_corto { get; set; }

    }
}
