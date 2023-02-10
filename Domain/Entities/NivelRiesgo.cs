using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Domain.Entities;

/// <summary>
/// Catálogo del niveles de riesgo asociados al proceso de patrullaje e infraestructura
/// </summary>
[Table("Nivel_Riesgo", Schema = "cat")]
public partial class NivelRiesgo
{
    /// <summary>
    /// Identificador único del nivel de riesgo
    /// </summary>
    [Key]
    [Column("id")]
    public short Id { get; set; }

    /// <summary>
    /// Acrónimo con el que se asocia la descripción del riesgo
    /// </summary>
    [Column("alias")]
    [StringLength(10)]
    [Unicode(false)]
    public string Alias { get; set; } = null!;

    /// <summary>
    /// Texto descriptivo del nivel de riesgo
    /// </summary>
    [Column("descripcion")]
    [StringLength(50)]
    [Unicode(false)]
    public string Descripcion { get; set; } = null!;

    [InverseProperty("IdNivelRiesgoNavigation")]
    public virtual ICollection<ProgramaPatrullaje> ProgramaPatrullajes { get; } = new List<ProgramaPatrullaje>();

    [InverseProperty("IdNivelRiesgoNavigation")]
    public virtual ICollection<PropuestaPatrullajeRutaContenedor> PropuestaPatrullajeRutaContenedors { get; } = new List<PropuestaPatrullajeRutaContenedor>();

    [InverseProperty("IdNivelRiesgoNavigation")]
    public virtual ICollection<PuntoPatrullaje> PuntoPatrullajes { get; } = new List<PuntoPatrullaje>();

    [InverseProperty("IdNivelRiesgoNavigation")]
    public virtual ICollection<ReporteIncidencia> ReporteIncidencia { get; } = new List<ReporteIncidencia>();
}
