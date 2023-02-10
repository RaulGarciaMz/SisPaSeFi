using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Domain.Entities;

/// <summary>
/// Catálogo de las clases de patrullaje basándose en el tiempo
/// </summary>
[Table("Clase_Patrullaje", Schema = "cat")]
public partial class ClasePatrullaje
{
    /// <summary>
    /// Identificador único de la clase de patrullaje
    /// </summary>
    [Key]
    [Column("id")]
    public byte Id { get; set; }

    /// <summary>
    /// Nombre de la clase de patrullaje
    /// </summary>
    [Column("nombre")]
    [StringLength(50)]
    [Unicode(false)]
    public string Nombre { get; set; } = null!;

    [InverseProperty("IdClasePatrullajeNavigation")]
    public virtual ICollection<PropuestaPatrullaje> PropuestaPatrullajes { get; } = new List<PropuestaPatrullaje>();
}
