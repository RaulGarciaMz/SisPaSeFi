using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Domain.Entities;

[Table("gerenciatransmision", Schema = "ssf")]
public partial class GerenciaTransmision
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("nombre")]
    [StringLength(50)]
    [Unicode(false)]
    public string Nombre { get; set; } = null!;

    [Column("clave")]
    [StringLength(10)]
    [Unicode(false)]
    public string Clave { get; set; } = null!;

    [Column("id_GrupoCorreo")]
    public int IdGrupoCorreo { get; set; }
}
