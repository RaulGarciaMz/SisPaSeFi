using Domain.Entities.Vistas;
using Microsoft.EntityFrameworkCore;

namespace SqlServerAdapter.Data
{
    public class EstadisticasContext : DbContext
    {
        public EstadisticasContext(DbContextOptions<EstadisticasContext> options)
        : base(options)
        {
            
        }

        public DbSet<EstadisticaSistemaVista> EstadisticasSistemaVista { get; set; }
    }
}
