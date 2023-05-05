﻿using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities;

[Table("nivelriesgo", Schema = "ssf")]
public partial class NivelRiesgo
{
    [Key]
    [Column("id_nivelRiesgo")]
    public int IdNivelRiesgo { get; set; }

    [Column("nivel")]
    [StringLength(50)]
    [Unicode(false)]
    public string Nivel { get; set; } = null!;

    [Column("descripcion")]
    [StringLength(50)]
    [Unicode(false)]
    public string Descripcion { get; set; } = null!;
}
