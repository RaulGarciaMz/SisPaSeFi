using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    [Table("itinerario", Schema = "ssf")]
    public class Itinerario
    {
        [Key]
        public int id_itinerario { get; set; }

        [Required]
        public int id_ruta { get; set; }

        [Required]
        public int id_punto { get; set; }

        [Required]
        public int posicion { get; set; }

        public DateTime ultimaActualizacion { get; set; }

    }
}
