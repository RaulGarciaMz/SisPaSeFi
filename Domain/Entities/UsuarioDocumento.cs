using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

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
