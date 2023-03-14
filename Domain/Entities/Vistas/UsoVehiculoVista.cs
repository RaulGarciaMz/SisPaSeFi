using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.Vistas
{
    [Keyless]
    public class UsoVehiculoVista
    {
        public int id_usoVehiculo { get; set; }
        public int id_programa { get; set; }
        public int id_vehiculo { get; set; }
        public int id_usuarioVehiculo { get; set; }
        public int kmInicio { get; set; }
        public int kmFin { get; set; }
        public int consumoCombustible { get; set; }
        public string? estadoVehiculo { get; set; }
        public string? numeroEconomico { get; set; }
        public string matricula { get; set; }
    }
}
