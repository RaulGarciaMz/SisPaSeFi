using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Domain.Entities;

[Table("eventos", Schema = "ssf")]
[Index("IdSesion", Name = "id_sesion")]
public partial class Evento
{
    [Key]
    [Column("id_evento")]
    public int IdEvento { get; set; }

    [Column("id_sesion")]
    public int IdSesion { get; set; }

    [StringLength(100)]
    [Unicode(false)]
    public string? DescripcionEvento { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime EstampaTiempo { get; set; }

    [ForeignKey("IdSesion")]
    [InverseProperty("Eventos")]
    public virtual Sesion IdSesionNavigation { get; set; } = null!;
}
