using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.Vistas
{
    [Keyless]
    public class InstalacionDeComandanciaVista
    {
        public int IdPunto { get; set; }
        public string Ubicacion { get; set; }
    }
}
