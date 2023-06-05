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
        public long intIdDocumento { get; set; }
        /// <summary>
        /// Identificador de la referencia del documento
        /// </summary>
        public long? intIdReferencia { get; set; }
        /// <summary>
        /// Identificador del tipo de documento
        /// </summary>
        public long intIdTipoDocumento { get; set; }
        /// <summary>
        /// Identificador de la comandancia a donde pertenece el documento
        /// </summary>
        public int intIdComandancia { get; set; }
        /// <summary>
        /// Fecha de registro del documento
        /// </summary>
        public DateTime strFechaRegistro { get; set; }
        /// <summary>
        /// Fecha de referencia del documento
        /// </summary>
        public DateTime strFechaReferencia { get; set; }
        /// <summary>
        /// Ruta o ubicación (path) del archivo
        /// </summary>
        public string strRutaArchivo { get; set; }
        /// <summary>
        /// Nombre del archivo
        /// </summary>
        public string strNombreArchivo { get; set; }
        /// <summary>
        /// Descripción del documento
        /// </summary>
        public string strDescripcionArchivo { get; set; }
        /// <summary>
        /// Identificador del usuario que registró el documento
        /// </summary>
        public int intIdUsuario { get; set; }
        /// <summary>
        /// Descripción del tipo de documento
        /// </summary>
        public string strDescripcionTipoDocumento { get; set; }
        /// <summary>
        /// Nombre y apellido del usuario que registró el documento
        /// </summary>
        public string strNombreCompletoUsuario { get; set; }
        /// <summary>
        /// Correo electrónico del usuario
        /// </summary>
        public string strCorreoElectronico { get; set; }
    }

    /// <summary>
    /// Documento de patrullaje
    /// </summary>
    public class DocumentoDtoForCreate
    {
        /// <summary>
        /// Identificador de la referencia del documento
        /// </summary>
        public long intIdReferencia { get; set; }
        /// <summary>
        /// Identificador del tipo de documento
        /// </summary>
        public long intIdTipoDocumento { get; set; }
        /// <summary>
        /// Identificador de la comandancia a donde pertenece el documento
        /// </summary>
        public int intIdComandancia { get; set; }

        /// <summary>
        /// Fecha de referencia del documento
        /// </summary>
        public DateTime strFechaReferencia { get; set; }
        /// <summary>
        /// Ruta o ubicación (path) del archivo
        /// </summary>
        public string strRutaArchivo { get; set; }
        /// <summary>
        /// Nombre del archivo
        /// </summary>
        public string strNombreArchivo { get; set; }
        /// <summary>
        /// Descripción del documento
        /// </summary>
        public string strDescripcionArchivo { get; set; }
        /// <summary>
        /// Identificador del usuario que registró el documento
        /// </summary>
        public int intIdUsuario { get; set; }
        /// <summary>
        /// Nombre del usuario que realiza la operación
        /// </summary>
        public string strUsuario { get; set; }
    }

}