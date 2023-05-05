using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities;

[Keyless]
[Table("conceptosafectacionreporteincidenciatransmision", Schema = "ssf")]
public partial class ConceptoAfectacionReporteIncidenciaTransmision
{
    [Column("id_reporteincidenciatransmision")]
    public int? IdReporteincidenciatransmision { get; set; }

    [Column("id_conceptoafectacion")]
    public int? IdConceptoafectacion { get; set; }

    [Column("total")]
    public int? Total { get; set; }
}
