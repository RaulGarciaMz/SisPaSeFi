using Domain.Entities.Vistas;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SqlServerAdapter.Data
{
    public class ProgramaMensualContext : DbContext
    {
        public ProgramaMensualContext(DbContextOptions<ProgramaMensualContext> options)
        : base(options)
        {
        }

        public DbSet<ResponsableRegionesVista> RegionesMilitares { get; set; }
        public DbSet<ProgramaItinerarioVista> ProgramasItinerarios { get; set; }
    }
}
