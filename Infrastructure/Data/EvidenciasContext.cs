using Domain.Entities;
using Domain.Entities.Vistas;
using Microsoft.EntityFrameworkCore;

namespace SqlServerAdapter.Data
{
    public class EvidenciasContext : DbContext
    {
        public EvidenciasContext(DbContextOptions<EvidenciasContext> options)
        : base(options)
        {
        }

        public DbSet<EvidenciaIncidencia> EvidenciasEstructura { get; set; }
        public DbSet<EvidenciaIncidenciaPunto> EvidenciasInstalacion { get; set; }
        public DbSet<EvidenciaSeguimientoIncidencia> EvidenciasSeguimientoEstructura { get; set; }
        public DbSet<EvidenciaSeguimientoIncidenciaPunto> EvidenciasSeguimientoInstalacion { get; set; }

        public DbSet<EvidenciaVista> EvidenciasEstructuraVista { get; set; }
        public DbSet<EvidenciaVista> EvidenciasInstalacionVista { get; set; }
    }
}
