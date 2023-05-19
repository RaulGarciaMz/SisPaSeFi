using Domain.Entities.Vistas;
using Microsoft.EntityFrameworkCore;

namespace SqlServerAdapter.Data
{
    public class OficioAutorizacionContext : DbContext
    {
        public OficioAutorizacionContext(DbContextOptions<OficioAutorizacionContext> options)
        : base(options)
        {
        }

        public DbSet<LineaEnPropuestaVista> LineasEnPropuestaVista { get; set; }
        public DbSet<VehiculoEnPropuestaVista> VehiculosEnPropuesta { get; set; }
        public DbSet<PropuestaConResponsableVista> Propuestas { get; set; }

    }
}
