using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DTOs
{
    public class IncidenciasDto
    {
        /// <summary>
        /// Identificador del reporte de incidencia
        /// </summary>
        public int IdReporte { get; set; }
        /// <summary>
        /// Identificador de la nota
        /// </summary>
        public int IdNota { get; set; }
        /// <summary>
        /// Identificador de la estructura
        /// </summary>
        public int IdEstructura { get; set; }
        /// <summary>
        /// Descripción de la incidencia
        /// </summary>
        public string Incidencia { get; set; }
        /// <summary>
        /// Estado de la incidencia
        /// </summary>
        public int EstadoIncidencia { get; set; }
        /// <summary>
        /// Prioridad de la incidencia
        /// </summary>
        public int PrioridadIncidencia { get; set; }
        /// <summary>
        /// Identificador de la clasificación de la incidencia
        /// </summary>
        public int IdClasificacionIncidencia { get; set; }
        /// <summary>
        /// Fecha de última actualización de la incidencia
        /// </summary>
        public DateTime? UltimaActualizacion { get; set; }
        /// <summary>
        /// Clave de la línea de la estructura
        /// </summary>
        public string? Clave { get; set; }
        /// <summary>
        /// Nombre de la estructura
        /// </summary>
        public string Nombre { get; set; }
        /// <summary>
        /// Coordenadas de la estructura
        /// </summary>
        public string Coordenadas { get; set; }
        /// <summary>
        /// Identificador del proceso responsable de la estructura
        /// </summary>
        public int IdProcesoResponsable { get; set; }
        /// <summary>
        /// Identigficador de la gerencia división a donde pertenece la estructura
        /// </summary>
        public int IdGerenciaDivision { get; set; }
        /// <summary>
        /// Descripción del estado de la incidencia
        /// </summary>
        public string DescripcionEstado { get; set; }
        /// <summary>
        /// Descripción del nivel de la incidencia
        /// </summary>
        public string DescripcionNivel { get; set; }
        /// <summary>
        /// Descripción del tipo de report "INSTALACION" o "ESTRUCTURA"
        /// </summary>
        public string TipoReporte { get; set; }
        /// <summary>
        /// Identificador del punto de la instalación
        /// </summary>
        public int Punto { get; set; }
        /// <summary>
        /// Ubicación de la instalación
        /// </summary>
        public string Ubicacion { get; set; }

    }

    public class IncidenciasDtoForCreate
    {
        public int Id { get; set; }
        public int IdNota { get; set; }        
        public string DescripcionIncidencia { get; set; }
        public int EstadoIncidencia { get; set; }
        public int PrioridadIncidencia { get; set; }
        public int IdClasificacionIncidencia { get; set; }
        public string Usuario { get; set; }
        public string TipoIncidencia { get;}
        public int IdTarjeta { get; }
    }

    public class IncidenciasDtoForUpdate
    {
        public int IdReporte { get; set; }
        public string DescripcionIncidencia { get; set; }
        public int EstadoIncidencia { get; set; }
        public int PrioridadIncidencia { get; set; }
        public int IdClasificacionIncidencia { get; set; }
        public string Usuario { get; set; }
        public string TipoIncidencia { get; }
        public int IdTarjeta { get; }
    }
}
