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
        public string IdRiesgoPatrullaje;
        public string SolicitudOficioComision;
        public string OficioComision;
        public string DescripcionNivelRiesgo;
        public string Itinerario;
        public string UltimaActualizacion;
        public int IdUsuario;
        public int UsuarioResponsablePatrullaje;
        public int IdRutaOriginal;
        public int IdPropuestaPatrullaje;
        public string FechaTermino;
        public List<int> LstPropuestasPatrullajesVehiculos;
        public List<int> LstPropuestasPatrullajesLineas;
        public List<string> LstPropuestasPatrullajesFechas;
        public int ApoyoPatrullaje;
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
