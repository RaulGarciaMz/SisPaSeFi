using Domain.Entities;
using Domain.Entities.Vistas;
using Microsoft.EntityFrameworkCore;

namespace SqlServerAdapter.Data
{
    public class IncidenciaContext : DbContext
    {
        public IncidenciaContext(DbContextOptions<IncidenciaContext> options)
        : base(options)
        {
        }

        public DbSet<IncidenciaGeneralVista> IncidenciasGenerales { get; set; }
        public DbSet<ReporteEstructura> ReportesEstructuras { get; set; }
        public DbSet<ReportePunto> ReportesInstalaciones { get; set; }
        public DbSet<ReporteIncidenciaAbierto> ReportesIncidenciasAbiertos { get; set; }       
        public DbSet<TarjetaInformativaReporte> TarjetaInformativaReportes { get; set; }
        public DbSet<TipoReporte> TiposReporte { get; set; }
    }
}
