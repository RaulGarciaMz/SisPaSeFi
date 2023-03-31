using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Domain.Entities;

[Keyless]
[Table("tarjetainformativareporte", Schema = "ssf")]
public partial class TarjetaInformativaReporte
{
    [Column("idtarjeta")]
    public int Idtarjeta { get; set; }

    [Column("idreporte")]
    public int Idreporte { get; set; }

    [Column("idtiporeporte")]
    public int Idtiporeporte { get; set; }
}
