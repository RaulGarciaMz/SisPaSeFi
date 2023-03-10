using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DTOs
{
    public class EstructuraDto
    {
        public int IdEstructura { get; set; }
        public string Nombre { get; set; } 
        public string Coordenadas { get; set; } 
        public string? Latitud { get; set; }
        public string? Longitud { get; set; }
        public int IdMunicipio { get; set; }
        public int? IdLinea { get; set; }
        public int IdProcesoResponsable { get; set; }
        public int IdGerenciaDivision { get; set; }
        public string? Clave { get; set; }
        public string? Descripcion { get; set; }
        public string Municipio { get; set; } 
        public int IdEstado { get; set; }
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
