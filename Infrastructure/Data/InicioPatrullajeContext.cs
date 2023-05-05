using Domain.Entities;
using Domain.Entities.Vistas;
using Microsoft.EntityFrameworkCore;

namespace SqlServerAdapter.Data
{
    public class InicioPatrullajeContext : DbContext
    {
        public InicioPatrullajeContext(DbContextOptions<InicioPatrullajeContext> options)
         : base(options)
        {
        }

        public DbSet<TarjetaInformativa> TarjetasInformativas { get; set; }
        public DbSet<ProgramaPatrullaje> ProgramasPatrullaje { get; set; }
        public DbSet<UsoVehiculo> UsosVehiculos { get; set; }
        public DbSet<InicioPatrullajeProgramaVista> IniciosPatrullajesVista { get; set; }
        public DbSet<InicioPatrullajePuntosVista> IniciosPatrullajePuntosVista { get; set; }
        
    }
}
