using Domain.Entities;
using Domain.Entities.Vistas;
using Microsoft.EntityFrameworkCore;

namespace SqlServerAdapter.Data
{
    public class TerminacionPatrullajeContext : DbContext
    {
        public TerminacionPatrullajeContext(DbContextOptions<TerminacionPatrullajeContext> options)
        : base(options)
        {
        }

        public DbSet<ProgramaPatrullaje> ProgramasPatrullaje { get; set; }
        public DbSet<TarjetaInformativa> TarjetasInformativas { get; set; }
        public DbSet<UsoVehiculo> UsosVehiculo { get; set; }
        public DbSet<ProgramaRegionVista> ProgramasRegionVista { get; set; }        
    }
}
