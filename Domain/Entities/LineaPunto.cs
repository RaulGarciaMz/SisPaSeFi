using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Domain.Entities;

/// <summary>
/// Listado de puntos de patrullaje asociados a una línea
/// </summary>
[PrimaryKey("IdLinea", "IdPunto")]
[Table("Linea_Punto", Schema = "dmn")]
public partial class LineaPunto
{
    /// <summary>
    /// Identificador único de una línea
    /// </summary>
    [Key]
    [Column("id_linea")]
    public int IdLinea { get; set; }

    /// <summary>
    /// Identificador único de un punto de patrullaje
    /// </summary>
    [Key]
    [Column("id_punto")]
    public int IdPunto { get; set; }
}
