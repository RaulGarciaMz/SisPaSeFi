using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Domain.Entities;

/// <summary>
/// Catálogo de causales de las incidencias detectadas en los patrullajes
/// </summary>
[Table("Tipo_Inicidencia", Schema = "cat")]
public partial class TipoInicidencia
{
    /// <summary>
    /// Identifidacor único de la causa de la incidencia
    /// </summary>
    [Key]
    [Column("id")]
    public byte Id { get; set; }

    /// <summary>
    /// Nombre de la causa de la incidencia
    /// </summary>
    [Column("desripcion")]
    [StringLength(50)]
    [Unicode(false)]
    public string Desripcion { get; set; } = null!;

    [InverseProperty("IdTipoIncidenciaNavigation")]
    public virtual ICollection<ReporteIncidencia> ReporteIncidencia { get; } = new List<ReporteIncidencia>();
}
