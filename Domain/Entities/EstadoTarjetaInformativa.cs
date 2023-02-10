using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Domain.Entities;

/// <summary>
/// Catálogo de estados que puede tener una Tarjeta informativa
/// </summary>
[Table("Estado_Tarjeta_Informativa", Schema = "cat")]
public partial class EstadoTarjetaInformativa
{
    /// <summary>
    /// Identificador único del estado de la tarjeta informativa
    /// </summary>
    [Key]
    [Column("id")]
    public byte Id { get; set; }

    /// <summary>
    /// Nombre del estado de la tarjeta informativa
    /// </summary>
    [Column("estado")]
    [StringLength(50)]
    [Unicode(false)]
    public string Estado { get; set; } = null!;
}
