using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using NetTopologySuite.Geometries;

namespace Domain.Entities;

/// <summary>
/// Listado de ubicaciones de los vehículos durante su trayecto
/// </summary>
[PrimaryKey("IdTarjetaInformativa", "Fecha")]
[Table("Posicionamiento", Schema = "dmn")]
public partial class Posicionamiento
{
    /// <summary>
    /// Identificador de la tarjeta informativa a la que se asocia el posicionamiento
    /// </summary>
    [Key]
    [Column("id_tarjeta_informativa")]
    public int IdTarjetaInformativa { get; set; }

    /// <summary>
    /// Estampa de tiempo del reporte de posicionamiento
    /// </summary>
    [Key]
    [Column("fecha", TypeName = "datetime")]
    public DateTime Fecha { get; set; }

    /// <summary>
    /// Coordenadas del reporte del patrullaje en distintos periodos de tiempo
    /// </summary>
    [Column("coordenadas_srid", TypeName = "geometry")]
    public Geometry CoordenadasSrid { get; set; } = null!;

    /// <summary>
    /// Id del equipo que está realizando el reporte de posicionamiento
    /// </summary>
    [Column("id_equipo")]
    [StringLength(50)]
    [Unicode(false)]
    public string? IdEquipo { get; set; }

    [ForeignKey("IdTarjetaInformativa")]
    [InverseProperty("Posicionamientos")]
    public virtual TarjetaInformativa IdTarjetaInformativaNavigation { get; set; } = null!;

    [ForeignKey("IdTarjetaInformativa, Fecha")]
    [InverseProperty("Posicionamiento")]
    public virtual InfoPosicionamiento InfoPosicionamiento { get; set; } = null!;
}
