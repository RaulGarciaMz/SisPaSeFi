using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Domain.Entities;

[Table("bitacoraseguimientoincidenciapunto", Schema = "ssf")]
[Index("IdEstadoIncidencia", Name = "id_estadoIncidencia")]
[Index("IdReportePunto", Name = "id_reportePunto")]
[Index("IdUsuario", Name = "id_usuario")]
public partial class BitacoraSeguimientoIncidenciaPunto
{
    [Key]
    [Column("id_bitacoraSeguimientoIncidenciaPunto")]
    public int IdBitacoraSeguimientoIncidenciaPunto { get; set; }

    [Column("id_reportePunto")]
    public int IdReportePunto { get; set; }

    [Column("id_usuario")]
    public int IdUsuario { get; set; }

    [Column("id_estadoIncidencia")]
    public int IdEstadoIncidencia { get; set; }

    [Column("ultimaActualizacion", TypeName = "datetime")]
    public DateTime UltimaActualizacion { get; set; }

    [Column("descripcion")]
    [StringLength(150)]
    [Unicode(false)]
    public string Descripcion { get; set; } = null!;

    [InverseProperty("IdBitacoraSeguimientoIncidenciaPuntoNavigation")]
    public virtual ICollection<EvidenciaSeguimientoIncidenciaPunto> Evidenciaseguimientoincidenciapuntos { get; } = new List<EvidenciaSeguimientoIncidenciaPunto>();

    [ForeignKey("IdEstadoIncidencia")]
    [InverseProperty("Bitacoraseguimientoincidenciapuntos")]
    public virtual EstadoIncidencia IdEstadoIncidenciaNavigation { get; set; } = null!;

    [ForeignKey("IdReportePunto")]
    [InverseProperty("Bitacoraseguimientoincidenciapuntos")]
    public virtual ReportePunto IdReportePuntoNavigation { get; set; } = null!;

    [ForeignKey("IdUsuario")]
    [InverseProperty("Bitacoraseguimientoincidenciapuntos")]
    public virtual Usuario IdUsuarioNavigation { get; set; } = null!;
}
