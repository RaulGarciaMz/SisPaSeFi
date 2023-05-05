using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities;

[Table("localidades", Schema = "ssf")]
public partial class Localidad
{
    [Key]
    [Column("id_localidad")]
    public int IdLocalidad { get; set; }

    [Column("id_municipio")]
    public int IdMunicipio { get; set; }

    [Column("clave")]
    [StringLength(50)]
    [Unicode(false)]
    public string Clave { get; set; } = null!;

    [Column("nombre")]
    [StringLength(200)]
    [Unicode(false)]
    public string Nombre { get; set; } = null!;

    [Column("nombre_corto")]
    [StringLength(200)]
    [Unicode(false)]
    public string? NombreCorto { get; set; }

    [Column("ambito")]
    [StringLength(50)]
    [Unicode(false)]
    public string? Ambito { get; set; }

    [Column("latitud")]
    public float? Latitud { get; set; }

    [Column("longitud")]
    public float? Longitud { get; set; }
}
