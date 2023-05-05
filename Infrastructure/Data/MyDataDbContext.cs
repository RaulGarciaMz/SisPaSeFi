using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Domain.Entities;

public partial class MyDataDbContext : DbContext
{
    public MyDataDbContext()
    {
    }

    public MyDataDbContext(DbContextOptions<MyDataDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Acceso> Accesos { get; set; }

    public virtual DbSet<AfectacionIncidencia> Afectacionincidencia { get; set; }

    public virtual DbSet<Aplicacion> Aplicaciones { get; set; }

    public virtual DbSet<ApoyoPatrullaje> Apoyopatrullajes { get; set; }

    public virtual DbSet<BitacoraSeguimientoIncidenciaPunto> Bitacoraseguimientoincidenciapuntos { get; set; }

    public virtual DbSet<BitacoraSeguimientoIncidencia> Bitacoraseguimientoincidencia { get; set; }

    public virtual DbSet<CatalogoHallazgo> Catalogohallazgos { get; set; }

    public virtual DbSet<ClasePatrullaje> Clasepatrullajes { get; set; }

    public virtual DbSet<ClasificacionIncidencia> Clasificacionincidencia { get; set; }

    public virtual DbSet<ComandanciaAlias> Comandanciaaliases { get; set; }

    public virtual DbSet<ComandanciaRegional> Comandanciasregionales { get; set; }

    public virtual DbSet<ConceptoAfectacion> Conceptosafectacions { get; set; }

    public virtual DbSet<ConceptoAfectacionReporteIncidenciaTransmision> Conceptosafectacionreporteincidenciatransmisions { get; set; }

    public virtual DbSet<Consolidado> Consolidados { get; set; }

    public virtual DbSet<DivisionDistribucion> Divisiondistribucions { get; set; }

    public virtual DbSet<DocumentoPatrullaje> Documentospatrullajes { get; set; }

    public virtual DbSet<Dominio> Dominios { get; set; }

    public virtual DbSet<EstadoPatrullaje> Estadopatrullajes { get; set; }

    public virtual DbSet<EstadoPropuesta> Estadopropuesta { get; set; }

    public virtual DbSet<EstadoIncidencia> Estadosincidencias { get; set; }

    public virtual DbSet<EstadoPais> Estadospais { get; set; }

    public virtual DbSet<EstadoTarjetaInformativa> Estadotarjetainformativas { get; set; }

    public virtual DbSet<Estructura> Estructuras { get; set; }

    public virtual DbSet<Evento> Eventos { get; set; }

    public virtual DbSet<EvidenciaIncidencia> Evidenciaincidencias { get; set; }

    public virtual DbSet<EvidenciaIncidenciaPunto> Evidenciaincidenciaspuntos { get; set; }

    public virtual DbSet<EvidenciaSeguimientoIncidenciaPunto> Evidenciaseguimientoincidenciapuntos { get; set; }

    public virtual DbSet<EvidenciaSeguimientoIncidencia> Evidenciaseguimientoincidencia { get; set; }

    public virtual DbSet<EvidenciaUsoVehiculo> Evidenciausovehiculos { get; set; }

    public virtual DbSet<GerenciaTransmision> Gerenciatransmisions { get; set; }

    public virtual DbSet<Grupo> Grupos { get; set; }

    public virtual DbSet<GrupoCorreoElectronico> Gruposcorreoelectronicos { get; set; }

    public virtual DbSet<HorasVueloMensual> Horasvuelomensuales { get; set; }

    public virtual DbSet<Itinerario> Itinerarios { get; set; }

    public virtual DbSet<Linea> Lineas { get; set; }

    public virtual DbSet<LineaPunto> Lineapuntos { get; set; }

    public virtual DbSet<Localidad> Localidades { get; set; }

    public virtual DbSet<Menu> Menus { get; set; }

    public virtual DbSet<Municipio> Municipios { get; set; }

    public virtual DbSet<Nivel> Niveles { get; set; }

    public virtual DbSet<NivelRiesgo> Nivelriesgos { get; set; }

    public virtual DbSet<NotaInformativa> Notainformativas { get; set; }

    public virtual DbSet<PermisoEdicionProcesoConduccion> Permisosedicionprocesoconduccions { get; set; }

    public virtual DbSet<ProcesoResponsable> Procesosresponsables { get; set; }

    public virtual DbSet<ProgramaPatrullaje> Programapatrullajes { get; set; }

    public virtual DbSet<PropuestaPatrullaje> Propuestaspatrullajes { get; set; }

    public virtual DbSet<PropuestaPatrullajeComplementossf> Propuestaspatrullajescomplementossfs { get; set; }

    public virtual DbSet<PropuestaPatrullajeLinea> Propuestaspatrullajeslineas { get; set; }

    public virtual DbSet<PropuestaPatrullajeVehiculo> Propuestaspatrullajesvehiculos { get; set; }

    public virtual DbSet<PuntoPatrullaje> Puntospatrullajes { get; set; }

    public virtual DbSet<ReporteEstructura> Reporteestructuras { get; set; }

    public virtual DbSet<ReporteIncidenciaTransmision> Reporteincidenciatransmisions { get; set; }

    public virtual DbSet<ReportePunto> Reportepuntos { get; set; }

    public virtual DbSet<ResultadoPatrullaje> Resultadopatrullajes { get; set; }

    public virtual DbSet<Rol> Roles { get; set; }

    public virtual DbSet<RolMenu> Rolmenus { get; set; }

    public virtual DbSet<Ruta> Rutas { get; set; }

    public virtual DbSet<Semana> Semanas { get; set; }

    public virtual DbSet<Sesion> Sesiones { get; set; }

    public virtual DbSet<TarjetaInformativa> Tarjetainformativas { get; set; }

    public virtual DbSet<TarjetaInformativaReporte> Tarjetainformativareportes { get; set; }

    public virtual DbSet<TempPuntosdeRutasporEstado> Temppuntosderutasporestados { get; set; }

    public virtual DbSet<TipoDocumento> Tipodocumentos { get; set; }

    public virtual DbSet<TipoPatrullaje> Tipopatrullajes { get; set; }

    public virtual DbSet<TipoReporte> Tiporeportes { get; set; }

    public virtual DbSet<TipoVehiculo> Tipovehiculos { get; set; }

    public virtual DbSet<UsoVehiculo> Usovehiculos { get; set; }

    public virtual DbSet<Usuario> Usuarios { get; set; }

    public virtual DbSet<UsuarioComandancia> Usuariocomandancia { get; set; }

    public virtual DbSet<UsuarioDocumento> Usuariodocumentos { get; set; }

    public virtual DbSet<UsuarioGrupoCorreoElectronico> Usuariogrupocorreoelectronicos { get; set; }

    public virtual DbSet<UsuarioPatrullaje> Usuariopatrullajes { get; set; }

    public virtual DbSet<UsuarioRol> Usuariorols { get; set; }

    public virtual DbSet<UsuarioMenu> Usuariosmenus { get; set; }

    public virtual DbSet<UsuarioSpotfire> Usuariospotfires { get; set; }

    public virtual DbSet<Vehiculo> Vehiculos { get; set; }

    public virtual DbSet<Vista1> Vista1s { get; set; }

    public virtual DbSet<Vista2> Vista2s { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=E02626;Initial Catalog=ssf;User Id=sa;Password=mi4lia5es_rg@rci@;Persist Security Info=True;TrustServerCertificate=yes");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.UseCollation("SQL_Latin1_General_CP1_CI_AS");

        modelBuilder.Entity<AfectacionIncidencia>(entity =>
        {
            entity.HasKey(e => e.IdAfectacionIncidencia).HasName("PK_afectacionincidencia_id_afectacionIncidencia");

            entity.HasOne(d => d.IdConceptoAfectacionNavigation).WithMany(p => p.Afectacionincidencia).HasConstraintName("afectacionincidencia$afectacionincidencia_ibfk_1");
        });

        modelBuilder.Entity<Aplicacion>(entity =>
        {
            entity.HasKey(e => e.IdAplicacion).HasName("PK_aplicaciones_id_aplicacion");
        });

        modelBuilder.Entity<ApoyoPatrullaje>(entity =>
        {
            entity.HasKey(e => e.IdApoyoPatrullaje).HasName("PK_apoyopatrullaje_id_apoyoPatrullaje");
        });

        modelBuilder.Entity<BitacoraSeguimientoIncidenciaPunto>(entity =>
        {
            entity.HasKey(e => e.IdBitacoraSeguimientoIncidenciaPunto).HasName("PK_bitacoraseguimientoincidenciapunto_id_bitacoraSeguimientoIncidenciaPunto");

            entity.Property(e => e.UltimaActualizacion).HasDefaultValueSql("(getdate())");

            entity.HasOne(d => d.IdEstadoIncidenciaNavigation).WithMany(p => p.Bitacoraseguimientoincidenciapuntos).HasConstraintName("bitacoraseguimientoincidenciapunto$bitacoraSeguimientoIncidenciaPunto_ibfk_3");

            entity.HasOne(d => d.IdReportePuntoNavigation).WithMany(p => p.Bitacoraseguimientoincidenciapuntos)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("bitacoraseguimientoincidenciapunto$bitacoraSeguimientoIncidenciaPunto_ibfk_1");

            entity.HasOne(d => d.IdUsuarioNavigation).WithMany(p => p.Bitacoraseguimientoincidenciapuntos).HasConstraintName("bitacoraseguimientoincidenciapunto$bitacoraSeguimientoIncidenciaPunto_ibfk_2");
        });

        modelBuilder.Entity<BitacoraSeguimientoIncidencia>(entity =>
        {
            entity.HasKey(e => e.IdBitacoraSeguimientoIncidencia).HasName("PK_bitacoraseguimientoincidencia_id_bitacoraSeguimientoIncidencia");

            entity.Property(e => e.UltimaActualizacion).HasDefaultValueSql("(getdate())");

            entity.HasOne(d => d.IdEstadoIncidenciaNavigation).WithMany(p => p.Bitacoraseguimientoincidencia).HasConstraintName("bitacoraseguimientoincidencia$bitacoraSeguimientoIncidencia_ibfk_3");

            entity.HasOne(d => d.IdReporteNavigation).WithMany(p => p.Bitacoraseguimientoincidencia)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("bitacoraseguimientoincidencia$bitacoraSeguimientoIncidencia_ibfk_1");

            entity.HasOne(d => d.IdUsuarioNavigation).WithMany(p => p.Bitacoraseguimientoincidencia).HasConstraintName("bitacoraseguimientoincidencia$bitacoraSeguimientoIncidencia_ibfk_2");
        });

        modelBuilder.Entity<ClasePatrullaje>(entity =>
        {
            entity.HasKey(e => e.IdClasePatrullaje).HasName("PK_clasepatrullaje_id_clasePatrullaje");

            entity.Property(e => e.IdClasePatrullaje).ValueGeneratedNever();
        });

        modelBuilder.Entity<ClasificacionIncidencia>(entity =>
        {
            entity.HasKey(e => e.IdClasificacionIncidencia).HasName("PK_clasificacionincidencia_id_clasificacionIncidencia");
        });

        modelBuilder.Entity<ComandanciaAlias>(entity =>
        {
            entity.HasKey(e => e.Idcomandancia).HasName("PK_comandanciaalias_idcomandancia");

            entity.Property(e => e.Idcomandancia).ValueGeneratedNever();
        });

        modelBuilder.Entity<ComandanciaRegional>(entity =>
        {
            entity.HasKey(e => e.IdComandancia).HasName("PK_comandanciasregionales_id_comandancia");
        });

        modelBuilder.Entity<ConceptoAfectacion>(entity =>
        {
            entity.HasKey(e => e.IdConceptoAfectacion).HasName("PK_conceptosafectacion_id_conceptoAfectacion");
        });

        modelBuilder.Entity<Consolidado>(entity =>
        {
            entity.ToView("consolidado", "ssf");
        });

        modelBuilder.Entity<DivisionDistribucion>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_divisiondistribucion_id");
        });

