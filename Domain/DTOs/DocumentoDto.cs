namespace Domain.DTOs
{
    /// <summary>
    /// Documento de patrullaje
    /// </summary>
    public class DocumentoDto
    {
        /// <summary>
        /// Identificador del documento de patrullaje
        /// </summary>
        public long IdDocumentoPatrullaje { get; set; }
        /// <summary>
        /// Identificador de la referencia del documento
        /// </summary>
        public long? IdReferencia { get; set; }
        /// <summary>
        /// Identificador del tipo de documento
        /// </summary>
        public long IdTipoDocumento { get; set; }
        /// <summary>
        /// Identificador de la comandancia a donde pertenece el documento
        /// </summary>
        public int IdComandancia { get; set; }
        /// <summary>
        /// Fecha de registro del documento
        /// </summary>
        public DateTime FechaRegistro { get; set; }
        /// <summary>
        /// Fecha de referencia del documento
        /// </summary>
        public DateTime FechaReferencia { get; set; }
        /// <summary>
        /// Ruta o ubicación (path) del archivo
        /// </summary>
        public string RutaArchivo { get; set; }
        /// <summary>
        /// Nombre del archivo
        /// </summary>
        public string NombreArchivo { get; set; }
        /// <summary>
        /// Descripción del documento
        /// </summary>
        public string Descripcion { get; set; }
        /// <summary>
        /// Identificador del usuario que registró el documento
        /// </summary>
        public int IdUsuario { get; set; }
        /// <summary>
        /// Descripción del tipo de documento
        /// </summary>
        public string DescripcionTipoDocumento { get; set; }
        /// <summary>
        /// Nombre y apellido del usuario que registró el documento
        /// </summary>
        public string Usuario { get; set; }
        /// <summary>
        /// Correo electrónico del usuario
        /// </summary>
        public string CorreoElectronico { get; set; }
    }

    /// <summary>
    /// Documento de patrullaje
    /// </summary>
    public class DocumentoDtoForQuery
    {
        /// <summary>
        /// Identificador de la comandancia
        /// </summary>
        public int IdComandancia { get; set; }
        /// <summary>
        /// Año de búsqueda
        /// </summary>
        public int Anio { get; set; }
        /// <summary>
        /// Mes de búsqueda
        /// </summary>
        public int Mes { get; set; }
        /// <summary>
        /// Nombre del usuario (usuario_nom) que realiza la consulta
        /// </summary>
        public string Usuario { get; set; }
    }


}