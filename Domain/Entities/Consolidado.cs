using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities;

[Keyless]
public partial class Consolidado
{
    [Column("fecha", TypeName = "date")]
    public DateTime? Fecha { get; set; }

    [Column("RegionSSF")]
    [StringLength(5)]
    [Unicode(false)]
    public string? RegionSsf { get; set; }

    [StringLength(5)]
    [Unicode(false)]
    public string? RegionSedena { get; set; }

    [Column("kmRecorrido")]
    public int KmRecorrido { get; set; }

    [Column("tiempoVuelo")]
    public TimeSpan TiempoVuelo { get; set; }

    [Column("calzoAcalzo")]
    public TimeSpan CalzoAcalzo { get; set; }

    public int TropaSedena { get; set; }

    public int OficialSedena { get; set; }

    [Column("CmteInstSSF")]
    public int CmteInstSsf { get; set; }

    [Column("CmteTurnoSSF")]
    public int CmteTurnoSsf { get; set; }

    [Column("OficialSSF")]
    public int OficialSsf { get; set; }

    [Column("ConductoresCSF")]
    public int ConductoresCsf { get; set; }

    [Column("observaciones")]
    [StringLength(1000)]
    [Unicode(false)]
    public string Observaciones { get; set; } = null!;

    [Column("ruta")]
    [StringLength(8000)]
    [Unicode(false)]
    public string? Ruta { get; set; }

    [Column("idtipo")]
    public int Idtipo { get; set; }

    [Column("idestado")]
    public int Idestado { get; set; }

    [Column("id_programa")]
    public int IdPrograma { get; set; }

    [Column("estado")]
    [StringLength(30)]
    [Unicode(false)]
    public string? Estado { get; set; }

    [Column("matricula")]
    [StringLength(8000)]
    [Unicode(false)]
    public string? Matricula { get; set; }

    [Column("vehiculos")]
    public int? Vehiculos { get; set; }

    [Column("minutos")]
    public int? Minutos { get; set; }

    [Column("idapoyo")]
    public int Idapoyo { get; set; }

    [Column("idmixto")]
    public int Idmixto { get; set; }

    [Column("semana")]
    [StringLength(84)]
    [Unicode(false)]
    public string? Semana { get; set; }
}