        modelBuilder.Entity<DocumentoPatrullaje>(entity =>
        {
            entity.HasKey(e => e.IdDocumentoPatrullaje).HasName("PK_documentospatrullaje_id_documentoPatrullaje");

            entity.Property(e => e.FechaRegistro).HasDefaultValueSql("(getdate())");

            entity.HasOne(d => d.IdTipoDocumentoNavigation).WithMany(p => p.Documentospatrullajes).HasConstraintName("documentospatrullaje$documentosPatrullaje_ibfk_1");

            entity.HasOne(d => d.IdUsuarioNavigation).WithMany(p => p.Documentospatrullajes).HasConstraintName("documentospatrullaje$documentosPatrullaje_ibfk_3");
        });

        modelBuilder.Entity<Dominio>(entity =>
        {
            entity.HasKey(e => e.IdDominio).HasName("PK_dominios_id_dominio");
        });

        modelBuilder.Entity<EstadoPatrullaje>(entity =>
        {
            entity.HasKey(e => e.IdEstadoPatrullaje).HasName("PK_estadopatrullaje_id_estadoPatrullaje");

            entity.Property(e => e.IdEstadoPatrullaje).ValueGeneratedNever();
        });

        modelBuilder.Entity<EstadoPropuesta>(entity =>
        {
            entity.HasKey(e => e.IdEstadoPropuesta).HasName("PK_estadopropuesta_id_estadoPropuesta");

            entity.Property(e => e.IdEstadoPropuesta).ValueGeneratedNever();
        });

