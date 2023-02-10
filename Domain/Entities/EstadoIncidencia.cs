using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Domain.Entities;

/// <summary>
/// Catálogo de estados que puede tener una incidencia
/// </summary>
[Table("Estado_Incidencia", Schema = "cat")]
public partial class EstadoIncidencia
{
    /// <summary>
    /// Identificador único del estado de la incidencia
    /// </summary>
    [Key]
    [Column("id")]
    public byte Id { get; set; }

    /// <summary>
    /// Nombre descriptivo del estado de la incidencia
    /// </summary>
    [Column("estado")]
    [StringLength(100)]
    [Unicode(false)]
    public string Estado { get; set; } = null!;

    [InverseProperty("IdEstadoIncidenciaNavigation")]
    public virtual ICollection<ReporteIncidencia> ReporteIncidencia { get; } = new List<ReporteIncidencia>();
}
