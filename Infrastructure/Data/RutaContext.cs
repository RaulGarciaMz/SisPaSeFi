using Domain.Entities;
using Domain.Entities.Vistas;
using Microsoft.EntityFrameworkCore;

namespace SqlServerAdapter.Data
{
    public class RutaContext : DbContext
    {
        public RutaContext(DbContextOptions<RutaContext> options)
        : base(options)
        {
        }

        public DbSet<Ruta> Rutas { get; set; }
        public DbSet<RutaVista> RutasVista { get; set; }

        public DbSet<RutaDisponibleVista> RutasDisponibleVista { get; set; }
        
        public DbSet<TipoPatrullaje> TiposPatrullaje { get; set; }

        public DbSet<PuntoPatrullaje> PuntosPatrullaje { get; set; }
        public DbSet<Itinerario> Itinerarios { get; set; }

        public DbSet<ProgramaPatrullaje> Programas { get; set; }
        public DbSet<PropuestaPatrullaje> Propuestas { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }
    }
}