        modelBuilder.Entity<EstadoIncidencia>(entity =>
        {
            entity.HasKey(e => e.IdEstadoIncidencia).HasName("PK_estadosincidencias_id_estadoIncidencia");

            entity.Property(e => e.IdEstadoIncidencia).ValueGeneratedNever();
        });

        modelBuilder.Entity<EstadoPais>(entity =>
        {
            entity.HasKey(e => e.IdEstado).HasName("PK_estadospais_id_estado");
        });

        modelBuilder.Entity<EstadoTarjetaInformativa>(entity =>
        {
            entity.HasKey(e => e.IdEstadoTarjetaInformativa).HasName("PK_estadotarjetainformativa_id_estadoTarjetaInformativa");
        });

        modelBuilder.Entity<Estructura>(entity =>
        {
            entity.HasKey(e => e.IdEstructura).HasName("PK_estructura_id_estructura");

            entity.Property(e => e.Modif).HasDefaultValueSql("(N'0')");
            entity.Property(e => e.UltimaActualizacion).HasDefaultValueSql("(getdate())");

            entity.HasOne(d => d.IdLineaNavigation).WithMany(p => p.Estructuras)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("estructura$estructura_ibfk_2");

            entity.HasOne(d => d.IdMunicipioNavigation).WithMany(p => p.Estructuras)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("estructura$estructura_ibfk_3");
        });

