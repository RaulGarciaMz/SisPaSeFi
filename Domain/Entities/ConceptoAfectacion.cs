using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities;

[Table("conceptosafectacion", Schema = "ssf")]
public partial class ConceptoAfectacion
{
    [Key]
    [Column("id_conceptoAfectacion")]
    public int IdConceptoAfectacion { get; set; }

    [Column("descripcion")]
    [StringLength(50)]
    [Unicode(false)]
    public string Descripcion { get; set; } = null!;

    [Column("unidades")]
    [StringLength(50)]
    [Unicode(false)]
    public string Unidades { get; set; } = null!;

    [Column("precioUnitario")]
    public float PrecioUnitario { get; set; }

    [InverseProperty("IdConceptoAfectacionNavigation")]
    public virtual ICollection<AfectacionIncidencia> Afectacionincidencia { get; } = new List<AfectacionIncidencia>();
}
