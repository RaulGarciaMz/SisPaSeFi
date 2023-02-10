using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Domain.Entities;

/// <summary>
/// Tabla temporal para almacenar información de coordenadas de las estructuras (se eliminará cuando se cambie la aplicación de mapas)
/// </summary>
[Table("Info_Estructura", Schema = "dmn")]
public partial class InfoEstructura
{
    /// <summary>
    /// Identificador único del punto de patrullaje
    /// </summary>
    [Key]
    [Column("id_estructura")]
    public int IdEstructura { get; set; }

    /// <summary>
    /// Campo concatenado de latitud y longitud del punto
    /// </summary>
    [Column("coordenadas")]
    [StringLength(50)]
    [Unicode(false)]
    public string Coordenadas { get; set; } = null!;

    /// <summary>
    /// Latitud de las coordenadas del punto de patrullaje
    /// </summary>
    [Column("latitud")]
    [StringLength(25)]
    [Unicode(false)]
    public string? Latitud { get; set; }

    /// <summary>
    /// Longitud de las coordenadas del punto de patrullaje
    /// </summary>
    [Column("longitud")]
    [StringLength(25)]
    [Unicode(false)]
    public string? Longitud { get; set; }

    [ForeignKey("IdEstructura")]
    [InverseProperty("InfoEstructura")]
    public virtual Estructura IdEstructuraNavigation { get; set; } = null!;
}
