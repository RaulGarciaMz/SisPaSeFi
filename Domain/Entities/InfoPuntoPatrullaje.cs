using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Domain.Entities;

/// <summary>
/// Tabla temporal para almacenar información de coordenadas del Punto de patrullaje (se eliminará cuando se cambie la aplicación de mapas)
/// </summary>
[Table("Info_Punto_Patrullaje", Schema = "dmn")]
public partial class InfoPuntoPatrullaje
{
    /// <summary>
    /// Identificador único del punto de patrullaje
    /// </summary>
    [Key]
    [Column("id_punto")]
    public int IdPunto { get; set; }

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

    [ForeignKey("IdPunto")]
    [InverseProperty("InfoPuntoPatrullaje")]
    public virtual PuntoPatrullaje IdPuntoNavigation { get; set; } = null!;
}
