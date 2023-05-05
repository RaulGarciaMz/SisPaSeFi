using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities;

[Table("dominios", Schema = "ssf")]
public partial class Dominio
{
    [Key]
    [Column("id_dominio")]
    public int IdDominio { get; set; }

    [Column("nombreDominio")]
    [StringLength(50)]
    [Unicode(false)]
    public string NombreDominio { get; set; } = null!;

    [Column("descripcionDominio")]
    [StringLength(50)]
    [Unicode(false)]
    public string DescripcionDominio { get; set; } = null!;
}
