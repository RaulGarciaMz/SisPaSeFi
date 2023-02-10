using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using NetTopologySuite.Geometries;

namespace Domain.Entities;

/// <summary>
/// Listado de estructuras sobre las que se encuentran las líneas
/// </summary>
[Table("Estructura", Schema = "dmn")]
public partial class Estructura
{
    /// <summary>
    /// Identificador único de la estructura
    /// </summary>
    [Key]
    [Column("id")]
    public int Id { get; set; }

    /// <summary>
    /// Identificador único de la línea a la que está asociada la estructura
    /// </summary>
    [Column("id_linea")]
    public int IdLinea { get; set; }

    /// <summary>
    /// Identificador del municipio en el que se encuentra la estructura
    /// </summary>
    [Column("id_municipio")]
    public short IdMunicipio { get; set; }

    /// <summary>
    /// Identificador de la División o Gerencia encargada de la estructura.
    /// </summary>
    [Column("id_proceso_responsable")]
    public short IdProcesoResponsable { get; set; }

    /// <summary>
    /// Nombre descriptivo de la estructura
    /// </summary>
    [Column("nombre")]
    [StringLength(50)]
    [Unicode(false)]
    public string Nombre { get; set; } = null!;

    /// <summary>
    /// Indicador de si la estructura está bloqueada para su modificación o eliminación
    /// </summary>
    [Column("bloqueado")]
    public bool Bloqueado { get; set; }

    /// <summary>
    /// Estampa de tiempo de la última modificación realizada a la estructura
    /// </summary>
    [Column("ultima_actualizacion", TypeName = "datetime")]
    public DateTime? UltimaActualizacion { get; set; }

    /// <summary>
    /// Coordenadas geoespaciales de la ubicación de la estructura
    /// </summary>
    [Column("coordenadas_srid", TypeName = "geometry")]
    public Geometry CoordenadasSrid { get; set; } = null!;

    [ForeignKey("IdLinea")]
    [InverseProperty("Estructuras")]
    public virtual Linea IdLineaNavigation { get; set; } = null!;

    [ForeignKey("IdMunicipio")]
    [InverseProperty("Estructuras")]
    public virtual Municipio IdMunicipioNavigation { get; set; } = null!;

    [ForeignKey("IdProcesoResponsable")]
    [InverseProperty("Estructuras")]
    public virtual ProcesoResponsable IdProcesoResponsableNavigation { get; set; } = null!;

    [InverseProperty("IdEstructuraNavigation")]
    public virtual InfoEstructura? InfoEstructura { get; set; }
}
