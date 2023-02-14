using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Domain.Entities;

[Table("estadopropuesta", Schema = "ssf")]
public partial class EstadoPropuesta
{
    [Key]
    [Column("id_estadoPropuesta")]
    public int IdEstadoPropuesta { get; set; }

    [Column("descripcionEstadoPropuesta")]
    [StringLength(50)]
    [Unicode(false)]
    public string DescripcionEstadoPropuesta { get; set; } = null!;
}
