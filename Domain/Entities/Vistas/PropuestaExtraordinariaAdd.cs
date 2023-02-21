using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.Vistas
{
    public class PropuestaExtraordinariaAdd
    {
        public PropuestaPatrullaje Propuesta { get; set; }
        public List<PropuestaPatrullajeVehiculo> Vehiculos { get;set; }
        public List<PropuestaPatrullajeLinea> Lineas { get; set; }
    }
}
