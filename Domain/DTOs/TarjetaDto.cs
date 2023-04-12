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
        public string IdUsuario { get; set; }
        /// <summary>
        /// Identificador de la tarjeta informativa
        /// </summary>
        public int IdNota { get; set; }
        /// <summary>
        /// Identificador del programa al que se refiere la tarjeta informativa
        /// </summary>
        public int IdPrograma { get; set; }
        /// <summary>
        /// Hora de inicio del patrullaje
        /// </summary>
        public string Inicio { get; set; }
        /// <summary>
        /// Hora de término del patrullaje
        /// </summary>
        public string Termino { get; set; }
        /// <summary>
        /// Tiempo de vuelo del patrullaje
        /// </summary>
        public string TiempoVuelo { get; set; }
        /// <summary>
        /// CALZONAZO !!!
        /// </summary>
        public string CalzoCalzo { get; set; }
        /// <summary>
        /// Observaciones del patrullaje
        /// </summary>
        public string Observaciones { get; set; }
        /// <summary>
        /// Fecha del patrullaje
        /// </summary>
        public string FechaPatrullaje { get; set; }
        /// <summary>
        /// Cantidad de comandantes de la instalación participante en el patrullaje
        /// </summary>
        public int ComandantesInstalacionSSF { get; set; }
        /// <summary>
        /// cantidad de personal militar oficial de SEDENA participante en el patrullaje
        /// </summary>
        public int PersonalMilitarSEDENAOficial { get; set; }
        /// <summary>
        /// Kilómetros recorridos en el patrullaje
        /// </summary>
        public int KmRecorrido { get; set; }
        /// <summary>
        /// Indicador del estado de la tarjeta informativa
        /// </summary>
        public int IdEstadoTarjetaInformativa { get; set; }
        /// <summary>
        /// cantidad de personal mimilat de tropa de la SEDENA participante en el patrullaje
        /// </summary>
        public int PersonalMilitarSEDENATropa { get; set; }
        /// <summary>
        /// Cantidad de linieros participantes en el patrullaje
        /// </summary>
        public int Linieros { get; set; }
        /// <summary>
        /// Cantidad de comandantes de turno de SSF participantes en el patrullaje
        /// </summary>
        public int ComandantesTurnoSSF { get; set; }
        /// <summary>
        /// Cantidad de oficiales de SSF participantes en el patrullaje
        /// </summary>
        public int OficialesSSF { get; set; }
        /// <summary>
        /// Cantidad de personal naval oficial de la SEMAR participante en el patrullaje
        /// </summary>
        public int PersonalNavalSEMAROficial { get; set; }
        /// <summary>
        /// Cantidad de personal naval de tropa de la SEMAR participante en el patrullaje
        /// </summary>
        public int PersonalNavalSEMARTropa { get; set; }
        /// <summary>
        /// Fecha de término del patrullaje
        /// </summary>
        public string FechaTermino { get; set; }
        /// <summary>
        /// Identificador del estado del patrullaje
        /// </summary>
        public int IdEstadoPatrullaje { get; set; }
        /// <summary>
        /// Descripción del estado del patrullaje
        /// </summary>
        public string DescripcionEstadoPatrullaje { get; set; }
        /// <summary>
        /// Descirpción de las matrículas participantes en el patrullaje
        /// </summary>
        public string Matriculas { get; set; }
        /// <summary>
        /// Descripción de los itinerarios realizados en el patrullaje
        /// </summary>
        public string Itinerarios { get; set; }
        /// <summary>
        /// Descripción de los reportes del patrullaje
        /// </summary>
        public string Reportes { get; set; }
        /// <summary>
        /// Descripción de los odómetros generados en el patrullaje
        /// </summary>
        public string Odometros { get; set; }
        /// <summary>
        /// Descripción de kilómetros recorridos por vehiculos durante el patrullaje
        /// </summary>
        public string KmVehiculos { get; set; }
        /// <summary>
        /// Fecha de última actualización de la tarjeta informativa
        /// </summary>
        public string UltimaActualizacion { get; set; }
        /// <summary>
        /// Identificador de la región SSf del patrullaje
        /// </summary>
        public int Region { get; set; }
        /// <summary>
        /// Identificador de la ruta realizada en el patrullaje
        /// </summary>
        public int IdRuta { get; set; }
        /// <summary>
        /// Identificador del tipo de patrullaje realizado
        /// </summary>
        public int IdTipoPatrullaje { get; set; }
        /// <summary>
        /// Identificador del resultado del patrullaje realizado
        /// </summary>
        public int IdResultadoPatrullaje { get; set; }
        /// <summary>
        /// Descripción del resultado del patrullaje realizado
        /// </summary>
        public string ResultadoPatrullaje { get; set; }
        /// <summary>
        /// Descripción de la línea de la estructura en la instalación
        /// </summary>
        public string LineaEstructuraInstalacion { get; set; }
        /// <summary>
        /// Descripción del responsable del vuelo en el patrullaje
        /// </summary>
        public string ResponsableVuelo { get; set; }
        /// <summary>
        /// Indicador de existencia de fuerza de reacción en el patrullaje
        /// </summary>
        public int FuerzaReaccion { get; set; }
    }
}
