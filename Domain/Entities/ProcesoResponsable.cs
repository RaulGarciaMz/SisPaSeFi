using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities;

[Table("procesosresponsables", Schema = "ssf")]
public partial class ProcesoResponsable
{
    [Key]
    [Column("id_procesoResponsable")]
    public int IdProcesoResponsable { get; set; }

    [Column("nombre")]
    [StringLength(20)]
    [Unicode(false)]
    public string Nombre { get; set; } = null!;

    [Column("tabla")]
    [StringLength(50)]
    [Unicode(false)]
    public string Tabla { get; set; } = null!;

    [Column("id_GrupoCorreo")]
    public int IdGrupoCorreo { get; set; }
}
