using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Domain.Entities;

/// <summary>
/// Listado de rutas  que pueden seleccionarse para que el usuario construya la propuesta o un programa de patrullaje
/// </summary>
[Table("Ruta", Schema = "dmn")]
public partial class Ruta
{
    /// <summary>
    /// Identificador único de la ruta.
    /// </summary>
    [Key]
    [Column("id")]
    public int Id { get; set; }

    /// <summary>
    /// Identificador del tipo de patrullaje al que se asocia la ruta
    /// </summary>
    [Column("id_tipo_patrullaje")]
    public byte? IdTipoPatrullaje { get; set; }

    /// <summary>
    /// Identificado de la Comandancia regional a la que se asocia la ruta.
    /// </summary>
    [Column("id_comandancia_regional_SSF")]
    public short? IdComandanciaRegionalSsf { get; set; }

    /// <summary>
    /// Nombre que permite reconocer la ruta
    /// </summary>
    [Column("clave")]
    [StringLength(50)]
    [Unicode(false)]
    public string? Clave { get; set; }

    /// <summary>
    /// Identificador único de la Región Militar de la SDN a la que se asocia la ruta.
    /// </summary>
    [Column("region_militar_SDN")]
    public short? RegionMilitarSdn { get; set; }

    /// <summary>
    /// Identificador único de la Zona Militar de la SDN a la que se asocia la ruta.
    /// </summary>
    [Column("zona_militar_SDN")]
    public short? ZonaMilitarSdn { get; set; }

    /// <summary>
    /// Notas o comentarios relacionados a la Ruta
    /// </summary>
    [Column("observaciones")]
    [StringLength(150)]
    [Unicode(false)]
    public string? Observaciones { get; set; }

    /// <summary>
    /// Número del consecutivo de la ruta dentro de la Región Militar de la SSF
    /// </summary>
    [Column("consecutivo_region_militar_SDN")]
    public short? ConsecutivoRegionMilitarSdn { get; set; }

    /// <summary>
    /// Indicador de que el registro no puede ser eliminado de la BD.
    /// </summary>
    [Column("bloqueado")]
    public bool? Bloqueado { get; set; }

    /// <summary>
    /// Indicador de sí la ruta se le muestra al usuario en la IHM al momento de crear una propuesta o programada patrullaje.
    /// </summary>
    [Column("habilitado")]
    public bool? Habilitado { get; set; }

    /// <summary>
    /// Fecha en la que se realizó la última actualización.
    /// </summary>
    [Column("ultima_actualizacion", TypeName = "datetime")]
    public DateTime? UltimaActualizacion { get; set; }

    [ForeignKey("IdComandanciaRegionalSsf")]
    [InverseProperty("Ruta")]
    public virtual ComandanciaRegional? IdComandanciaRegionalSsfNavigation { get; set; }

    [ForeignKey("IdTipoPatrullaje")]
    [InverseProperty("Ruta")]
    public virtual TipoPatrullaje? IdTipoPatrullajeNavigation { get; set; }

    [InverseProperty("IdRutaNavigation")]
    public virtual ICollection<Itinerario> Itinerarios { get; } = new List<Itinerario>();

    [InverseProperty("IdRutaNavigation")]
    public virtual ICollection<ProgramaPatrullaje> ProgramaPatrullajes { get; } = new List<ProgramaPatrullaje>();

    [InverseProperty("IdRutaNavigation")]
    public virtual ICollection<PropuestaPatrullajeRutaContenedor> PropuestaPatrullajeRutaContenedors { get; } = new List<PropuestaPatrullajeRutaContenedor>();
}
