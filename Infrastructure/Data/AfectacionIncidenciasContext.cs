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
    public class AfectacionIncidenciasContext : DbContext
    {
        public AfectacionIncidenciasContext(DbContextOptions<AfectacionIncidenciasContext> options)
        : base(options)
        {
        }

        public DbSet<AfectacionIncidenciaVista> AfectacionesIncidenciasVista { get; set; }
        public DbSet<AfectacionIncidencia> AfectacionesIncidencias { get; set; }
    }
}
