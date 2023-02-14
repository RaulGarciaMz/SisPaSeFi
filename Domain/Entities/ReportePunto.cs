using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Domain.Entities;

[Table("reportepunto", Schema = "ssf")]
[Index("EstadoIncidencia", Name = "estadoIncidencia")]
[Index("IdPunto", Name = "id_punto")]
public partial class ReportePunto
{
    [Key]
    [Column("id_reportePunto")]
    public int IdReportePunto { get; set; }

    [Column("id_nota")]
    public int IdNota { get; set; }

    [Column("id_punto")]
    public int IdPunto { get; set; }

    [Column("incidencia")]
    [StringLength(800)]
    [Unicode(false)]
    public string Incidencia { get; set; } = null!;

    [Column("estadoIncidencia")]
    public int EstadoIncidencia { get; set; }

    [Column("ultimaActualizacion")]
    public DateTime? UltimaActualizacion { get; set; }

    [Column("prioridadIncidencia")]
    public int PrioridadIncidencia { get; set; }

    [Column("ultimoRegistroEnBitacora")]
    public DateTime? UltimoRegistroEnBitacora { get; set; }

    [Column("id_clasificacionIncidencia")]
    public int IdClasificacionIncidencia { get; set; }

    [InverseProperty("IdReportePuntoNavigation")]
    public virtual ICollection<BitacoraSeguimientoIncidenciaPunto> Bitacoraseguimientoincidenciapuntos { get; } = new List<BitacoraSeguimientoIncidenciaPunto>();

    [ForeignKey("EstadoIncidencia")]
    [InverseProperty("Reportepuntos")]
    public virtual EstadoIncidencia EstadoIncidenciaNavigation { get; set; } = null!;

    [InverseProperty("IdReportePuntoNavigation")]
    public virtual ICollection<EvidenciaIncidenciaPunto> Evidenciaincidenciaspuntos { get; } = new List<EvidenciaIncidenciaPunto>();

    [ForeignKey("IdPunto")]
    [InverseProperty("Reportepuntos")]
    public virtual PuntoPatrullaje IdPuntoNavigation { get; set; } = null!;
}
