using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Domain.Entities;

[Table("usuariospotfire", Schema = "ssf")]
public partial class UsuarioSpotfire
{
    [Key]
    [Column("id_usuario")]
    public int IdUsuario { get; set; }

    [Column("nombre")]
    [StringLength(45)]
    [Unicode(false)]
    public string? Nombre { get; set; }
}
