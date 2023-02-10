using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Domain.Entities;

/// <summary>
/// Catálogo de unidades de medidas para la definición de conecptos afectados detectados en el patrullaje
/// </summary>
[Table("Unidad_Medida", Schema = "cat")]
public partial class UnidadMedida
{
    /// <summary>
    /// Identificador único de la unidad de medida
    /// </summary>
    [Key]
    [Column("id")]
    public short Id { get; set; }

    /// <summary>
    /// Nombre de la unidad de medida
    /// </summary>
    [Column("nombre")]
    [StringLength(50)]
    [Unicode(false)]
    public string Nombre { get; set; } = null!;

    [InverseProperty("IdUnidadMedidaNavigation")]
    public virtual ICollection<ConceptoAfectacion> ConceptoAfectacions { get; } = new List<ConceptoAfectacion>();
}
