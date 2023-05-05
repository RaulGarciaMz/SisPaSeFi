using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities;

[Table("semana", Schema = "ssf")]
public partial class Semana
{
    [Key]
    [Column("date", TypeName = "date")]
    public DateTime Date { get; set; }

    [Column("inicio", TypeName = "date")]
    public DateTime? Inicio { get; set; }

    [Column("fin", TypeName = "date")]
    public DateTime? Fin { get; set; }

    [Column("name")]
    [StringLength(100)]
    [Unicode(false)]
    public string? Name { get; set; }
}
