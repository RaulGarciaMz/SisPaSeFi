using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Domain.Entities;


namespace SqlServerAdapter.Data;

public partial class BddContext : DbContext
{
    public BddContext()
    {
    }

    public BddContext(DbContextOptions<BddContext> options)
        : base(options)
    {
    }

    public virtual DbSet<AfectacionIncidencia> AfectacionIncidencia { get; set; }

    public virtual DbSet<ApoyoPatrullaje> ApoyoPatrullajes { get; set; }

    public virtual DbSet<BitacoraSeguimientoReporte> BitacoraSeguimientoReportes { get; set; }

    public virtual DbSet<ClasePatrullaje> ClasePatrullajes { get; set; }

    public virtual DbSet<ComandanciaRegional> ComandanciaRegionals { get; set; }

    public virtual DbSet<ConceptoAfectacion> ConceptoAfectacions { get; set; }

    public virtual DbSet<ControlConsecutivoRutasRegionMilitar> ControlConsecutivoRutasRegionMilitars { get; set; }

    public virtual DbSet<EstadoIncidencia> EstadoIncidencia { get; set; }

    public virtual DbSet<EstadoPais> EstadoPais { get; set; }

    public virtual DbSet<EstadoPatrullaje> EstadoPatrullajes { get; set; }

    public virtual DbSet<EstadoPropuesta> EstadoPropuesta { get; set; }

    public virtual DbSet<EstadoTarjetaInformativa> EstadoTarjetaInformativas { get; set; }

    public virtual DbSet<Estructura> Estructuras { get; set; }

    public virtual DbSet<Evidencia> Evidencia { get; set; }

    public virtual DbSet<InfoEstructura> InfoEstructuras { get; set; }

    public virtual DbSet<InfoPosicionamiento> InfoPosicionamientos { get; set; }

    public virtual DbSet<InfoPuntoPatrullaje> InfoPuntoPatrullajes { get; set; }

    public virtual DbSet<Itinerario> Itinerarios { get; set; }

    public virtual DbSet<Linea> Lineas { get; set; }

    public virtual DbSet<LineaPunto> LineaPuntos { get; set; }

    public virtual DbSet<Municipio> Municipios { get; set; }

    public virtual DbSet<NivelRiesgo> NivelRiesgos { get; set; }

    public virtual DbSet<PersonalParticipantePatrullaje> PersonalParticipantePatrullajes { get; set; }

    public virtual DbSet<Posicionamiento> Posicionamientos { get; set; }

    public virtual DbSet<ProcesoResponsable> ProcesoResponsables { get; set; }

    public virtual DbSet<ProgramaPatrullaje> ProgramaPatrullajes { get; set; }

    public virtual DbSet<PropuestaPatrullaje> PropuestaPatrullajes { get; set; }

    public virtual DbSet<PropuestaPatrullajeFecha> PropuestaPatrullajeFechas { get; set; }

    public virtual DbSet<PropuestaPatrullajeRutaContenedor> PropuestaPatrullajeRutaContenedors { get; set; }

    public virtual DbSet<PuntoPatrullaje> PuntoPatrullajes { get; set; }

    public virtual DbSet<ReporteIncidencia> ReporteIncidencia { get; set; }

    public virtual DbSet<ResumenAcceso> ResumenAccesos { get; set; }

    public virtual DbSet<Ruta> Ruta { get; set; }

    public virtual DbSet<TarjetaInformativa> TarjetaInformativas { get; set; }

    public virtual DbSet<TipoEvidencia> TipoEvidencia { get; set; }

    public virtual DbSet<TipoInicidencia> TipoInicidencia { get; set; }

    public virtual DbSet<TipoPatrullaje> TipoPatrullajes { get; set; }

    public virtual DbSet<TipoPersonalPatrullaje> TipoPersonalPatrullajes { get; set; }

    public virtual DbSet<TipoProcesoResponsable> TipoProcesoResponsables { get; set; }

    public virtual DbSet<TipoReporte> TipoReportes { get; set; }

    public virtual DbSet<TipoVehiculo> TipoVehiculos { get; set; }

    public virtual DbSet<UnidadMedida> UnidadMedida { get; set; }

    public virtual DbSet<UsoVehiculo> UsoVehiculos { get; set; }

    public virtual DbSet<Usuario> Usuarios { get; set; }

