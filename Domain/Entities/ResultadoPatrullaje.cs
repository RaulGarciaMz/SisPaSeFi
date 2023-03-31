using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ScaffoldSSF.Models;

[Table("resultadopatrullaje", Schema = "ssf")]
public partial class ResultadoPatrullaje
{
    [Key]
    [Column("idresultadopatrullaje")]
    public int IdResultadoPatrullaje { get; set; }

    [Column("descripcion")]
    [StringLength(100)]
    [Unicode(false)]
    public string Descripcion { get; set; } = null!;
}
