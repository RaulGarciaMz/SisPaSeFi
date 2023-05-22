using Domain.Entities.Vistas;
using Microsoft.EntityFrameworkCore;

namespace SqlServerAdapter.Data
{
    public class DatosPropuestaExtraordinariaContext : DbContext
    {
        public DatosPropuestaExtraordinariaContext(DbContextOptions<DatosPropuestaExtraordinariaContext> options)
        : base(options)
        {
            
        }

        public DbSet<UbicacionPropuestaExtraVista> UbicacionesPropVista { get; set; }
        public DbSet<VehiculoPropuestaExtraVista> VehiculosPropVista { get; set; }
        public DbSet<UsuarioPropuestaExtraVista> UsuariosPropVista { get; set; }
    }
}
