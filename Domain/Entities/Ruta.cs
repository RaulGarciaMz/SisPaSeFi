using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Domain.Entities;

[Table("rutas", Schema = "ssf")]
[Index("IdTipoPatrullaje", Name = "id_tipoPatrullaje")]
public partial class Ruta
{
    [Key]
    [Column("id_ruta")]
    public int IdRuta { get; set; }

    [Column("clave")]
    [StringLength(50)]
    [Unicode(false)]
    public string Clave { get; set; } = null!;

    [Column("regionMilitarSDN")]
    [StringLength(3)]
    [Unicode(false)]
    public string RegionMilitarSdn { get; set; } = null!;

    [Column("regionSSF")]
    [StringLength(3)]
    [Unicode(false)]
    public string RegionSsf { get; set; } = null!;

    [Column("id_tipoPatrullaje")]
    public int IdTipoPatrullaje { get; set; }

    [Column("bloqueado")]
    public int Bloqueado { get; set; }

    [Column("zonaMilitarSDN")]
    public int ZonaMilitarSdn { get; set; }

    [StringLength(100)]
    [Unicode(false)]
    public string? Observaciones { get; set; }

    [Column("consecutivoRegionMilitarSDN")]
    public int ConsecutivoRegionMilitarSdn { get; set; }

    [Column("totalRutasRegionMilitarSDN")]
    public int TotalRutasRegionMilitarSdn { get; set; }

    [Column("ultimaActualizacion", TypeName = "datetime")]
    public DateTime UltimaActualizacion { get; set; }

    [Column("habilitado")]
    public int Habilitado { get; set; }

    [ForeignKey("IdTipoPatrullaje")]
    [InverseProperty("Ruta")]
    public virtual TipoPatrullaje IdTipoPatrullajeNavigation { get; set; } = null!;

    [InverseProperty("IdRutaNavigation")]
    public virtual ICollection<Itinerario> Itinerarios { get; } = new List<Itinerario>();

    [InverseProperty("IdRutaNavigation")]
    public virtual ICollection<ProgramaPatrullaje> Programapatrullajes { get; } = new List<ProgramaPatrullaje>();

    [InverseProperty("IdRutaNavigation")]
    public virtual ICollection<PropuestaPatrullaje> Propuestaspatrullajes { get; } = new List<PropuestaPatrullaje>();
}
