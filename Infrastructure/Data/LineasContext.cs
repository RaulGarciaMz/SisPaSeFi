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
    public class LineasContext : DbContext
    {
        public LineasContext(DbContextOptions<LineasContext> options)
        : base(options)
        {            
        }

        public DbSet<LineaVista> LineasVista { get; set; }
        public DbSet<Linea> Lineas { get; set; }
    }
}
