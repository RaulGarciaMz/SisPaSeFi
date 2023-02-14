using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Domain.Entities;

[Table("evidenciausovehiculo", Schema = "ssf")]
[Index("IdUsoVehiculo", Name = "id_usoVehiculo")]
public partial class EvidenciaUsoVehiculo
{
    [Key]
    [Column("id_evidenciaUsoVehiculo")]
    public int IdEvidenciaUsoVehiculo { get; set; }

    [Column("id_usoVehiculo")]
    public int IdUsoVehiculo { get; set; }

    [Column("rutaArchivo")]
    [StringLength(150)]
    [Unicode(false)]
    public string RutaArchivo { get; set; } = null!;

    [Column("nombreArchivo")]
    [StringLength(150)]
    [Unicode(false)]
    public string NombreArchivo { get; set; } = null!;

    [Column("descripcion")]
    [StringLength(150)]
    [Unicode(false)]
    public string? Descripcion { get; set; }

    [Column("ultimaActualizacion", TypeName = "datetime")]
    public DateTime UltimaActualizacion { get; set; }

    [ForeignKey("IdUsoVehiculo")]
    [InverseProperty("Evidenciausovehiculos")]
    public virtual UsoVehiculo IdUsoVehiculoNavigation { get; set; } = null!;
}
