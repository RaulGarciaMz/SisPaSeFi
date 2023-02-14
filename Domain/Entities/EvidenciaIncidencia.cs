using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Domain.Entities;

[Table("evidenciaincidencias", Schema = "ssf")]
[Index("IdReporte", Name = "id_reporte")]
public partial class EvidenciaIncidencia
{
    [Key]
    [Column("id_evidenciaIncidencia")]
    public int IdEvidenciaIncidencia { get; set; }

    [Column("id_reporte")]
    public int IdReporte { get; set; }

    [Column("rutaArchivo")]
    [StringLength(150)]
    [Unicode(false)]
    public string RutaArchivo { get; set; } = null!;

    [Column("nombreArchivo")]
    [StringLength(150)]
    [Unicode(false)]
    public string NombreArchivo { get; set; } = null!;

    [Column("ultimaActualizacion", TypeName = "datetime")]
    public DateTime UltimaActualizacion { get; set; }

    [Column("descripcion")]
    [StringLength(150)]
    [Unicode(false)]
    public string? Descripcion { get; set; }

    [ForeignKey("IdReporte")]
    [InverseProperty("Evidenciaincidencia")]
    public virtual ReporteEstructura IdReporteNavigation { get; set; } = null!;
}
