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
    public class VehiculosPatrullajeContext : DbContext
    {
        public VehiculosPatrullajeContext(DbContextOptions<VehiculosPatrullajeContext> options)
        : base(options)
        {
        }

        public DbSet<VehiculoPatrullajeVista> VehiculosPatrullajeVistas { get; set; }
        public DbSet<Vehiculo> Vehiculos { get; set; }
        public DbSet<UsoVehiculo> UsosVehiculos { get; set; }
    }
}
