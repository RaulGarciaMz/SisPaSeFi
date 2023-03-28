﻿using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.Vistas
{
    [Keyless]
    public class LineaVista
    {
        public int id_linea { get; set; }
        public string clave { get; set; }
        public string descripcion { get; set; }
        public int id_punto_inicio { get; set; }
        public string ubicacion_punto_inicio { get; set; }
        public string estado_punto_inicio { get; set; }
        public int id_punto_fin { get; set; }
        public string ubicacion_punto_fin { get; set; }
        public string municipio_punto_fin { get; set; }
        public string estado_punto_fin { get; set; }
    }
}                                             