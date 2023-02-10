using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Domain.Entities;

/// <summary>
/// Catálogo de estados de la propuesta de patrullaje
/// </summary>
[Table("Estado_Propuesta", Schema = "cat")]
public partial class EstadoPropuesta
{
    /// <summary>
    /// Identificador único del estado de la propuesta de patrullaje
    /// </summary>
    [Key]
    [Column("id")]
    public byte Id { get; set; }

    /// <summary>
    /// Nombre descriptivo del estado de la propuesta
    /// </summary>
    [Column("estado")]
    [StringLength(50)]
    [Unicode(false)]
    public string Estado { get; set; } = null!;

    [InverseProperty("IdEstadoPropuestaNavigation")]
    public virtual ICollection<PropuestaPatrullajeFecha> PropuestaPatrullajeFechas { get; } = new List<PropuestaPatrullajeFecha>();
}
