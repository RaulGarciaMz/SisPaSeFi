using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Domain.Entities;

[Keyless]
[Table("usuariogrupocorreoelectronico", Schema = "ssf")]
[Index("IdGrupoCorreo", Name = "id_grupoCorreo")]
[Index("IdUsuario", Name = "id_usuario")]
public partial class UsuarioGrupoCorreoElectronico
{
    [Column("id_usuario")]
    public int? IdUsuario { get; set; }

    [Column("id_grupoCorreo")]
    public int? IdGrupoCorreo { get; set; }

    [ForeignKey("IdGrupoCorreo")]
    public virtual GrupoCorreoElectronico? IdGrupoCorreoNavigation { get; set; }

    [ForeignKey("IdUsuario")]
    public virtual Usuario? IdUsuarioNavigation { get; set; }
}
