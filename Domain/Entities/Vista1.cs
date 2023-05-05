using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities;

[Keyless]
public partial class Vista1
{
    [Column("id_ruta")]
    public int IdRuta { get; set; }

    [Column("clave")]
    [StringLength(50)]
    [Unicode(false)]
    public string Clave { get; set; } = null!;

    [Column("ruta")]
    [StringLength(8000)]
    [Unicode(false)]
    public string? Ruta { get; set; }

    [Column("region")]
    [StringLength(5)]
    [Unicode(false)]
    public string? Region { get; set; }

    [Column("id_tipoPatrullaje")]
    public int IdTipoPatrullaje { get; set; }

    [Column("regionsdn")]
    [StringLength(5)]
    [Unicode(false)]
    public string? Regionsdn { get; set; }
}
