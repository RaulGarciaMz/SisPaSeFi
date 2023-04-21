using System.ComponentModel.DataAnnotations;

namespace Domain.DTOs
{
    /// <summary>
    /// Estructura
    /// </summary>
    public class EstructuraDto
    {
        /// <summary>
        /// Identificador de la estructura
        /// </summary>
        public int intIdEstructura { get; set; }
        /// <summary>
        /// Nombre de la estructura
        /// </summary>
        public string strNombre { get; set; } 
        /// <summary>
        /// Coordenadas de la ubicación de la estructura
        /// </summary>
        public string strCoordenadas { get; set; } 
        /// <summary>
        /// Latitud donde se ubica la estructura
        /// </summary>
        public string? Latitud { get; set; }
        /// <summary>
        /// Longitud donde se ubica la estructura
        /// </summary>
        public string? Longitud { get; set; }
        /// <summary>
        /// Identificador del municipio donde se encuentra la estructura
        /// </summary>
        public int intIdMunicipio { get; set; }
        /// <summary>
        /// Identificador de la línea
        /// </summary>
        public int? intIdLinea { get; set; }
        /// <summary>
        /// Identificador del proceso responsable de la estructura
        /// </summary>
        public int intIdProcesoResponsable { get; set; }
        /// <summary>
        /// Identificador de la gernecia y división donde pertenece la estructura
        /// </summary>
        public int intIdGerenciaDivision { get; set; }
        /// <summary>
        /// Clave de la estructura
        /// </summary>
        public string? strClaveLinea { get; set; }
        /// <summary>
        /// Descripción de la estuctura
        /// </summary>
        public string? strDescripcionLinea { get; set; }
        /// <summary>
        /// Nombre del municipio donde se ubica la estructura
        /// </summary>
        public string strMunicipio { get; set; } 
        /// <summary>
        /// Identificador del estado del país donde se ubica la estructura
        /// </summary>
        public int intIdEstado { get; set; }
        /// <summary>
        /// Nombre del estado del país donde se ubica la estructura
        /// </summary>
        public string strEstado { get; set; } 
    }

    public class EstructuraDtoForUbicacionUpdate
    {
        /// <summary>
        /// Identificador de la estructura
        /// </summary>
        [Required]
        public int intIdEstructura { get; set; }
        /// <summary>
        /// Nombre de la estructura
        /// </summary>
        [Required]
        public string strNombre { get; set; }
        /// <summary>
        /// Identificador del municipio
        /// </summary>
        [Required]
        public int intIdMunicipio { get; set; }
        /// <summary>
        ///  Latitud donde se ubica la estructura
        /// </summary>
        //[Required]
        public string Latitud { get; set; }
        /// <summary>
        ///  Longitud donde se ubica la estructura
        /// </summary>
        //[Required]
        public string Longitud { get; set; }
        /// <summary>
        /// Usuario (usuario_nom o alias) del usuario que realiza la operación
        /// </summary>
        [Required]
        public string strUsuario { get; set; }
        public string strCoordenadas { get; set; }
    }

    public class EstructuraDtoForCreate
    {
        /// <summary>
        /// Identificador de la línea
        /// </summary>
        [Required]
        public int intIdLinea { get; set; }
        /// <summary>
        /// Nombre de la estructura
        /// </summary>
        [Required]
        public string strNombre { get; set; }
        /// <summary>
        /// Identificador del municipio
        /// </summary>
        [Required]
        public int intIdMunicipio { get; set; }
        /// <summary>
        /// Latitud donde se ubica la estructura
        /// </summary>
        //[Required]
        public string? Latitud { get; set; }
        /// <summary>
        /// Longitud donde se ubica la estructura
        /// </summary>
        //[Required]
        public string? Longitud { get; set; }
        public string? strCoordenadas { get; set; }
        /// <summary>
        /// Usuario (usuario_nom o alias) del usuario que realiza la operación
        /// </summary>
        [Required]
        public string strUsuario { get; set; }
    }
}
