using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Domain.Entities;

[Keyless]
[Table("usuariorol", Schema = "ssf")]
[Index("IdRol", Name = "IdMenu")]
[Index("IdUsuario", Name = "IdUsuario")]
public partial class UsuarioRol
{
    [Column("id_usuario")]
    public int IdUsuario { get; set; }

    [Column("id_rol")]
    public int IdRol { get; set; }

    [ForeignKey("IdRol")]
    public virtual Rol IdRolNavigation { get; set; } = null!;

    [ForeignKey("IdUsuario")]
    public virtual Usuario IdUsuarioNavigation { get; set; } = null!;
}
