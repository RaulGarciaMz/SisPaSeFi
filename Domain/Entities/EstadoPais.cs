using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Domain.Entities;

/// <summary>
/// Catálogo de Estados integrantes de México
/// </summary>
[Table("Estado_Pais", Schema = "cat")]
public partial class EstadoPais
{
    /// <summary>
    /// Identificador único del Estado
    /// </summary>
    [Key]
    [Column("id")]
    public byte Id { get; set; }

    /// <summary>
    /// Acrónimo del estado
    /// </summary>
    [Column("clave")]
    [StringLength(2)]
    [Unicode(false)]
    public string Clave { get; set; } = null!;

    /// <summary>
    /// Nombre del estado
    /// </summary>
    [Column("nombre")]
    [StringLength(50)]
    [Unicode(false)]
    public string Nombre { get; set; } = null!;

    /// <summary>
    /// Nombre corto o abreviatura del Estado
    /// </summary>
    [Column("nombre_corto")]
    [StringLength(10)]
    [Unicode(false)]
    public string NombreCorto { get; set; } = null!;

    [InverseProperty("IdEstadoNavigation")]
    public virtual ICollection<Municipio> Municipios { get; } = new List<Municipio>();
}
