using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Domain.Entities;

[Table("tipovehiculo", Schema = "ssf")]
public partial class TipoVehiculo
{
    [Key]
    [Column("id_tipoVehiculo")]
    public int IdTipoVehiculo { get; set; }

    [Column("descripciontipoVehiculo")]
    [StringLength(50)]
    [Unicode(false)]
    public string DescripciontipoVehiculo { get; set; } = null!;

    [InverseProperty("IdTipoVehiculoNavigation")]
    public virtual ICollection<Vehiculo> Vehiculos { get; } = new List<Vehiculo>();
}
