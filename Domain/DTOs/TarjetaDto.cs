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
        public string IdUsuario;
        /// <summary>
        /// Identificador de la tarjeta informativa
        /// </summary>
        public int IdNota;
        /// <summary>
        /// Identificador del programa al que se refiere la tarjeta informativa
        /// </summary>
        public int IdPrograma;
        /// <summary>
        /// Hora de inicio del patrullaje
        /// </summary>
        public string Inicio;
        /// <summary>
        /// Hora de término del patrullaje
        /// </summary>
        public string Termino;
        /// <summary>
        /// Tiempo de vuelo del patrullaje
        /// </summary>
        public string TiempoVuelo;
        /// <summary>
        /// CALZONAZO !!!
        /// </summary>
        public string CalzoCalzo;
        /// <summary>
        /// Observaciones del patrullaje
        /// </summary>
        public string Observaciones;
        /// <summary>
        /// Fecha del patrullaje
        /// </summary>
        public string FechaPatrullaje;
        /// <summary>
        /// Cantidad de comandantes de la instalación participante en el patrullaje
        /// </summary>
        public int ComandantesInstalacionSSF;
        /// <summary>
        /// cantidad de personal militar oficial de SEDENA participante en el patrullaje
        /// </summary>
        public int PersonalMilitarSEDENAOficial;
        /// <summary>
        /// Kilómetros recorridos en el patrullaje
        /// </summary>
        public int KmRecorrido;
        /// <summary>
        /// Indicador del estado de la tarjeta informativa
        /// </summary>
        public int IdEstadoTarjetaInformativa;
        /// <summary>
        /// cantidad de personal mimilat de tropa de la SEDENA participante en el patrullaje
        /// </summary>
        public int PersonalMilitarSEDENATropa;
        /// <summary>
        /// Cantidad de linieros participantes en el patrullaje
        /// </summary>
        public int Linieros;
        /// <summary>
        /// Cantidad de comandantes de turno de SSF participantes en el patrullaje
        /// </summary>
        public int ComandantesTurnoSSF;
        /// <summary>
        /// Cantidad de oficiales de SSF participantes en el patrullaje
        /// </summary>
        public int OficialesSSF;
        /// <summary>
        /// Cantidad de personal naval oficial de la SEMAR participante en el patrullaje
        /// </summary>
        public int PersonalNavalSEMAROficial;
        /// <summary>
        /// Cantidad de personal naval de tropa de la SEMAR participante en el patrullaje
        /// </summary>
        public int PersonalNavalSEMARTropa;
        /// <summary>
        /// Fecha de término del patrullaje
        /// </summary>
        public string FechaTermino;
        /// <summary>
        /// Identificador del estado del patrullaje
        /// </summary>
        public int IdEstadoPatrullaje;
        /// <summary>
        /// Descripción del estado del patrullaje
        /// </summary>
        public string DescripcionEstadoPatrullaje;
        /// <summary>
        /// Descirpción de las matrículas participantes en el patrullaje
        /// </summary>
        public string Matriculas;
        /// <summary>
        /// Descripción de los itinerarios realizados en el patrullaje
        /// </summary>
        public string Itinerarios;
        /// <summary>
        /// Descripción de los reportes del patrullaje
        /// </summary>
        public string Reportes;
        /// <summary>
        /// Descripción de los odómetros generados en el patrullaje
        /// </summary>
        public string Odometros;
        /// <summary>
        /// Descripción de kilómetros recorridos por vehiculos durante el patrullaje
        /// </summary>
        public string KmVehiculos;
        /// <summary>
        /// Fecha de última actualización de la tarjeta informativa
        /// </summary>
        public string UltimaActualizacion;
        /// <summary>
        /// Identificador de la región SSf del patrullaje
        /// </summary>
        public int Region;
        /// <summary>
        /// Identificador de la ruta realizada en el patrullaje
        /// </summary>
        public int IdRuta;
        /// <summary>
        /// Identificador del tipo de patrullaje realizado
        /// </summary>
        public int IdTipoPatullaje;
    }
}
