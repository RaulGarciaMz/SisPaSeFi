using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Domain.Entities;

[Table("tipodocumento", Schema = "ssf")]
public partial class TipoDocumento
{
    [Key]
    [Column("id_tipoDocumento")]
    public long IdTipoDocumento { get; set; }

    [Column("descripcion")]
    [StringLength(50)]
    [Unicode(false)]
    public string Descripcion { get; set; } = null!;

    [InverseProperty("IdTipoDocumentoNavigation")]
    public virtual ICollection<DocumentoPatrullaje> Documentospatrullajes { get; } = new List<DocumentoPatrullaje>();
}
