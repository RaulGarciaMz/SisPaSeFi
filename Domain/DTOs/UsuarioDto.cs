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
        public int intIdUsuario { get; set; }
        /// <summary>
        /// Nombre real del usuario
        /// </summary>
        public string strNombre { get; set; }
        /// <summary>
        /// Nombre del usuario para la cuenta registrada
        /// </summary>
        public string strNombreDeUsuario { get; set; }
        /// <summary>
        /// Apellido paterno del usuario
        /// </summary>
        public string strApellido1 { get; set; }
        /// <summary>
        /// Apellido materno del usuario
        /// </summary>
        public string strApellido2 { get; set; }
        /// <summary>
        /// Correo electrónico del usuario
        /// </summary>
        public string? strCorreoElectronico { get; set; }
        /// <summary>
        /// Número de celular del usuario
        /// </summary>
        public string? strCel { get; set; }
        /// <summary>
        /// Indicador del estado como configurador del usuario (1 - es configurador)
        /// </summary>
        public int? intConfigurador { get; set; }
        /// <summary>
        /// Indicador de la región SSF
        /// </summary>
        public int intRegionSSF { get; set; }
        /// <summary>
        /// Indicador del estado del usuario (bloqueado o desbloqueado)
        /// </summary>
        public int intDesbloquearRegistros { get; set; }
        /// <summary>
        /// Tiempo de espera de la sesión del usuario
        /// </summary>
        public int intTiempoEspera { get; set; }
    }

    public class UsuarioDtoForUpdate
    {
        public string strNombreDeUsuario { get; set; }
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