        modelBuilder.Entity<Evento>(entity =>
        {
            entity.HasKey(e => e.IdEvento).HasName("PK_eventos_id_evento");

            entity.Property(e => e.EstampaTiempo).HasDefaultValueSql("(getdate())");

            entity.HasOne(d => d.IdSesionNavigation).WithMany(p => p.Eventos).HasConstraintName("eventos$Eventos_ibfk_1");
        });

        modelBuilder.Entity<EvidenciaIncidencia>(entity =>
        {
            entity.HasKey(e => e.IdEvidenciaIncidencia).HasName("PK_evidenciaincidencias_id_evidenciaIncidencia");

            entity.Property(e => e.UltimaActualizacion).HasDefaultValueSql("(getdate())");

            entity.HasOne(d => d.IdReporteNavigation).WithMany(p => p.Evidenciaincidencia).HasConstraintName("evidenciaincidencias$evidenciaIncidencias_ibfk_1");
        });

        modelBuilder.Entity<EvidenciaIncidenciaPunto>(entity =>
        {
            entity.HasKey(e => e.IdEvidenciaIncidenciaPunto).HasName("PK_evidenciaincidenciaspunto_id_evidenciaIncidenciaPunto");

            entity.Property(e => e.UltimaActualizacion).HasDefaultValueSql("(getdate())");

            entity.HasOne(d => d.IdReportePuntoNavigation).WithMany(p => p.Evidenciaincidenciaspuntos).HasConstraintName("evidenciaincidenciaspunto$evidenciaIncidenciasPunto_ibfk_1");
        });

        modelBuilder.Entity<EvidenciaSeguimientoIncidenciaPunto>(entity =>
        {
            entity.HasKey(e => e.IdEvidenciaSeguimientoIncidenciaPunto).HasName("PK_evidenciaseguimientoincidenciapunto_id_evidenciaSeguimientoIncidenciaPunto");

            entity.Property(e => e.UltimaActualizacion).HasDefaultValueSql("(getdate())");

            entity.HasOne(d => d.IdBitacoraSeguimientoIncidenciaPuntoNavigation).WithMany(p => p.Evidenciaseguimientoincidenciapuntos).HasConstraintName("evidenciaseguimientoincidenciapunto$evidenciaSeguimientoIncidenciaPunto_ibfk_1");
        });

        modelBuilder.Entity<EvidenciaSeguimientoIncidencia>(entity =>
        {
            entity.HasKey(e => e.IdEvidenciaSeguimientoIncidencia).HasName("PK_evidenciaseguimientoincidencia_id_evidenciaSeguimientoIncidencia");

            entity.Property(e => e.UltimaActualizacion).HasDefaultValueSql("(getdate())");

            entity.HasOne(d => d.IdBitacoraSeguimientoIncidenciaNavigation).WithMany(p => p.Evidenciaseguimientoincidencia).HasConstraintName("evidenciaseguimientoincidencia$evidenciaSeguimientoIncidencia_ibfk_1");
        });

        modelBuilder.Entity<EvidenciaUsoVehiculo>(entity =>
        {
            entity.HasKey(e => e.IdEvidenciaUsoVehiculo).HasName("PK_evidenciausovehiculo_id_evidenciaUsoVehiculo");

            entity.Property(e => e.UltimaActualizacion).HasDefaultValueSql("(getdate())");

            entity.HasOne(d => d.IdUsoVehiculoNavigation).WithMany(p => p.Evidenciausovehiculos).HasConstraintName("evidenciausovehiculo$evidenciaUsoVehiculo_ibfk_1");
        });

