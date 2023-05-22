using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace SqlServerAdapter.Data
{
    public class UbicacionPatrullajeContext : DbContext
    {
        public UbicacionPatrullajeContext(DbContextOptions<UbicacionPatrullajeContext> options)
        : base(options)
        {
        }

        public DbSet<ProgramaPatrullaje> Programas { get; set; }
    }
}