    public virtual DbSet<Vehiculo> Vehiculos { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=E02626;Initial Catalog=SSF2;User Id=sa;Password=mi4lia5es_rg@rci@;Persist Security Info=True;TrustServerCertificate=yes", x => x.UseNetTopologySuite());

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<AfectacionIncidencia>(entity =>
        {
            entity.ToTable("Afectacion_Incidencia", "dmn", tb => tb.HasComment("Listado de afectaciones asociadas a los reportes de patrullajes"));

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasComment("Identificador único de la afectación");
            entity.Property(e => e.Cantidad).HasComment("Número de conceptos necesarios para reparar la incidencia");
            entity.Property(e => e.IdConceptoAfectacion).HasComment("Id del concepto o producto que fue afectado y que debe reemplazarse");
            entity.Property(e => e.IdReporte).HasComment("Id del reporte al que se asocia la incidencia");
            entity.Property(e => e.PrecioUnitario).HasComment("Precio unitario estimado del concepto al momento de que se detecta la incidencia");

            entity.HasOne(d => d.IdConceptoAfectacionNavigation).WithMany(p => p.AfectacionIncidencia)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Afectacion_Incidencia_Concepto_Afectacion");

            entity.HasOne(d => d.IdReporteNavigation).WithMany(p => p.AfectacionIncidencia).HasConstraintName("FK_Afectacion_Incidencia_Reporte_Incidencia");
        });

        modelBuilder.Entity<ApoyoPatrullaje>(entity =>
        {
            entity.ToTable("Apoyo_Patrullaje", "cat", tb => tb.HasComment("Catálogo de dependiencias encargadas de brindar el apoyo durante los patrullajes"));

            entity.Property(e => e.Id).HasComment("Identificador único de la dependencia de apoyo del patrullaje");
            entity.Property(e => e.Nombre).HasComment("Nombre de la dependencia");
        });

        modelBuilder.Entity<BitacoraSeguimientoReporte>(entity =>
        {
            entity.ToTable("Bitacora_Seguimiento_Reporte", "dmn", tb => tb.HasComment("Listado de actualizaciones a las incidencias reportadas durante el patrullaje"));

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasComment("Identificador único de la bitácora");
            entity.Property(e => e.Descripcion).HasComment("Nota descriptiva de las actividades realizadas para la corrección de la incidencia");
            entity.Property(e => e.Fecha).HasComment("Fecha en que se agrega el registro a la bitácora");
            entity.Property(e => e.IdReporteIncidencia).HasComment("Id del reporte al que está asociada este registro de la bitácora");
            entity.Property(e => e.IdUsuario).HasComment("Id del usuario que está insertando el registro de seguimiento");

            entity.HasOne(d => d.IdReporteIncidenciaNavigation).WithMany(p => p.BitacoraSeguimientoReportes).HasConstraintName("FK_Bitacora_Seguimiento_Reporte_Reporte_Incidencia");

            entity.HasOne(d => d.IdUsuarioNavigation).WithMany(p => p.BitacoraSeguimientoReportes)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Bitacora_Seguimiento_Reporte_Usuario");
        });

        modelBuilder.Entity<ClasePatrullaje>(entity =>
        {
            entity.ToTable("Clase_Patrullaje", "cat", tb => tb.HasComment("Catálogo de las clases de patrullaje basándose en el tiempo"));

            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .HasComment("Identificador único de la clase de patrullaje");
            entity.Property(e => e.Nombre).HasComment("Nombre de la clase de patrullaje");
        });

        modelBuilder.Entity<ComandanciaRegional>(entity =>
        {
            entity.ToTable("Comandancia_Regional", "cat", tb => tb.HasComment("Catálogo de las Comandancias Regionales de la SSF"));

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasComment("Identificador único de la Comandancia");
            entity.Property(e => e.Alias).HasComment("Alias del ID de la comandancia en formato de número romano");
            entity.Property(e => e.IdPunto).HasComment("Identificador del punto asociado a esta comandancia");
            entity.Property(e => e.IdUsuarioResponsable).HasComment("Identificador del usuario que registró la Comandancia");

            entity.HasOne(d => d.IdUsuarioResponsableNavigation).WithMany(p => p.ComandanciaRegionals).HasConstraintName("FK_Comandancia_Regional_Usuario");
        });

        modelBuilder.Entity<ConceptoAfectacion>(entity =>
        {
            entity.ToTable("Concepto_Afectacion", "cat", tb => tb.HasComment("Catálogo de conceptos afectados que se pueden detectar durante un patrullaje"));

            entity.Property(e => e.Id).HasComment("Identificador único del concepto afectado");
            entity.Property(e => e.IdUnidadMedida).HasComment("Identificador de la unidad de medida aplicada al concepto");
            entity.Property(e => e.Nombre).HasComment("Nombre del concepto afectado");
            entity.Property(e => e.Peso).HasComment("Peso individual del concepto afectado");
            entity.Property(e => e.PrecioUnitario)
                .HasDefaultValueSql("((0.00))")
                .HasComment("Costo derivado de la reposición del concepto afectado");

            entity.HasOne(d => d.IdUnidadMedidaNavigation).WithMany(p => p.ConceptoAfectacions).HasConstraintName("FK_Concepto_Afectacion_Unidad_Medida");
        });

        modelBuilder.Entity<ControlConsecutivoRutasRegionMilitar>(entity =>
        {
            entity.ToTable("Control_Consecutivo_Rutas_Region_Militar", "dmn", tb => tb.HasComment("Tabla de control del consecutivo de rutas de las Regiones Militales de la SDN"));

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasComment("Identficador único de la Region Militar");
            entity.Property(e => e.Alias).HasComment("Identificador en formato de número romano");
            entity.Property(e => e.Consecutivo)
                .HasDefaultValueSql("((1))")
                .HasComment("Número de consecutivo de la ruta. Este número se controla por cada Region militar. ");
        });

        modelBuilder.Entity<EstadoIncidencia>(entity =>
        {
            entity.ToTable("Estado_Incidencia", "cat", tb => tb.HasComment("Catálogo de estados que puede tener una incidencia"));

            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .HasComment("Identificador único del estado de la incidencia");
            entity.Property(e => e.Estado).HasComment("Nombre descriptivo del estado de la incidencia");
        });

        modelBuilder.Entity<EstadoPais>(entity =>
        {
            entity.ToTable("Estado_Pais", "cat", tb => tb.HasComment("Catálogo de Estados integrantes de México"));

            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .HasComment("Identificador único del Estado");
            entity.Property(e => e.Clave).HasComment("Acrónimo del estado");
            entity.Property(e => e.Nombre).HasComment("Nombre del estado");
            entity.Property(e => e.NombreCorto).HasComment("Nombre corto o abreviatura del Estado");
        });

        modelBuilder.Entity<EstadoPatrullaje>(entity =>
        {
            entity.ToTable("Estado_Patrullaje", "cat", tb => tb.HasComment("Catálogo de estados que pueden aplicarse al patrullaje"));

            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .HasComment("Identificador único del estado del patrullaje");
            entity.Property(e => e.Estado).HasComment("Nombre largo del estado de patrullaje");
            entity.Property(e => e.NombreCorto).HasComment("Nombre corto del estado a usar en la aplicación.");
        });

        modelBuilder.Entity<EstadoPropuesta>(entity =>
        {
            entity.ToTable("Estado_Propuesta", "cat", tb => tb.HasComment("Catálogo de estados de la propuesta de patrullaje"));

            entity.Property(e => e.Id).HasComment("Identificador único del estado de la propuesta de patrullaje");
            entity.Property(e => e.Estado).HasComment("Nombre descriptivo del estado de la propuesta");
        });

        modelBuilder.Entity<EstadoTarjetaInformativa>(entity =>
        {
            entity.ToTable("Estado_Tarjeta_Informativa", "cat", tb => tb.HasComment("Catálogo de estados que puede tener una Tarjeta informativa"));

            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .HasComment("Identificador único del estado de la tarjeta informativa");
            entity.Property(e => e.Estado).HasComment("Nombre del estado de la tarjeta informativa");
        });

        modelBuilder.Entity<Estructura>(entity =>
        {
            entity.ToTable("Estructura", "dmn", tb => tb.HasComment("Listado de estructuras sobre las que se encuentran las líneas"));

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasComment("Identificador único de la estructura");
            entity.Property(e => e.Bloqueado).HasComment("Indicador de si la estructura está bloqueada para su modificación o eliminación");
            entity.Property(e => e.CoordenadasSrid).HasComment("Coordenadas geoespaciales de la ubicación de la estructura");
            entity.Property(e => e.IdLinea).HasComment("Identificador único de la línea a la que está asociada la estructura");
            entity.Property(e => e.IdMunicipio).HasComment("Identificador del municipio en el que se encuentra la estructura");
            entity.Property(e => e.IdProcesoResponsable).HasComment("Identificador de la División o Gerencia encargada de la estructura.");
            entity.Property(e => e.Nombre).HasComment("Nombre descriptivo de la estructura");
            entity.Property(e => e.UltimaActualizacion).HasComment("Estampa de tiempo de la última modificación realizada a la estructura");

            entity.HasOne(d => d.IdLineaNavigation).WithMany(p => p.Estructuras)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Estructura_Linea");

            entity.HasOne(d => d.IdMunicipioNavigation).WithMany(p => p.Estructuras).HasConstraintName("FK_Estructura_Municipio");

            entity.HasOne(d => d.IdProcesoResponsableNavigation).WithMany(p => p.Estructuras).HasConstraintName("FK_Estructura_Proceso_Responsable");
        });

        modelBuilder.Entity<Evidencia>(entity =>
        {
            entity.ToTable("Evidencia", "dmn", tb => tb.HasComment("Tabla de control de evidencias derivadas de los patrullajes"));

            entity.Property(e => e.Id).HasComment("Identificador único de la evidencia");
            entity.Property(e => e.IdElementoAsociado).HasComment("Id del reporte o bitácora a la que está asociada la evidencia");
            entity.Property(e => e.ItTipoEvidencia).HasComment("Id del tipo de evidencia que se está agregando");

            entity.HasOne(d => d.ItTipoEvidenciaNavigation).WithMany(p => p.Evidencia)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Evidencia_Tipo_Evidencia");
        });

        modelBuilder.Entity<InfoEstructura>(entity =>
        {
            entity.ToTable("Info_Estructura", "dmn", tb => tb.HasComment("Tabla temporal para almacenar información de coordenadas de las estructuras (se eliminará cuando se cambie la aplicación de mapas)"));

            entity.Property(e => e.IdEstructura)
                .ValueGeneratedNever()
                .HasComment("Identificador único del punto de patrullaje");
            entity.Property(e => e.Coordenadas).HasComment("Campo concatenado de latitud y longitud del punto");
            entity.Property(e => e.Latitud).HasComment("Latitud de las coordenadas del punto de patrullaje");
            entity.Property(e => e.Longitud).HasComment("Longitud de las coordenadas del punto de patrullaje");

            entity.HasOne(d => d.IdEstructuraNavigation).WithOne(p => p.InfoEstructura)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Info_Estructura_Estructura");
        });

        modelBuilder.Entity<InfoPosicionamiento>(entity =>
        {
            entity.ToTable("Info_Posicionamiento", "dmn", tb => tb.HasComment("Información de coordenadas reportadas en formato latitud, longitud"));

            entity.Property(e => e.IdTarjetaInformativa).HasComment("Id de la tarjeta informativa a la que se está asociando la ubicación");
            entity.Property(e => e.Fecha).HasComment("Estampa de tiempo del reporte de ubicación");
            entity.Property(e => e.Latitud).HasComment("Latitud reportada por el equipo");
            entity.Property(e => e.Longitud).HasComment("Longitud reportada por el equipo");
        });

        modelBuilder.Entity<InfoPuntoPatrullaje>(entity =>
        {
            entity.HasKey(e => e.IdPunto).HasName("PK_Info_Punto");

            entity.ToTable("Info_Punto_Patrullaje", "dmn", tb => tb.HasComment("Tabla temporal para almacenar información de coordenadas del Punto de patrullaje (se eliminará cuando se cambie la aplicación de mapas)"));

            entity.Property(e => e.IdPunto)
                .ValueGeneratedNever()
                .HasComment("Identificador único del punto de patrullaje");
            entity.Property(e => e.Coordenadas).HasComment("Campo concatenado de latitud y longitud del punto");
            entity.Property(e => e.Latitud).HasComment("Latitud de las coordenadas del punto de patrullaje");
            entity.Property(e => e.Longitud).HasComment("Longitud de las coordenadas del punto de patrullaje");

            entity.HasOne(d => d.IdPuntoNavigation).WithOne(p => p.InfoPuntoPatrullaje).HasConstraintName("FK_Info_Punto_Patrullaje_Punto_Patrullaje");
        });

        modelBuilder.Entity<Itinerario>(entity =>
        {
            entity.ToTable("Itinerario", "dmn", tb => tb.HasComment("Almacén del detalle de puntos a visitar en la ruta"));

            entity.Property(e => e.IdRuta).HasComment("Identificador único de la ruta a realizar en el itinerario");
            entity.Property(e => e.IdPunto).HasComment("Identificador único del punto de patrullaje a visitar en la ruta");
            entity.Property(e => e.Posicion).HasComment("Indicador del orden de visita de los puntos de patrullaje");

            entity.HasOne(d => d.IdPuntoNavigation).WithMany(p => p.Itinerarios)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Itinerario_Punto_Patrullaje");

            entity.HasOne(d => d.IdRutaNavigation).WithMany(p => p.Itinerarios)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Itinerario_Ruta");
        });

        modelBuilder.Entity<Linea>(entity =>
        {
            entity.ToTable("Linea", "dmn", tb => tb.HasComment("Listado de líneas de transmisión sujetas a patrullaje"));

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasComment("Identificador único de la línea de transmisión");
            entity.Property(e => e.Bloqueado).HasComment("Indicador de sí la línea está bloqueada para su incorporación a una propuesta o programa de patrullaje");
            entity.Property(e => e.Clave).HasComment("Nombre corto de la línea");
            entity.Property(e => e.Descripcion).HasComment("Texto descriptivo de la línea");
            entity.Property(e => e.IdPuntoFin).HasComment("Identificador del punto de patrullaje donde termina la línea");
            entity.Property(e => e.IdPuntoInicio).HasComment("Identificador del punto de patrullaje donde inicia la línea");
            entity.Property(e => e.IdUsuario).HasComment("Identificador del usuario que dio de alta la línea");
            entity.Property(e => e.UltimaActualizacion).HasComment("Fecha de la última actualización");
        });

        modelBuilder.Entity<LineaPunto>(entity =>
        {
            entity.ToTable("Linea_Punto", "dmn", tb => tb.HasComment("Listado de puntos de patrullaje asociados a una línea"));

            entity.Property(e => e.IdLinea).HasComment("Identificador único de una línea");
            entity.Property(e => e.IdPunto).HasComment("Identificador único de un punto de patrullaje");
        });

        modelBuilder.Entity<Municipio>(entity =>
        {
            entity.Property(e => e.Id).HasComment("Identificador único del Municio");
            entity.Property(e => e.Clave).HasComment("Acrónimo del Municipio");
            entity.Property(e => e.IdEstado).HasComment("Identificador del Estado al que pertenece el Municipio");
            entity.Property(e => e.Nombre).HasComment("Nombre del Municipio");

            entity.HasOne(d => d.IdEstadoNavigation).WithMany(p => p.Municipios).HasConstraintName("FK_Municipio_Estado_Pais");
        });

        modelBuilder.Entity<NivelRiesgo>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_nivel");

            entity.ToTable("Nivel_Riesgo", "cat", tb => tb.HasComment("Catálogo del niveles de riesgo asociados al proceso de patrullaje e infraestructura"));

            entity.Property(e => e.Id).HasComment("Identificador único del nivel de riesgo");
            entity.Property(e => e.Alias).HasComment("Acrónimo con el que se asocia la descripción del riesgo");
            entity.Property(e => e.Descripcion).HasComment("Texto descriptivo del nivel de riesgo");
        });

        modelBuilder.Entity<PersonalParticipantePatrullaje>(entity =>
        {
            entity.ToTable("Personal_Participante_Patrullaje", "dmn", tb => tb.HasComment("Relación de personal que apoya durante un Patrullaje"));

            entity.Property(e => e.IdTarjetaInformativa).HasComment("Identificador del programa de patrullaje");
            entity.Property(e => e.IdTipoPersonalPatrullaje).HasComment("Identificador del tipo de personal participante durante un patrullaje");
            entity.Property(e => e.Cantidad).HasComment("Número de personas del tipo seleccionado que participan en un patrullaje");

            entity.HasOne(d => d.IdTarjetaInformativaNavigation).WithMany(p => p.PersonalParticipantePatrullajes)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Personal_Participante_Patrullaje_Tarjeta_Informativa");

            entity.HasOne(d => d.IdTipoPersonalPatrullajeNavigation).WithMany(p => p.PersonalParticipantePatrullajes)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Personal_Participante_Patrullaje_Tipo_Personal_Patrullaje");
        });

        modelBuilder.Entity<Posicionamiento>(entity =>
        {
            entity.ToTable("Posicionamiento", "dmn", tb => tb.HasComment("Listado de ubicaciones de los vehículos durante su trayecto"));

            entity.Property(e => e.IdTarjetaInformativa).HasComment("Identificador de la tarjeta informativa a la que se asocia el posicionamiento");
            entity.Property(e => e.Fecha).HasComment("Estampa de tiempo del reporte de posicionamiento");
            entity.Property(e => e.CoordenadasSrid).HasComment("Coordenadas del reporte del patrullaje en distintos periodos de tiempo");
            entity.Property(e => e.IdEquipo).HasComment("Id del equipo que está realizando el reporte de posicionamiento");

            entity.HasOne(d => d.IdTarjetaInformativaNavigation).WithMany(p => p.Posicionamientos).HasConstraintName("FK_Posicionamiento_Tarjeta_Informativa");

            entity.HasOne(d => d.InfoPosicionamiento).WithOne(p => p.Posicionamiento).HasConstraintName("FK_Posicionamiento_Info_Posicionamiento");
        });

        modelBuilder.Entity<ProcesoResponsable>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_Proceso_Responsable_1");

            entity.ToTable("Proceso_Responsable", "cat", tb => tb.HasComment("Catálogo de los Procesos Responsables de llevar a cabo el proceso de patrullaje"));

            entity.Property(e => e.Id).HasComment("Identificador único del proceso responsable del patrullaje");
            entity.Property(e => e.Clave).HasComment("Acrónimo usado para facilidar la identificación del proceso responable");
            entity.Property(e => e.IdTipoProcesoResponsable).HasComment("Identificador del tipo de proceso responsable del patrullaje asociado");
            entity.Property(e => e.Nombre).HasComment("Nombre de la División o Gerencia responsable del proceso");

            entity.HasOne(d => d.IdTipoProcesoResponsableNavigation).WithMany(p => p.ProcesoResponsables)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Proceso_Responsable_Tipo_Proceso_Responsable");
        });

        modelBuilder.Entity<ProgramaPatrullaje>(entity =>
        {
            entity.ToTable("Programa_Patrullaje", "dmn", tb => tb.HasComment("Listado de Programas de patrullaje creado mediante la autorización de la propuesta de patrullaje"));

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasComment("Identificador único del Programa de Patrullaje");
            entity.Property(e => e.Fecha).HasComment("Fecha del programa de patrullaje. Para extraordinarios sólo se pondra la fecha inicial del rango autorizado");
            entity.Property(e => e.IdApoyoPatrullaje).HasComment("Identificador de la agencia de seguridad encargada de aocmpañar durante el patrullaje");
            entity.Property(e => e.IdEstadoPatrullaje).HasComment("Identificador del estado que guarda la propuesta de patrullaje en el momento actual");
            entity.Property(e => e.IdNivelRiesgo).HasComment("Identificador del nivel de riesgo asociado a la propuesta de patrullaje ");
            entity.Property(e => e.IdPropuestaPatrullaje).HasComment("Identificador de la propuesta de patrullaje; si es null, es  una ruta que se agregó durante la autorización.");
            entity.Property(e => e.IdPuntoResponsable).HasComment("Identificador de la instalación (punto de patrullaje) que es el responsable del patrullaje");
            entity.Property(e => e.IdRuta).HasComment("Identificador de la ruta sobre la que se realizará el patrullaje");
            entity.Property(e => e.IdRutaOriginal).HasComment("Identificador de la ruta de patrullaje que originalmente se solicitó autorizar");
            entity.Property(e => e.IdUsuario).HasComment("Identificador del número de usuario que autoriza la propuesta");
            entity.Property(e => e.Observaciones).HasComment("Notas asociadas al programa de patrullaje durante su autorización");

            entity.HasOne(d => d.IdApoyoPatrullajeNavigation).WithMany(p => p.ProgramaPatrullajes)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Programa_Patrullaje_Apoyo_Patrullaje");

            entity.HasOne(d => d.IdEstadoPatrullajeNavigation).WithMany(p => p.ProgramaPatrullajes)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Programa_Patrullaje_Estado_Patrullaje");

            entity.HasOne(d => d.IdNivelRiesgoNavigation).WithMany(p => p.ProgramaPatrullajes)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Programa_Patrullaje_Nivel_Riesgo");

            entity.HasOne(d => d.IdPuntoResponsableNavigation).WithMany(p => p.ProgramaPatrullajes)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Programa_Patrullaje_Punto_Patrullaje");

            entity.HasOne(d => d.IdRutaNavigation).WithMany(p => p.ProgramaPatrullajes)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Programa_Patrullaje_Ruta");

            entity.HasOne(d => d.IdUsuarioNavigation).WithMany(p => p.ProgramaPatrullajes)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Programa_Patrullaje_Usuario");
        });

        modelBuilder.Entity<PropuestaPatrullaje>(entity =>
        {
            entity.ToTable("Propuesta_Patrullaje", "dmn", tb => tb.HasComment("Listado de propuestas de patrullaje a realizar"));

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasComment("Identificador único de la propuesta de patrullaje");
            entity.Property(e => e.IdClasePatrullaje).HasComment("Id de la clase de patrullaje (Programado o extraordinario)");
            entity.Property(e => e.IdUsuario).HasComment("Id del usuario que da de alta la propuesta");

            entity.HasOne(d => d.IdClasePatrullajeNavigation).WithMany(p => p.PropuestaPatrullajes)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Propuesta_Patrullaje_Clase_Patrullaje");

            entity.HasOne(d => d.IdUsuarioNavigation).WithMany(p => p.PropuestaPatrullajes)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Propuesta_Patrullaje_Usuario");
        });

        modelBuilder.Entity<PropuestaPatrullajeFecha>(entity =>
        {
            entity.Property(e => e.IdPropuestaPatrullaje).HasComment("Id de la propuesta de patrullaje a la que está asociada");
            entity.Property(e => e.IdRuta).HasComment("Id de la ruta que se está proponiendo en el patrullaje");
            entity.Property(e => e.Fecha).HasComment("Fecha en la que se propone realizar el patrullaje ");

            entity.HasOne(d => d.IdEstadoPropuestaNavigation).WithMany(p => p.PropuestaPatrullajeFechas)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Propuesta_Patrullaje_Fecha_Estado_Propuesta");

            entity.HasOne(d => d.Id).WithMany(p => p.PropuestaPatrullajeFechas).HasConstraintName("FK_Propuesta_Patrullaje_Fecha_Propuesta_Patrullaje_Ruta_Contenedor");
        });

        modelBuilder.Entity<PropuestaPatrullajeRutaContenedor>(entity =>
        {
            entity.HasKey(e => new { e.IdPropuestaPatrullaje, e.IdRuta }).HasName("PK_Propuesta_Patrullaje_Ruta2");

            entity.Property(e => e.IdPropuestaPatrullaje).HasComment("Id de la propuesta de patrullaje a la que está asociada");
            entity.Property(e => e.IdRuta).HasComment("Id de la ruta que se está proponiendo en el patrullaje");
            entity.Property(e => e.IdApoyoPatrullaje).HasComment("Id de la entidad responsable de apoyar el patrullaje");
            entity.Property(e => e.IdNivelRiesgo).HasComment("Id del nivel de riesgo asociado a la ruta en el tiempo específico del patrullaje");
            entity.Property(e => e.IdPuntoResponsable).HasComment("Id del punto de patrullaje (instalación) responsable de llevar a cabo el patrullaje");
            entity.Property(e => e.Observaciones).HasComment("Notas u observaciones asociadas a la ruta en la propuesta de patrullaje");

            entity.HasOne(d => d.IdApoyoPatrullajeNavigation).WithMany(p => p.PropuestaPatrullajeRutaContenedors)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Propuesta_Patrullaje_Ruta_Contenedor_Apoyo_Patrullaje");

            entity.HasOne(d => d.IdNivelRiesgoNavigation).WithMany(p => p.PropuestaPatrullajeRutaContenedors)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Propuesta_Patrullaje_Ruta_Contenedor_Nivel_Riesgo");

            entity.HasOne(d => d.IdPropuestaPatrullajeNavigation).WithMany(p => p.PropuestaPatrullajeRutaContenedors).HasConstraintName("FK_Propuesta_Patrullaje_Ruta_Contenedor_Propuesta_Patrullaje");

            entity.HasOne(d => d.IdPuntoResponsableNavigation).WithMany(p => p.PropuestaPatrullajeRutaContenedors)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Propuesta_Patrullaje_Ruta_Contenedor_Punto_Patrullaje");

            entity.HasOne(d => d.IdRutaNavigation).WithMany(p => p.PropuestaPatrullajeRutaContenedors)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Propuesta_Patrullaje_Ruta_Contenedor_Ruta");

            entity.HasMany(d => d.IdVehiculos).WithMany(p => p.Ids)
                .UsingEntity<Dictionary<string, object>>(
                    "PropuestaPatrullajeVehiculo",
                    r => r.HasOne<Vehiculo>().WithMany()
                        .HasForeignKey("IdVehiculo")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK_Propuesta_Patrullaje_Vehiculo_Vehiculo"),
                    l => l.HasOne<PropuestaPatrullajeRutaContenedor>().WithMany()
                        .HasForeignKey("IdPropuestaPatrullaje", "IdRuta")
                        .HasConstraintName("FK_Propuesta_Patrullaje_Vehiculo_Propuesta_Patrullaje_Ruta_Contenedor"),
                    j =>
                    {
                        j.HasKey("IdPropuestaPatrullaje", "IdRuta", "IdVehiculo");
                        j.ToTable("Propuesta_Patrullaje_Vehiculo", "dmn", tb => tb.HasComment("Listado de vehículos propuestos a utilizar durante un patrullaje"));
                    });
        });

        modelBuilder.Entity<PuntoPatrullaje>(entity =>
        {
            entity.ToTable("Punto_Patrullaje", "dmn", tb => tb.HasComment("Listado de puntos que pueden ser visitados durante un patrullaje"));

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasComment("Identificador único del punto de patrullaje");
            entity.Property(e => e.Bloqueado).HasComment("Indicador de sí el punto de patrullaje está bloqueado (no se mostrará al momento de definir una propuesta de patrullaje)");
            entity.Property(e => e.CoordenadasSrid).HasComment("Coordenadas geoespaciales de la ubicación del punto");
            entity.Property(e => e.EsInstalacion).HasComment("Indicador de si el punto está dentro de una instalación de CFE");
            entity.Property(e => e.IdComandancia).HasComment("Identificador de la Comandancia Regional a la que está asociado el punto");
            entity.Property(e => e.IdMunicipio).HasComment("Identificador del municipio al que pertenece el punto de patrullaje");
            entity.Property(e => e.IdNivelRiesgo).HasComment("Identificador del nivel de riesgo asociado al punto");
            entity.Property(e => e.IdProcesoResponsable).HasComment("Identificador del proceso responsable encargado de la gestión del punto");
            entity.Property(e => e.Ubicacion).HasComment("Referencias de la ubicación en donde se encuentra el punto");
            entity.Property(e => e.UltimaActualizacion).HasComment("Fecha de la última actualización del punto");

            entity.HasOne(d => d.IdComandanciaNavigation).WithMany(p => p.PuntoPatrullajes).HasConstraintName("FK_Punto_Patrullaje_Comandancia_Regional");

            entity.HasOne(d => d.IdMunicipioNavigation).WithMany(p => p.PuntoPatrullajes).HasConstraintName("FK_Punto_Patrullaje_Municipio");

            entity.HasOne(d => d.IdNivelRiesgoNavigation).WithMany(p => p.PuntoPatrullajes).HasConstraintName("FK_Punto_Patrullaje_Nivel_Riesgo");

            entity.HasOne(d => d.IdProcesoResponsableNavigation).WithMany(p => p.PuntoPatrullajes).HasConstraintName("FK_Punto_Patrullaje_Proceso_Responsable");
        });

        modelBuilder.Entity<ReporteIncidencia>(entity =>
        {
            entity.ToTable("Reporte_Incidencia", "dmn", tb => tb.HasComment("Reporte de incidencias detectadas durante los patrullajes (tanto de puntos como de estructuras)"));

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasComment("Identificador único del reporte");
            entity.Property(e => e.IdElemento).HasComment("Punto o estructura sobre la que se está levantado el reporte de incidencia");
            entity.Property(e => e.IdEstadoIncidencia).HasComment("Estado que guarda la incidencia a lo largo de su ciclo de vida");
            entity.Property(e => e.IdNivelRiesgo).HasComment("Id del nivel de riesgo que representa la incidencia");
            entity.Property(e => e.IdTarjetaInformativa).HasComment("Id de la tarjeta informativa a la que se asocia el reporte");
            entity.Property(e => e.IdTipoIncidencia).HasComment("Identificador del tipo de incidencia");
            entity.Property(e => e.IdTipoReporte).HasComment("Id del tipo de reporte (punto o estructura)");
            entity.Property(e => e.Incidencia).HasComment("Descripción de la incidencia encontrada");
            entity.Property(e => e.UltimaActualizacion).HasComment("Fecha en la que se realizó la última actualización sobre el reporte");

            entity.HasOne(d => d.IdEstadoIncidenciaNavigation).WithMany(p => p.ReporteIncidencia)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Reporte_Incidencia_Estado_Incidencia");

            entity.HasOne(d => d.IdNivelRiesgoNavigation).WithMany(p => p.ReporteIncidencia).HasConstraintName("FK_Reporte_Incidencia_Nivel_Riesgo");

            entity.HasOne(d => d.IdTarjetaInformativaNavigation).WithMany(p => p.ReporteIncidencia).HasConstraintName("FK_Reporte_Incidencia_Tarjeta_Informativa");

            entity.HasOne(d => d.IdTipoIncidenciaNavigation).WithMany(p => p.ReporteIncidencia)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Reporte_Incidencia_Tipo_Inicidencia");

            entity.HasOne(d => d.IdTipoReporteNavigation).WithMany(p => p.ReporteIncidencia)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Reporte_Incidencia_Tipo_Reporte");
        });

        modelBuilder.Entity<ResumenAcceso>(entity =>
        {
            entity.ToTable("Resumen_Accesos", "audit", tb => tb.HasComment("Conteo del número de accesos al sistema por día"));

            entity.Property(e => e.Fecha).HasComment("Día del cual se está haciendo el reporte de registros");
            entity.Property(e => e.TotalAccesos).HasComment("Total de accesos del día reportado");
        });

        modelBuilder.Entity<Ruta>(entity =>
        {
            entity.ToTable("Ruta", "dmn", tb => tb.HasComment("Listado de rutas  que pueden seleccionarse para que el usuario construya la propuesta o un programa de patrullaje"));

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasComment("Identificador único de la ruta.");
            entity.Property(e => e.Bloqueado).HasComment("Indicador de que el registro no puede ser eliminado de la BD.");
            entity.Property(e => e.Clave).HasComment("Nombre que permite reconocer la ruta");
            entity.Property(e => e.ConsecutivoRegionMilitarSdn).HasComment("Número del consecutivo de la ruta dentro de la Región Militar de la SSF");
            entity.Property(e => e.Habilitado).HasComment("Indicador de sí la ruta se le muestra al usuario en la IHM al momento de crear una propuesta o programada patrullaje.");
            entity.Property(e => e.IdComandanciaRegionalSsf).HasComment("Identificado de la Comandancia regional a la que se asocia la ruta.");
            entity.Property(e => e.IdTipoPatrullaje).HasComment("Identificador del tipo de patrullaje al que se asocia la ruta");
            entity.Property(e => e.Observaciones).HasComment("Notas o comentarios relacionados a la Ruta");
            entity.Property(e => e.RegionMilitarSdn).HasComment("Identificador único de la Región Militar de la SDN a la que se asocia la ruta.");
            entity.Property(e => e.UltimaActualizacion).HasComment("Fecha en la que se realizó la última actualización.");
            entity.Property(e => e.ZonaMilitarSdn).HasComment("Identificador único de la Zona Militar de la SDN a la que se asocia la ruta.");

            entity.HasOne(d => d.IdComandanciaRegionalSsfNavigation).WithMany(p => p.Ruta).HasConstraintName("FK_Ruta_Comandancia_Regional");

            entity.HasOne(d => d.IdTipoPatrullajeNavigation).WithMany(p => p.Ruta)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_Ruta_Tipo_Patrullaje");
        });

        modelBuilder.Entity<TarjetaInformativa>(entity =>
        {
            entity.ToTable("Tarjeta_Informativa", "dmn", tb => tb.HasComment("Listado de tarjetas informativas asociadas a un Programa de patrullaje"));

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasComment("Identificador único de la nota informativa");
            entity.Property(e => e.CalzoACalzo).HasComment("Tiempo transcurrido desde que se encendió el vehículo hasta que se apagó");
            entity.Property(e => e.FechaPatrullaje).HasComment("Fecha real en que se está realizando el patrullaje");
            entity.Property(e => e.FechaReporteTarjeta).HasComment("Fecha del momento en que se creo la tarjeta informativa");
            entity.Property(e => e.FechaUltimaActualizacion).HasComment("Fecha de la última actualización a la tarjeta informativa");
            entity.Property(e => e.IdEstadoTarjetaInformativa).HasComment("Id del estado en que se encuentra la tarjeta informativa");
            entity.Property(e => e.IdPrograma).HasComment("Identificador único del programa de patrullaje");
            entity.Property(e => e.IdUsurio).HasComment("Id del usuario que eestá dando de alta la nota");
            entity.Property(e => e.Inicio).HasComment("Estampa de tiempo del momento en que inició el patrullaje");
            entity.Property(e => e.KmRecorridos).HasComment("Cantidad de KM que se recorrieron durante el patrullaje");
            entity.Property(e => e.Observaciones).HasComment("Notas asociadas a la tarjeta informativa");
            entity.Property(e => e.Termino).HasComment("Estampa de tiempo del momento en que terminó el patrullaje");
            entity.Property(e => e.TiempoVuelo).HasComment("Tiempo que la unidad aérea estuvo en vuelo");

            entity.HasOne(d => d.IdProgramaNavigation).WithMany(p => p.TarjetaInformativas).HasConstraintName("FK_Tarjeta_Informativa_Programa_Patrullaje");
        });

        modelBuilder.Entity<TipoEvidencia>(entity =>
        {
            entity.ToTable("Tipo_Evidencia", "cat", tb => tb.HasComment("Listado de tipos de evidencia"));

            entity.Property(e => e.Id).HasComment("Identificador único del tipo de evidencia");
            entity.Property(e => e.Nombre).HasComment("Nombre del tipo de evidencia");
        });

        modelBuilder.Entity<TipoInicidencia>(entity =>
        {
            entity.ToTable("Tipo_Inicidencia", "cat", tb => tb.HasComment("Catálogo de causales de las incidencias detectadas en los patrullajes"));

            entity.Property(e => e.Id).HasComment("Identifidacor único de la causa de la incidencia");
            entity.Property(e => e.Desripcion).HasComment("Nombre de la causa de la incidencia");
        });

        modelBuilder.Entity<TipoPatrullaje>(entity =>
        {
            entity.ToTable("Tipo_Patrullaje", "cat", tb => tb.HasComment("Listado de tipos de patrullajes (en referencia al medio de transporte)"));

            entity.Property(e => e.Id).HasComment("Identificador único del tipo de patrullaje ");
            entity.Property(e => e.Nombre).HasComment("Nombre descriptivo del tipo de patrullaje");
        });

        modelBuilder.Entity<TipoPersonalPatrullaje>(entity =>
        {
            entity.ToTable("Tipo_Personal_Patrullaje", "cat", tb => tb.HasComment("Listado de personal que puede participar en un patrullaje"));

            entity.Property(e => e.Id).HasComment("Identificador único del Personal que puede apoyar en un programa de patrullaje");
            entity.Property(e => e.Nombre).HasComment("Nombre del Tipo de personal");
        });

        modelBuilder.Entity<TipoProcesoResponsable>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_Proceso_Responsable");

            entity.ToTable("Tipo_Proceso_Responsable", "cat", tb => tb.HasComment("Catálogo de los tipos de procesos responsables de los patrullajes"));

            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .HasComment("Identificador único del tipo de proceso responsable del patrullaje");
            entity.Property(e => e.Nombre).HasComment("Nombre del tipo de proceso responsable de llevar a cabo los patrullajes");
        });

        modelBuilder.Entity<TipoReporte>(entity =>
        {
            entity.ToTable("Tipo_Reporte", "cat", tb => tb.HasComment("Catálogo de tipo de reportes a almacenar"));

            entity.Property(e => e.Id).HasComment("Identificador único del tipo de reporte");
            entity.Property(e => e.Nombre).HasComment("Nombre del tipo de reporte");
        });

        modelBuilder.Entity<TipoVehiculo>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_Tipo_Vehiculo_Patrullaje");

            entity.ToTable("Tipo_Vehiculo", "cat", tb => tb.HasComment("Catálogo de las tipos de vehículo utilizados en los patrullajes"));

            entity.Property(e => e.Id).ValueGeneratedOnAdd();
        });

        modelBuilder.Entity<UnidadMedida>(entity =>
        {
            entity.ToTable("Unidad_Medida", "cat", tb => tb.HasComment("Catálogo de unidades de medidas para la definición de conecptos afectados detectados en el patrullaje"));

            entity.Property(e => e.Id).HasComment("Identificador único de la unidad de medida");
            entity.Property(e => e.Nombre).HasComment("Nombre de la unidad de medida");
        });

        modelBuilder.Entity<UsoVehiculo>(entity =>
        {
            entity.ToTable("Uso_Vehiculo", "dmn", tb => tb.HasComment("Detalle del reporte de vehículos usados en el patrullaje"));

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasComment("Identificador único del reporte del uso del vehículo");
            entity.Property(e => e.ConsumoCombustible).HasComment("Consumo de combustible del vehículo");
            entity.Property(e => e.EstadoVehiculo).HasComment("Estado en que se encuentra el vehículo al terminar el patrullaje");
            entity.Property(e => e.IdTarjetaInformativa).HasComment("Id de la tarjeta informativa de la que se está haciendo el reporte de los vehículos involucrados");
            entity.Property(e => e.IdVehiculo).HasComment("Identificador único del vehículo del cual se está haciendo su reporte");
            entity.Property(e => e.KmFinal).HasComment("Kilómetraje de vehículo reportado al final del patrullaje");
            entity.Property(e => e.KmInicio).HasComment("Kilómetraje de vehículo reportado al inicio del patrullaje");

            entity.HasOne(d => d.IdTarjetaInformativaNavigation).WithMany(p => p.UsoVehiculos).HasConstraintName("FK_Uso_Vehiculo_Tarjeta_Informativa");

            entity.HasOne(d => d.IdVehiculoNavigation).WithMany(p => p.UsoVehiculos)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Uso_Vehiculo_Vehiculo");
        });

        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.Property(e => e.Id).HasComment("Identificador único del usuario");
            entity.Property(e => e.ApellidoMaterno).HasComment("Apellido materno del usuario");
            entity.Property(e => e.ApellidoPaterno).HasComment("Apellido paterno del usuario");
            entity.Property(e => e.Bloqueado).HasComment("Indicador de sí se encuentra bloqueado el acceso al sistema.");
            entity.Property(e => e.Celular).HasComment("Número telefónico en el que se puede contactar al usuario");
            entity.Property(e => e.Email).HasComment("Correo electrónico al que enviar las notificaciones");
            entity.Property(e => e.EsConfigurador).HasComment("Indicador de si el usuario tiene perfil de configuración (Se debe eliminar de esta tabla)");
            entity.Property(e => e.FechaAceptacionAvisoLegal).HasComment("Fecha en la que el usuario aceptó el aviso de legalidad de acceso al sistema. Si no lo ha aceptado, su valor será nulo.");
            entity.Property(e => e.FechaUltimoAcceso).HasComment("Fecha del último acceso al sistema");
            entity.Property(e => e.IdComandanciaRegional).HasComment("Identificador de la comandancia a la que pertenece el usuario. Puede contener un null si el usuario no pertenece a una comandancia");
            entity.Property(e => e.Login).HasComment("Nombre de usuario dentro de la aplicación");
            entity.Property(e => e.Nombre).HasComment("Nombre del usuario");
            entity.Property(e => e.NotificarAcceso).HasComment("Indicador de si el sistema debe notificar por correo electrónico cuando el usuario ha accecido al sistema.");
            entity.Property(e => e.Password).HasComment("Contraseña de acceso al sistema");

            entity.HasOne(d => d.IdComandanciaRegionalNavigation).WithMany(p => p.Usuarios).HasConstraintName("FK_Usuario_Comandancia_Regional");

            entity.HasMany(d => d.IdComandancia).WithMany(p => p.IdUsuarios)
                .UsingEntity<Dictionary<string, object>>(
                    "Cdr",
                    r => r.HasOne<ComandanciaRegional>().WithMany()
                        .HasForeignKey("IdComandancia")
                        .HasConstraintName("FK_CDR_Comandancia_Regional"),
                    l => l.HasOne<Usuario>().WithMany()
                        .HasForeignKey("IdUsuario")
                        .HasConstraintName("FK_CDR_Usuario"),
                    j =>
                    {
                        j.HasKey("IdUsuario", "IdComandancia");
                        j.ToTable("CDR", "dmn", tb => tb.HasComment("Listado de Comandancias a las que el usuario tiene acceso"));
                    });
        });

        modelBuilder.Entity<Vehiculo>(entity =>
        {
            entity.ToTable("Vehiculo", "cat", tb => tb.HasComment("Listado de vehículos disponibles para realizar los patrullajes"));

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasComment("Identificador único del véhículo");
            entity.Property(e => e.Habilitado).HasComment("Indicador de si el vehículo puede ser utiizado para realizar patrullajes");
            entity.Property(e => e.IdComandancia).HasComment("Identificador de la Comandancia Regional a la que está asignado el vehículo");
            entity.Property(e => e.IdTipoPatrullaje).HasComment("Identificador del tipo de patrullaje que se puede realizar en este vehículo");
            entity.Property(e => e.IdTipoVehiculo).HasComment("Identificador del tipo de vehículo ");
            entity.Property(e => e.Kilometraje).HasComment("Cantidad de kilómetros recorridos por el vehículo");
            entity.Property(e => e.Matricula).HasComment("Número de placa o matrícula asignado al vehículo");
            entity.Property(e => e.NumeroEconomico).HasComment("Identificador único del vehículo como activo");
            entity.Property(e => e.UltimaActualizacion).HasComment("Fecha de la última actualización realizada a la información del vehículo");

            entity.HasOne(d => d.IdComandanciaNavigation).WithMany(p => p.Vehiculos)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_Vehiculo_Comandancia_Regional");

            entity.HasOne(d => d.IdTipoPatrullajeNavigation).WithMany(p => p.Vehiculos).HasConstraintName("FK_Vehiculo_Tipo_Patrullaje");

            entity.HasOne(d => d.IdTipoVehiculoNavigation).WithMany(p => p.Vehiculos).HasConstraintName("FK_Vehiculo_Tipo_Vehiculo");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
