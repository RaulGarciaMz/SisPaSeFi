using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Domain.Entities;

[Table("estadosincidencias", Schema = "ssf")]
public partial class EstadoIncidencia
{
    [Key]
    [Column("id_estadoIncidencia")]
    public int IdEstadoIncidencia { get; set; }

    [Column("descripcionEstado")]
    [StringLength(100)]
    [Unicode(false)]
    public string DescripcionEstado { get; set; } = null!;

    [InverseProperty("IdEstadoIncidenciaNavigation")]
    public virtual ICollection<BitacoraSeguimientoIncidencia> Bitacoraseguimientoincidencia { get; } = new List<BitacoraSeguimientoIncidencia>();

    [InverseProperty("IdEstadoIncidenciaNavigation")]
    public virtual ICollection<BitacoraSeguimientoIncidenciaPunto> Bitacoraseguimientoincidenciapuntos { get; } = new List<BitacoraSeguimientoIncidenciaPunto>();

    [InverseProperty("EstadoIncidenciaNavigation")]
    public virtual ICollection<ReporteEstructura> Reporteestructuras { get; } = new List<ReporteEstructura>();

    [InverseProperty("EstadoIncidenciaNavigation")]
    public virtual ICollection<ReportePunto> Reportepuntos { get; } = new List<ReportePunto>();
}