        modelBuilder.Entity<GerenciaTransmision>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_gerenciatransmision_id");
        });

        modelBuilder.Entity<Grupo>(entity =>
        {
            entity.HasKey(e => e.IdGrupo).HasName("PK_grupos_IdGrupo");
        });

        modelBuilder.Entity<GrupoCorreoElectronico>(entity =>
        {
            entity.HasKey(e => e.IdGrupoCorreo).HasName("PK_gruposcorreoelectronico_Id_GrupoCorreo");
        });

        modelBuilder.Entity<Itinerario>(entity =>
        {
            entity.HasKey(e => e.IdItinerario).HasName("PK_itinerario_id_itinerario");

            entity.Property(e => e.UltimaActualizacion).HasDefaultValueSql("(getdate())");

            entity.HasOne(d => d.IdPuntoNavigation).WithMany(p => p.Itinerarios).HasConstraintName("itinerario$itinerario_ibfk_2");

            entity.HasOne(d => d.IdRutaNavigation).WithMany(p => p.Itinerarios).HasConstraintName("itinerario$itinerario_ibfk_1");
        });

        modelBuilder.Entity<Linea>(entity =>
        {
            entity.HasKey(e => e.IdLinea).HasName("PK_linea_id_linea");

            entity.Property(e => e.UltimaActualizacion).HasDefaultValueSql("(getdate())");

            entity.HasOne(d => d.IdPuntoInicioNavigation).WithMany(p => p.Lineas)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("linea$linea_ibfk_1");
        });

        modelBuilder.Entity<LineaPunto>(entity =>
        {
            entity.HasOne(d => d.IdLineaNavigation).WithMany().HasConstraintName("lineapunto$lineaPunto_ibfk_2");

            entity.HasOne(d => d.IdPuntoNavigation).WithMany()
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("lineapunto$lineaPunto_ibfk_1");
        });

        modelBuilder.Entity<Localidad>(entity =>
        {
            entity.HasKey(e => e.IdLocalidad).HasName("PK_localidades_id_localidad");
        });

        modelBuilder.Entity<Menu>(entity =>
        {
            entity.HasKey(e => e.IdMenu).HasName("PK_menus_IdMenu");

            entity.Property(e => e.IdGrupo).HasDefaultValueSql("((1))");
            entity.Property(e => e.Padre).HasDefaultValueSql("((0))");
            entity.Property(e => e.Posicion).HasDefaultValueSql("((1))");

            entity.HasOne(d => d.IdGrupoNavigation).WithMany(p => p.Menus).HasConstraintName("menus$menus_ibfk_1");
        });

        modelBuilder.Entity<Municipio>(entity =>
        {
            entity.HasKey(e => e.IdMunicipio).HasName("PK_municipios_id_municipio");

            entity.HasOne(d => d.IdEstadoNavigation).WithMany(p => p.Municipios).HasConstraintName("municipios$municipios_ibfk_1");
        });

        modelBuilder.Entity<Nivel>(entity =>
        {
            entity.HasKey(e => e.IdNivel).HasName("PK_niveles_id_nivel");

            entity.Property(e => e.IdNivel).ValueGeneratedNever();
        });

        modelBuilder.Entity<NivelRiesgo>(entity =>
        {
            entity.HasKey(e => e.IdNivelRiesgo).HasName("PK_nivelriesgo_id_nivelRiesgo");
        });

        modelBuilder.Entity<NotaInformativa>(entity =>
        {
            entity.HasKey(e => e.IdNota).HasName("PK_notainformativa_id_nota");

            entity.Property(e => e.UltimaActualizacion).HasDefaultValueSql("(getdate())");

            entity.HasOne(d => d.IdProgramaNavigation).WithMany(p => p.Notainformativas)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("notainformativa$notaInformativa_ibfk_1");

            entity.HasOne(d => d.IdUsuarioNavigation).WithMany(p => p.Notainformativas).HasConstraintName("notainformativa$notaInformativa_ibfk_2");
        });

        modelBuilder.Entity<PermisoEdicionProcesoConduccion>(entity =>
        {
            entity.HasKey(e => e.Idpermisoedicionprocesoconduccion).HasName("PK_permisosedicionprocesoconduccion_idpermisoedicionprocesoconduccion");
        });

        modelBuilder.Entity<ProcesoResponsable>(entity =>
        {
            entity.HasKey(e => e.IdProcesoResponsable).HasName("PK_procesosresponsables_id_procesoResponsable");

            entity.Property(e => e.Nombre).HasDefaultValueSql("(N'No definido')");
            entity.Property(e => e.Tabla).HasDefaultValueSql("(N'No definido')");
        });

        modelBuilder.Entity<ProgramaPatrullaje>(entity =>
        {
            entity.HasKey(e => e.IdPrograma).HasName("PK_programapatrullajes_id_programa");

            entity.Property(e => e.IdApoyoPatrullaje).HasDefaultValueSql("((1))");
            entity.Property(e => e.Observaciones).HasDefaultValueSql("(N'')");
            entity.Property(e => e.UltimaActualizacion).HasDefaultValueSql("(getdate())");

            entity.HasOne(d => d.IdApoyoPatrullajeNavigation).WithMany(p => p.Programapatrullajes).HasConstraintName("programapatrullajes$programaPatrullajes_ibfk_5");

            entity.HasOne(d => d.IdEstadoPatrullajeNavigation).WithMany(p => p.Programapatrullajes).HasConstraintName("programapatrullajes$programaPatrullajes_ibfk_3");

            entity.HasOne(d => d.IdPuntoResponsableNavigation).WithMany(p => p.Programapatrullajes).HasConstraintName("programapatrullajes$programaPatrullajes_ibfk_4");

            entity.HasOne(d => d.IdRutaNavigation).WithMany(p => p.Programapatrullajes).HasConstraintName("programapatrullajes$programaPatrullajes_ibfk_1");

            entity.HasOne(d => d.IdUsuarioNavigation).WithMany(p => p.Programapatrullajes).HasConstraintName("programapatrullajes$programaPatrullajes_ibfk_2");
        });

        modelBuilder.Entity<PropuestaPatrullaje>(entity =>
        {
            entity.HasKey(e => e.IdPropuestaPatrullaje).HasName("PK_propuestaspatrullajes_id_propuestaPatrullaje");

            entity.Property(e => e.IdApoyoPatrullaje).HasDefaultValueSql("((1))");
            entity.Property(e => e.IdClasePatrullaje).HasDefaultValueSql("((1))");
            entity.Property(e => e.UltimaActualizacion).HasDefaultValueSql("(getdate())");

            entity.HasOne(d => d.IdApoyoPatrullajeNavigation).WithMany(p => p.Propuestaspatrullajes).HasConstraintName("propuestaspatrullajes$propuestasPatrullajes_ibfk_5");

            entity.HasOne(d => d.IdClasePatrullajeNavigation).WithMany(p => p.Propuestaspatrullajes).HasConstraintName("propuestaspatrullajes$propuestasPatrullajes_ibfk_6");

            entity.HasOne(d => d.IdPuntoResponsableNavigation).WithMany(p => p.Propuestaspatrullajes).HasConstraintName("propuestaspatrullajes$propuestasPatrullajes_ibfk_4");

            entity.HasOne(d => d.IdRutaNavigation).WithMany(p => p.Propuestaspatrullajes).HasConstraintName("propuestaspatrullajes$propuestasPatrullajes_ibfk_1");

            entity.HasOne(d => d.IdUsuarioNavigation).WithMany(p => p.Propuestaspatrullajes).HasConstraintName("propuestaspatrullajes$propuestasPatrullajes_ibfk_2");
        });

        modelBuilder.Entity<PropuestaPatrullajeComplementossf>(entity =>
        {
            entity.HasKey(e => e.IdPropuestaPatrullajeComplementoSsf).HasName("PK_propuestaspatrullajescomplementossf_id_propuestaPatrullajeComplementoSSF");

            entity.Property(e => e.FechaTermino).HasDefaultValueSql("('2016-06-05')");
            entity.Property(e => e.IdPropuestaPatrullaje).HasDefaultValueSql("((0))");
        });

        modelBuilder.Entity<PropuestaPatrullajeLinea>(entity =>
        {
            entity.HasOne(d => d.IdLineaNavigation).WithMany().HasConstraintName("propuestaspatrullajeslineas$propuestasPatrullajesLineas_ibfk_2");

            entity.HasOne(d => d.IdPropuestaPatrullajeNavigation).WithMany()
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("propuestaspatrullajeslineas$propuestasPatrullajesLineas_ibfk_1");
        });

        modelBuilder.Entity<PropuestaPatrullajeVehiculo>(entity =>
        {
            entity.HasOne(d => d.IdPropuestaPatrullajeNavigation).WithMany().HasConstraintName("propuestaspatrullajesvehiculos$propuestasPatrullajesVehiculos_ibfk_1");

            entity.HasOne(d => d.IdVehiculoNavigation).WithMany()
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("propuestaspatrullajesvehiculos$propuestasPatrullajesVehiculos_ibfk_2");
        });

        modelBuilder.Entity<PuntoPatrullaje>(entity =>
        {
            entity.HasKey(e => e.IdPunto).HasName("PK_puntospatrullaje_id_punto");

            entity.Property(e => e.IdComandancia).HasDefaultValueSql("((0))");
            entity.Property(e => e.IdNivelRiesgo).HasDefaultValueSql("((0))");
            entity.Property(e => e.IdUsuario).HasDefaultValueSql("((0))");
            entity.Property(e => e.UltimaActualizacion).HasDefaultValueSql("(getdate())");

            entity.HasOne(d => d.IdMunicipioNavigation).WithMany(p => p.Puntospatrullajes).HasConstraintName("puntospatrullaje$puntosPatrullaje_ibfk_1");
        });

        modelBuilder.Entity<ReporteEstructura>(entity =>
        {
            entity.HasKey(e => e.IdReporte).HasName("PK_reporteestructuras_id_reporte");

            entity.Property(e => e.IdClasificacionIncidencia).HasDefaultValueSql("((1))");
            entity.Property(e => e.UltimaActualizacion).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.UltimoRegistroEnBitacora).HasDefaultValueSql("(getdate())");

            entity.HasOne(d => d.EstadoIncidenciaNavigation).WithMany(p => p.Reporteestructuras).HasConstraintName("reporteestructuras$reporteEstructuras_ibfk_3");

            entity.HasOne(d => d.IdEstructuraNavigation).WithMany(p => p.Reporteestructuras).HasConstraintName("reporteestructuras$reporteEstructuras_ibfk_2");
        });

        modelBuilder.Entity<ReportePunto>(entity =>
        {
            entity.HasKey(e => e.IdReportePunto).HasName("PK_reportepunto_id_reportePunto");

            entity.Property(e => e.IdClasificacionIncidencia).HasDefaultValueSql("((1))");
            entity.Property(e => e.UltimaActualizacion).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.UltimoRegistroEnBitacora).HasDefaultValueSql("(getdate())");

            entity.HasOne(d => d.EstadoIncidenciaNavigation).WithMany(p => p.Reportepuntos).HasConstraintName("reportepunto$reportePunto_ibfk_3");

            entity.HasOne(d => d.IdPuntoNavigation).WithMany(p => p.Reportepuntos).HasConstraintName("reportepunto$reportePunto_ibfk_2");
        });

        modelBuilder.Entity<ResultadoPatrullaje>(entity =>
        {
            entity.HasKey(e => e.Idresultadopatrullaje).HasName("PK_resultadopatrullaje_idresultadopatrullaje");

            entity.Property(e => e.Idresultadopatrullaje).ValueGeneratedNever();
        });

        modelBuilder.Entity<Rol>(entity =>
        {
            entity.HasKey(e => e.IdRol).HasName("PK_roles_id_rol");
        });

        modelBuilder.Entity<RolMenu>(entity =>
        {
            entity.Property(e => e.Navegar).HasDefaultValueSql("((0))");

            entity.HasOne(d => d.IdMenuNavigation).WithMany().HasConstraintName("rolmenu$rilMenu_ibfk_2");

            entity.HasOne(d => d.IdRolNavigation).WithMany().HasConstraintName("rolmenu$rolMenu_ibfk_1");
        });

        modelBuilder.Entity<Ruta>(entity =>
        {
            entity.HasKey(e => e.IdRuta).HasName("PK_rutas_id_ruta");

            entity.Property(e => e.ConsecutivoRegionMilitarSdn).HasDefaultValueSql("((1))");
            entity.Property(e => e.Habilitado).HasDefaultValueSql("((1))");
            entity.Property(e => e.IdTipoPatrullaje).HasDefaultValueSql("((1))");
            entity.Property(e => e.Observaciones).HasDefaultValueSql("(N'')");
            entity.Property(e => e.TotalRutasRegionMilitarSdn).HasDefaultValueSql("((1))");
            entity.Property(e => e.UltimaActualizacion).HasDefaultValueSql("(getdate())");

            entity.HasOne(d => d.IdTipoPatrullajeNavigation).WithMany(p => p.Ruta).HasConstraintName("rutas$rutas_ibfk_1");
        });

        modelBuilder.Entity<Semana>(entity =>
        {
            entity.HasKey(e => e.Date).HasName("PK_semana_date");
        });

        modelBuilder.Entity<Sesion>(entity =>
        {
            entity.HasKey(e => e.IdSesion).HasName("PK_sesiones_id_sesion");

            entity.Property(e => e.EstampaTiempoInicio).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.EstampaTiempoTerminacion).HasDefaultValueSql("(getdate())");

            entity.HasOne(d => d.IdUsuarioNavigation).WithMany(p => p.Sesiones).HasConstraintName("sesiones$Sesiones_ibfk_1");
        });

        modelBuilder.Entity<TarjetaInformativa>(entity =>
        {
            entity.HasKey(e => e.IdNota).HasName("PK_tarjetainformativa_id_nota");

            entity.Property(e => e.IdEstadoTarjetaInformativa).HasDefaultValueSql("((1))");
            entity.Property(e => e.Lineaestructurainstalacion).HasDefaultValueSql("(N'')");
            entity.Property(e => e.Responsablevuelo).HasDefaultValueSql("(N'')");
            entity.Property(e => e.UltimaActualizacion).HasDefaultValueSql("(getdate())");

            entity.HasOne(d => d.IdEstadoTarjetaInformativaNavigation).WithMany(p => p.Tarjetainformativas).HasConstraintName("tarjetainformativa$tarjetaInformativa_ibfk_3");

            entity.HasOne(d => d.IdProgramaNavigation).WithMany(p => p.Tarjetainformativas)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("tarjetainformativa$tarjetaInformativa_ibfk_1");

            entity.HasOne(d => d.IdUsuarioNavigation).WithMany(p => p.Tarjetainformativas).HasConstraintName("tarjetainformativa$tarjetaInformativa_ibfk_2");
        });

        modelBuilder.Entity<TipoDocumento>(entity =>
        {
            entity.HasKey(e => e.IdTipoDocumento).HasName("PK_tipodocumento_id_tipoDocumento");

            entity.Property(e => e.IdTipoDocumento).ValueGeneratedNever();
        });

        modelBuilder.Entity<TipoPatrullaje>(entity =>
        {
            entity.HasKey(e => e.IdTipoPatrullaje).HasName("PK_tipopatrullaje_id_tipoPatrullaje");

            entity.Property(e => e.Clave).HasDefaultValueSql("(N'PAE')");
        });

        modelBuilder.Entity<TipoVehiculo>(entity =>
        {
            entity.HasKey(e => e.IdTipoVehiculo).HasName("PK_tipovehiculo_id_tipoVehiculo");
        });

        modelBuilder.Entity<UsoVehiculo>(entity =>
        {
            entity.HasKey(e => e.IdUsoVehiculo).HasName("PK_usovehiculo_id_usoVehiculo");

            entity.HasOne(d => d.IdProgramaNavigation).WithMany(p => p.Usovehiculos)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("usovehiculo$usoVehiculo_ibfk_1");

            entity.HasOne(d => d.IdUsuarioVehiculoNavigation).WithMany(p => p.Usovehiculos).HasConstraintName("usovehiculo$usoVehiculo_ibfk_3");

            entity.HasOne(d => d.IdVehiculoNavigation).WithMany(p => p.Usovehiculos)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("usovehiculo$usoVehiculo_ibfk_2");
        });

        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.HasKey(e => e.IdUsuario).HasName("PK_usuarios_id_usuario");

            entity.Property(e => e.Bloqueado).HasDefaultValueSql("((0))");
            entity.Property(e => e.EstampaTiempoAceptacionUso).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.EstampaTiempoUltimoAcceso).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.Intentos).HasDefaultValueSql("((0))");
            entity.Property(e => e.NotificarAcceso).HasDefaultValueSql("((0))");
            entity.Property(e => e.TiempoEspera).HasDefaultValueSql("((30))");
        });

        modelBuilder.Entity<UsuarioComandancia>(entity =>
        {
            entity.HasOne(d => d.IdComandanciaNavigation).WithMany().HasConstraintName("usuariocomandancia$usuarioComandancia_ibfk_2");

            entity.HasOne(d => d.IdUsuarioNavigation).WithMany().HasConstraintName("usuariocomandancia$usuarioComandancia_ibfk_1");
        });

        modelBuilder.Entity<UsuarioGrupoCorreoElectronico>(entity =>
        {
            entity.HasOne(d => d.IdGrupoCorreoNavigation).WithMany()
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("usuariogrupocorreoelectronico$usuarioGrupoCorreoElectronico_ibfk_2");

            entity.HasOne(d => d.IdUsuarioNavigation).WithMany()
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("usuariogrupocorreoelectronico$usuarioGrupoCorreoElectronico_ibfk_1");
        });

        modelBuilder.Entity<UsuarioRol>(entity =>
        {
            entity.HasOne(d => d.IdRolNavigation).WithMany().HasConstraintName("usuariorol$usuarioRol_ibfk_2");

            entity.HasOne(d => d.IdUsuarioNavigation).WithMany().HasConstraintName("usuariorol$usuarioRol_ibfk_1");
        });

        modelBuilder.Entity<UsuarioMenu>(entity =>
        {
            entity.Property(e => e.Navegar).HasDefaultValueSql("((0))");

            entity.HasOne(d => d.IdMenuNavigation).WithMany().HasConstraintName("usuariosmenus$usuariosmenus_ibfk_2");

            entity.HasOne(d => d.IdUsuarioNavigation).WithMany().HasConstraintName("usuariosmenus$usuariosmenus_ibfk_1");
        });

        modelBuilder.Entity<UsuarioSpotfire>(entity =>
        {
            entity.HasKey(e => e.IdUsuario).HasName("PK_usuariospotfire_id_usuario");

            entity.Property(e => e.IdUsuario).ValueGeneratedNever();
        });

        modelBuilder.Entity<Vehiculo>(entity =>
        {
            entity.HasKey(e => e.IdVehiculo).HasName("PK_vehiculos_id_vehiculo");

            entity.Property(e => e.Habilitado).HasDefaultValueSql("((1))");
            entity.Property(e => e.IdTipoVehiculo).HasDefaultValueSql("((1))");
            entity.Property(e => e.UltimaActualizacion).HasDefaultValueSql("(getdate())");

            entity.HasOne(d => d.IdComandanciaNavigation).WithMany(p => p.Vehiculos)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("vehiculos$vehiculos_ibfk_2");

            entity.HasOne(d => d.IdTipoPatrullajeNavigation).WithMany(p => p.Vehiculos).HasConstraintName("vehiculos$vehiculos_ibfk_1");

            entity.HasOne(d => d.IdTipoVehiculoNavigation).WithMany(p => p.Vehiculos).HasConstraintName("vehiculos$vehiculos_ibfk_3");
        });

        modelBuilder.Entity<Vista1>(entity =>
        {
            entity.ToView("vista1", "ssf");
        });

        modelBuilder.Entity<Vista2>(entity =>
        {
            entity.ToView("vista2", "ssf");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
