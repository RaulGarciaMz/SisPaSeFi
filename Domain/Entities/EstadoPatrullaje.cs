using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Domain.Entities;

/// <summary>
/// Catálogo de estados que pueden aplicarse al patrullaje
/// </summary>
[Table("Estado_Patrullaje", Schema = "cat")]
public partial class EstadoPatrullaje
{
    /// <summary>
    /// Identificador único del estado del patrullaje
    /// </summary>
    [Key]
    [Column("id")]
    public byte Id { get; set; }

    /// <summary>
    /// Nombre largo del estado de patrullaje
    /// </summary>
    [Column("estado")]
    [StringLength(50)]
    [Unicode(false)]
    public string Estado { get; set; } = null!;

    /// <summary>
    /// Nombre corto del estado a usar en la aplicación.
    /// </summary>
    [Column("nombre_corto")]
    [StringLength(30)]
    [Unicode(false)]
    public string NombreCorto { get; set; } = null!;

    [InverseProperty("IdEstadoPatrullajeNavigation")]
    public virtual ICollection<ProgramaPatrullaje> ProgramaPatrullajes { get; } = new List<ProgramaPatrullaje>();
}
