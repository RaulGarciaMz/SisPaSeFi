using Domain.Entities;
using Domain.Entities.Vistas;
using Microsoft.EntityFrameworkCore;

namespace SqlServerAdapter.Data
{
    public class BitacoraIncidenciaContext : DbContext
    {
        public BitacoraIncidenciaContext(DbContextOptions<BitacoraIncidenciaContext> options)
        : base(options)
        {            
        }

        public DbSet<BitacoraSeguimientoVista> BitacoraSeguimientosVista { get; set; }
        public DbSet<BitacoraSeguimientoIncidencia> BitacorasIncidenciaEstructura { get; set; }
        public DbSet<BitacoraSeguimientoIncidenciaPunto> BitacorasIncidenciaInstalacion { get; set; }
        public DbSet<ReportePunto> ReportesInstalacion { get; set; }
        public DbSet<ReporteEstructura> ReportesEstructura { get; set; }
    }
}
