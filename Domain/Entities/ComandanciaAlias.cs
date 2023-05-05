using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities;

[Table("comandanciaalias", Schema = "ssf")]
public partial class ComandanciaAlias
{
    [Key]
    [Column("idcomandancia")]
    public int Idcomandancia { get; set; }

    [Column("alias")]
    [StringLength(5)]
    [Unicode(false)]
    public string? Alias { get; set; }
}
