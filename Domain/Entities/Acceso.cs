using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Domain.Entities;

[Keyless]
[Table("accesos", Schema = "ssf")]
public partial class Acceso
{
    [Column("fecha", TypeName = "date")]
    public DateTime? Fecha { get; set; }

    [Column("totalaccesos")]
    public int? Totalaccesos { get; set; }
}
