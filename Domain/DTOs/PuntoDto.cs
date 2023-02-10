using Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DTOs
{
    public class PuntoDto
    {
        public int id_punto { get; set; }

        public string ubicacion { get; set; }

        public string coordenadas { get; set; }

        public bool esInstalacion { get; set; }

        public short id_nivelRiesgo { get; set; }

        public short id_comandancia { get; set; }

        public short id_ProcesoResponsable { get; set; }

        public short id_GerenciaDivision { get; set; }    

        public bool bloqueado { get; set; }

        public short id_municipio { get; set; }

        public string municipio { get; set; }
        public short id_estado { get; set; }
        public string estado { get; set; }
    }
}
