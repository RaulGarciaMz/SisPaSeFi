using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Domain.Entities;

[Table("municipios", Schema = "ssf")]
[Index("IdEstado", Name = "id_estado")]
public partial class Municipio
{
    [Key]
    [Column("id_municipio")]
    public int IdMunicipio { get; set; }

    [Column("id_estado")]
    public int IdEstado { get; set; }

    [Column("clave")]
    [StringLength(3)]
    [Unicode(false)]
    public string Clave { get; set; } = null!;

    [Column("nombre")]
    [StringLength(100)]
    [Unicode(false)]
    public string Nombre { get; set; } = null!;

    [Column("nombre_corto")]
    [StringLength(4)]
    [Unicode(false)]
    public string NombreCorto { get; set; } = null!;

    [InverseProperty("IdMunicipioNavigation")]
    public virtual ICollection<Estructura> Estructuras { get; } = new List<Estructura>();

    [ForeignKey("IdEstado")]
    [InverseProperty("Municipios")]
    public virtual EstadoPais IdEstadoNavigation { get; set; } = null!;

    [InverseProperty("IdMunicipioNavigation")]
    public virtual ICollection<PuntoPatrullaje> Puntospatrullajes { get; } = new List<PuntoPatrullaje>();
}
