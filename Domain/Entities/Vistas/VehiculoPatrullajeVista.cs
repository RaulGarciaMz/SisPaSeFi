using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.Vistas
{
    [Keyless]
    public class VehiculoPatrullajeVista
    {
        public int id_vehiculo { get; set; }
        public int id_tipopatrullaje { get; set; }
        public string matricula { get; set; }
        public int? id_comandancia { get; set; }
        public int id_tipovehiculo { get; set; }
        public string? numeroeconomico { get; set; }
        public int habilitado { get; set; }
        public string descripcion { get; set; }
        public string descripciontipoVehiculo { get; set; }
    }
}