using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Domain.Entities;

/// <summary>
/// Relación de personal que apoya durante un Patrullaje
/// </summary>
[PrimaryKey("IdTarjetaInformativa", "IdTipoPersonalPatrullaje")]
[Table("Personal_Participante_Patrullaje", Schema = "dmn")]
public partial class PersonalParticipantePatrullaje
{
    /// <summary>
    /// Identificador del programa de patrullaje
    /// </summary>
    [Key]
    [Column("id_tarjeta_informativa")]
    public int IdTarjetaInformativa { get; set; }

    /// <summary>
    /// Identificador del tipo de personal participante durante un patrullaje
    /// </summary>
    [Key]
    [Column("id_tipo_personal_patrullaje")]
    public byte IdTipoPersonalPatrullaje { get; set; }

    /// <summary>
    /// Número de personas del tipo seleccionado que participan en un patrullaje
    /// </summary>
    [Column("cantidad")]
    public byte Cantidad { get; set; }

    [ForeignKey("IdTarjetaInformativa")]
    [InverseProperty("PersonalParticipantePatrullajes")]
    public virtual TarjetaInformativa IdTarjetaInformativaNavigation { get; set; } = null!;

    [ForeignKey("IdTipoPersonalPatrullaje")]
    [InverseProperty("PersonalParticipantePatrullajes")]
    public virtual TipoPersonalPatrullaje IdTipoPersonalPatrullajeNavigation { get; set; } = null!;
}
