namespace Domain.DTOs
{
    /// <summary>
    /// Usuario participante en patrullaje
    /// </summary>
    public class PersonalParticipanteDto
    {
        /// <summary>
        /// Identificador del programa de patrullaje
        /// </summary>
        public int IdPrograma { get; set; }
        /// <summary>
        /// Identificador del usuario participante en el patrullaje
        /// </summary>
        public int IdUsuario { get; set; }
        /// <summary>
        /// Nombre del usuario (alias o usuario_nom) que realiza el registro
        /// </summary>
        public string Usuario { get; set; }
    }
}
