using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Domain.Entities;

[Table("permisosedicionprocesoconduccion", Schema = "ssf")]
public partial class Permisosedicionprocesoconduccion
{
    [Key]
    [Column("idpermisoedicionprocesoconduccion")]
    public int Idpermisoedicionprocesoconduccion { get; set; }

    [Column("regionssf")]
    public int Regionssf { get; set; }

    [Column("mes")]
    public int Mes { get; set; }

    [Column("anio")]
    public int Anio { get; set; }
}
