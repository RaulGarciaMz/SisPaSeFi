using Domain.Entities;
using Domain.Entities.Vistas;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SqlServerAdapter.Data
{
    public class DocumentosContext : DbContext
    {
        public DocumentosContext(DbContextOptions<DocumentosContext> options)
        : base(options)
        {            
        }

        public DbSet<DocumentosVista> DocumentosVista { get; set; }
    }
}
