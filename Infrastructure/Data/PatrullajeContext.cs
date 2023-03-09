using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SqlServerAdapter.Data
{
    public class PatrullajeContext : DbContext
    {
        public PatrullajeContext(DbContextOptions<PatrullajeContext> options) 
            : base(options)
        {
        }

        public  DbSet<PuntoPatrullaje> puntospatrullaje { get; set; }
        public  DbSet<Municipio> Municipios { get; set; }
        public  DbSet<EstadoPais> Estados { get; set; }
        public DbSet<Itinerario> Itinerarios { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }
    }
}
