using Domain.Entities;
using Domain.Entities.Vistas;
using Microsoft.EntityFrameworkCore;

namespace SqlServerAdapter.Data
{
    public class TarjetaInformativaContext : DbContext
    {
        public TarjetaInformativaContext(DbContextOptions<TarjetaInformativaContext> options)
            : base(options)
        {

        }

        public DbSet<TarjetaInformativaVista> TarjetasInformativasVista { get; set; }
        public DbSet<TarjetaInformativaIdVista> TarjetaInformativaIdVista { get; set; }
        public DbSet<TarjetaInformativa> TarjetasInformativas { get; set; }
        public DbSet<ProgramaPatrullaje> Programas { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }
    }
}
