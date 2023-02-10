using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Domain.Entities;

/// <summary>
/// Catálogo de las Comandancias Regionales de la SSF
/// </summary>
[Table("Comandancia_Regional", Schema = "cat")]
public partial class ComandanciaRegional
{
    /// <summary>
    /// Identificador único de la Comandancia
    /// </summary>
    [Key]
    [Column("id")]
    public short Id { get; set; }

    /// <summary>
    /// Alias del ID de la comandancia en formato de número romano
    /// </summary>
    [Column("alias")]
    [StringLength(5)]
    [Unicode(false)]
    public string Alias { get; set; } = null!;

    /// <summary>
    /// Identificador del punto asociado a esta comandancia
    /// </summary>
    [Column("id_punto")]
    public int? IdPunto { get; set; }

    /// <summary>
    /// Identificador del usuario que registró la Comandancia
    /// </summary>
    [Column("id_usuario_responsable")]
    public int? IdUsuarioResponsable { get; set; }

    [ForeignKey("IdUsuarioResponsable")]
    [InverseProperty("ComandanciaRegionals")]
    public virtual Usuario? IdUsuarioResponsableNavigation { get; set; }

    [InverseProperty("IdComandanciaNavigation")]
    public virtual ICollection<PuntoPatrullaje> PuntoPatrullajes { get; } = new List<PuntoPatrullaje>();

    [InverseProperty("IdComandanciaRegionalSsfNavigation")]
    public virtual ICollection<Ruta> Ruta { get; } = new List<Ruta>();

    [InverseProperty("IdComandanciaRegionalNavigation")]
    public virtual ICollection<Usuario> Usuarios { get; } = new List<Usuario>();

    [InverseProperty("IdComandanciaNavigation")]
    public virtual ICollection<Vehiculo> Vehiculos { get; } = new List<Vehiculo>();

    [ForeignKey("IdComandancia")]
    [InverseProperty("IdComandancia")]
    public virtual ICollection<Usuario> IdUsuarios { get; } = new List<Usuario>();
}
