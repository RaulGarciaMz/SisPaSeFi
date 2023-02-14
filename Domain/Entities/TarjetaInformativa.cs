using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Domain.Entities;

[Table("tarjetainformativa", Schema = "ssf")]
[Index("IdEstadoTarjetaInformativa", Name = "id_estadoTarjetaInformativa")]
[Index("IdPrograma", Name = "id_programa")]
[Index("IdUsuario", Name = "id_usuario")]
public partial class TarjetaInformativa
{
    [Key]
    [Column("id_nota")]
    public int IdNota { get; set; }

    [Column("id_programa")]
    public int IdPrograma { get; set; }

    [Column("id_usuario")]
    public int IdUsuario { get; set; }

    [Column("ultimaActualizacion", TypeName = "datetime")]
    public DateTime UltimaActualizacion { get; set; }

    [Column("inicio")]
    public TimeSpan Inicio { get; set; }

    [Column("termino")]
    public TimeSpan Termino { get; set; }

    [Column("tiempoVuelo")]
    public TimeSpan TiempoVuelo { get; set; }

    [Column("calzoAcalzo")]
    public TimeSpan CalzoAcalzo { get; set; }

    [Column("observaciones")]
    [StringLength(1000)]
    [Unicode(false)]
    public string Observaciones { get; set; } = null!;

    [Column("fechaPatrullaje", TypeName = "date")]
    public DateTime? FechaPatrullaje { get; set; }

    [Column("comandantesInstalacionSSF")]
    public int ComandantesInstalacionSsf { get; set; }

    [Column("personalMilitarSEDENAOficial")]
    public int PersonalMilitarSedenaoficial { get; set; }

    [Column("kmRecorrido")]
    public int KmRecorrido { get; set; }

    [Column("id_estadoTarjetaInformativa")]
    public int IdEstadoTarjetaInformativa { get; set; }

    [Column("personalMilitarSEDENATropa")]
    public int PersonalMilitarSedenatropa { get; set; }

    [Column("linieros")]
    public int Linieros { get; set; }

    [Column("comandantesTurnoSSF")]
    public int ComandantesTurnoSsf { get; set; }

    [Column("oficialesSSF")]
    public int OficialesSsf { get; set; }

    [Column("personalNavalSEMAROficial")]
    public int PersonalNavalSemaroficial { get; set; }

    [Column("personalNavalSEMARTropa")]
    public int PersonalNavalSemartropa { get; set; }

    [ForeignKey("IdEstadoTarjetaInformativa")]
    [InverseProperty("Tarjetainformativas")]
    public virtual EstadoTarjetaInformativa IdEstadoTarjetaInformativaNavigation { get; set; } = null!;

    [ForeignKey("IdPrograma")]
    [InverseProperty("Tarjetainformativas")]
    public virtual ProgramaPatrullaje IdProgramaNavigation { get; set; } = null!;

    [ForeignKey("IdUsuario")]
    [InverseProperty("Tarjetainformativas")]
    public virtual Usuario IdUsuarioNavigation { get; set; } = null!;
}
