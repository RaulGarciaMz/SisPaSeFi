using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Domain.Entities;

[Keyless]
[Table("usuariosmenus", Schema = "ssf")]
[Index("IdMenu", Name = "IdMenu")]
[Index("IdUsuario", Name = "IdUsuario")]
public partial class UsuarioMenu
{
    public int IdUsuario { get; set; }

    public int IdMenu { get; set; }

    public int? Navegar { get; set; }

    [ForeignKey("IdMenu")]
    public virtual Menu IdMenuNavigation { get; set; } = null!;

    [ForeignKey("IdUsuario")]
    public virtual Usuario IdUsuarioNavigation { get; set; } = null!;
}
