using Domain.Entities;
using Domain.Entities.Vistas;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SqlServerAdapter.Data
{
    public class UsoVehiculoContext : DbContext
    {
        public UsoVehiculoContext(DbContextOptions<UsoVehiculoContext> options)
        : base(options)
        {
        }

        public DbSet<UsoVehiculo> UsosVehiculos { get; set; }
        public DbSet<Vehiculo> Vehiculos { get; set; }
        public DbSet<UsoVehiculoVista> UsosVehiculosVista { get; set; }
    }
}
