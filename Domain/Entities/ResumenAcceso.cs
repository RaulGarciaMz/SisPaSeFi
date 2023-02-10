using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Domain.Entities;

/// <summary>
/// Conteo del número de accesos al sistema por día
/// </summary>
[Table("Resumen_Accesos", Schema = "audit")]
public partial class ResumenAcceso
{
    /// <summary>
    /// Día del cual se está haciendo el reporte de registros
    /// </summary>
    [Key]
    [Column("fecha", TypeName = "date")]
    public DateTime Fecha { get; set; }

    /// <summary>
    /// Total de accesos del día reportado
    /// </summary>
    [Column("total_accesos")]
    public short TotalAccesos { get; set; }
}
