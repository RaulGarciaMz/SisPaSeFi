using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities;

[Table("reporteestructuras", Schema = "ssf")]
[Index("EstadoIncidencia", Name = "estadoIncidencia")]
[Index("IdEstructura", Name = "id_estructura")]
public partial class ReporteEstructura
{
    [Key]
    [Column("id_reporte")]
    public int IdReporte { get; set; }

    [Column("id_nota")]
    public int IdNota { get; set; }

    [Column("id_estructura")]
    public int IdEstructura { get; set; }

    [Column("incidencia")]
    [StringLength(800)]
    [Unicode(false)]
    public string Incidencia { get; set; } = null!;

    [Column("estadoIncidencia")]
    public int EstadoIncidencia { get; set; }

    [Column("ultimaActualizacion", TypeName = "datetime")]
    public DateTime UltimaActualizacion { get; set; }

    [Column("prioridadIncidencia")]
    public int PrioridadIncidencia { get; set; }

    [Column("ultimoRegistroEnBitacora", TypeName = "datetime")]
    public DateTime UltimoRegistroEnBitacora { get; set; }

    [Column("id_clasificacionIncidencia")]
    public int IdClasificacionIncidencia { get; set; }

    [InverseProperty("IdReporteNavigation")]
    public virtual ICollection<BitacoraSeguimientoIncidencia> Bitacoraseguimientoincidencia { get; } = new List<BitacoraSeguimientoIncidencia>();

    [ForeignKey("EstadoIncidencia")]
    [InverseProperty("Reporteestructuras")]
    public virtual EstadoIncidencia EstadoIncidenciaNavigation { get; set; } = null!;

    [InverseProperty("IdReporteNavigation")]
    public virtual ICollection<EvidenciaIncidencia> Evidenciaincidencia { get; } = new List<EvidenciaIncidencia>();

    [ForeignKey("IdEstructura")]
    [InverseProperty("Reporteestructuras")]
    public virtual Estructura IdEstructuraNavigation { get; set; } = null!;
}
