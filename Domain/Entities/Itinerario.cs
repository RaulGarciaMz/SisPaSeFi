using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Domain.Entities;

/// <summary>
/// Almacén del detalle de puntos a visitar en la ruta
/// </summary>
[PrimaryKey("IdRuta", "IdPunto")]
[Table("Itinerario", Schema = "dmn")]
public partial class Itinerario
{
    /// <summary>
    /// Identificador único de la ruta a realizar en el itinerario
    /// </summary>
    [Key]
    [Column("id_ruta")]
    public int IdRuta { get; set; }

    /// <summary>
    /// Identificador único del punto de patrullaje a visitar en la ruta
    /// </summary>
    [Key]
    [Column("id_punto")]
    public int IdPunto { get; set; }

    /// <summary>
    /// Indicador del orden de visita de los puntos de patrullaje
    /// </summary>
    [Column("posicion")]
    public byte Posicion { get; set; }

    [ForeignKey("IdPunto")]
    [InverseProperty("Itinerarios")]
    public virtual PuntoPatrullaje IdPuntoNavigation { get; set; } = null!;

    [ForeignKey("IdRuta")]
    [InverseProperty("Itinerarios")]
    public virtual Ruta IdRutaNavigation { get; set; } = null!;
}
