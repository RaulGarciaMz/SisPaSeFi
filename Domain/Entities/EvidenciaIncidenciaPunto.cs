using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities;

[Table("evidenciaincidenciaspunto", Schema = "ssf")]
[Index("IdReportePunto", Name = "id_reportePunto")]
public partial class EvidenciaIncidenciaPunto
{
    [Key]
    [Column("id_evidenciaIncidenciaPunto")]
    public int IdEvidenciaIncidenciaPunto { get; set; }

    [Column("id_reportePunto")]
    public int IdReportePunto { get; set; }

    [Column("rutaArchivo")]
    [StringLength(150)]
    [Unicode(false)]
    public string RutaArchivo { get; set; } = null!;

    [Column("nombreArchivo")]
    [StringLength(150)]
    [Unicode(false)]
    public string NombreArchivo { get; set; } = null!;

    [Column("ultimaActualizacion", TypeName = "datetime")]
    public DateTime UltimaActualizacion { get; set; }

    [Column("descripcion")]
    [StringLength(150)]
    [Unicode(false)]
    public string? Descripcion { get; set; }

    [ForeignKey("IdReportePunto")]
    [InverseProperty("Evidenciaincidenciaspuntos")]
    public virtual ReportePunto IdReportePuntoNavigation { get; set; } = null!;
}
