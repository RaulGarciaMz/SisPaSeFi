using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities;

[Table("vehiculos", Schema = "ssf")]
[Index("IdTipoPatrullaje", Name = "id_tipoPatrullaje")]
[Index("IdTipoVehiculo", Name = "id_tipoVehiculo")]
[Index("IdComandancia", Name = "vehiculos_ibfk_2")]
public partial class Vehiculo
{
    [Key]
    [Column("id_vehiculo")]
    public int IdVehiculo { get; set; }

    [Column("id_tipoPatrullaje")]
    public int IdTipoPatrullaje { get; set; }

    [Column("matricula")]
    [StringLength(50)]
    [Unicode(false)]
    public string Matricula { get; set; } = null!;

    [Column("id_comandancia")]
    public int? IdComandancia { get; set; }

    [Column("id_tipoVehiculo")]
    public int IdTipoVehiculo { get; set; }

    [Column("aseguradora")]
    [StringLength(50)]
    [Unicode(false)]
    public string? Aseguradora { get; set; }

    [Column("numeroPoliza")]
    [StringLength(50)]
    [Unicode(false)]
    public string? NumeroPoliza { get; set; }

    [Column("kilometraje")]
    public int Kilometraje { get; set; }

    [Column("ultimaActualizacion", TypeName = "datetime")]
    public DateTime UltimaActualizacion { get; set; }

    [Column("kmMantenimientoProgramado")]
    public int KmMantenimientoProgramado { get; set; }

    [Column("fechaVencimientoSeguro", TypeName = "date")]
    public DateTime? FechaVencimientoSeguro { get; set; }

    [Column("fechaMantenimientoProgramado", TypeName = "date")]
    public DateTime? FechaMantenimientoProgramado { get; set; }

    [Column("numeroEconomico")]
    [StringLength(20)]
    [Unicode(false)]
    public string? NumeroEconomico { get; set; }

    [Column("ubicacionTecnica")]
    [StringLength(10)]
    [Unicode(false)]
    public string? UbicacionTecnica { get; set; }

    [Column("centroCostos")]
    [StringLength(10)]
    [Unicode(false)]
    public string? CentroCostos { get; set; }

    [Column("sociedad")]
    [StringLength(10)]
    [Unicode(false)]
    public string? Sociedad { get; set; }

    [Column("division")]
    [StringLength(10)]
    [Unicode(false)]
    public string? Division { get; set; }

    [Column("propiedad")]
    [StringLength(10)]
    [Unicode(false)]
    public string? Propiedad { get; set; }

    [Column("habilitado")]
    public int Habilitado { get; set; }

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
    public virtual ICollection<UsoVehiculo> Usovehiculos { get; } = new List<UsoVehiculo>();
}
