using Domain.Entities;
using Domain.Entities.Vistas;
using Microsoft.EntityFrameworkCore;

namespace SqlServerAdapter.Data
{
    public class UsuariosContext: DbContext
    {
        public UsuariosContext(DbContextOptions<UsuariosContext> options)
        : base(options)
        {
        }

        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<UsuarioVista> UsuariosVista { get; set; }
        public DbSet<UsuarioDocumento> UsuarioDocumentos { get; set; }
        public DbSet<UsuarioRegistroVista> UsuariosRegistroVista { get; set; }
        
    }
}
