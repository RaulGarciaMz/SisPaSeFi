using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities;

[Table("linea", Schema = "ssf")]
[Index("IdPuntoFin", Name = "id_punto_fin")]
[Index("IdPuntoInicio", Name = "id_punto_inicio")]
public partial class Linea
{
    [Key]
    [Column("id_linea")]
    public int IdLinea { get; set; }

    [Column("clave")]
    [StringLength(150)]
    [Unicode(false)]
    public string? Clave { get; set; }

    [Column("descripcion")]
    [StringLength(150)]
    [Unicode(false)]
    public string? Descripcion { get; set; }

    [Column("id_punto_inicio")]
    public int? IdPuntoInicio { get; set; }

    [Column("id_punto_fin")]
    public int? IdPuntoFin { get; set; }

    [Column("bloqueado")]
    public int Bloqueado { get; set; }

    [Column("id_usuario")]
    public int IdUsuario { get; set; }

    [Column("ultimaActualizacion", TypeName = "datetime")]
    public DateTime UltimaActualizacion { get; set; }

    [InverseProperty("IdLineaNavigation")]
    public virtual ICollection<Estructura> Estructuras { get; } = new List<Estructura>();

    [ForeignKey("IdPuntoInicio")]
    [InverseProperty("Lineas")]
    public virtual PuntoPatrullaje? IdPuntoInicioNavigation { get; set; }
}
