using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Domain.Entities;

[Keyless]
[Table("usuariocomandancia", Schema = "ssf")]
[Index("IdComandancia", Name = "id_comandancia")]
[Index("IdUsuario", Name = "id_usuario")]
public partial class UsuarioComandancia
{
    [Column("id_usuario")]
    public int IdUsuario { get; set; }

    [Column("id_comandancia")]
    public int IdComandancia { get; set; }

    [ForeignKey("IdComandancia")]
    public virtual ComandanciaRegional IdComandanciaNavigation { get; set; } = null!;

    [ForeignKey("IdUsuario")]
    public virtual Usuario IdUsuarioNavigation { get; set; } = null!;
}
