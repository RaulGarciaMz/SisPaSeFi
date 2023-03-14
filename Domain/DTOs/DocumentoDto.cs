using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DTOs
{
    /// <summary>
    /// Documento de patrullaje
    /// </summary>
    public class DocumentoDto
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
