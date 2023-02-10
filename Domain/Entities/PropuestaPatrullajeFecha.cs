using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Domain.Entities;

[PrimaryKey("IdPropuestaPatrullaje", "IdRuta", "Fecha")]
[Table("Propuesta_Patrullaje_Fecha", Schema = "dmn")]
public partial class PropuestaPatrullajeFecha
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
    /// Fecha en la que se propone realizar el patrullaje 
    /// </summary>
    [Key]
    [Column("fecha", TypeName = "date")]
    public DateTime Fecha { get; set; }

    [Column("id_estado_propuesta")]
    public byte IdEstadoPropuesta { get; set; }

    [ForeignKey("IdPropuestaPatrullaje, IdRuta")]
    [InverseProperty("PropuestaPatrullajeFechas")]
    public virtual PropuestaPatrullajeRutaContenedor Id { get; set; } = null!;

    [ForeignKey("IdEstadoPropuesta")]
    [InverseProperty("PropuestaPatrullajeFechas")]
    public virtual EstadoPropuesta IdEstadoPropuestaNavigation { get; set; } = null!;
}
