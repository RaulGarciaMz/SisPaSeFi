using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Domain.Entities;

/// <summary>
/// Listado de afectaciones asociadas a los reportes de patrullajes
/// </summary>
[Table("Afectacion_Incidencia", Schema = "dmn")]
public partial class AfectacionIncidencia
{
    /// <summary>
    /// Identificador único de la afectación
    /// </summary>
    [Key]
    [Column("id")]
    public int Id { get; set; }

    /// <summary>
    /// Id del reporte al que se asocia la incidencia
    /// </summary>
    [Column("id_reporte")]
    public int IdReporte { get; set; }

    /// <summary>
    /// Id del concepto o producto que fue afectado y que debe reemplazarse
    /// </summary>
    [Column("id_concepto_afectacion")]
    public int IdConceptoAfectacion { get; set; }

    /// <summary>
    /// Número de conceptos necesarios para reparar la incidencia
    /// </summary>
    [Column("cantidad")]
    public short Cantidad { get; set; }

    /// <summary>
    /// Precio unitario estimado del concepto al momento de que se detecta la incidencia
    /// </summary>
    [Column("precio_unitario", TypeName = "money")]
    public decimal? PrecioUnitario { get; set; }

    [ForeignKey("IdConceptoAfectacion")]
    [InverseProperty("AfectacionIncidencia")]
    public virtual ConceptoAfectacion IdConceptoAfectacionNavigation { get; set; } = null!;

    [ForeignKey("IdReporte")]
    [InverseProperty("AfectacionIncidencia")]
    public virtual ReporteIncidencia IdReporteNavigation { get; set; } = null!;
}
