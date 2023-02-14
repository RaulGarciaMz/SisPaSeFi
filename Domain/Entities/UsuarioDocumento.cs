using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Domain.Entities;

[Keyless]
[Table("usuariodocumento", Schema = "ssf")]
public partial class UsuarioDocumento
{
    [Column("id_usuario")]
    public int IdUsuario { get; set; }

    [Column("id_documentoPatrullaje")]
    public int IdDocumentoPatrullaje { get; set; }
}
