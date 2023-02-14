using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Domain.Entities;

[Keyless]
[Table("propuestaspatrullajeslineas", Schema = "ssf")]
[Index("IdLinea", Name = "id_linea")]
[Index("IdPropuestaPatrullaje", Name = "id_propuestaPatrullaje")]
public partial class PropuestaPatrullajeLinea
{
    [Column("id_propuestaPatrullaje")]
    public int IdPropuestaPatrullaje { get; set; }

    [Column("id_linea")]
    public int IdLinea { get; set; }

    [ForeignKey("IdLinea")]
    public virtual Linea IdLineaNavigation { get; set; } = null!;

    [ForeignKey("IdPropuestaPatrullaje")]
    public virtual PropuestaPatrullaje IdPropuestaPatrullajeNavigation { get; set; } = null!;
}
