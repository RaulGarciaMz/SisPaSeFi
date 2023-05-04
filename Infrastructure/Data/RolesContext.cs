using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace SqlServerAdapter.Data
{
    public class RolesContext : DbContext
    {
        public RolesContext(DbContextOptions<RolesContext> options)
        : base(options)
        {
        }

        public DbSet<Rol> Roles { get; set; }

    }
}
