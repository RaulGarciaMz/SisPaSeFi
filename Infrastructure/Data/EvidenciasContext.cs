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
    public class EvidenciasContext : DbContext
    {
        public EvidenciasContext(DbContextOptions<EvidenciasContext> options)
        : base(options)
        {
        }

        public DbSet<EvidenciaIncidencia> EvidenciasEstructura { get; set; }
        public DbSet<EvidenciaIncidenciaPunto> EvidenciasInstalacion { get; set; }

        public DbSet<EvidenciaVista> EvidenciasEstructuraVista { get; set; }
        public DbSet<EvidenciaVista> EvidenciasInstalacionVista { get; set; }
    }
}
