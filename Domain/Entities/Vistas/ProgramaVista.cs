using Domain.Ports.Driving;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Intrinsics.X86;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Domain.Entities.Vistas
{
    public class PatrullajeVista
    {
        [Key]
        public int id;
        public int id_ruta;
        public DateTime fechapatrullaje;
        public TimeSpan inicio;
        public int id_puntoresponsable;
        public string clave;
        public int regionmilitarsdn;
        public int regionssf;
        public string observacionesruta;
        public string descripcionestadopatrullaje;
        public string observaciones;
        public int riesgopatrullaje;
        public string solicitudoficiocomision;
        public string oficiocomision;
        public string descripcionnivel;
        public string itinerario;
        public DateTime ultimaactualizacion;
        public int id_usuario;
        public int id_usuarioresponsablepatrullaje;
        public DateTime fechatermino;
    }
}

