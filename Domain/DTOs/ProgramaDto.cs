using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DTOs
{
    /// <summary>
    /// Programa de patrullaje
    /// </summary>
    public class ProgramaDto
    {
        /// <summary>
        /// Identificador del programa de patrullaje
        /// </summary>
        public int IdPrograma;
        /// <summary>
        /// Identificador de la ruta del programa de patrullaje
        /// </summary>
        public int IdRuta;
        /// <summary>
        /// Fecha del patrullaje
        /// </summary>
        public string FechaPatrullaje;
        /// <summary>
        /// Hora de inicio del patrullaje
        /// </summary>
        public string Inicio;
        /// <summary>
        /// Identificador del punto de patrullaje responsable del programa de patrullaje
        /// </summary>
        public int IdPuntoResponsable;
        /// <summary>
        /// Clave del programa de patrullaje
        /// </summary>
        public string Clave;
        /// <summary>
        /// Identificador de la región militar SDN del programa de patrullaje
        /// </summary>
        public int RegionMilitarSDN;
        /// <summary>
        /// Ídentificador de la región SSF del programa de patrullaje
        /// </summary>
        public int RegionSSF;
        /// <summary>
        /// Observaciones realizadas a las ruta del programa de patrullaje
        /// </summary>
        public string ObservacionesRuta;
        /// <summary>
        /// Descripción del estado del patrullaje
        /// </summary>
        public string DescripcionEstadoPatrullaje;
        /// <summary>
        /// Observaciones realizadas al programa de patrullaje
        /// </summary>
        public string ObservacionesPrograma;
        /// <summary>
        /// Identificador del nivel de riesgo del programa de patrullaje
        /// </summary>
        public string IdRiesgoPatrullaje;
        /// <summary>
        /// Descripción de la solicitud del oficio de comisión del programa de patrullaje
        /// </summary>
        public string SolicitudOficioComision;
        /// <summary>
        /// Descripción del oficio de comisión del programa de patrullaje
        /// </summary>
        public string OficioComision;
        /// <summary>
        /// Descripción del nivel de riesgo del programa de patrullaje
        /// </summary>
        public string DescripcionNivelRiesgo;
        /// <summary>
        /// Descripción del itinerario del programa de patrullaje
        /// </summary>
        public string Itinerario;
        /// <summary>
        /// Fecha de la última actualización del registro del programa de patrullaje
        /// </summary>
        public string UltimaActualizacion;
        /// <summary>
        /// Identificador del usuario que regstró el programa de patrullaje
        /// </summary>
        public int IdUsuario;
        /// <summary>
        /// Identificador del usuario responsable del patrullaje en el programa
        /// </summary>
        public int UsuarioResponsablePatrullaje;
        /// <summary>
        /// Identificador de la ruta de patrullaje original solicitada para el programa de patrullaje
        /// </summary>
        public int IdRutaOriginal;
        /// <summary>
        /// Identificador de la propuesta de patrullaje correspondiente al programa
        /// </summary>
        public int IdPropuestaPatrullaje;
        /// <summary>
        /// FEcha de término del program de patrullaje
        /// </summary>
        public string FechaTermino;
        /// <summary>
        /// Lista de identificadores de las propuestas de vehículos para el programa de patrullaje
        /// </summary>
        public List<int> LstPropuestasPatrullajesVehiculos;
        /// <summary>
        /// Lista de identificadores de las propuestas de líneas para el programa de patrullaje
        /// </summary>
        public List<int> LstPropuestasPatrullajesLineas;
        /// <summary>
        /// Lista de las propuestas de fechas patrullaje para el programa
        /// </summary>
        public List<string> LstPropuestasPatrullajesFechas;
        /// <summary>
        /// Indicador para conocer si se requiere apoyo para el programa de patrullaje
        /// </summary>
        public int ApoyoPatrullaje;
    }

    /// <summary>
    /// Patrullaje
    /// </summary>
    public class PatrullajeDto
    {
        /// <summary>
        /// Identificador del programa de patrullaje
        /// </summary>
        public int IdPrograma;
        /// <summary>
        /// Identificador de la ruta del patrullaje
        /// </summary>
        public int IdRuta;
        /// <summary>
        /// Fecha del patrullaje
        /// </summary>
        public string FechaPatrullaje;
        /// <summary>
        /// Hora de inicio del patrullaje
        /// </summary>
        public string Inicio;
        /// <summary>
        /// Identificador del punto de patrullaje responsable
        /// </summary>
        public int IdPuntoResponsable;
        /// <summary>
        /// Identificador de la clave del patrullaje
        /// </summary>
        public string Clave;
        /// <summary>
        /// Identificador de la región militar SDN del patrullaje
        /// </summary>
        public int RegionMilitarSDN;
        /// <summary>
        /// Identificador de la región SSF del patrullaje
        /// </summary>
        public int RegionSSF;
        /// <summary>
        /// descripción de observaciones de las rutas del patrullaje
        /// </summary>
        public string ObservacionesRuta;
        /// <summary>
        /// Descripción del estado del patrullaje
        /// </summary>
        public string DescripcionEstadoPatrullaje;
        /// <summary>
        /// Descripción de observaciones realizadas al patrullaje
        /// </summary>
        public string ObservacionesPrograma;
        /// <summary>
        /// Descripción de la solicitud del oficio de comisión del patrullaje
        /// </summary>
        public string SolicitudOficioComision;
        /// <summary>
        /// Descripción del oficip de comisión del patrullaje
        /// </summary>
        public string OficioComision;
        /// <summary>
        /// Desripción del nivel de riesgo del patrullaje
        /// </summary>
        public string DescripcionNivelRiesgo;
        /// <summary>
        /// Descripción el itinerario del patrullaje
        /// </summary>
        public string Itinerario;
        /// <summary>
        /// Fecha de última actualización del registro del patrullaje
        /// </summary>
        public string UltimaActualizacion;
        /// <summary>
        /// Identificador del usuario que registra el patrullaje
        /// </summary>
        public int IdUsuario;
        /// <summary>
        /// Identificador del usuario responsable de llevar a cabo el patrullaje
        /// </summary>
        public int UsuarioResponsablePatrullaje;        
        /// <summary>
        /// Fecha de término del patrullaje
        /// </summary>
        public string FechaTermino;
    }

}
