using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities;

[Table("propuestaspatrullajes", Schema = "ssf")]
[Index("IdApoyoPatrullaje", Name = "id_apoyoPatrullaje")]
[Index("IdClasePatrullaje", Name = "id_clasePatrullaje")]
[Index("IdPuntoResponsable", Name = "id_puntoResponsable")]
[Index("IdRuta", Name = "id_ruta")]
[Index("IdUsuario", Name = "id_usuario")]
public partial class PropuestaPatrullaje
{
    [Key]
    [Column("id_propuestaPatrullaje")]
    public int IdPropuestaPatrullaje { get; set; }

    [Column("id_ruta")]
    public int IdRuta { get; set; }

    [Column("id_usuario")]
    public int IdUsuario { get; set; }

    [Column("ultimaActualizacion", TypeName = "datetime")]
    public DateTime UltimaActualizacion { get; set; }

    [Column("fechaPatrullaje", TypeName = "date")]
    public DateTime? FechaPatrullaje { get; set; }

    [Column("observaciones")]
    [StringLength(50)]
    [Unicode(false)]
    public string Observaciones { get; set; } = null!;

    [Column("riesgoPatrullaje")]
    public int RiesgoPatrullaje { get; set; }

    [Column("id_puntoResponsable")]
    public int IdPuntoResponsable { get; set; }

    [Column("id_estadoPropuesta")]
    public int IdEstadoPropuesta { get; set; }

    [Column("id_apoyoPatrullaje")]
    public int IdApoyoPatrullaje { get; set; }

    [Column("id_clasePatrullaje")]
    public int IdClasePatrullaje { get; set; }

    [Column("solicitudOficioAutorizacion")]
    [StringLength(50)]
    [Unicode(false)]
    public string? SolicitudOficioAutorizacion { get; set; }

    [Column("oficioAutorizacion")]
    [StringLength(50)]
    [Unicode(false)]
    public string? OficioAutorizacion { get; set; }

    [ForeignKey("IdApoyoPatrullaje")]
    [InverseProperty("Propuestaspatrullajes")]
    public virtual ApoyoPatrullaje IdApoyoPatrullajeNavigation { get; set; } = null!;

    [ForeignKey("IdClasePatrullaje")]
    [InverseProperty("Propuestaspatrullajes")]
    public virtual ClasePatrullaje IdClasePatrullajeNavigation { get; set; } = null!;

    [ForeignKey("IdPuntoResponsable")]
    [InverseProperty("Propuestaspatrullajes")]
    public virtual PuntoPatrullaje IdPuntoResponsableNavigation { get; set; } = null!;

    [ForeignKey("IdRuta")]
    [InverseProperty("Propuestaspatrullajes")]
    public virtual Ruta IdRutaNavigation { get; set; } = null!;

    [ForeignKey("IdUsuario")]
    [InverseProperty("Propuestaspatrullajes")]
    public virtual Usuario IdUsuarioNavigation { get; set; } = null!;
}
