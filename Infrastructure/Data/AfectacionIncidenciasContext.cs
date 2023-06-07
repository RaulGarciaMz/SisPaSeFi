using Domain.Entities;
using Domain.Entities.Vistas;
using Microsoft.EntityFrameworkCore;

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
        public DbSet<TipoReporte> TiposReporte { get; set; }
    }
}
