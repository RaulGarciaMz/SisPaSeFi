using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities;

[Keyless]
public partial class Vista2
{
    [Column("id_programa")]
    public int IdPrograma { get; set; }

    [Column("vehiculos")]
    public int? Vehiculos { get; set; }

    [Column("matricula")]
    [StringLength(8000)]
    [Unicode(false)]
    public string? Matricula { get; set; }
}
