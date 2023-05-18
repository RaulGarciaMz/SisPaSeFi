﻿namespace Domain.DTOs
{
    public class ProgramaDtoForCreate
    {
        /// <summary>
        /// Nombre del usuario
        /// </summary>
        public string strUsuario { get; set; }
        /// <summary>
        /// Fecha del patrullaje
        /// </summary>
        public string strFechaPatrullaje { get; set; }
        /// <summary>
        /// Identificador de la ruta del programa de patrullaje
        /// </summary>
        public int intIdRuta { get; set; }
        /// <summary>
        /// Identificador de la ruta de patrullaje original solicitada para el programa de patrullaje
        /// </summary>
        public int intidrutaoriginal { get; set; }
        /// <summary>
        /// Identificador del punto de patrullaje responsable del programa de patrullaje
        /// </summary>
        public int intIdPuntoResponsable { get; set; }
        /// <summary>
        /// Identificador del nivel de riesgo del programa de patrullaje
        /// </summary>
        public string intIdRiesgoPatrullaje { get; set; }
        /// <summary>
        /// Identificador de la propuesta de patrullaje correspondiente al programa
        /// </summary>
        public int intidpropuestapatrullaje { get; set; }
        /// <summary>
        /// Indicador para conocer si se requiere apoyo para el programa de patrullaje
        /// </summary>
        public int intApoyoPatrullaje { get; set; }
    }

    public class ProgramaDtoForCreateWithListas
    {
        /// <summary>
        /// Nombre del usuario
        /// </summary>
        public string strUsuario { get; set; }
        /// <summary>
        /// Fecha del patrullaje
        /// </summary>
        public string strFechaPatrullaje { get; set; }
        /// <summary>
        /// Identificador de la ruta del programa de patrullaje
        /// </summary>
        public int intIdRuta { get; set; }
        /// <summary>
        /// Identificador de la ruta de patrullaje original solicitada para el programa de patrullaje
        /// </summary>
        public int intidrutaoriginal { get; set; }
        /// <summary>
        /// Identificador del punto de patrullaje responsable del programa de patrullaje
        /// </summary>
        public int intIdPuntoResponsable { get; set; }
        /// <summary>
        /// Identificador del nivel de riesgo del programa de patrullaje
        /// </summary>
        public int intIdRiesgoPatrullaje { get; set; }
        /// <summary>
        /// Identificador de la propuesta de patrullaje correspondiente al programa
        /// </summary>
        public int intidpropuestapatrullaje { get; set; }
        /// <summary>
        /// Indicador para conocer si se requiere apoyo para el programa de patrullaje
        /// </summary>
        public int intApoyoPatrullaje { get; set; }
        /// <summary>
        /// FEcha de término del program de patrullaje
        /// </summary>
        public string strFechaTermino { get; set; }
        /// <summary>
        /// Lista de las propuestas de fechas patrullaje para el programa
        /// </summary>
        public List<string> lstPropuestasPatrullajesFechas { get; set; }
        /// <summary>
        /// Lista de identificadores de las propuestas de vehículos para el programa de patrullaje
        /// </summary>
        public List<int> lstPropuestasPatrullajesVehiculos { get; set; }
        /// <summary>
        /// Lista de identificadores de las propuestas de líneas para el programa de patrullaje
        /// </summary>
        public List<int> lstPropuestasPatrullajesLineas { get; set; }
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
    /// Programa de patrullaje para actualizar cambio de ruta
    /// </summary>
    public class ProgramaDtoForUpdatePorOpcion
    {
        /// <summary>
        /// Identificador del programa de patrullaje
        /// </summary>
        public int intIdPrograma { get; set; }
        /// <summary>
        /// Identificador de la propuesta de patrullaje
        /// </summary>
        public int intidpropuestapatrullaje { get; set; }
        /// <summary>
        /// Hora de inicio del patrullaje
        /// </summary>
        public string strInicio { get; set; }
        /// <summary>
        /// Identificador de la ruta del programa de patrullaje
        /// </summary>
        public int intIdRuta { get; set; }
        /// <summary>
        /// Identificador del nivel de riesgo del programa de patrullaje
        /// </summary>
        public int intIdRiesgoPatrullaje { get; set; }
/*
        /// <summary>
        /// Identificador del usuario que regstró el programa de patrullaje
        /// </summary>
        public int IdUsuario { get; set; }
*/
        /// <summary>
        /// Identificador del estado del patrullaje
        /// </summary>
        public int intIdEstadoPatrullaje { get; set; }
        /// <summary>
        /// Descripción de la solicitud del oficio de comisión del programa de patrullaje
        /// </summary>
        public string strSolicitudOficio { get; set; }
        /// <summary>
        /// Descripción del oficio de comisión del programa de patrullaje
        /// </summary>
        public string strOficio { get; set; }
        /// <summary>
        /// Fecha del patrullaje
        /// </summary>
        public string strFechaPatrullaje { get; set; }

    }

    /// <summary>
    /// Propuesta de patrullaje para actualizar ó actualizar y crear programa de patrullaje
    /// </summary>
    public class PropuestaDtoForListaUpdate
    {
        /// <summary>
        /// Identificador de la propuesta de patrullaje
        /// </summary>
        public int intidpropuestapatrullaje { get; set; }
        /// <summary>
        /// Identificador de la ruta del programa de patrullaje
        /// </summary>
        public int intIdRuta { get; set; }
        /// <summary>
        /// Fecha del patrullaje
        /// </summary>
        public string strFechaPatrullaje { get; set; }
        /// <summary>
        /// Identificador del nivel de riesgo del programa de patrullaje
        /// </summary>
        public int intIdRiesgoPatrullaje { get; set; }
                // <summary>
        // Identificador del usuario que regstró el programa de patrullaje
        // </summary>
        public int intIdUsuario { get; set; }
        /// <summary>
        /// Identificador del punto de patrullaje responsable del programa de patrullaje
        /// </summary>
        public int intIdPuntoResponsable { get; set; }
        /// <summary>
        /// Identificador de la ruta de patrullaje original solicitada para el programa de patrullaje
        /// </summary>
        public int intidrutaoriginal { get; set; }
    }

    /// <summary>
    /// Patrullaje
    /// </summary>
    public class PatrullajeDto
    {
        /// <summary>
        /// Identificador del programa de patrullaje
        /// </summary>
        public int intIdPrograma { get; set; }
        /// <summary>
        /// Identificador de la ruta del patrullaje
        /// </summary>
        public int intIdRuta { get; set; }
        /// <summary>
        /// Fecha del patrullaje
        /// </summary>
        public string strFechaPatrullaje { get; set; }
        /// <summary>
        /// Hora de inicio del patrullaje
        /// </summary>
        public string strInicio { get; set; }
        /// <summary>
        /// Identificador del punto de patrullaje responsable
        /// </summary>
        public int intIdPuntoResponsable { get; set; }
        /// <summary>
        /// Identificador de la clave del patrullaje
        /// </summary>
        public string strClave { get; set; }
        /// <summary>
        /// Identificador de la región militar SDN del patrullaje
        /// </summary>
        public int intRegionMilitarSDN { get; set; }
        /// <summary>
        /// Identificador de la región SSF del patrullaje
        /// </summary>
        public int intRegionSSF { get; set; }
        /// <summary>
        /// descripción de observaciones de las rutas del patrullaje
        /// </summary>
        public string strObservacionesRuta { get; set; }
        /// <summary>
        /// Descripción del estado del patrullaje
        /// </summary>
        public string strDescripcionEstadoPatrullaje { get; set; }
        /// <summary>
        /// Descripción de observaciones realizadas al patrullaje
        /// </summary>
        public string strObservacionesPrograma { get; set; }
        /// <summary>
        /// Descripción de la solicitud del oficio de comisión del patrullaje
        /// </summary>
        public string strSolicitudOficio { get; set; }
        /// <summary>
        /// Descripción del oficip de comisión del patrullaje
        /// </summary>
        public string strOficio { get; set; }
        /// <summary>
        /// Desripción del nivel de riesgo del patrullaje
        /// </summary>
        public string strDescripcionNivelRiesgo { get; set; }
        /// <summary>
        /// Descripción el itinerario del patrullaje
        /// </summary>
        public string strItinerario { get; set; }
        /// <summary>
        /// Fecha de última actualización del registro del patrullaje
        /// </summary>
        public string strUltimaActualizacion { get; set; }
        /// <summary>
        /// Identificador del usuario que registra el patrullaje
        /// </summary>
        public int intIdUsuario { get; set; }
        /// <summary>
        /// Identificador del usuario responsable de llevar a cabo el patrullaje
        /// </summary>
        public int intUsuarioResponsablePatrullaje { get; set; }        
        /// <summary>
        /// Fecha de término del patrullaje
        /// </summary>
        public string strFechaTermino { get; set; }
        /// <summary>
        /// Identificador del apoyo de patrullaje
        /// </summary>
        public int intApoyoPatrullaje { get; set; }
    }

    public class PatrullajeSoloDto
    {
        /// <summary>
        /// Identificador del programa de patrullaje
        /// </summary>
        public int intIdPrograma { get; set; }
        /// <summary>
        /// Identificador de la ruta del patrullaje
        /// </summary>
        public int intIdRuta { get; set; }
        /// <summary>
        /// Fecha del patrullaje
        /// </summary>
        public string strFechaPatrullaje { get; set; }
        /// <summary>
        /// Hora de inicio del patrullaje
        /// </summary>
        public string strInicio { get; set; }
        /// <summary>
        /// Identificador del punto de patrullaje responsable
        /// </summary>
        public int intIdPuntoResponsable { get; set; }
        /// <summary>
        /// Identificador de la clave del patrullaje
        /// </summary>
        public string strClave { get; set; }
        /// <summary>
        /// Identificador de la región militar SDN del patrullaje
        /// </summary>
        public int intRegionMilitarSDN { get; set; }
        /// <summary>
        /// Identificador de la región SSF del patrullaje
        /// </summary>
        public int intRegionSSF { get; set; }
        /// <summary>
        /// descripción de observaciones de las rutas del patrullaje
        /// </summary>
        public string strObservacionesRuta { get; set; }
        /// <summary>
        /// Descripción del estado del patrullaje
        /// </summary>
        public string strDescripcionEstadoPatrullaje { get; set; }
        /// <summary>
        /// Descripción de observaciones realizadas al patrullaje
        /// </summary>
        public string strObservacionesPrograma { get; set; }
        /// <summary>
        /// Identificador del riesgo del patrullaje
        /// </summary>
        public int intIdRiesgoPatrullaje { get; set; }
        /// <summary>
        /// Descripción de la solicitud del oficio de comisión del patrullaje
        /// </summary>
        public string strSolicitudOficio { get; set; }
        /// <summary>
        /// Descripción del oficip de comisión del patrullaje
        /// </summary>
        public string strOficio { get; set; }
        /// <summary>
        /// Desripción del nivel de riesgo del patrullaje
        /// </summary>
        public string strDescripcionNivelRiesgo { get; set; }
        /// <summary>
        /// Descripción el itinerario del patrullaje
        /// </summary>
        public string strItinerario { get; set; }
        /// <summary>
        /// Fecha de última actualización del registro del patrullaje
        /// </summary>
        public string strUltimaActualizacion { get; set; }
        /// <summary>
        /// Identificador del usuario que registra el patrullaje
        /// </summary>
        public int intIdUsuario { get; set; }
        /// <summary>
        /// Identificador del usuario responsable de llevar a cabo el patrullaje
        /// </summary>
        public int intUsuarioResponsablePatrullaje { get; set; }
        /// <summary>
        /// Identificador del apoyo de patrullaje
        /// </summary>
        public int intApoyoPatrullaje { get; set; }
        /// <summary>
        /// Identificador de la ruta original del patrullaje
        public int intidrutaoriginal { get; set; }
        /// <summary>
        /// Nombre de la instalación
        /// </summary>
        public string strInstalacion { get; set; }
        /// <summary>
        /// Nombre del municipio
        /// </summary>
        public string strMunicipio { get; set; }
        /// <summary>
        /// Nombre del estado
        /// </summary>
        public string strEstado { get; set; }
        /// <summary>
        /// Nombre del comandante regional
        /// </summary>
        public string strComandanteRegional { get; set; }
    }
}
