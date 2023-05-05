using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities;

[Table("puntospatrullaje", Schema = "ssf")]
[Index("IdMunicipio", Name = "id_municipio")]
public partial class PuntoPatrullaje
{
    [Key]
    [Column("id_punto")]
    public int IdPunto { get; set; }

    [Column("ubicacion")]
    [StringLength(50)]
    [Unicode(false)]
    public string Ubicacion { get; set; } = null!;

    [Column("coordenadas")]
    [StringLength(50)]
    [Unicode(false)]
    public string Coordenadas { get; set; } = null!;

    [Column("id_municipio")]
    public int IdMunicipio { get; set; }

    [Column("esInstalacion")]
    public int EsInstalacion { get; set; }

    [Column("id_nivelRiesgo")]
    public int? IdNivelRiesgo { get; set; }

    [Column("id_comandancia")]
    public int? IdComandancia { get; set; }

    [Column("id_ProcesoResponsable")]
    public int IdProcesoResponsable { get; set; }

    [Column("id_GerenciaDivision")]
    public int IdGerenciaDivision { get; set; }

    [Column("id_usuario")]
    public int? IdUsuario { get; set; }

    [Column("ultimaActualizacion", TypeName = "datetime")]
    public DateTime? UltimaActualizacion { get; set; }

    [Column("bloqueado")]
    public int Bloqueado { get; set; }

    [Column("latitud")]
    [StringLength(25)]
    [Unicode(false)]
    public string? Latitud { get; set; }

    [Column("longitud")]
    [StringLength(25)]
    [Unicode(false)]
    public string? Longitud { get; set; }

    [ForeignKey("IdMunicipio")]
    [InverseProperty("Puntospatrullajes")]
    public virtual Municipio IdMunicipioNavigation { get; set; } = null!;

    [InverseProperty("IdPuntoNavigation")]
    public virtual ICollection<Itinerario> Itinerarios { get; } = new List<Itinerario>();

    [InverseProperty("IdPuntoInicioNavigation")]
    public virtual ICollection<Linea> Lineas { get; } = new List<Linea>();

    [InverseProperty("IdPuntoResponsableNavigation")]
    public virtual ICollection<ProgramaPatrullaje> Programapatrullajes { get; } = new List<ProgramaPatrullaje>();

    [InverseProperty("IdPuntoResponsableNavigation")]
    public virtual ICollection<PropuestaPatrullaje> Propuestaspatrullajes { get; } = new List<PropuestaPatrullaje>();

    [InverseProperty("IdPuntoNavigation")]
    public virtual ICollection<ReportePunto> Reportepuntos { get; } = new List<ReportePunto>();
}
