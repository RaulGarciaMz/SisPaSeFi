using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Domain.Entities;

[Table("tipopatrullaje", Schema = "ssf")]
public partial class TipoPatrullaje
{
    [Key]
    [Column("id_tipoPatrullaje")]
    public int IdTipoPatrullaje { get; set; }

    [Column("descripcion")]
    [StringLength(50)]
    [Unicode(false)]
    public string Descripcion { get; set; } = null!;

    [InverseProperty("IdTipoPatrullajeNavigation")]
    public virtual ICollection<Ruta> Ruta { get; } = new List<Ruta>();

    [InverseProperty("IdTipoPatrullajeNavigation")]
    public virtual ICollection<Vehiculo> Vehiculos { get; } = new List<Vehiculo>();
}
