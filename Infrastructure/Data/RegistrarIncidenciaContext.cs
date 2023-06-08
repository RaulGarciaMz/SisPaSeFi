﻿using Domain.Entities;
using Domain.Entities.Vistas;
using Microsoft.EntityFrameworkCore;

namespace SqlServerAdapter.Data
{
    public class RegistrarIncidenciaContext : DbContext
    {
        public RegistrarIncidenciaContext(DbContextOptions<RegistrarIncidenciaContext> options)
        : base(options)
        {
                    
        }

        public DbSet<AfectacionIncidencia> AfectacionesIncidencia { get; set; }
        public DbSet<EvidenciaIncidencia> EvidenciasIncidencia { get; set; }
        public DbSet<EvidenciaIncidenciaPunto> EvidenciasIncidenciaInstalacion { get; set; }

        public DbSet<ReportePunto> ReportesInstalacion { get; set; }
        public DbSet<ReporteEstructura> ReportesEstructuras { get; set; }
        public DbSet<TarjetaInformativa> TarjetasInformativas { get; set; }
        public DbSet<ProgramaRegionVista> ProgramasRegionesVista { get; set; }
        public DbSet<EstadoIncidencia> EstadosIncidencia { get; set; }
        public DbSet<TipoReporte> TiposReporte { get; set; }
        public DbSet<EstadoPatrullaje> EstadosPatrullaje { get; set; }

        public DbSet<ProgramaPatrullaje> Programas { get; set; }
        public DbSet<Ruta> Rutas { get; set; }

    }
}

 
