using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Domain.Entities;

/// <summary>
/// Catálogo de dependiencias encargadas de brindar el apoyo durante los patrullajes
/// </summary>
[Table("Apoyo_Patrullaje", Schema = "cat")]
public partial class ApoyoPatrullaje
{
    /// <summary>
    /// Identificador único de la dependencia de apoyo del patrullaje
    /// </summary>
    [Key]
    [Column("id")]
    public byte Id { get; set; }

    /// <summary>
    /// Nombre de la dependencia
    /// </summary>
    [Column("nombre")]
    [StringLength(50)]
    [Unicode(false)]
    public string Nombre { get; set; } = null!;

    [InverseProperty("IdApoyoPatrullajeNavigation")]
    public virtual ICollection<ProgramaPatrullaje> ProgramaPatrullajes { get; } = new List<ProgramaPatrullaje>();

    [InverseProperty("IdApoyoPatrullajeNavigation")]
    public virtual ICollection<PropuestaPatrullajeRutaContenedor> PropuestaPatrullajeRutaContenedors { get; } = new List<PropuestaPatrullajeRutaContenedor>();
}
