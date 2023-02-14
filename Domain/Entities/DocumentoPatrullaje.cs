using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Domain.Entities;

[Table("documentospatrullaje", Schema = "ssf")]
[Index("IdTipoDocumento", Name = "id_tipoDocumento")]
[Index("IdUsuario", Name = "id_usuario")]
public partial class DocumentoPatrullaje
{
    [Key]
    [Column("id_documentoPatrullaje")]
    public long IdDocumentoPatrullaje { get; set; }

    [Column("id_referencia")]
    public long? IdReferencia { get; set; }

    [Column("id_tipoDocumento")]
    public long IdTipoDocumento { get; set; }

    [Column("id_comandancia")]
    public int IdComandancia { get; set; }

    [Column("fechaRegistro", TypeName = "datetime")]
    public DateTime FechaRegistro { get; set; }

    [Column("fechaReferencia", TypeName = "date")]
    public DateTime FechaReferencia { get; set; }

    [Column("rutaArchivo")]
    [StringLength(150)]
    [Unicode(false)]
    public string RutaArchivo { get; set; } = null!;

    [Column("nombreArchivo")]
    [StringLength(150)]
    [Unicode(false)]
    public string NombreArchivo { get; set; } = null!;

    [Column("descripcion")]
    [StringLength(150)]
    [Unicode(false)]
    public string Descripcion { get; set; } = null!;

    [Column("id_usuario")]
    public int IdUsuario { get; set; }

    [ForeignKey("IdTipoDocumento")]
    [InverseProperty("Documentospatrullajes")]
    public virtual TipoDocumento IdTipoDocumentoNavigation { get; set; } = null!;

    [ForeignKey("IdUsuario")]
    [InverseProperty("Documentospatrullajes")]
    public virtual Usuario IdUsuarioNavigation { get; set; } = null!;
}
