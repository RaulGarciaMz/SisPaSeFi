using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Domain.Entities;

[Table("Municipio", Schema = "cat")]
public partial class Municipio
{
    /// <summary>
    /// Identificador único del Municio
    /// </summary>
    [Key]
    [Column("id")]
    public short Id { get; set; }

    /// <summary>
    /// Identificador del Estado al que pertenece el Municipio
    /// </summary>
    [Column("id_estado")]
    public byte IdEstado { get; set; }

    /// <summary>
    /// Acrónimo del Municipio
    /// </summary>
    [Column("clave")]
    [StringLength(5)]
    [Unicode(false)]
    public string Clave { get; set; } = null!;

    /// <summary>
    /// Nombre del Municipio
    /// </summary>
    [Column("nombre")]
    [StringLength(100)]
    [Unicode(false)]
    public string Nombre { get; set; } = null!;

    [InverseProperty("IdMunicipioNavigation")]
    public virtual ICollection<Estructura> Estructuras { get; } = new List<Estructura>();

    [ForeignKey("IdEstado")]
    [InverseProperty("Municipios")]
    public virtual EstadoPais IdEstadoNavigation { get; set; } = null!;

    [InverseProperty("IdMunicipioNavigation")]
    public virtual ICollection<PuntoPatrullaje> PuntoPatrullajes { get; } = new List<PuntoPatrullaje>();
}
