using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities;

[Table("resultadopatrullaje", Schema = "ssf")]
public partial class ResultadoPatrullaje
{
    [Key]
    [Column("idresultadopatrullaje")]
    public int Idresultadopatrullaje { get; set; }

    [Column("descripcion")]
    [StringLength(100)]
    [Unicode(false)]
    public string Descripcion { get; set; } = null!;
}
