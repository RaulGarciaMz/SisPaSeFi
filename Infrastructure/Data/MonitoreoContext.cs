using Domain.Entities;
using Domain.Entities.Vistas;
using Microsoft.EntityFrameworkCore;

namespace SqlServerAdapter.Data
{
    public class MonitoreoContext : DbContext
    {
        public MonitoreoContext(DbContextOptions<MonitoreoContext> options)
        : base(options)
        {
        }

        public DbSet<MonitoreoVista> MonitoreosVista { get; set; }
        public DbSet<PuntoEnRutaVista> PuntosEnRutaVista { get; set; }
        public DbSet<TarjetaInformativa> TarjetasInformativas { get; set; }
        public DbSet<IncidenciaTarjetaVista> IncidenciasTarjetasVista { get; set; }
        public DbSet<UsoVehiculoMonitoreoVista> UsosVehiculosMonitoreoVista { get; set; }

        

    }
}
