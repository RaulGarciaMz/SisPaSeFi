using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities;

[Table("apoyopatrullaje", Schema = "ssf")]
public partial class ApoyoPatrullaje
{
    [Key]
    [Column("id_apoyoPatrullaje")]
    public int IdApoyoPatrullaje { get; set; }

    [Column("descripcion")]
    [StringLength(50)]
    [Unicode(false)]
    public string Descripcion { get; set; } = null!;

    [InverseProperty("IdApoyoPatrullajeNavigation")]
    public virtual ICollection<ProgramaPatrullaje> Programapatrullajes { get; } = new List<ProgramaPatrullaje>();

    [InverseProperty("IdApoyoPatrullajeNavigation")]
    public virtual ICollection<PropuestaPatrullaje> Propuestaspatrullajes { get; } = new List<PropuestaPatrullaje>();
}
