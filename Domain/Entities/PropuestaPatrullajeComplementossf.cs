using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Domain.Entities;

[Table("propuestaspatrullajescomplementossf", Schema = "ssf")]
public partial class PropuestaPatrullajeComplementossf
{
    [Key]
    [Column("id_propuestaPatrullajeComplementoSSF")]
    public int IdPropuestaPatrullajeComplementoSsf { get; set; }

    [Column("id_propuestaPatrullaje")]
    public int? IdPropuestaPatrullaje { get; set; }

    [Column("fechaTermino", TypeName = "date")]
    public DateTime? FechaTermino { get; set; }
}
