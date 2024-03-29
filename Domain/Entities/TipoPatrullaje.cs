﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Domain.Entities;

/// <summary>
/// Listado de tipos de patrullajes (en referencia al medio de transporte)
/// </summary>
[Table("Tipo_Patrullaje", Schema = "cat")]
public partial class TipoPatrullaje
{
    /// <summary>
    /// Identificador único del tipo de patrullaje 
    /// </summary>
    [Key]
    [Column("id")]
    public byte Id { get; set; }

    /// <summary>
    /// Nombre descriptivo del tipo de patrullaje
    /// </summary>
    [Column("nombre")]
    [StringLength(50)]
    [Unicode(false)]
    public string Nombre { get; set; } = null!;

    [InverseProperty("IdTipoPatrullajeNavigation")]
    public virtual ICollection<Ruta> Ruta { get; } = new List<Ruta>();

    [InverseProperty("IdTipoPatrullajeNavigation")]
    public virtual ICollection<Vehiculo> Vehiculos { get; } = new List<Vehiculo>();
}
