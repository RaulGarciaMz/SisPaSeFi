using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DTOs
{
    public class ProgramaDto
    {
        public string strUsuario;
        public string strToken;
        public int intIdPrograma;
        public int intIdRuta;
        public string strFechaPatrullaje;
        public string strInicio;
        public int intIdPuntoResponsable;
        public string strClave;
        public int intRegionMilitarSDN;
        public int intRegionSSF;
        public string strObservacionesRuta;
        public string strDescripcionEstadoPatrullaje;
        public string strObservacionesPrograma;
        public string intIdRiesgoPatrullaje;
        public string strSolicitudOficioComision;
        public string strOficioComision;
        public string strDescripcionNivelRiesgo;
        public string strItinerario;
        public string strUltimaActualizacion;
        public int intIdUsuario;
        public int intUsuarioResponsablePatrullaje;
        public int intidrutaoriginal;
        public int intidpropuestapatrullaje;
        public string strFechaTermino;
        public List<int> lstPropuestasPatrullajesVehiculos;
        public List<int> lstPropuestasPatrullajesLineas;
        public List<string> lstPropuestasPatrullajesFechas;
        public int intApoyoPatrullaje;
    }
}
