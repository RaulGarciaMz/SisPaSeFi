using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities;

[Keyless]
[Table("reporteincidenciatransmision", Schema = "ssf")]
public partial class ReporteIncidenciaTransmision
{
    [Column("id_reporteincidenciatransmision")]
    public int? IdReporteincidenciatransmision { get; set; }

    [Column("fecha", TypeName = "date")]
    public DateTime? Fecha { get; set; }

    [Column("descripcionlineas")]
    [StringLength(500)]
    [Unicode(false)]
    public string? Descripcionlineas { get; set; }

    [Column("descripcionafectacionentorresestructuras")]
    [StringLength(500)]
    [Unicode(false)]
    public string? Descripcionafectacionentorresestructuras { get; set; }

    [Column("id_gerenciatransmision")]
    public int? IdGerenciatransmision { get; set; }

    [Column("zonatransmision")]
    [StringLength(50)]
    [Unicode(false)]
    public string? Zonatransmision { get; set; }

    [Column("id_catalogohallazgo")]
    public int? IdCatalogohallazgo { get; set; }

    [Column("id_localidad")]
    public int? IdLocalidad { get; set; }
}
