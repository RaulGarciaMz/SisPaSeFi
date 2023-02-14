using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Domain.Entities;

[Table("sesiones", Schema = "ssf")]
[Index("IdUsuario", Name = "id_usuario")]
public partial class Sesion
{
    [Key]
    [Column("id_sesion")]
    public int IdSesion { get; set; }

    [Column("id_usuario")]
    public int IdUsuario { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime EstampaTiempoTerminacion { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime EstampaTiempoInicio { get; set; }

    [InverseProperty("IdSesionNavigation")]
    public virtual ICollection<Evento> Eventos { get; } = new List<Evento>();

    [ForeignKey("IdUsuario")]
    [InverseProperty("Sesiones")]
    public virtual Usuario IdUsuarioNavigation { get; set; } = null!;
}
