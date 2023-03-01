using Domain.Entities.Vistas;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SqlServerAdapter.Data
{
    public class TarjetaInformativaContext : DbContext
    {
        public TarjetaInformativaContext(DbContextOptions<TarjetaInformativaContext> options)
            : base(options)
        {

        }
/*        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseSqlServer("Data source = E02626; Initial Catalog = ssf; Integrated Security=True; TrustServerCertificate=True; Trusted_Connection=True; User Id=sa; Password=mi4lia5es_rg@rci@");
        }*/

        public DbSet<TarjetaInformativaVista> TarjetasInformativasVista { get; set; }
        public DbSet<TarjetaInformativa> TarjetasInformativas { get; set; }
        public DbSet<ProgramaPatrullaje> Programas { get; set; }

        public DbSet<Usuario> Usuarios { get; set; }
    }
}
