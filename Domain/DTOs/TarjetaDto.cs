using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DTOs
{
    /// <summary>
    /// Tarjeta informativa de patrullajes
    /// </summary>
    public class TarjetaDto
    {
        /// <summary>
        /// Identificador del usuario que registra la tarjeta
        /// </summary>
        public string strIdUsuario { get; set; }
        /// <summary>
        /// Identificador de la tarjeta informativa
        /// </summary>
        public int intIdNota { get; set; }
        /// <summary>
        /// Identificador del programa al que se refiere la tarjeta informativa
        /// </summary>
        public int intIdPrograma { get; set; }
        /// <summary>
        /// Hora de inicio del patrullaje
        /// </summary>
        public string strInicio { get; set; }
        /// <summary>
        /// Hora de término del patrullaje
        /// </summary>
        public string strTermino { get; set; }
        /// <summary>
        /// Tiempo de vuelo del patrullaje
        /// </summary>
        public string strTiempoVuelo { get; set; }
        /// <summary>
        /// CALZONAZO !!!
        /// </summary>
        public string strCalzoCalzo { get; set; }
        /// <summary>
        /// Observaciones del patrullaje
        /// </summary>
        public string strObservaciones { get; set; }
        /// <summary>
        /// Fecha del patrullaje
        /// </summary>
        public string strFechaPatrullaje { get; set; }
        /// <summary>
        /// Cantidad de comandantes de la instalación participante en el patrullaje
        /// </summary>
        public int intComandantesInstalacionSSF { get; set; }
        /// <summary>
        /// cantidad de personal militar oficial de SEDENA participante en el patrullaje
        /// </summary>
        public int intPersonalMilitarSEDENAOficial { get; set; }
        /// <summary>
        /// Kilómetros recorridos en el patrullaje
        /// </summary>
        public int intKmRecorrido { get; set; }
        /// <summary>
        /// Indicador del estado de la tarjeta informativa
        /// </summary>
        public int intIdEstadoTarjetaInformativa { get; set; }
        /// <summary>
        /// cantidad de personal mimilat de tropa de la SEDENA participante en el patrullaje
        /// </summary>
        public int intPersonalMilitarSEDENATropa { get; set; }
        /// <summary>
        /// Cantidad de linieros participantes en el patrullaje
        /// </summary>
        public int intLinieros { get; set; }
        /// <summary>
        /// Cantidad de comandantes de turno de SSF participantes en el patrullaje
        /// </summary>
        public int intComandantesTurnoSSF { get; set; }
        /// <summary>
        /// Cantidad de oficiales de SSF participantes en el patrullaje
        /// </summary>
        public int intOficialesSSF { get; set; }
        /// <summary>
        /// Cantidad de personal naval oficial de la SEMAR participante en el patrullaje
        /// </summary>
        public int intPersonalNavalSEMAROficial { get; set; }
        /// <summary>
        /// Cantidad de personal naval de tropa de la SEMAR participante en el patrullaje
        /// </summary>
        public int intPersonalNavalSEMARTropa { get; set; }
        /// <summary>
        /// Fecha de término del patrullaje
        /// </summary>
        public string strFechaTermino { get; set; }
        /// <summary>
        /// Identificador del estado del patrullaje
        /// </summary>
        public int intIdEstadoPatrullaje { get; set; }
        /// <summary>
        /// Descripción del estado del patrullaje
        /// </summary>
        public string strDescripcionEstadoPatrullaje { get; set; }
        /// <summary>
        /// Descirpción de las matrículas participantes en el patrullaje
        /// </summary>
        public string strMatriculas { get; set; }
        /// <summary>
        /// Descripción de los itinerarios realizados en el patrullaje
        /// </summary>
        public string strItinerarios { get; set; }
        /// <summary>
        /// Descripción de los reportes del patrullaje
        /// </summary>
        public string strReportes { get; set; }
        /// <summary>
        /// Descripción de los odómetros generados en el patrullaje
        /// </summary>
        public string strOdometros { get; set; }
        /// <summary>
        /// Descripción de kilómetros recorridos por vehiculos durante el patrullaje
        /// </summary>
        public string strKmVehiculos { get; set; }
        /// <summary>
        /// Fecha de última actualización de la tarjeta informativa
        /// </summary>
        public string strUltimaActualizacion { get; set; }
        /// <summary>
        /// Identificador de la región SSf del patrullaje
        /// </summary>
        public int intRegion { get; set; }
        /// <summary>
        /// Identificador de la ruta realizada en el patrullaje
        /// </summary>
        public int intIdRuta { get; set; }
        /// <summary>
        /// Identificador del tipo de patrullaje realizado
        /// </summary>
        public int intIdTipoPatullaje { get; set; }
        /// <summary>
        /// Identificador del resultado del patrullaje realizado
        /// </summary>
        public int intIdResultadoPatrullaje { get; set; }
        /// <summary>
        /// Descripción del resultado del patrullaje realizado
        /// </summary>
        public string strResultadoPatrullaje { get; set; }
        /// <summary>
        /// Descripción de la línea de la estructura en la instalación
        /// </summary>
        public string strLineaEstructuraInstalacion { get; set; }
        /// <summary>
        /// Descripción del responsable del vuelo en el patrullaje
        /// </summary>
        public string strResponsableVuelo { get; set; }
        /// <summary>
        /// Indicador de existencia de fuerza de reacción en el patrullaje
        /// </summary>
        public int intFuerzaDeReaccion { get; set; }
    }
}
