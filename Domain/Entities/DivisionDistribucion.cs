using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities;

[Table("divisiondistribucion", Schema = "ssf")]
public partial class DivisionDistribucion
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("nombre")]
    [StringLength(50)]
    [Unicode(false)]
    public string Nombre { get; set; } = null!;

    [Column("clave")]
    [StringLength(10)]
    [Unicode(false)]
    public string Clave { get; set; } = null!;

    [Column("id_GrupoCorreo")]
    public int IdGrupoCorreo { get; set; }
}
