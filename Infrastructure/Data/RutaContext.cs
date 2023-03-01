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
    public class RutaContext : DbContext
    {
        public RutaContext(DbContextOptions<RutaContext> options)
            : base(options)
        {

        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseSqlServer("Data source = E02626; Initial Catalog = ssf; Integrated Security=True; TrustServerCertificate=True; Trusted_Connection=True; User Id=sa; Password=mi4lia5es_rg@rci@");
        }

        public DbSet<Ruta> Rutas { get; set; }
        public DbSet<RutaVista> RutasVista { get; set; }
        public DbSet<TipoPatrullaje> TiposPatrullaje { get; set; }

        public DbSet<PuntoPatrullaje> PuntosPatrullaje { get; set; }
        public DbSet<Itinerario> Itinerarios { get; set; }

        public DbSet<ProgramaPatrullaje> Programas { get; set; }
        public DbSet<PropuestaPatrullaje> Propuestas { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }
    }
}
