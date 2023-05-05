using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities;

[Keyless]
[Table("catalogohallazgos", Schema = "ssf")]
public partial class CatalogoHallazgo
{
    [Column("id_hallazgo")]
    public int? IdHallazgo { get; set; }

    [Column("descripcion")]
    [StringLength(50)]
    [Unicode(false)]
    public string? Descripcion { get; set; }
}
