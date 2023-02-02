using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    [Table("usuarios", Schema = "ssf")]
    public class Usuario
    {
        [Key]
        public int id_usuario { get; set; }

        [Required]
        [StringLength(50)]
        public string nombre { get; set; }

        [Required]
        [StringLength(50)]
        public string apellido1 { get; set; }

        [Required]
        [StringLength(50)]
        public string apellido2 { get; set; }

        [Required]
        [StringLength(15)]
        public string usuario_nom { get; set; }

        [StringLength(20)]
        public string? cel { get; set; }

        [StringLength(32)]
        public string? pass { get; set; }

        public int? configurador { get; set; }

        public int? bloqueado { get; set; }

        public int? AceptacionAvisoLegal { get; set; }

        public int? intentos { get; set; }

        public int? NotificarAcceso { get; set; }

        public DateTime? EstampaTiempoUltimoAcceso { get; set; }

        public DateTime? EstampaTiempoAceptacionUso { get; set; }

        [StringLength(100)]
        public string? CorreoElectronico { get; set; }

        [Required]
        public int regionSSF { get; set; }

        [Required]
        public int tiempoEspera { get; set; }

        [StringLength(32)]
        public string? passTemp { get; set; }

        [Required]
        public int DesbloquearRegistros { get; set; }
    }
}
