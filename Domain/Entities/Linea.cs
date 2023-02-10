using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Domain.Entities;

/// <summary>
/// Listado de líneas de transmisión sujetas a patrullaje
/// </summary>
[Table("Linea", Schema = "dmn")]
public partial class Linea
{
    /// <summary>
    /// Identificador único de la línea de transmisión
    /// </summary>
    [Key]
    [Column("id")]
    public int Id { get; set; }

    /// <summary>
    /// Identificador del punto de patrullaje donde inicia la línea
    /// </summary>
    [Column("id_punto_inicio")]
    public int IdPuntoInicio { get; set; }

    /// <summary>
    /// Identificador del punto de patrullaje donde termina la línea
    /// </summary>
    [Column("id_punto_fin")]
    public int IdPuntoFin { get; set; }

    /// <summary>
    /// Identificador del usuario que dio de alta la línea
    /// </summary>
    [Column("id_usuario")]
    public int IdUsuario { get; set; }

    /// <summary>
    /// Nombre corto de la línea
    /// </summary>
    [Column("clave")]
    [StringLength(150)]
    [Unicode(false)]
    public string Clave { get; set; } = null!;

    /// <summary>
    /// Texto descriptivo de la línea
    /// </summary>
    [Column("descripcion")]
    [StringLength(150)]
    [Unicode(false)]
    public string? Descripcion { get; set; }

    /// <summary>
    /// Indicador de sí la línea está bloqueada para su incorporación a una propuesta o programa de patrullaje
    /// </summary>
    [Column("bloqueado")]
    public bool? Bloqueado { get; set; }

    /// <summary>
    /// Fecha de la última actualización
    /// </summary>
    [Column("ultima_actualizacion", TypeName = "datetime")]
    public DateTime UltimaActualizacion { get; set; }

    [InverseProperty("IdLineaNavigation")]
    public virtual ICollection<Estructura> Estructuras { get; } = new List<Estructura>();
}
