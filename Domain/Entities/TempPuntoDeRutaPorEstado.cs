using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Domain.Entities;

[Keyless]
[Table("temppuntosderutasporestado", Schema = "ssf")]
public partial class TempPuntoDeRutaPorEstado
{
    [Column("id_ruta")]
    public int? IdRuta { get; set; }

    [Column("nombre")]
    [StringLength(50)]
    [Unicode(false)]
    public string? Nombre { get; set; }

    public int? Total { get; set; }
}
