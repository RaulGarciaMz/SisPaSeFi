using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities;

[Keyless]
[Table("propuestaspatrullajesvehiculos", Schema = "ssf")]
[Index("IdPropuestaPatrullaje", Name = "id_propuestaPatrullaje")]
[Index("IdVehiculo", Name = "id_vehiculo")]
public partial class PropuestaPatrullajeVehiculo
{
    [Column("id_propuestaPatrullaje")]
    public int IdPropuestaPatrullaje { get; set; }

    [Column("id_vehiculo")]
    public int IdVehiculo { get; set; }

    [ForeignKey("IdPropuestaPatrullaje")]
    public virtual PropuestaPatrullaje IdPropuestaPatrullajeNavigation { get; set; } = null!;

    [ForeignKey("IdVehiculo")]
    public virtual Vehiculo IdVehiculoNavigation { get; set; } = null!;
}
