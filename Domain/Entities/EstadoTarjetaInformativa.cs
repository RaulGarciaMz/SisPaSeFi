using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Domain.Entities;

[Table("estadotarjetainformativa", Schema = "ssf")]
public partial class EstadoTarjetaInformativa
{
    [Key]
    [Column("id_estadoTarjetaInformativa")]
    public int IdEstadoTarjetaInformativa { get; set; }

    [Column("descripcionEstadoTarjetaInformativa")]
    [StringLength(50)]
    [Unicode(false)]
    public string DescripcionEstadoTarjetaInformativa { get; set; } = null!;

    [InverseProperty("IdEstadoTarjetaInformativaNavigation")]
    public virtual ICollection<TarjetaInformativa> Tarjetainformativas { get; } = new List<TarjetaInformativa>();
}
