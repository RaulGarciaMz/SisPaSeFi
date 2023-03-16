﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DTOs
{
    public class ItinerarioDtoForCreate
    {
        public int IdItinerario { get; set; }
        public int IdRuta { get; set; }
        public int IdPunto { get; set; }
        public int Posicion { get; set; }
        public int IdProcesoResponsable { get; set; }
        public int IdGerenciaDivision { get; set; }
        public string Ubicacion { get; set; }
        public string Coordenadas { get; set; }
        public string Usuario { get; set; }
    }
}
