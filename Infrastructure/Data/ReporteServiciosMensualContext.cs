using Domain.Entities.Vistas;
using Microsoft.EntityFrameworkCore;

namespace SqlServerAdapter.Data
{
    public class ReporteServiciosMensualContext : DbContext
    {
        public ReporteServiciosMensualContext(DbContextOptions<ReporteServiciosMensualContext> options)
        : base(options)
        {
            
        }

        public DbSet<ReporteServicioMensualVista> ReporteServiciosMensual { get; set; }
        public DbSet<DetalleReporteServicioMensualVista> DetallesReporteServicioMensualVista { get; set; }
        
    }
}
