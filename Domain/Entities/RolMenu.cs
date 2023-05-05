using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities;

[Keyless]
[Table("rolmenu", Schema = "ssf")]
[Index("IdMenu", Name = "IdMenu")]
[Index("IdRol", Name = "IdUsuario")]
public partial class RolMenu
{
    [Column("id_rol")]
    public int IdRol { get; set; }

    public int IdMenu { get; set; }

    public int? Navegar { get; set; }

    [ForeignKey("IdMenu")]
    public virtual Menu IdMenuNavigation { get; set; } = null!;

    [ForeignKey("IdRol")]
    public virtual Rol IdRolNavigation { get; set; } = null!;
}
