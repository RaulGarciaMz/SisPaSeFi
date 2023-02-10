using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Domain.Entities;

/// <summary>
/// Catálogo de los tipos de procesos responsables de los patrullajes
/// </summary>
[Table("Tipo_Proceso_Responsable", Schema = "cat")]
public partial class TipoProcesoResponsable
{
    /// <summary>
    /// Identificador único del tipo de proceso responsable del patrullaje
    /// </summary>
    [Key]
    [Column("id")]
    public byte Id { get; set; }

    /// <summary>
    /// Nombre del tipo de proceso responsable de llevar a cabo los patrullajes
    /// </summary>
    [Column("nombre")]
    [StringLength(20)]
    [Unicode(false)]
    public string Nombre { get; set; } = null!;

    [InverseProperty("IdTipoProcesoResponsableNavigation")]
    public virtual ICollection<ProcesoResponsable> ProcesoResponsables { get; } = new List<ProcesoResponsable>();
}
