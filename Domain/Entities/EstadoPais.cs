using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Domain.Entities;

[Table("estadospais", Schema = "ssf")]
public partial class EstadoPais
{
    [Key]
    [Column("id_estado")]
    public int IdEstado { get; set; }

    [Column("clave")]
    [StringLength(2)]
    [Unicode(false)]
    public string Clave { get; set; } = null!;

    [Column("nombre")]
    [StringLength(50)]
    [Unicode(false)]
    public string Nombre { get; set; } = null!;

    [Column("nombre_corto")]
    [StringLength(16)]
    [Unicode(false)]
    public string NombreCorto { get; set; } = null!;

    [InverseProperty("IdEstadoNavigation")]
    public virtual ICollection<Municipio> Municipios { get; } = new List<Municipio>();
}
