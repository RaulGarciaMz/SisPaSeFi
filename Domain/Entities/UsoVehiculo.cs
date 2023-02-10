using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Domain.Entities;

/// <summary>
/// Detalle del reporte de vehículos usados en el patrullaje
/// </summary>
[Table("Uso_Vehiculo", Schema = "dmn")]
public partial class UsoVehiculo
{
    /// <summary>
    /// Identificador único del reporte del uso del vehículo
    /// </summary>
    [Key]
    [Column("id")]
    public int Id { get; set; }

    /// <summary>
    /// Id de la tarjeta informativa de la que se está haciendo el reporte de los vehículos involucrados
    /// </summary>
    [Column("id_tarjeta_informativa")]
    public int IdTarjetaInformativa { get; set; }

    /// <summary>
    /// Identificador único del vehículo del cual se está haciendo su reporte
    /// </summary>
    [Column("id_vehiculo")]
    public int IdVehiculo { get; set; }

    /// <summary>
    /// Kilómetraje de vehículo reportado al inicio del patrullaje
    /// </summary>
    [Column("km_inicio")]
    public int KmInicio { get; set; }

    /// <summary>
    /// Kilómetraje de vehículo reportado al final del patrullaje
    /// </summary>
    [Column("km_final")]
    public int KmFinal { get; set; }

    /// <summary>
    /// Consumo de combustible del vehículo
    /// </summary>
    [Column("consumo_combustible")]
    public short ConsumoCombustible { get; set; }

    /// <summary>
    /// Estado en que se encuentra el vehículo al terminar el patrullaje
    /// </summary>
    [Column("estado_vehiculo")]
    [StringLength(250)]
    [Unicode(false)]
    public string? EstadoVehiculo { get; set; }

    [ForeignKey("IdTarjetaInformativa")]
    [InverseProperty("UsoVehiculos")]
    public virtual TarjetaInformativa IdTarjetaInformativaNavigation { get; set; } = null!;

    [ForeignKey("IdVehiculo")]
    [InverseProperty("UsoVehiculos")]
    public virtual Vehiculo IdVehiculoNavigation { get; set; } = null!;
}
