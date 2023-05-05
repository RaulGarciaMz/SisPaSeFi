using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

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
