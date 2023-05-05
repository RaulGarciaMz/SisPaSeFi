using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities;

[Table("itinerario", Schema = "ssf")]
[Index("IdPunto", Name = "id_punto")]
[Index("IdRuta", Name = "id_ruta")]
public partial class Itinerario
{
    [Key]
    [Column("id_itinerario")]
    public int IdItinerario { get; set; }

    [Column("id_ruta")]
    public int IdRuta { get; set; }

    [Column("id_punto")]
    public int IdPunto { get; set; }

    [Column("posicion")]
    public int Posicion { get; set; }

    [Column("ultimaActualizacion", TypeName = "datetime")]
    public DateTime UltimaActualizacion { get; set; }

    [ForeignKey("IdPunto")]
    [InverseProperty("Itinerarios")]
    public virtual PuntoPatrullaje IdPuntoNavigation { get; set; } = null!;

    [ForeignKey("IdRuta")]
    [InverseProperty("Itinerarios")]
    public virtual Ruta IdRutaNavigation { get; set; } = null!;
}
