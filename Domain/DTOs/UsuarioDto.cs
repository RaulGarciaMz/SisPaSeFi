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
        /// <summary>
        /// Nombre real del usuario
        /// </summary>
        public string strClave { get; set; }
    }

    public class UsuarioDtoForGet
    {
        public string strNombreDeUsuario { get; set; }
        public string strClaveEncriptada { get; set; }
    }

    public class UsuarioRegistradoDto
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
        /// <summary>
        /// Nombre real del usuario
        /// </summary>
        public string strClave { get; set; }
        /// <summary>
        /// Descripción del estado del usuario
        /// </summary>
        public string strResultado { get; set; }
        /// <summary>
        /// Indicador de bloqueo del usuario
        /// </summary>
        public int intBloqueado { get; set; }
        /// <summary>
        /// Indicador de aceptación de aviso legal
        /// </summary>
        public int intAceptacionAvisoLegal { get; set; }
        /// <summary>
        /// Número de intentos del usuario
        /// </summary>
        public int intIntentos { get; set; }
        /// <summary>
        /// Indicador de notificación de accesos
        /// </summary>
        public int intNotificarAccesos { get; set; }
        /// <summary>
        /// Fecha del último acceso
        /// </summary>
        public string strUltimoAcceso { get; set; }

        /// <summary>
        /// Token del usuaio
        /// </summary>
        public string strToken { get; set; }
    }

    public class UsuarioForPostDto
    {
        /// <summary>
        /// Identificador del usuario
        /// </summary>
        public int intIdUsuario { get; set; }
        /// <summary>
        /// Nombre del usuario para la cuenta registrada
        /// </summary>
        public string strNombreDeUsuario { get; set; }
        /// <summary>
        /// Número de sesiones del usuario
        /// </summary>
        public int intNumeroSesionUsuario { get; set; }
        /// <summary>
        /// Número de intentos del usuario
        /// </summary>
        public int intIntentos { get; set; }
        /// <summary>
        /// Descripción del estado del usuario
        /// </summary>
        public string strResultado { get; set; }
    }
}









