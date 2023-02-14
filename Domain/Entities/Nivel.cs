using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Domain.Entities;

[Table("niveles", Schema = "ssf")]
public partial class Nivel
{
    [Key]
    [Column("id_nivel")]
    public int IdNivel { get; set; }

    [Column("descripcionNivel")]
    [StringLength(50)]
    [Unicode(false)]
    public string DescripcionNivel { get; set; } = null!;
}
