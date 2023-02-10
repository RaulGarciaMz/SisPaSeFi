using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Domain.Entities;

/// <summary>
/// Información de coordenadas reportadas en formato latitud, longitud
/// </summary>
[PrimaryKey("IdTarjetaInformativa", "Fecha")]
[Table("Info_Posicionamiento", Schema = "dmn")]
public partial class InfoPosicionamiento
{
    /// <summary>
    /// Id de la tarjeta informativa a la que se está asociando la ubicación
    /// </summary>
    [Key]
    [Column("id_tarjeta_informativa")]
    public int IdTarjetaInformativa { get; set; }

    /// <summary>
    /// Estampa de tiempo del reporte de ubicación
    /// </summary>
    [Key]
    [Column("fecha", TypeName = "datetime")]
    public DateTime Fecha { get; set; }

    /// <summary>
    /// Latitud reportada por el equipo
    /// </summary>
    [Column("latitud")]
    [StringLength(25)]
    [Unicode(false)]
    public string Latitud { get; set; } = null!;

    /// <summary>
    /// Longitud reportada por el equipo
    /// </summary>
    [Column("longitud")]
    [StringLength(25)]
    [Unicode(false)]
    public string Longitud { get; set; } = null!;

    [InverseProperty("InfoPosicionamiento")]
    public virtual Posicionamiento? Posicionamiento { get; set; }
}
