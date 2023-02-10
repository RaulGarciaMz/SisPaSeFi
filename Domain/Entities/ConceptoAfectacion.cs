using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Domain.Entities;

/// <summary>
/// Catálogo de conceptos afectados que se pueden detectar durante un patrullaje
/// </summary>
[Table("Concepto_Afectacion", Schema = "cat")]
public partial class ConceptoAfectacion
{
    /// <summary>
    /// Identificador único del concepto afectado
    /// </summary>
    [Key]
    [Column("id")]
    public int Id { get; set; }

    /// <summary>
    /// Nombre del concepto afectado
    /// </summary>
    [Column("nombre")]
    [StringLength(50)]
    [Unicode(false)]
    public string Nombre { get; set; } = null!;

    /// <summary>
    /// Identificador de la unidad de medida aplicada al concepto
    /// </summary>
    [Column("id_unidad_medida")]
    public short IdUnidadMedida { get; set; }

    /// <summary>
    /// Costo derivado de la reposición del concepto afectado
    /// </summary>
    [Column("precio_unitario", TypeName = "money")]
    public decimal PrecioUnitario { get; set; }

    /// <summary>
    /// Peso individual del concepto afectado
    /// </summary>
    [Column("peso")]
    public short Peso { get; set; }

    [InverseProperty("IdConceptoAfectacionNavigation")]
    public virtual ICollection<AfectacionIncidencia> AfectacionIncidencia { get; } = new List<AfectacionIncidencia>();

    [ForeignKey("IdUnidadMedida")]
    [InverseProperty("ConceptoAfectacions")]
    public virtual UnidadMedida IdUnidadMedidaNavigation { get; set; } = null!;
}
