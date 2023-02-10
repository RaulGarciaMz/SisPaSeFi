using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using NetTopologySuite.Geometries;

namespace Domain.Entities;

/// <summary>
/// Listado de puntos que pueden ser visitados durante un patrullaje
/// </summary>
[Table("Punto_Patrullaje", Schema = "dmn")]
public partial class PuntoPatrullaje
{
    /// <summary>
    /// Identificador único del punto de patrullaje
    /// </summary>
    [Key]
    [Column("id")]
    public int Id { get; set; }

    /// <summary>
    /// Identificador del municipio al que pertenece el punto de patrullaje
    /// </summary>
    [Column("id_municipio")]
    public short IdMunicipio { get; set; }

    /// <summary>
    /// Identificador del nivel de riesgo asociado al punto
    /// </summary>
    [Column("id_nivel_riesgo")]
    public short IdNivelRiesgo { get; set; }

    /// <summary>
    /// Identificador de la Comandancia Regional a la que está asociado el punto
    /// </summary>
    [Column("id_comandancia")]
    public short IdComandancia { get; set; }

    /// <summary>
    /// Identificador del proceso responsable encargado de la gestión del punto
    /// </summary>
    [Column("id_proceso_responsable")]
    public short IdProcesoResponsable { get; set; }

    /// <summary>
    /// Referencias de la ubicación en donde se encuentra el punto
    /// </summary>
    [Column("ubicacion")]
    [StringLength(50)]
    [Unicode(false)]
    public string Ubicacion { get; set; } = null!;

    /// <summary>
    /// Indicador de si el punto está dentro de una instalación de CFE
    /// </summary>
    [Column("es_instalacion")]
    public bool EsInstalacion { get; set; }

    /// <summary>
    /// Fecha de la última actualización del punto
    /// </summary>
    [Column("ultima_actualizacion", TypeName = "datetime")]
    public DateTime? UltimaActualizacion { get; set; }

    /// <summary>
    /// Indicador de sí el punto de patrullaje está bloqueado (no se mostrará al momento de definir una propuesta de patrullaje)
    /// </summary>
    [Column("bloqueado")]
    public bool Bloqueado { get; set; }

    /// <summary>
    /// Coordenadas geoespaciales de la ubicación del punto
    /// </summary>
    [Column("coordenadas_srid", TypeName = "geometry")]
    public Geometry CoordenadasSrid { get; set; } = null!;

    [ForeignKey("IdComandancia")]
    [InverseProperty("PuntoPatrullajes")]
    public virtual ComandanciaRegional IdComandanciaNavigation { get; set; } = null!;

    [ForeignKey("IdMunicipio")]
    [InverseProperty("PuntoPatrullajes")]
    public virtual Municipio IdMunicipioNavigation { get; set; } = null!;

    [ForeignKey("IdNivelRiesgo")]
    [InverseProperty("PuntoPatrullajes")]
    public virtual NivelRiesgo IdNivelRiesgoNavigation { get; set; } = null!;

    [ForeignKey("IdProcesoResponsable")]
    [InverseProperty("PuntoPatrullajes")]
    public virtual ProcesoResponsable IdProcesoResponsableNavigation { get; set; } = null!;

    [InverseProperty("IdPuntoNavigation")]
    public virtual InfoPuntoPatrullaje? InfoPuntoPatrullaje { get; set; }

    [InverseProperty("IdPuntoNavigation")]
    public virtual ICollection<Itinerario> Itinerarios { get; } = new List<Itinerario>();

    [InverseProperty("IdPuntoResponsableNavigation")]
    public virtual ICollection<ProgramaPatrullaje> ProgramaPatrullajes { get; } = new List<ProgramaPatrullaje>();

    [InverseProperty("IdPuntoResponsableNavigation")]
    public virtual ICollection<PropuestaPatrullajeRutaContenedor> PropuestaPatrullajeRutaContenedors { get; } = new List<PropuestaPatrullajeRutaContenedor>();
}
