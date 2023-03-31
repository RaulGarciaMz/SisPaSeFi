using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.Vistas
{
    [Keyless]
    public class CatalogoVista
    {
        public int id { get; set; }
        public string nombre { get; set; }
    }
}
