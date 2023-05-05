using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities;

[Table("evidenciaseguimientoincidenciapunto", Schema = "ssf")]
[Index("IdBitacoraSeguimientoIncidenciaPunto", Name = "id_bitacoraSeguimientoIncidenciaPunto")]
public partial class EvidenciaSeguimientoIncidenciaPunto
{
    [Key]
    [Column("id_evidenciaSeguimientoIncidenciaPunto")]
    public int IdEvidenciaSeguimientoIncidenciaPunto { get; set; }

    [Column("id_bitacoraSeguimientoIncidenciaPunto")]
    public int IdBitacoraSeguimientoIncidenciaPunto { get; set; }

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

    [ForeignKey("IdBitacoraSeguimientoIncidenciaPunto")]
    [InverseProperty("Evidenciaseguimientoincidenciapuntos")]
    public virtual BitacoraSeguimientoIncidenciaPunto IdBitacoraSeguimientoIncidenciaPuntoNavigation { get; set; } = null!;
}
