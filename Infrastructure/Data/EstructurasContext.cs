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
    public class EstructurasContext: DbContext
    {
        public EstructurasContext(DbContextOptions<EstructurasContext> options)
        : base(options)
        {
        }

        public DbSet<Estructura> Estructuras { get; set; }
        public DbSet<EstructurasVista> EstructurasVistas { get; set; }
    }
}
