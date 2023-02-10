using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Domain.Entities;

/// <summary>
/// Catálogo de los Procesos Responsables de llevar a cabo el proceso de patrullaje
/// </summary>
[Table("Proceso_Responsable", Schema = "cat")]
public partial class ProcesoResponsable
{
    /// <summary>
    /// Identificador único del proceso responsable del patrullaje
    /// </summary>
    [Key]
    [Column("id")]
    public short Id { get; set; }

    /// <summary>
    /// Identificador del tipo de proceso responsable del patrullaje asociado
    /// </summary>
    [Column("id_tipo_proceso_responsable")]
    public byte IdTipoProcesoResponsable { get; set; }

    /// <summary>
    /// Nombre de la División o Gerencia responsable del proceso
    /// </summary>
    [Column("nombre")]
    [StringLength(20)]
    [Unicode(false)]
    public string Nombre { get; set; } = null!;

    /// <summary>
    /// Acrónimo usado para facilidar la identificación del proceso responable
    /// </summary>
    [Column("clave")]
    [StringLength(2)]
    [Unicode(false)]
    public string Clave { get; set; } = null!;

    [InverseProperty("IdProcesoResponsableNavigation")]
    public virtual ICollection<Estructura> Estructuras { get; } = new List<Estructura>();

    [ForeignKey("IdTipoProcesoResponsable")]
    [InverseProperty("ProcesoResponsables")]
    public virtual TipoProcesoResponsable IdTipoProcesoResponsableNavigation { get; set; } = null!;

    [InverseProperty("IdProcesoResponsableNavigation")]
    public virtual ICollection<PuntoPatrullaje> PuntoPatrullajes { get; } = new List<PuntoPatrullaje>();
}
