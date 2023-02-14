using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Domain.Entities;

[Table("semana", Schema = "ssf")]
public partial class Semana
{
    [Key]
    [Column("date", TypeName = "date")]
    public DateTime Date { get; set; }

    /// <summary>
    /// AdventureWorks2012 Sample Database
    /// </summary>
    [Column("inicio", TypeName = "date")]
    public DateTime? Inicio { get; set; }

    /// <summary>
    /// Fecha final
    /// </summary>
    [Column("fin", TypeName = "date")]
    public DateTime? Fin { get; set; }

    [Column("name")]
    [StringLength(100)]
    [Unicode(false)]
    public string? Name { get; set; }
}
