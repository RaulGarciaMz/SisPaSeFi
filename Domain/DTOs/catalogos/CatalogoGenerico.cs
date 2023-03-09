using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DTOs.catalogos
{
    /// <summary>
    /// Estructura genérica para catálogos ligados a combo boxes
    /// </summary>
    public class CatalogoGenerico
    {
        /// <summary>
        /// Identificador del elemento
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Descripción del elemento
        /// </summary>
        public string Descripcion { get; set; }
    }
}
