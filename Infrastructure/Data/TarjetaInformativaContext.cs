using Domain.Entities.Vistas;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SqlServerAdapter.Data
{
    public class TarjetaInformativaContext : DbContext
    {
        public TarjetaInformativaContext(DbContextOptions<TarjetaInformativaContext> options)
            : base(options)
        {

        }

        public DbSet<TarjetaInformativaVista> TarjetasInformativasVista { get; set; }
        public DbSet<TarjetaInformativa> TarjetasInformativas { get; set; }
        public DbSet<ProgramaPatrullaje> Programas { get; set; }

        public DbSet<Usuario> Usuarios { get; set; }
    }
}
