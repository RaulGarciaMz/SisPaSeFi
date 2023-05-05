using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities;

[Table("clasepatrullaje", Schema = "ssf")]
public partial class ClasePatrullaje
{
    [Key]
    [Column("id_clasePatrullaje")]
    public int IdClasePatrullaje { get; set; }

    [Column("descripcion")]
    [StringLength(50)]
    [Unicode(false)]
    public string Descripcion { get; set; } = null!;

    [InverseProperty("IdClasePatrullajeNavigation")]
    public virtual ICollection<PropuestaPatrullaje> Propuestaspatrullajes { get; } = new List<PropuestaPatrullaje>();
}
