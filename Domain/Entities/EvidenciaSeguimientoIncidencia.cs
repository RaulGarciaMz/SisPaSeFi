using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Domain.Entities;

[Table("evidenciaseguimientoincidencia", Schema = "ssf")]
[Index("IdBitacoraSeguimientoIncidencia", Name = "id_bitacoraSeguimientoIncidencia")]
public partial class EvidenciaSeguimientoIncidencia
{
    [Key]
    [Column("id_evidenciaSeguimientoIncidencia")]
    public int IdEvidenciaSeguimientoIncidencia { get; set; }

    [Column("id_bitacoraSeguimientoIncidencia")]
    public int IdBitacoraSeguimientoIncidencia { get; set; }

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

    [ForeignKey("IdBitacoraSeguimientoIncidencia")]
    [InverseProperty("Evidenciaseguimientoincidencia")]
    public virtual BitacoraSeguimientoIncidencia IdBitacoraSeguimientoIncidenciaNavigation { get; set; } = null!;
}
