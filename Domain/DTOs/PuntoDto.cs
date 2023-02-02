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

        public int esInstalacion { get; set; }

        public int? id_nivelRiesgo { get; set; }

        public int? id_comandancia { get; set; }

        public int id_ProcesoResponsable { get; set; }

        public int id_GerenciaDivision { get; set; }    

        public int bloqueado { get; set; }

        public int id_municipio { get; set; }

        public string municipio { get; set; }
        public int id_estado { get; set; }
        public string estado { get; set; }
    }
}
