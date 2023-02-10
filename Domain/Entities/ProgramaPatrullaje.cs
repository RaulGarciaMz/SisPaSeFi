using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Domain.Entities;

/// <summary>
/// Listado de Programas de patrullaje creado mediante la autorización de la propuesta de patrullaje
/// </summary>
[Table("Programa_Patrullaje", Schema = "dmn")]
public partial class ProgramaPatrullaje
{
    /// <summary>
    /// Identificador único del Programa de Patrullaje
    /// </summary>
    [Key]
    [Column("id")]
    public int Id { get; set; }

    /// <summary>
    /// Identificador de la ruta sobre la que se realizará el patrullaje
    /// </summary>
    [Column("id_ruta")]
    public int IdRuta { get; set; }

    /// <summary>
    /// Identificador de la propuesta de patrullaje; si es null, es  una ruta que se agregó durante la autorización.
    /// </summary>
    [Column("id_propuesta_patrullaje")]
    public int? IdPropuestaPatrullaje { get; set; }

    /// <summary>
    /// Identificador del número de usuario que autoriza la propuesta
    /// </summary>
    [Column("id_usuario")]
    public int IdUsuario { get; set; }

    /// <summary>
    /// Identificador de la instalación (punto de patrullaje) que es el responsable del patrullaje
    /// </summary>
    [Column("id_punto_responsable")]
    public int IdPuntoResponsable { get; set; }

    /// <summary>
    /// Identificador de la ruta de patrullaje que originalmente se solicitó autorizar
    /// </summary>
    [Column("id_ruta_original")]
    public int? IdRutaOriginal { get; set; }

    /// <summary>
    /// Identificador del estado que guarda la propuesta de patrullaje en el momento actual
    /// </summary>
    [Column("id_estado_patrullaje")]
    public byte IdEstadoPatrullaje { get; set; }

    /// <summary>
    /// Identificador de la agencia de seguridad encargada de aocmpañar durante el patrullaje
    /// </summary>
    [Column("id_apoyo_patrullaje")]
    public byte IdApoyoPatrullaje { get; set; }

    /// <summary>
    /// Identificador del nivel de riesgo asociado a la propuesta de patrullaje 
    /// </summary>
    [Column("id_nivel_riesgo")]
    public short IdNivelRiesgo { get; set; }

    /// <summary>
    /// Fecha del programa de patrullaje. Para extraordinarios sólo se pondra la fecha inicial del rango autorizado
    /// </summary>
    [Column("fecha", TypeName = "date")]
    public DateTime Fecha { get; set; }

    /// <summary>
    /// Notas asociadas al programa de patrullaje durante su autorización
    /// </summary>
    [Column("observaciones")]
    [StringLength(10)]
    [Unicode(false)]
    public string? Observaciones { get; set; }

    [ForeignKey("IdApoyoPatrullaje")]
    [InverseProperty("ProgramaPatrullajes")]
    public virtual ApoyoPatrullaje IdApoyoPatrullajeNavigation { get; set; } = null!;

    [ForeignKey("IdEstadoPatrullaje")]
    [InverseProperty("ProgramaPatrullajes")]
    public virtual EstadoPatrullaje IdEstadoPatrullajeNavigation { get; set; } = null!;

    [ForeignKey("IdNivelRiesgo")]
    [InverseProperty("ProgramaPatrullajes")]
    public virtual NivelRiesgo IdNivelRiesgoNavigation { get; set; } = null!;

    [ForeignKey("IdPuntoResponsable")]
    [InverseProperty("ProgramaPatrullajes")]
    public virtual PuntoPatrullaje IdPuntoResponsableNavigation { get; set; } = null!;

    [ForeignKey("IdRuta")]
    [InverseProperty("ProgramaPatrullajes")]
    public virtual Ruta IdRutaNavigation { get; set; } = null!;

    [ForeignKey("IdUsuario")]
    [InverseProperty("ProgramaPatrullajes")]
    public virtual Usuario IdUsuarioNavigation { get; set; } = null!;

    [InverseProperty("IdProgramaNavigation")]
    public virtual ICollection<TarjetaInformativa> TarjetaInformativas { get; } = new List<TarjetaInformativa>();
}
