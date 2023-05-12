using Domain.Entities;
using Domain.Entities.Vistas;
using Microsoft.EntityFrameworkCore;

namespace SqlServerAdapter.Data
{
    public class MenuContext : DbContext
    {
        public MenuContext(DbContextOptions<MenuContext> options)
        : base(options)
        {
            
        }

        public DbSet<Menu> Menus { get; set; }
        public DbSet<MenuVista> MenusVista { get; set; }
        
    }
}
