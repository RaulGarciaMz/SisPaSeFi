using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Domain.Entities;

/// <summary>
/// Listado de tipos de evidencia
/// </summary>
[Table("Tipo_Evidencia", Schema = "cat")]
public partial class TipoEvidencia
{
    /// <summary>
    /// Identificador único del tipo de evidencia
    /// </summary>
    [Key]
    [Column("id")]
    public byte Id { get; set; }

    /// <summary>
    /// Nombre del tipo de evidencia
    /// </summary>
    [Column("nombre")]
    [StringLength(50)]
    [Unicode(false)]
    public string Nombre { get; set; } = null!;

    [InverseProperty("ItTipoEvidenciaNavigation")]
    public virtual ICollection<Evidencia> Evidencia { get; } = new List<Evidencia>();
}
