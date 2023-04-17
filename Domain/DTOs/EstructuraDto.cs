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
        public int IdEstructura { get; set; }
        /// <summary>
        /// Nombre de la estructura
        /// </summary>
        public string Nombre { get; set; } 
        /// <summary>
        /// Coordenadas de la ubicación de la estructura
        /// </summary>
        public string Coordenadas { get; set; } 
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
        public int IdMunicipio { get; set; }
        /// <summary>
        /// Identificador de la línea
        /// </summary>
        public int? IdLinea { get; set; }
        /// <summary>
        /// Identificador del proceso responsable de la estructura
        /// </summary>
        public int IdProcesoResponsable { get; set; }
        /// <summary>
        /// Identificador de la gernecia y división donde pertenece la estructura
        /// </summary>
        public int IdGerenciaDivision { get; set; }
        /// <summary>
        /// Clave de la estructura
        /// </summary>
        public string? Clave { get; set; }
        /// <summary>
        /// Descripción de la estuctura
        /// </summary>
        public string? Descripcion { get; set; }
        /// <summary>
        /// Nombre del municipio donde se ubica la estructura
        /// </summary>
        public string Municipio { get; set; } 
        /// <summary>
        /// Identificador del estado del país donde se ubica la estructura
        /// </summary>
        public int IdEstado { get; set; }
        /// <summary>
        /// Nombre del estado del país donde se ubica la estructura
        /// </summary>
        public string Estado { get; set; } 
    }

    public class EstructuraDtoForUbicacionUpdate
    {
        /// <summary>
        /// Identificador de la estructura
        /// </summary>
        [Required]
        public int IdEstructura { get; set; }
        /// <summary>
        /// Nombre de la estructura
        /// </summary>
        [Required]
        public string Nombre { get; set; }
        /// <summary>
        /// Identificador del municipio
        /// </summary>
        [Required]
        public int IdMunicipio { get; set; }
        /// <summary>
        ///  Latitud donde se ubica la estructura
        /// </summary>
        [Required]
        public string Latitud { get; set; }
        /// <summary>
        ///  Longitud donde se ubica la estructura
        /// </summary>
        [Required]
        public string Longitud { get; set; }
        /// <summary>
        /// Usuario (usuario_nom o alias) del usuario que realiza la operación
        /// </summary>
        [Required]
        public string Usuario { get; set; }
    }

    public class EstructuraDtoForCreate
    {
        /// <summary>
        /// Identificador de la línea
        /// </summary>
        [Required]
        public int IdLinea { get; set; }
        /// <summary>
        /// Nombre de la estructura
        /// </summary>
        [Required]
        public string Nombre { get; set; }
        /// <summary>
        /// Identificador del municipio
        /// </summary>
        [Required]
        public int IdMunicipio { get; set; }
        /// <summary>
        /// Latitud donde se ubica la estructura
        /// </summary>
        [Required]
        public string? Latitud { get; set; }
        /// <summary>
        /// Longitud donde se ubica la estructura
        /// </summary>
        [Required]
        public string? Longitud { get; set; }
        /// <summary>
        /// Usuario (usuario_nom o alias) del usuario que realiza la operación
        /// </summary>
        [Required]
        public string Usuario { get; set; }
    }
}
