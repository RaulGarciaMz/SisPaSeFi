using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    [Table("tiporeporte", Schema = "ssf")]
    public partial class Tiporeporte
    {
        [Key]
        [Column("idtiporeporte")]
        public int Idtiporeporte { get; set; }

        [Column("descripcion")]
        [StringLength(50)]
        [Unicode(false)]
        public string Descripcion { get; set; } = null!;
    }
}
