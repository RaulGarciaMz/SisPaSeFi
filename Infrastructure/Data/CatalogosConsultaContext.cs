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
    public class CatalogosConsultaContext : DbContext
    {
        public CatalogosConsultaContext(DbContextOptions<CatalogosConsultaContext> options)
            : base(options)
        {
        }

        public DbSet<ComandanciaRegional> Comandancias { get; set; }
        public DbSet<TipoPatrullaje> TiposPatrullaje { get; set; }
        public virtual DbSet<TipoVehiculo> TiposVehiculo { get; set; }
        public virtual DbSet<ClasificacionIncidencia> ClasificacionesIncidencia { get; set; }
        public virtual DbSet<Nivel> Niveles { get; set; }
        public virtual DbSet<ConceptoAfectacion> ConceptosAfectacion { get; set; }
        public virtual DbSet<Usuario> Usuarios { get; set; }
        public virtual DbSet<Ruta> Rutas { get; set; }
        public virtual DbSet<EstadoPais> EstadosPais { get; set; }
        public virtual DbSet<Municipio> Municipios { get; set; }
        public virtual DbSet<ProcesoResponsable> ProcesosResponsables { get; set; }
        public virtual DbSet<TipoDocumento> TiposDocumentos { get; set; }
        public virtual DbSet<ResultadoPatrullaje> ResultadosPatrullaje { get; set; }
        public virtual DbSet<CatalogoVista> CatalogosVista { get; set; }
        public virtual DbSet<EstadoPatrullaje> EstadosPatrullaje { get; set; }
        public virtual DbSet<ApoyoPatrullaje> ApoyosPatrullaje { get; set; }
        public virtual DbSet<PuntoPatrullaje> PuntosPatrullaje { get; set; }
        public virtual DbSet<NivelRiesgo> NivelesRiesgo { get; set; }
    }
}

