﻿using Microsoft.Extensions.Options;
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
        public int IdPrograma { get; set; }
        /// <summary>
        /// Identificador de la ruta del programa de patrullaje
        /// </summary>
        public int IdRuta { get; set; }
        /// <summary>
        /// Fecha del patrullaje
        /// </summary>
        public string FechaPatrullaje { get; set; }
        /// <summary>
        /// Hora de inicio del patrullaje
        /// </summary>
        public string Inicio { get; set; }
        /// <summary>
        /// Identificador del punto de patrullaje responsable del programa de patrullaje
        /// </summary>
        public int IdPuntoResponsable { get; set; }
        /// <summary>
        /// Clave del programa de patrullaje
        /// </summary>
        public string Clave { get; set; }
        /// <summary>
        /// Identificador de la región militar SDN del programa de patrullaje
        /// </summary>
        public int RegionMilitarSDN { get; set; }
        /// <summary>
        /// Ídentificador de la región SSF del programa de patrullaje
        /// </summary>
        public int RegionSSF { get; set; }
        /// <summary>
        /// Observaciones realizadas a las ruta del programa de patrullaje
        /// </summary>
        public string ObservacionesRuta { get; set; }
        /// <summary>
        /// Descripción del estado del patrullaje
        /// </summary>
        public string DescripcionEstadoPatrullaje { get; set; }
        /// <summary>
        /// Observaciones realizadas al programa de patrullaje
        /// </summary>
        public string ObservacionesPrograma { get; set; }
        /// <summary>
        /// Identificador del nivel de riesgo del programa de patrullaje
        /// </summary>
        public string IdRiesgoPatrullaje { get; set; }
        /// <summary>
        /// Descripción de la solicitud del oficio de comisión del programa de patrullaje
        /// </summary>
        public string SolicitudOficioComision { get; set; }
        /// <summary>
        /// Descripción del oficio de comisión del programa de patrullaje
        /// </summary>
        public string OficioComision { get; set; }
        /// <summary>
        /// Descripción del nivel de riesgo del programa de patrullaje
        /// </summary>
        public string DescripcionNivelRiesgo { get; set; }
        /// <summary>
        /// Descripción del itinerario del programa de patrullaje
        /// </summary>
        public string Itinerario { get; set; }
        /// <summary>
        /// Fecha de la última actualización del registro del programa de patrullaje
        /// </summary>
        public string UltimaActualizacion { get; set; }
        /// <summary>
        /// Identificador del usuario que regstró el programa de patrullaje
        /// </summary>
        public int IdUsuario { get; set; }
        /// <summary>
        /// Identificador del usuario responsable del patrullaje en el programa
        /// </summary>
        public int UsuarioResponsablePatrullaje { get; set; }
        /// <summary>
        /// Identificador de la ruta de patrullaje original solicitada para el programa de patrullaje
        /// </summary>
        public int IdRutaOriginal { get; set; }
        /// <summary>
        /// Identificador de la propuesta de patrullaje correspondiente al programa
        /// </summary>
        public int IdPropuestaPatrullaje { get; set; }
        /// <summary>
        /// FEcha de término del program de patrullaje
        /// </summary>
        public string FechaTermino { get; set; }
        /// <summary>
        /// Lista de identificadores de las propuestas de vehículos para el programa de patrullaje
        /// </summary>
        public List<int> LstPropuestasPatrullajesVehiculos { get; set; }
        /// <summary>
        /// Lista de identificadores de las propuestas de líneas para el programa de patrullaje
        /// </summary>
        public List<int> LstPropuestasPatrullajesLineas { get; set; }
        /// <summary>
        /// Lista de las propuestas de fechas patrullaje para el programa
        /// </summary>
        public List<string> LstPropuestasPatrullajesFechas { get; set; }
        /// <summary>
        /// Indicador para conocer si se requiere apoyo para el programa de patrullaje
        /// </summary>
        public int ApoyoPatrullaje { get; set; }
    }

    /// <summary>
    /// Programa de patrullaje para actualización por inicio de patrullaje
    /// </summary>
    public class ProgramaDtoForUpdateInicio
    {
        /// <summary>
        /// Identificador del programa de patrullaje
        /// </summary>
        public int IdPrograma { get; set; }
        /// <summary>
        /// Hora de inicio del patrullaje
        /// </summary>
        public string Inicio { get; set; }
        /// <summary>
        /// Identificador del nivel de riesgo del programa de patrullaje
        /// </summary>
        public int IdRiesgoPatrullaje { get; set; }
        /// <summary>
        /// Fecha de la última actualización del registro del programa de patrullaje
        /// </summary>
        public string UltimaActualizacion { get; set; }
        /// <summary>
        /// Identificador del usuario que regstró el programa de patrullaje
        /// </summary>
        public int IdUsuario { get; set; }
        /// <summary>
        /// Identificador del estado del patrullaje
        /// </summary>
        public int IdEstadoPatrullaje { get; set; }
    }

    /// <summary>
    /// Programa de patrullaje para actualizar cambio de ruta
    /// </summary>
    public class ProgramaDtoForUpdateRuta
    {
        /// <summary>
        /// Identificador del programa de patrullaje
        /// </summary>
        public int IdPrograma { get; set; }
        /// <summary>
        /// Identificador de la ruta del programa de patrullaje
        /// </summary>
        public int IdRuta { get; set; }     
    }


    /// <summary>
    /// Patrullaje
    /// </summary>
    public class PatrullajeDto
    {
        /// <summary>
        /// Identificador del programa de patrullaje
        /// </summary>
        public int IdPrograma { get; set; }
        /// <summary>
        /// Identificador de la ruta del patrullaje
        /// </summary>
        public int IdRuta { get; set; }
        /// <summary>
        /// Fecha del patrullaje
        /// </summary>
        public string FechaPatrullaje { get; set; }
        /// <summary>
        /// Hora de inicio del patrullaje
        /// </summary>
        public string Inicio;
        /// <summary>
        /// Identificador del punto de patrullaje responsable
        /// </summary>
        public int IdPuntoResponsable { get; set; }
        /// <summary>
        /// Identificador de la clave del patrullaje
        /// </summary>
        public string Clave { get; set; }
        /// <summary>
        /// Identificador de la región militar SDN del patrullaje
        /// </summary>
        public int RegionMilitarSDN { get; set; }
        /// <summary>
        /// Identificador de la región SSF del patrullaje
        /// </summary>
        public int RegionSSF { get; set; }
        /// <summary>
        /// descripción de observaciones de las rutas del patrullaje
        /// </summary>
        public string ObservacionesRuta { get; set; }
        /// <summary>
        /// Descripción del estado del patrullaje
        /// </summary>
        public string DescripcionEstadoPatrullaje { get; set; }
        /// <summary>
        /// Descripción de observaciones realizadas al patrullaje
        /// </summary>
        public string ObservacionesPrograma { get; set; }
        /// <summary>
        /// Descripción de la solicitud del oficio de comisión del patrullaje
        /// </summary>
        public string SolicitudOficioComision { get; set; }
        /// <summary>
        /// Descripción del oficip de comisión del patrullaje
        /// </summary>
        public string OficioComision { get; set; }
        /// <summary>
        /// Desripción del nivel de riesgo del patrullaje
        /// </summary>
        public string DescripcionNivelRiesgo { get; set; }
        /// <summary>
        /// Descripción el itinerario del patrullaje
        /// </summary>
        public string Itinerario { get; set; }
        /// <summary>
        /// Fecha de última actualización del registro del patrullaje
        /// </summary>
        public string UltimaActualizacion { get; set; }
        /// <summary>
        /// Identificador del usuario que registra el patrullaje
        /// </summary>
        public int IdUsuario { get; set; }
        /// <summary>
        /// Identificador del usuario responsable de llevar a cabo el patrullaje
        /// </summary>
        public int UsuarioResponsablePatrullaje { get; set; }        
        /// <summary>
        /// Fecha de término del patrullaje
        /// </summary>
        public string FechaTermino { get; set; }
    }

}
