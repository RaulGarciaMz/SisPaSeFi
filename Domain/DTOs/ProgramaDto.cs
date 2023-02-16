using Microsoft.Extensions.Options;
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

    public class PatrullajeDto
    {
        public int IdPrograma;
        public int IdRuta;
        public string FechaPatrullaje;
        public string Inicio;
        public int IdPuntoResponsable;
        public string Clave;
        public int RegionMilitarSDN;
        public int RegionSSF;
        public string ObservacionesRuta;
        public string DescripcionEstadoPatrullaje;
        public string ObservacionesPrograma;
        public string SolicitudOficioComision;
        public string OficioComision;
        public string DescripcionNivelRiesgo;
        public string Itinerario;
        public string UltimaActualizacion;
        public int IdUsuario;
        public int UsuarioResponsablePatrullaje;        
        public string FechaTermino;
    }

}
