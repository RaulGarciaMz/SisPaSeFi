using Domain.Ports.Driving;
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

    //Caso 0 Ordinario
    public class PatrullajeProgYExtRegistradoVista
    {
        [Key]
        public int IdPrograma;
        public int IdRuta;
        [DataType(DataType.Date)]
        public DateTime FechaPatrullaje;
        public TimeSpan Inicio;
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
        public DateTime UltimaActualizacion;
        public int IdUsuario;
        public int UsuarioResponsablePatrullaje;
    }


    //Caso 0 Ordinario
    public class PatrullajeProgYExtRegistradoVista
    {
        [Key]
        public int id_programa;
        public int IdRuta;
        [DataType(DataType.Date)]
        public DateTime FechaPatrullaje;
        public TimeSpan Inicio;
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
        public DateTime UltimaActualizacion;
        public int IdUsuario;
        public int UsuarioResponsablePatrullaje;
    }


    a.id_programa,
a.id_ruta,
a.fechapatrullaje,
a.inicio,
a.id_puntoresponsable,
b.clave,
b.regionmilitarsdn,
b.regionssf,
b.observaciones obsruta
, c.descripcionestadopatrullaje,
a.observaciones,
a.riesgopatrullaje
,a.ultimaactualizacion
,a.id_usuario
,a.id_usuarioresponsablepatrullaje,
d.descripcionnivel
,COALESCE(a.solicitudoficiocomision,'') solicitudoficio
,COALESCE(a.oficiocomision,'') oficio

,(SELECT GROUP_CONCAT(g.ubicacion ORDER BY f.posicion ASC SEPARATOR '-')  
    FROM itinerario f, puntospatrullaje g
    WHERE f.id_ruta=a.id_ruta AND f.id_punto= g.id_punto) as itinerario

}

