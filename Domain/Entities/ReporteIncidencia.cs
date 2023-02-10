using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Domain.Entities;

/// <summary>
/// Reporte de incidencias detectadas durante los patrullajes (tanto de puntos como de estructuras)
/// </summary>
[Table("Reporte_Incidencia", Schema = "dmn")]
public partial class ReporteIncidencia
{
    /// <summary>
    /// Identificador único del reporte
    /// </summary>
    [Key]
    [Column("id")]
    public int Id { get; set; }

    /// <summary>
    /// Id de la tarjeta informativa a la que se asocia el reporte
    /// </summary>
    [Column("id_tarjeta_informativa")]
    public int IdTarjetaInformativa { get; set; }

    /// <summary>
    /// Id del tipo de reporte (punto o estructura)
    /// </summary>
    [Column("id_tipo_reporte")]
    public byte IdTipoReporte { get; set; }

    /// <summary>
    /// Punto o estructura sobre la que se está levantado el reporte de incidencia
    /// </summary>
    [Column("id_elemento")]
    public int IdElemento { get; set; }

    /// <summary>
    /// Estado que guarda la incidencia a lo largo de su ciclo de vida
    /// </summary>
    [Column("id_estado_incidencia")]
    public byte IdEstadoIncidencia { get; set; }

    /// <summary>
    /// Id del nivel de riesgo que representa la incidencia
    /// </summary>
    [Column("id_nivel_riesgo")]
    public short IdNivelRiesgo { get; set; }

    /// <summary>
    /// Identificador del tipo de incidencia
    /// </summary>
    [Column("id_tipo_incidencia")]
    public byte IdTipoIncidencia { get; set; }

    /// <summary>
    /// Descripción de la incidencia encontrada
    /// </summary>
    [Column("incidencia")]
    [StringLength(800)]
    [Unicode(false)]
    public string Incidencia { get; set; } = null!;

    /// <summary>
    /// Fecha en la que se realizó la última actualización sobre el reporte
    /// </summary>
    [Column("ultima_actualizacion", TypeName = "datetime")]
    public DateTime UltimaActualizacion { get; set; }

    [InverseProperty("IdReporteNavigation")]
    public virtual ICollection<AfectacionIncidencia> AfectacionIncidencia { get; } = new List<AfectacionIncidencia>();

    [InverseProperty("IdReporteIncidenciaNavigation")]
    public virtual ICollection<BitacoraSeguimientoReporte> BitacoraSeguimientoReportes { get; } = new List<BitacoraSeguimientoReporte>();

    [ForeignKey("IdEstadoIncidencia")]
    [InverseProperty("ReporteIncidencia")]
    public virtual EstadoIncidencia IdEstadoIncidenciaNavigation { get; set; } = null!;

    [ForeignKey("IdNivelRiesgo")]
    [InverseProperty("ReporteIncidencia")]
    public virtual NivelRiesgo IdNivelRiesgoNavigation { get; set; } = null!;

    [ForeignKey("IdTarjetaInformativa")]
    [InverseProperty("ReporteIncidencia")]
    public virtual TarjetaInformativa IdTarjetaInformativaNavigation { get; set; } = null!;

    [ForeignKey("IdTipoIncidencia")]
    [InverseProperty("ReporteIncidencia")]
    public virtual TipoInicidencia IdTipoIncidenciaNavigation { get; set; } = null!;

    [ForeignKey("IdTipoReporte")]
    [InverseProperty("ReporteIncidencia")]
    public virtual TipoReporte IdTipoReporteNavigation { get; set; } = null!;
}
