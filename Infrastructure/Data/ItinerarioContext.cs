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
    public class ItinerarioContext : DbContext
    {
        public ItinerarioContext(DbContextOptions<ItinerarioContext> options)
        : base(options)
        {
        }

        public DbSet<ItinerarioVista> ItinerariosVista { get; set; }
        public DbSet<Itinerario> Itinerarios { get; set; }
    }
}
