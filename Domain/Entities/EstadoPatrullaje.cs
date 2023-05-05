using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities;

[Table("estadopatrullaje", Schema = "ssf")]
public partial class EstadoPatrullaje
{
    [Key]
    [Column("id_estadoPatrullaje")]
    public int IdEstadoPatrullaje { get; set; }

    [Column("descripcionEstadoPatrullaje")]
    [StringLength(50)]
    [Unicode(false)]
    public string DescripcionEstadoPatrullaje { get; set; } = null!;

    [Column("nombreCorto")]
    [StringLength(30)]
    [Unicode(false)]
    public string? NombreCorto { get; set; }

    [InverseProperty("IdEstadoPatrullajeNavigation")]
    public virtual ICollection<ProgramaPatrullaje> Programapatrullajes { get; } = new List<ProgramaPatrullaje>();
}
