using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace SqlServerAdapter.Data
{
    public class RegistroEntradaUsuarioContext : DbContext
    {
        public RegistroEntradaUsuarioContext(DbContextOptions<RegistroEntradaUsuarioContext> options)
        : base(options)
        {
        }

        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Acceso> Accesos { get; set; }
        public DbSet<Sesion> Sesiones { get; set; }
        public DbSet<Evento> Eventos { get; set; }
        public DbSet<Rol> Roles { get; set; }
        public DbSet<Menu> Menues { get; set; }
        public DbSet<UsuarioRol> UsuariosRol { get; set; }
    }
}
