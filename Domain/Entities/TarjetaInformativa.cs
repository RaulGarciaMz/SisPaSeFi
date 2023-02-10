using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Domain.Entities;

/// <summary>
/// Listado de tarjetas informativas asociadas a un Programa de patrullaje
/// </summary>
[Table("Tarjeta_Informativa", Schema = "dmn")]
public partial class TarjetaInformativa
{
    /// <summary>
    /// Identificador único de la nota informativa
    /// </summary>
    [Key]
    [Column("id")]
    public int Id { get; set; }

    /// <summary>
    /// Identificador único del programa de patrullaje
    /// </summary>
    [Column("id_programa")]
    public int IdPrograma { get; set; }

    /// <summary>
    /// Id del usuario que eestá dando de alta la nota
    /// </summary>
    [Column("id_usurio")]
    public int IdUsurio { get; set; }

    /// <summary>
    /// Id del estado en que se encuentra la tarjeta informativa
    /// </summary>
    [Column("id_estado_tarjeta_informativa")]
    public byte IdEstadoTarjetaInformativa { get; set; }

    /// <summary>
    /// Fecha real en que se está realizando el patrullaje
    /// </summary>
    [Column("fecha_patrullaje", TypeName = "date")]
    public DateTime FechaPatrullaje { get; set; }

    /// <summary>
    /// Estampa de tiempo del momento en que inició el patrullaje
    /// </summary>
    [Column("inicio", TypeName = "datetime")]
    public DateTime Inicio { get; set; }

    /// <summary>
    /// Estampa de tiempo del momento en que terminó el patrullaje
    /// </summary>
    [Column("termino", TypeName = "datetime")]
    public DateTime? Termino { get; set; }

    /// <summary>
    /// Tiempo que la unidad aérea estuvo en vuelo
    /// </summary>
    [Column("tiempo_vuelo")]
    public TimeSpan? TiempoVuelo { get; set; }

    /// <summary>
    /// Tiempo transcurrido desde que se encendió el vehículo hasta que se apagó
    /// </summary>
    [Column("calzo_a_calzo")]
    public TimeSpan? CalzoACalzo { get; set; }

    /// <summary>
    /// Notas asociadas a la tarjeta informativa
    /// </summary>
    [Column("observaciones")]
    [StringLength(1000)]
    [Unicode(false)]
    public string? Observaciones { get; set; }

    /// <summary>
    /// Cantidad de KM que se recorrieron durante el patrullaje
    /// </summary>
    [Column("km_recorridos")]
    public short KmRecorridos { get; set; }

    [Column("uso_fuerza_reaccion")]
    public bool UsoFuerzaReaccion { get; set; }

    /// <summary>
    /// Fecha del momento en que se creo la tarjeta informativa
    /// </summary>
    [Column("fecha_reporte_tarjeta", TypeName = "datetime")]
    public DateTime? FechaReporteTarjeta { get; set; }

    /// <summary>
    /// Fecha de la última actualización a la tarjeta informativa
    /// </summary>
    [Column("fecha_ultima_actualizacion", TypeName = "datetime")]
    public DateTime FechaUltimaActualizacion { get; set; }

    [ForeignKey("IdPrograma")]
    [InverseProperty("TarjetaInformativas")]
    public virtual ProgramaPatrullaje IdProgramaNavigation { get; set; } = null!;

    [InverseProperty("IdTarjetaInformativaNavigation")]
    public virtual ICollection<PersonalParticipantePatrullaje> PersonalParticipantePatrullajes { get; } = new List<PersonalParticipantePatrullaje>();

    [InverseProperty("IdTarjetaInformativaNavigation")]
    public virtual ICollection<Posicionamiento> Posicionamientos { get; } = new List<Posicionamiento>();

    [InverseProperty("IdTarjetaInformativaNavigation")]
    public virtual ICollection<ReporteIncidencia> ReporteIncidencia { get; } = new List<ReporteIncidencia>();

    [InverseProperty("IdTarjetaInformativaNavigation")]
    public virtual ICollection<UsoVehiculo> UsoVehiculos { get; } = new List<UsoVehiculo>();
}
