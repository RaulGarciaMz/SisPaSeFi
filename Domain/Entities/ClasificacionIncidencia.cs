using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities;

[Table("clasificacionincidencia", Schema = "ssf")]
public partial class ClasificacionIncidencia
{
    [Key]
    [Column("id_clasificacionIncidencia")]
    public int IdClasificacionIncidencia { get; set; }

    [Column("descripcion")]
    [StringLength(50)]
    [Unicode(false)]
    public string Descripcion { get; set; } = null!;
}
