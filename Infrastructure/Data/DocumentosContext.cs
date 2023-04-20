using Domain.Entities;
using Domain.Entities.Vistas;
using Microsoft.EntityFrameworkCore;

namespace SqlServerAdapter.Data
{
    public class DocumentosContext : DbContext
    {
        public DocumentosContext(DbContextOptions<DocumentosContext> options)
        : base(options)
        {            
        }

        public DbSet<DocumentosVista> DocumentosVista { get; set; }
        public DbSet<DocumentoPatrullaje> DocumentosPatrullaje { get; set; }
    }
}
