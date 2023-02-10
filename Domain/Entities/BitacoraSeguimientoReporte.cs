using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Domain.Entities;

/// <summary>
/// Listado de actualizaciones a las incidencias reportadas durante el patrullaje
/// </summary>
[Table("Bitacora_Seguimiento_Reporte", Schema = "dmn")]
public partial class BitacoraSeguimientoReporte
{
    /// <summary>
    /// Identificador único de la bitácora
    /// </summary>
    [Key]
    [Column("id")]
    public int Id { get; set; }

    /// <summary>
    /// Id del reporte al que está asociada este registro de la bitácora
    /// </summary>
    [Column("id_reporte_incidencia")]
    public int IdReporteIncidencia { get; set; }

    /// <summary>
    /// Id del usuario que está insertando el registro de seguimiento
    /// </summary>
    [Column("id_usuario")]
    public int IdUsuario { get; set; }

    /// <summary>
    /// Nota descriptiva de las actividades realizadas para la corrección de la incidencia
    /// </summary>
    [Column("descripcion")]
    [StringLength(150)]
    [Unicode(false)]
    public string Descripcion { get; set; } = null!;

    /// <summary>
    /// Fecha en que se agrega el registro a la bitácora
    /// </summary>
    [Column("fecha", TypeName = "datetime")]
    public DateTime Fecha { get; set; }

    [ForeignKey("IdReporteIncidencia")]
    [InverseProperty("BitacoraSeguimientoReportes")]
    public virtual ReporteIncidencia IdReporteIncidenciaNavigation { get; set; } = null!;

    [ForeignKey("IdUsuario")]
    [InverseProperty("BitacoraSeguimientoReportes")]
    public virtual Usuario IdUsuarioNavigation { get; set; } = null!;
}
