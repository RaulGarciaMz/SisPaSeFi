using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities;

[Table("aplicaciones", Schema = "ssf")]
public partial class Aplicacion
{
    [Key]
    [Column("id_aplicacion")]
    public int IdAplicacion { get; set; }

    [Column("nombreAplicacion")]
    [StringLength(50)]
    [Unicode(false)]
    public string NombreAplicacion { get; set; } = null!;

    [Column("versionAplicacion")]
    [StringLength(50)]
    [Unicode(false)]
    public string VersionAplicacion { get; set; } = null!;
}
