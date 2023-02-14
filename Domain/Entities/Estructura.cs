using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Domain.Entities;

[Table("estructura", Schema = "ssf")]
[Index("IdLinea", Name = "id_linea")]
[Index("IdMunicipio", Name = "id_municipio")]
public partial class Estructura
{
    [Key]
    [Column("id_estructura")]
    public int IdEstructura { get; set; }

    [Column("nombre")]
    [StringLength(50)]
    [Unicode(false)]
    public string Nombre { get; set; } = null!;

    [Column("coordenadas")]
    [StringLength(50)]
    [Unicode(false)]
    public string Coordenadas { get; set; } = null!;

    [Column("id_linea")]
    public int? IdLinea { get; set; }

    [Column("id_municipio")]
    public int IdMunicipio { get; set; }

    [Column("bloqueado")]
    public int Bloqueado { get; set; }

    [Column("latitud")]
    [StringLength(25)]
    [Unicode(false)]
    public string? Latitud { get; set; }

    [Column("longitud")]
    [StringLength(25)]
    [Unicode(false)]
    public string? Longitud { get; set; }

    [Column("ultimaActualizacion", TypeName = "datetime")]
    public DateTime UltimaActualizacion { get; set; }

    [Column("modif")]
    [StringLength(1)]
    [Unicode(false)]
    public string? Modif { get; set; }

    [Column("temporal")]
    [StringLength(50)]
    [Unicode(false)]
    public string? Temporal { get; set; }

    [Column("id_ProcesoResponsable")]
    public int IdProcesoResponsable { get; set; }

    [Column("id_GerenciaDivision")]
    public int IdGerenciaDivision { get; set; }

    [ForeignKey("IdLinea")]
    [InverseProperty("Estructuras")]
    public virtual Linea? IdLineaNavigation { get; set; }

    [ForeignKey("IdMunicipio")]
    [InverseProperty("Estructuras")]
    public virtual Municipio IdMunicipioNavigation { get; set; } = null!;

    [InverseProperty("IdEstructuraNavigation")]
    public virtual ICollection<ReporteEstructura> Reporteestructuras { get; } = new List<ReporteEstructura>();
}
