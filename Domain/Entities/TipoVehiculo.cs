using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Domain.Entities;

/// <summary>
/// Catálogo de las tipos de vehículo utilizados en los patrullajes
/// </summary>
[Table("Tipo_Vehiculo", Schema = "cat")]
public partial class TipoVehiculo
{
    [Key]
    [Column("id")]
    public byte Id { get; set; }

    [Column("nombre")]
    [StringLength(50)]
    [Unicode(false)]
    public string Nombre { get; set; } = null!;

    [InverseProperty("IdTipoVehiculoNavigation")]
    public virtual ICollection<Vehiculo> Vehiculos { get; } = new List<Vehiculo>();
}
