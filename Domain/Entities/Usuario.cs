using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Domain.Entities;

[Table("Usuario", Schema = "dmn")]
public partial class Usuario
{
    /// <summary>
    /// Identificador único del usuario
    /// </summary>
    [Key]
    [Column("id")]
    public int Id { get; set; }

    /// <summary>
    /// Identificador de la comandancia a la que pertenece el usuario. Puede contener un null si el usuario no pertenece a una comandancia
    /// </summary>
    [Column("id_comandancia_regional")]
    public short? IdComandanciaRegional { get; set; }

    /// <summary>
    /// Nombre del usuario
    /// </summary>
    [Column("nombre")]
    [StringLength(50)]
    [Unicode(false)]
    public string Nombre { get; set; } = null!;

    /// <summary>
    /// Apellido paterno del usuario
    /// </summary>
    [Column("apellido_paterno")]
    [StringLength(50)]
    [Unicode(false)]
    public string ApellidoPaterno { get; set; } = null!;

    /// <summary>
    /// Apellido materno del usuario
    /// </summary>
    [Column("apellido_materno")]
    [StringLength(50)]
    [Unicode(false)]
    public string? ApellidoMaterno { get; set; }

    /// <summary>
    /// Nombre de usuario dentro de la aplicación
    /// </summary>
    [Column("login")]
    [StringLength(15)]
    [Unicode(false)]
    public string Login { get; set; } = null!;

    /// <summary>
    /// Correo electrónico al que enviar las notificaciones
    /// </summary>
    [Column("email")]
    [StringLength(100)]
    [Unicode(false)]
    public string Email { get; set; } = null!;

    /// <summary>
    /// Número telefónico en el que se puede contactar al usuario
    /// </summary>
    [Column("celular")]
    [StringLength(20)]
    [Unicode(false)]
    public string Celular { get; set; } = null!;

    /// <summary>
    /// Contraseña de acceso al sistema
    /// </summary>
    [Column("password")]
    [StringLength(50)]
    [Unicode(false)]
    public string Password { get; set; } = null!;

    /// <summary>
    /// Indicador de si el usuario tiene perfil de configuración (Se debe eliminar de esta tabla)
    /// </summary>
    [Column("es_configurador")]
    public bool EsConfigurador { get; set; }

    /// <summary>
    /// Indicador de sí se encuentra bloqueado el acceso al sistema.
    /// </summary>
    [Column("bloqueado")]
    public bool Bloqueado { get; set; }

    /// <summary>
    /// Fecha en la que el usuario aceptó el aviso de legalidad de acceso al sistema. Si no lo ha aceptado, su valor será nulo.
    /// </summary>
    [Column("fecha_aceptacion_aviso_legal", TypeName = "datetime")]
    public DateTime? FechaAceptacionAvisoLegal { get; set; }

    /// <summary>
    /// Indicador de si el sistema debe notificar por correo electrónico cuando el usuario ha accecido al sistema.
    /// </summary>
    [Column("notificar_acceso")]
    public bool NotificarAcceso { get; set; }

    /// <summary>
    /// Fecha del último acceso al sistema
    /// </summary>
    [Column("fecha_ultimo_acceso", TypeName = "datetime")]
    public DateTime? FechaUltimoAcceso { get; set; }

    [InverseProperty("IdUsuarioNavigation")]
    public virtual ICollection<BitacoraSeguimientoReporte> BitacoraSeguimientoReportes { get; } = new List<BitacoraSeguimientoReporte>();

    [InverseProperty("IdUsuarioResponsableNavigation")]
    public virtual ICollection<ComandanciaRegional> ComandanciaRegionals { get; } = new List<ComandanciaRegional>();

    [ForeignKey("IdComandanciaRegional")]
    [InverseProperty("Usuarios")]
    public virtual ComandanciaRegional? IdComandanciaRegionalNavigation { get; set; }

    [InverseProperty("IdUsuarioNavigation")]
    public virtual ICollection<ProgramaPatrullaje> ProgramaPatrullajes { get; } = new List<ProgramaPatrullaje>();

    [InverseProperty("IdUsuarioNavigation")]
    public virtual ICollection<PropuestaPatrullaje> PropuestaPatrullajes { get; } = new List<PropuestaPatrullaje>();

    [ForeignKey("IdUsuario")]
    [InverseProperty("IdUsuarios")]
    public virtual ICollection<ComandanciaRegional> IdComandancia { get; } = new List<ComandanciaRegional>();
}
