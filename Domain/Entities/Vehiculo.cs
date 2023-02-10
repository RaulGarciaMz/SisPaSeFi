using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Domain.Entities;

/// <summary>
/// Listado de vehículos disponibles para realizar los patrullajes
/// </summary>
[Table("Vehiculo", Schema = "cat")]
public partial class Vehiculo
{
    /// <summary>
    /// Identificador único del véhículo
    /// </summary>
    [Key]
    [Column("id")]
    public int Id { get; set; }

    /// <summary>
    /// Identificador del tipo de patrullaje que se puede realizar en este vehículo
    /// </summary>
    [Column("id_tipo_patrullaje")]
    public byte IdTipoPatrullaje { get; set; }

    /// <summary>
    /// Identificador del tipo de vehículo 
    /// </summary>
    [Column("id_tipo_vehiculo")]
    public byte IdTipoVehiculo { get; set; }

    /// <summary>
    /// Identificador de la Comandancia Regional a la que está asignado el vehículo
    /// </summary>
    [Column("id_comandancia")]
    public short? IdComandancia { get; set; }

    /// <summary>
    /// Número de placa o matrícula asignado al vehículo
    /// </summary>
    [Column("matricula")]
    [StringLength(50)]
    [Unicode(false)]
    public string Matricula { get; set; } = null!;

    /// <summary>
    /// Identificador único del vehículo como activo
    /// </summary>
    [Column("numero_economico")]
    [StringLength(20)]
    [Unicode(false)]
    public string? NumeroEconomico { get; set; }

    /// <summary>
    /// Cantidad de kilómetros recorridos por el vehículo
    /// </summary>
    [Column("kilometraje")]
    public int Kilometraje { get; set; }

    /// <summary>
    /// Fecha de la última actualización realizada a la información del vehículo
    /// </summary>
    [Column("ultima_actualizacion", TypeName = "datetime")]
    public DateTime UltimaActualizacion { get; set; }

    /// <summary>
    /// Indicador de si el vehículo puede ser utiizado para realizar patrullajes
    /// </summary>
    [Column("habilitado")]
    public bool Habilitado { get; set; }

    [ForeignKey("IdComandancia")]
    [InverseProperty("Vehiculos")]
    public virtual ComandanciaRegional? IdComandanciaNavigation { get; set; }

    [ForeignKey("IdTipoPatrullaje")]
    [InverseProperty("Vehiculos")]
    public virtual TipoPatrullaje IdTipoPatrullajeNavigation { get; set; } = null!;

    [ForeignKey("IdTipoVehiculo")]
    [InverseProperty("Vehiculos")]
    public virtual TipoVehiculo IdTipoVehiculoNavigation { get; set; } = null!;

    [InverseProperty("IdVehiculoNavigation")]
    public virtual ICollection<UsoVehiculo> UsoVehiculos { get; } = new List<UsoVehiculo>();

    [ForeignKey("IdVehiculo")]
    [InverseProperty("IdVehiculos")]
    public virtual ICollection<PropuestaPatrullajeRutaContenedor> Ids { get; } = new List<PropuestaPatrullajeRutaContenedor>();
}
