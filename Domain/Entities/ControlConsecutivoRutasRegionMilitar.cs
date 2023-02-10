using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Domain.Entities;

/// <summary>
/// Tabla de control del consecutivo de rutas de las Regiones Militales de la SDN
/// </summary>
[Table("Control_Consecutivo_Rutas_Region_Militar", Schema = "dmn")]
public partial class ControlConsecutivoRutasRegionMilitar
{
    /// <summary>
    /// Identficador único de la Region Militar
    /// </summary>
    [Key]
    [Column("id")]
    public short Id { get; set; }

    /// <summary>
    /// Identificador en formato de número romano
    /// </summary>
    [Column("alias")]
    [StringLength(10)]
    [Unicode(false)]
    public string Alias { get; set; } = null!;

    /// <summary>
    /// Número de consecutivo de la ruta. Este número se controla por cada Region militar. 
    /// </summary>
    [Column("consecutivo")]
    public short Consecutivo { get; set; }
}
