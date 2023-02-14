using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Domain.Entities;

[Table("usuarios", Schema = "ssf")]
[Index("UsuarioNom", Name = "usuarios$usuario_nom", IsUnique = true)]
public partial class Usuario
{
    [Key]
    [Column("id_usuario")]
    public int IdUsuario { get; set; }

    [Column("nombre")]
    [StringLength(50)]
    [Unicode(false)]
    public string Nombre { get; set; } = null!;

    [Column("apellido1")]
    [StringLength(50)]
    [Unicode(false)]
    public string Apellido1 { get; set; } = null!;

    [Column("apellido2")]
    [StringLength(50)]
    [Unicode(false)]
    public string Apellido2 { get; set; } = null!;

    [Column("usuario_nom")]
    [StringLength(15)]
    [Unicode(false)]
    public string UsuarioNom { get; set; } = null!;

    [Column("cel")]
    [StringLength(20)]
    [Unicode(false)]
    public string? Cel { get; set; }

    [Column("pass")]
    [StringLength(32)]
    [Unicode(false)]
    public string? Pass { get; set; }

    [Column("configurador")]
    public int? Configurador { get; set; }

    [Column("bloqueado")]
    public int? Bloqueado { get; set; }

    public int? AceptacionAvisoLegal { get; set; }

    [Column("intentos")]
    public int? Intentos { get; set; }

    public int? NotificarAcceso { get; set; }

    public DateTime? EstampaTiempoUltimoAcceso { get; set; }

    public DateTime? EstampaTiempoAceptacionUso { get; set; }

    [StringLength(100)]
    [Unicode(false)]
    public string? CorreoElectronico { get; set; }

    [Column("regionSSF")]
    public int RegionSsf { get; set; }

    [Column("tiempoEspera")]
    public int TiempoEspera { get; set; }

    [Column("passTemp")]
    [StringLength(32)]
    [Unicode(false)]
    public string? PassTemp { get; set; }

    public int DesbloquearRegistros { get; set; }

    [InverseProperty("IdUsuarioNavigation")]
    public virtual ICollection<BitacoraSeguimientoIncidencia> Bitacoraseguimientoincidencia { get; } = new List<BitacoraSeguimientoIncidencia>();

    [InverseProperty("IdUsuarioNavigation")]
    public virtual ICollection<BitacoraSeguimientoIncidenciaPunto> Bitacoraseguimientoincidenciapuntos { get; } = new List<BitacoraSeguimientoIncidenciaPunto>();

    [InverseProperty("IdUsuarioNavigation")]
    public virtual ICollection<DocumentoPatrullaje> Documentospatrullajes { get; } = new List<DocumentoPatrullaje>();

    [InverseProperty("IdUsuarioNavigation")]
    public virtual ICollection<NotaInformativa> Notainformativas { get; } = new List<NotaInformativa>();

    [InverseProperty("IdUsuarioNavigation")]
    public virtual ICollection<ProgramaPatrullaje> Programapatrullajes { get; } = new List<ProgramaPatrullaje>();

    [InverseProperty("IdUsuarioNavigation")]
    public virtual ICollection<PropuestaPatrullaje> Propuestaspatrullajes { get; } = new List<PropuestaPatrullaje>();

    [InverseProperty("IdUsuarioNavigation")]
    public virtual ICollection<Sesion> Sesiones { get; } = new List<Sesion>();

    [InverseProperty("IdUsuarioNavigation")]
    public virtual ICollection<TarjetaInformativa> Tarjetainformativas { get; } = new List<TarjetaInformativa>();

    [InverseProperty("IdUsuarioVehiculoNavigation")]
    public virtual ICollection<UsoVehiculo> Usovehiculos { get; } = new List<UsoVehiculo>();
}
