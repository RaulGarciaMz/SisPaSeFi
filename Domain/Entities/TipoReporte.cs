using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Domain.Entities;

/// <summary>
/// Catálogo de tipo de reportes a almacenar
/// </summary>
[Table("Tipo_Reporte", Schema = "cat")]
public partial class TipoReporte
{
    /// <summary>
    /// Identificador único del tipo de reporte
    /// </summary>
    [Key]
    [Column("id")]
    public byte Id { get; set; }

    /// <summary>
    /// Nombre del tipo de reporte
    /// </summary>
    [Column("nombre")]
    [StringLength(20)]
    [Unicode(false)]
    public string Nombre { get; set; } = null!;

    [InverseProperty("IdTipoReporteNavigation")]
    public virtual ICollection<ReporteIncidencia> ReporteIncidencia { get; } = new List<ReporteIncidencia>();
}
