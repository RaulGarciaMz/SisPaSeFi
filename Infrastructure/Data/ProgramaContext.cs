using Domain.Entities;
using Domain.Entities.Vistas;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SqlServerAdapter.Data
{
    public class ProgramaContext : DbContext
    {

        public ProgramaContext(DbContextOptions<ProgramaContext> options)
            : base(options)
        {

        }
/*        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseSqlServer("Data source = E02626; Initial Catalog = ssf; Integrated Security=True; TrustServerCertificate=True; Trusted_Connection=True; User Id=sa; Password=mi4lia5es_rg@rci@");
            optionsBuilder.LogTo(message => Debug.WriteLine(message));
        }*/

        public DbSet<ClasePatrullaje> ClasesPatrullaje { get; set; }
        public DbSet<PropuestaPatrullajeComplementossf> PropuestasComplementosSsf { get; set; }
        public DbSet<PropuestaPatrullajeVehiculo> PropuestasVehiculos { get; set; }
        public DbSet<PropuestaPatrullajeLinea> PropuestasLineas { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<EstadoPropuesta> EstadosPropuesta { get; set; }
        public DbSet<PropuestaPatrullaje> PropuestasPatrullajes { get; set; }
        public DbSet<ProgramaPatrullaje> ProgramasPatrullajes { get; set; }     
        public DbSet<PatrullajeVista> PatrullajesVista { get; set; }
    }
}
