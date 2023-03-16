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
    public class IncidenciaContext : DbContext
    {
        public IncidenciaContext(DbContextOptions<IncidenciaContext> options)
    : base(options)
        {
        }

        public DbSet<IncidenciaEstructuraVista> IncidenciasEstructuras { get; set; }
        public DbSet<IncidenciaInstalacionVista> IncidenciasInstalaciones { get; set; }
        public DbSet<ReporteEstructura> ReportesEstructuras { get; set; }
        public DbSet<ReportePunto> ReportesInstalaciones { get; set; }
        public DbSet<ReporteIncidenciaAbierto> ReportesIncidenciasAbiertos { get; set; }
        

        //public DbSet<TarjetaInformativaReporte> TarjetaInformativaReportes { get; set; }
    }
}
