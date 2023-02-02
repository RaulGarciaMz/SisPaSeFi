using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SqlServerAdapter.Data
{
    public class PatrullajeContext : DbContext
    {
    /*    public PatrullajeContext(DbContextOptions<PatrullajeContext> options) : base(options)
        {
        }*/

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseSqlServer("Data source = E02626; Initial Catalog = ssf; Integrated Security=True; TrustServerCertificate=True; Trusted_Connection=True; User Id=sa; Password=mi4lia5es_rg@rci@");
        }

        public  DbSet<PuntoPatrullaje> puntospatrullaje { get; set; }
        public  DbSet<Municipio> Municipios { get; set; }
        public  DbSet<EstadosPais> Estados { get; set; }
        public DbSet<Itinerario> Itinerarios { get; set; }
    }
}
