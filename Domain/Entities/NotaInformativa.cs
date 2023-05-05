using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities;

[Table("notainformativa", Schema = "ssf")]
[Index("IdPrograma", Name = "id_programa")]
[Index("IdUsuario", Name = "id_usuario")]
public partial class NotaInformativa
{
    [Key]
    [Column("id_nota")]
    public int IdNota { get; set; }

    [Column("id_programa")]
    public int IdPrograma { get; set; }

    [Column("id_usuario")]
    public int IdUsuario { get; set; }

    [Column("ultimaActualizacion", TypeName = "datetime")]
    public DateTime UltimaActualizacion { get; set; }

    [Column("inicio")]
    public TimeSpan Inicio { get; set; }

    [Column("termino")]
    public TimeSpan Termino { get; set; }

    [Column("tiempoVuelo")]
    public TimeSpan TiempoVuelo { get; set; }

    [Column("calzoAcalzo")]
    public TimeSpan CalzoAcalzo { get; set; }

    [Column("observaciones")]
    [StringLength(200)]
    [Unicode(false)]
    public string Observaciones { get; set; } = null!;

    [ForeignKey("IdPrograma")]
    [InverseProperty("Notainformativas")]
    public virtual ProgramaPatrullaje IdProgramaNavigation { get; set; } = null!;

    [ForeignKey("IdUsuario")]
    [InverseProperty("Notainformativas")]
    public virtual Usuario IdUsuarioNavigation { get; set; } = null!;
}
