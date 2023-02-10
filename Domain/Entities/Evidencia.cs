using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Domain.Entities;

/// <summary>
/// Tabla de control de evidencias derivadas de los patrullajes
/// </summary>
[Table("Evidencia", Schema = "dmn")]
public partial class Evidencia
{
    /// <summary>
    /// Identificador único de la evidencia
    /// </summary>
    [Key]
    [Column("id")]
    public int Id { get; set; }

    /// <summary>
    /// Id del tipo de evidencia que se está agregando
    /// </summary>
    [Column("it_tipo_evidencia")]
    public byte ItTipoEvidencia { get; set; }

    /// <summary>
    /// Id del reporte o bitácora a la que está asociada la evidencia
    /// </summary>
    [Column("id_elemento_asociado")]
    public int IdElementoAsociado { get; set; }

    [ForeignKey("ItTipoEvidencia")]
    [InverseProperty("Evidencia")]
    public virtual TipoEvidencia ItTipoEvidenciaNavigation { get; set; } = null!;
}
