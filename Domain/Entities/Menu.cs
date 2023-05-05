using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities;

[Table("menus", Schema = "ssf")]
[Index("IdGrupo", Name = "IdGrupo")]
public partial class Menu
{
    [Key]
    public int IdMenu { get; set; }

    public int IdGrupo { get; set; }

    [StringLength(100)]
    [Unicode(false)]
    public string? Desplegado { get; set; }

    [StringLength(100)]
    [Unicode(false)]
    public string? Descripcion { get; set; }

    [StringLength(150)]
    [Unicode(false)]
    public string? Liga { get; set; }

    public int? Padre { get; set; }

    [Column("posicion")]
    public int? Posicion { get; set; }

    [ForeignKey("IdGrupo")]
    [InverseProperty("Menus")]
    public virtual Grupo IdGrupoNavigation { get; set; } = null!;
}
