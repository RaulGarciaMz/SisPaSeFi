using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Domain.Entities;

/// <summary>
/// Listado de personal que puede participar en un patrullaje
/// </summary>
[Table("Tipo_Personal_Patrullaje", Schema = "cat")]
public partial class TipoPersonalPatrullaje
{
    /// <summary>
    /// Identificador único del Personal que puede apoyar en un programa de patrullaje
    /// </summary>
    [Key]
    [Column("id")]
    public byte Id { get; set; }

    /// <summary>
    /// Nombre del Tipo de personal
    /// </summary>
    [Column("nombre")]
    [StringLength(50)]
    [Unicode(false)]
    public string Nombre { get; set; } = null!;

    [InverseProperty("IdTipoPersonalPatrullajeNavigation")]
    public virtual ICollection<PersonalParticipantePatrullaje> PersonalParticipantePatrullajes { get; } = new List<PersonalParticipantePatrullaje>();
}
