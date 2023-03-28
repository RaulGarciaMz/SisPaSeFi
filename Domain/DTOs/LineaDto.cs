using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DTOs
{
    public class LineaDtoForCreate
    {
        public string Clave { get; set; }
        public string Descripcion { get; set; }
        public int IdPuntoInicio { get; set; }
        public int IdPuntoFin { get; set; }
        public string Usuario { get; set; }

    }
    public class LineaDtoForUpdate
    {
        public int IdLinea { get; set; }
        public string Clave { get; set; }
        public string Descripcion { get; set; }
        public int IdPuntoInicio { get; set; }
        public int IdPuntoFin { get; set; }
        public int Bloqueado { get; set; }
        public string Usuario { get; set; }       
    }
}

