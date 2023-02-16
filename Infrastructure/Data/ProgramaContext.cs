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
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseSqlServer("Data source = E02626; Initial Catalog = ssf; Integrated Security=True; TrustServerCertificate=True; Trusted_Connection=True; User Id=sa; Password=mi4lia5es_rg@rci@");
            optionsBuilder.LogTo(message => Debug.WriteLine(message));
        }


        public DbSet<ProgramaPatrullaje> ProgramasPatrullajes { get; set; }     
        public DbSet<PatrullajeVista> PatrullajesVista { get; set; }
    }
}
