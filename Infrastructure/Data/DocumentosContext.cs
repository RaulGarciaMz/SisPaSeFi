using Domain.Entities;
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

        public DbSet<DocumentoPatrullaje> Documentos { get; set; }
    }
}
