using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Domain.Entities;

[Table("usovehiculo", Schema = "ssf")]
[Index("IdUsoVehiculo", Name = "id_programa")]
[Index("IdUsuarioVehiculo", Name = "id_usuarioVehiculo")]
[Index("IdVehiculo", Name = "id_vehiculo")]
[Index("IdPrograma", Name = "usoVehiculo_ibfk_1")]
public partial class UsoVehiculo
{
    [Key]
    [Column("id_usoVehiculo")]
    public int IdUsoVehiculo { get; set; }

    [Column("id_programa")]
    public int IdPrograma { get; set; }

    [Column("id_vehiculo")]
    public int IdVehiculo { get; set; }

    [Column("id_usuarioVehiculo")]
    public int IdUsuarioVehiculo { get; set; }

    [Column("kmInicio")]
    public int KmInicio { get; set; }

    [Column("kmFin")]
    public int KmFin { get; set; }

    [Column("consumoCombustible")]
    public int ConsumoCombustible { get; set; }

    [Column("estadoVehiculo")]
    [StringLength(250)]
    [Unicode(false)]
    public string? EstadoVehiculo { get; set; }

    [InverseProperty("IdUsoVehiculoNavigation")]
    public virtual ICollection<EvidenciaUsoVehiculo> Evidenciausovehiculos { get; } = new List<EvidenciaUsoVehiculo>();

    [ForeignKey("IdPrograma")]
    [InverseProperty("Usovehiculos")]
    public virtual ProgramaPatrullaje IdProgramaNavigation { get; set; } = null!;

    [ForeignKey("IdUsuarioVehiculo")]
    [InverseProperty("Usovehiculos")]
    public virtual Usuario IdUsuarioVehiculoNavigation { get; set; } = null!;

    [ForeignKey("IdVehiculo")]
    [InverseProperty("Usovehiculos")]
    public virtual Vehiculo IdVehiculoNavigation { get; set; } = null!;
}
