using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Domain.Entities;

/// <summary>
/// Listado de propuestas de patrullaje a realizar
/// </summary>
[Table("Propuesta_Patrullaje", Schema = "dmn")]
public partial class PropuestaPatrullaje
{
    /// <summary>
    /// Identificador único de la propuesta de patrullaje
    /// </summary>
    [Key]
    [Column("id")]
    public int Id { get; set; }

    /// <summary>
    /// Id del usuario que da de alta la propuesta
    /// </summary>
    [Column("id_usuario")]
    public int IdUsuario { get; set; }

    /// <summary>
    /// Id de la clase de patrullaje (Programado o extraordinario)
    /// </summary>
    [Column("id_clase_patrullaje")]
    public byte IdClasePatrullaje { get; set; }

    [ForeignKey("IdClasePatrullaje")]
    [InverseProperty("PropuestaPatrullajes")]
    public virtual ClasePatrullaje IdClasePatrullajeNavigation { get; set; } = null!;

    [ForeignKey("IdUsuario")]
    [InverseProperty("PropuestaPatrullajes")]
    public virtual Usuario IdUsuarioNavigation { get; set; } = null!;

    [InverseProperty("IdPropuestaPatrullajeNavigation")]
    public virtual ICollection<PropuestaPatrullajeRutaContenedor> PropuestaPatrullajeRutaContenedors { get; } = new List<PropuestaPatrullajeRutaContenedor>();
}
