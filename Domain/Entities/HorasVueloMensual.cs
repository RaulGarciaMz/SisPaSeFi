using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities;

[Keyless]
[Table("horasvuelomensuales", Schema = "ssf")]
public partial class HorasVueloMensual
{
    [Column("mes")]
    public int? Mes { get; set; }

    [Column("anio")]
    public int? Anio { get; set; }

    [Column("horas")]
    public int? Horas { get; set; }
}
