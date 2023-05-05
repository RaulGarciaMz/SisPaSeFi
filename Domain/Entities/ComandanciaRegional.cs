using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities;

[Table("comandanciasregionales", Schema = "ssf")]
public partial class ComandanciaRegional
{
    [Key]
    [Column("id_comandancia")]
    public int IdComandancia { get; set; }

    [Column("numero")]
    public int Numero { get; set; }

    [Column("id_punto")]
    public int? IdPunto { get; set; }

    [Column("id_usuario")]
    public int? IdUsuario { get; set; }

    [InverseProperty("IdComandanciaNavigation")]
    public virtual ICollection<Vehiculo> Vehiculos { get; } = new List<Vehiculo>();
}
