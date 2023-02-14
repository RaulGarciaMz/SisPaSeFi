using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Domain.Entities;

[Table("programapatrullajes", Schema = "ssf")]
[Index("IdEstadoPatrullaje", Name = "id_estadoPatrullaje")]
[Index("IdPuntoResponsable", Name = "id_puntoResponsable")]
[Index("IdRuta", Name = "id_ruta")]
[Index("IdUsuario", Name = "id_usuario")]
[Index("IdUsuarioResponsablePatrullaje", Name = "id_usuarioResponsablePatrullaje")]
[Index("IdApoyoPatrullaje", Name = "programaPatrullajes_ibfk_5")]
public partial class ProgramaPatrullaje
{
    [Key]
    [Column("id_programa")]
    public int IdPrograma { get; set; }

    [Column("id_ruta")]
    public int IdRuta { get; set; }

    [Column("id_usuario")]
    public int IdUsuario { get; set; }

    [Column("ultimaActualizacion", TypeName = "datetime")]
    public DateTime UltimaActualizacion { get; set; }

    [Column("fechaPatrullaje", TypeName = "date")]
    public DateTime? FechaPatrullaje { get; set; }

    [Column("inicio")]
    public TimeSpan? Inicio { get; set; }

    [Column("termino")]
    public TimeSpan? Termino { get; set; }

    [Column("id_estadoPatrullaje")]
    public int IdEstadoPatrullaje { get; set; }

    [Column("observaciones")]
    [StringLength(100)]
    [Unicode(false)]
    public string? Observaciones { get; set; }

    [Column("riesgoPatrullaje")]
    public int RiesgoPatrullaje { get; set; }

    [Column("id_usuarioResponsablePatrullaje")]
    public int IdUsuarioResponsablePatrullaje { get; set; }

    [Column("id_propuestaPatrullaje")]
    public int IdPropuestaPatrullaje { get; set; }

    [Column("id_puntoResponsable")]
    public int IdPuntoResponsable { get; set; }

    [Column("id_ruta_original")]
    public int IdRutaOriginal { get; set; }

    [Column("id_apoyoPatrullaje")]
    public int IdApoyoPatrullaje { get; set; }

    [Column("latitud")]
    [StringLength(25)]
    [Unicode(false)]
    public string? Latitud { get; set; }

    [Column("longitud")]
    [StringLength(25)]
    [Unicode(false)]
    public string? Longitud { get; set; }

    [Column("solicitudOficioComision")]
    [StringLength(50)]
    [Unicode(false)]
    public string? SolicitudOficioComision { get; set; }

    [Column("oficioComision")]
    [StringLength(50)]
    [Unicode(false)]
    public string? OficioComision { get; set; }

    [ForeignKey("IdApoyoPatrullaje")]
    [InverseProperty("Programapatrullajes")]
    public virtual ApoyoPatrullaje IdApoyoPatrullajeNavigation { get; set; } = null!;

    [ForeignKey("IdEstadoPatrullaje")]
    [InverseProperty("Programapatrullajes")]
    public virtual EstadoPatrullaje IdEstadoPatrullajeNavigation { get; set; } = null!;

    [ForeignKey("IdPuntoResponsable")]
    [InverseProperty("Programapatrullajes")]
    public virtual PuntoPatrullaje IdPuntoResponsableNavigation { get; set; } = null!;

    [ForeignKey("IdRuta")]
    [InverseProperty("Programapatrullajes")]
    public virtual Ruta IdRutaNavigation { get; set; } = null!;

    [ForeignKey("IdUsuario")]
    [InverseProperty("Programapatrullajes")]
    public virtual Usuario IdUsuarioNavigation { get; set; } = null!;

    [InverseProperty("IdProgramaNavigation")]
    public virtual ICollection<NotaInformativa> Notainformativas { get; } = new List<NotaInformativa>();

    [InverseProperty("IdProgramaNavigation")]
    public virtual ICollection<TarjetaInformativa> Tarjetainformativas { get; } = new List<TarjetaInformativa>();

    [InverseProperty("IdProgramaNavigation")]
    public virtual ICollection<UsoVehiculo> Usovehiculos { get; } = new List<UsoVehiculo>();
}
