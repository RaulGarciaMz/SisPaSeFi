using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities;

[Keyless]
[Table("temppuntosderutasporestado", Schema = "ssf")]
public partial class TempPuntosdeRutasporEstado
{
    [Column("id_ruta")]
    public int? IdRuta { get; set; }

    [Column("nombre")]
    [StringLength(50)]
    [Unicode(false)]
    public string? Nombre { get; set; }

    public int? Total { get; set; }
}
