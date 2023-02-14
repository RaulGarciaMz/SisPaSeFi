using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Domain.Entities;

[Table("grupos", Schema = "ssf")]
public partial class Grupo
{
    [Key]
    public int IdGrupo { get; set; }

    [StringLength(100)]
    [Unicode(false)]
    public string? Descripcion { get; set; }

    [InverseProperty("IdGrupoNavigation")]
    public virtual ICollection<Menu> Menus { get; } = new List<Menu>();
}
