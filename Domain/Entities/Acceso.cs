﻿using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities;

[Table("accesos", Schema = "ssf")]
public partial class Acceso
{
    [Key]
    [Column("fecha", TypeName = "date")]
    public DateTime? Fecha { get; set; }

    [Column("totalaccesos")]
    public int? Totalaccesos { get; set; }
}
