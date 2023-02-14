using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Domain.Entities;

[Table("bitacoraseguimientoincidencia", Schema = "ssf")]
[Index("IdEstadoIncidencia", Name = "id_estadoIncidencia")]
[Index("IdReporte", Name = "id_reporte")]
[Index("IdUsuario", Name = "id_usuario")]
public partial class BitacoraSeguimientoIncidencia
{
    [Key]
    [Column("id_bitacoraSeguimientoIncidencia")]
    public int IdBitacoraSeguimientoIncidencia { get; set; }

    [Column("id_reporte")]
    public int IdReporte { get; set; }

    [Column("id_usuario")]
    public int IdUsuario { get; set; }

    [Column("id_estadoIncidencia")]
    public int IdEstadoIncidencia { get; set; }

    [Column("ultimaActualizacion", TypeName = "datetime")]
    public DateTime UltimaActualizacion { get; set; }

    [Column("descripcion")]
    [StringLength(150)]
    [Unicode(false)]
    public string Descripcion { get; set; } = null!;

    [InverseProperty("IdBitacoraSeguimientoIncidenciaNavigation")]
    public virtual ICollection<EvidenciaSeguimientoIncidencia> Evidenciaseguimientoincidencia { get; } = new List<EvidenciaSeguimientoIncidencia>();

    [ForeignKey("IdEstadoIncidencia")]
    [InverseProperty("Bitacoraseguimientoincidencia")]
    public virtual EstadoIncidencia IdEstadoIncidenciaNavigation { get; set; } = null!;

    [ForeignKey("IdReporte")]
    [InverseProperty("Bitacoraseguimientoincidencia")]
    public virtual ReporteEstructura IdReporteNavigation { get; set; } = null!;

    [ForeignKey("IdUsuario")]
    [InverseProperty("Bitacoraseguimientoincidencia")]
    public virtual Usuario IdUsuarioNavigation { get; set; } = null!;
}
