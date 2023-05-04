using Domain.Entities;
using Domain.Ports.Driving;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.Intrinsics.X86;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Domain.Entities.Vistas
{
    [Keyless]
    public class PatrullajeVista
    {
        public int id { get; set; }
        public int id_ruta { get; set; }
        public DateTime fechapatrullaje { get; set; }
        public TimeSpan? inicio{ get; set; }
        public int id_puntoresponsable{ get; set; }
        public string clave{ get; set; }
        public string regionmilitarsdn{ get; set; }
        public string regionssf{ get; set; }
        public string observacionesruta{ get; set; }
        public string descripcionestadopatrullaje{ get; set; }
        public string observaciones{ get; set; }
        public int riesgopatrullaje{ get; set; }
        public string solicitudoficiocomision{ get; set; }
        public string oficiocomision{ get; set; }
        public string descripcionnivel{ get; set; }
        public string itinerario{ get; set; }
        public DateTime ultimaactualizacion{ get; set; }
        public int id_usuario{ get; set; }
        public int id_usuarioresponsablepatrullaje{ get; set; }
        public DateTime fechatermino{ get; set; }
        public int? id_apoyopatrullaje { get; set; }
    }
}

