using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Domain.Entities;

[Keyless]
[Table("lineapunto", Schema = "ssf")]
[Index("IdLinea", Name = "id_linea")]
[Index("IdPunto", Name = "id_punto")]
public partial class LineaPunto
{
    [Column("id_linea")]
    public int IdLinea { get; set; }

    [Column("id_punto")]
    public int IdPunto { get; set; }

    [ForeignKey("IdLinea")]
    public virtual Linea IdLineaNavigation { get; set; } = null!;

    [ForeignKey("IdPunto")]
    public virtual PuntoPatrullaje IdPuntoNavigation { get; set; } = null!;
}
