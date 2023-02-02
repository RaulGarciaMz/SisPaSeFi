using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    [Table("tipopatrullaje", Schema = "ssf")]
    public class TipoPatrullaje
    {
        [Key]
        public int id_tipoPatrullaje { get; set; }

        [Required]
        [StringLength(50)]
        public string descripcion { get; set; }
    }
}
