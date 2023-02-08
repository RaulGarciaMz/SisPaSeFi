using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Domain.Entities.Vistas
{
    public class ProgramaVista
    {
        [Key]
        public int IdPrograma;
        public int IdRuta;
        public string FechaPatrullaje;
        public string FechaTermino;
        public string Inicio;
        public int IdPuntoResponsable;
        public string Clave;
        public int RegionMilitarSDN;
        public int RegionSSF;
        public string ObservacionesRuta;
        public string DescripcionEstadoPatrullaje;
        public string ObservacionesPrograma;
        public int IdRiesgoPatrullaje;
        public string SolicitudOficioComision;
        public string OficioComision;
        public string DescripcionNivelRiesgo;
        public string Itinerario;
        public string UltimaActualizacion;
        public int IdUsuario;
        // public int UsuarioResponsablePatrullaje;
    }

    public class PropuestaVista
    {
        [Key]
        public int IdPrograma;
        public int IdRuta;
        public string FechaPatrullaje;
        public string FechaTermino;
        public string Inicio;
        public int IdPuntoResponsable;
        public string Clave;
        public int RegionMilitarSDN;
        public int RegionSSF;
        public string ObservacionesRuta;
        public string DescripcionEstadoPatrullaje;
        public string ObservacionesPrograma;
        public int IdRiesgoPatrullaje;
        public string SolicitudOficioComision;
        public string OficioComision;
        public string DescripcionNivelRiesgo;
        public string Itinerario;
        public string UltimaActualizacion;
        public int IdUsuario;
        // public int UsuarioResponsablePatrullaje;
    }
}

