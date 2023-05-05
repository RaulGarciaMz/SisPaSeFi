using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities;

[Table("afectacionincidencia", Schema = "ssf")]
[Index("IdConceptoAfectacion", Name = "id_conceptoAfectacion")]
public partial class AfectacionIncidencia
{
    [Key]
    [Column("id_afectacionIncidencia")]
    public int IdAfectacionIncidencia { get; set; }

    [Column("id_incidencia")]
    public int IdIncidencia { get; set; }

    [Column("id_conceptoAfectacion")]
    public int IdConceptoAfectacion { get; set; }

    [Column("tipo_incidencia")]
    public int TipoIncidencia { get; set; }

    [Column("cantidad")]
    public int Cantidad { get; set; }

    [Column("precioUnitario")]
    public float PrecioUnitario { get; set; }

    [ForeignKey("IdConceptoAfectacion")]
    [InverseProperty("Afectacionincidencia")]
    public virtual ConceptoAfectacion IdConceptoAfectacionNavigation { get; set; } = null!;
}
