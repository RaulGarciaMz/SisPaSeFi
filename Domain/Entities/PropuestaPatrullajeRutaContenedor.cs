using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Domain.Entities;

[PrimaryKey("IdPropuestaPatrullaje", "IdRuta")]
[Table("Propuesta_Patrullaje_Ruta_Contenedor", Schema = "dmn")]
public partial class PropuestaPatrullajeRutaContenedor
{
    /// <summary>
    /// Id de la propuesta de patrullaje a la que está asociada
    /// </summary>
    [Key]
    [Column("id_propuesta_patrullaje")]
    public int IdPropuestaPatrullaje { get; set; }

    /// <summary>
    /// Id de la ruta que se está proponiendo en el patrullaje
    /// </summary>
    [Key]
    [Column("id_ruta")]
    public int IdRuta { get; set; }

    /// <summary>
    /// Id del punto de patrullaje (instalación) responsable de llevar a cabo el patrullaje
    /// </summary>
    [Column("id_punto_responsable")]
    public int IdPuntoResponsable { get; set; }

    /// <summary>
    /// Id del nivel de riesgo asociado a la ruta en el tiempo específico del patrullaje
    /// </summary>
    [Column("id_nivel_riesgo")]
    public short IdNivelRiesgo { get; set; }

    /// <summary>
    /// Id de la entidad responsable de apoyar el patrullaje
    /// </summary>
    [Column("id_apoyo_patrullaje")]
    public byte IdApoyoPatrullaje { get; set; }

    /// <summary>
    /// Notas u observaciones asociadas a la ruta en la propuesta de patrullaje
    /// </summary>
    [Column("observaciones")]
    [StringLength(150)]
    [Unicode(false)]
    public string? Observaciones { get; set; }

    [ForeignKey("IdApoyoPatrullaje")]
    [InverseProperty("PropuestaPatrullajeRutaContenedors")]
    public virtual ApoyoPatrullaje IdApoyoPatrullajeNavigation { get; set; } = null!;

    [ForeignKey("IdNivelRiesgo")]
    [InverseProperty("PropuestaPatrullajeRutaContenedors")]
    public virtual NivelRiesgo IdNivelRiesgoNavigation { get; set; } = null!;

    [ForeignKey("IdPropuestaPatrullaje")]
    [InverseProperty("PropuestaPatrullajeRutaContenedors")]
    public virtual PropuestaPatrullaje IdPropuestaPatrullajeNavigation { get; set; } = null!;

    [ForeignKey("IdPuntoResponsable")]
    [InverseProperty("PropuestaPatrullajeRutaContenedors")]
    public virtual PuntoPatrullaje IdPuntoResponsableNavigation { get; set; } = null!;

    [ForeignKey("IdRuta")]
    [InverseProperty("PropuestaPatrullajeRutaContenedors")]
    public virtual Ruta IdRutaNavigation { get; set; } = null!;

    [InverseProperty("Id")]
    public virtual ICollection<PropuestaPatrullajeFecha> PropuestaPatrullajeFechas { get; } = new List<PropuestaPatrullajeFecha>();

    [ForeignKey("IdPropuestaPatrullaje, IdRuta")]
    [InverseProperty("Ids")]
    public virtual ICollection<Vehiculo> IdVehiculos { get; } = new List<Vehiculo>();
}
