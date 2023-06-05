using Domain.Entities;
using Domain.Entities.Vistas;
using Microsoft.EntityFrameworkCore;

namespace SqlServerAdapter.Data
{
    public class PatrullajeContext : DbContext
    {
        public PatrullajeContext(DbContextOptions<PatrullajeContext> options) 
            : base(options)
        {
        }

        public  DbSet<PuntoPatrullaje> puntospatrullaje { get; set; }
        public DbSet<PuntoPatrullajeVista> PuntosPatrullajeVista { get; set; }
        public  DbSet<Municipio> Municipios { get; set; }
        public  DbSet<EstadoPais> Estados { get; set; }
        public DbSet<Itinerario> Itinerarios { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }
    }
}
