using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DTOs
{
    /// <summary>
    /// Usuarios del sistema
    /// </summary>
    public class UsuarioDto
    {
        /// <summary>
        /// Identificador del usuario
        /// </summary>
        public int id_usuario { get; set; }
        /// <summary>
        /// Nombre real del usuario
        /// </summary>
        public string nombre { get; set; }
        /// <summary>
        /// Nombre del usuario para la cuenta registrada
        /// </summary>
        public string usuario_nom { get; set; }
        /// <summary>
        /// Apellido paterno del usuario
        /// </summary>
        public string apellido1 { get; set; }
        /// <summary>
        /// Apellido materno del usuario
        /// </summary>
        public string apellido2 { get; set; }
        /// <summary>
        /// Correo electrónico del usuario
        /// </summary>
        public string? correoelectronico { get; set; }
        /// <summary>
        /// Número de celular del usuario
        /// </summary>
        public string? cel { get; set; }
        /// <summary>
        /// Indicador del estado como configurador del usuario (1 - es configurador)
        /// </summary>
        public int? configurador { get; set; }
        /// <summary>
        /// Indicador de la región SSF
        /// </summary>
        public int regionSSF { get; set; }
        /// <summary>
        /// Indicador del estado del usuario (bloqueado o desbloqueado)
        /// </summary>
        public int desbloquearregistros { get; set; }
        /// <summary>
        /// Tiempo de espera de la sesión del usuario
        /// </summary>
        public int tiempoespera { get; set; }
    }

    public class UsuarioDtoForAutentication
    {
        public string Nombre { get; set; }
        public string Clave { get; set; }
    }

    public class UsuarioDtoRegistro
    {
        public string Nombre { get; set; }
        public string Apellido1 { get; set; }
        public string Apellido2 { get; set; }
        public string? Cel { get; set; }
        public int? Configurador { get; set; }
        public int? Bloqueado { get; set; }
        public int? AceptacionAvisoLegal { get; set; }
        public int? Intentos { get; set; }
        public int? NotificarAcceso { get; set; }
        public string? CorreoElectronico { get; set; }
        public int RegionSSF { get; set; }
        public int TiempoEspera { get; set; }
        public int DesbloquearRegistros { get; set; }
        public DateTime? UltimoAcceso { get; set; }
        public string Resultado { get; set; }
        public string Token { get; set; }
    }
}









