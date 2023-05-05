using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities;

[Table("gruposcorreoelectronico", Schema = "ssf")]
public partial class GrupoCorreoElectronico
{
    [Key]
    [Column("Id_GrupoCorreo")]
    public int IdGrupoCorreo { get; set; }

    [Column("nombre_grupo")]
    [StringLength(100)]
    [Unicode(false)]
    public string NombreGrupo { get; set; } = null!;

    [Column("descripcion_grupo")]
    [StringLength(100)]
    [Unicode(false)]
    public string? DescripcionGrupo { get; set; }
}
