﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Domain.Entities;

[Keyless]
[Table("usuariopatrullaje", Schema = "ssf")]
public partial class UsuarioPatrullaje
{
    [Column("id_usuario")]
    public int IdUsuario { get; set; }

    [Column("id_programa")]
    public int IdPrograma { get; set; }
}