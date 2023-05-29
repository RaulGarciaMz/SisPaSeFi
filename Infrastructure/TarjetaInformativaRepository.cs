using Domain.Entities;
using Domain.Entities.Vistas;
using Domain.Ports.Driven.Repositories;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using SqlServerAdapter.Data;

namespace SqlServerAdapter
{
    public class TarjetaInformativaRepository : ITarjetaInformativaRepo
    {
        protected readonly TarjetaInformativaContext _tarjetaContext;

        public TarjetaInformativaRepository(TarjetaInformativaContext tarjetaContext)
        {
            _tarjetaContext = tarjetaContext ?? throw  new ArgumentNullException(nameof(tarjetaContext));
        }

        public async Task AgregaTransaccionalAsync(TarjetaInformativa tarjeta, int idEstadoPatrullaje , int usuarioId) 
        { 
            tarjeta.IdUsuario= usuarioId;

            using (var transaction = _tarjetaContext.Database.BeginTransaction())
            {
                try
                {
                    _tarjetaContext.TarjetasInformativas.Add(tarjeta);
                    var cuantos = await _tarjetaContext.SaveChangesAsync();

                    if (cuantos > 0) 
                    {
                        var programa = await ObtenerProgramaPatrullajePorIdAsync(tarjeta.IdPrograma);

                        if (programa != null)
                        {
                            programa.IdUsuario = usuarioId;
                            programa.UltimaActualizacion = tarjeta.UltimaActualizacion;
                            programa.Termino = tarjeta.Termino;
                            programa.IdEstadoPatrullaje = idEstadoPatrullaje;
                            programa.Observaciones = tarjeta.Observaciones;

                            _tarjetaContext.Programas.Update(programa);
                        }
                    }

                    await _tarjetaContext.SaveChangesAsync();
                    transaction.Commit();
                }
                catch (Exception)
                {
                    transaction.Rollback();
                    throw;
                }
            }
        }

        public async Task UpdateTarjetaAndProgramaTransaccionalAsync(TarjetaInformativa tarjeta, int idEstadoPatrullaje, int usuarioId, int idPuntoResponsable) 
        {
            using (var transaction = _tarjetaContext.Database.BeginTransaction())
            {
                try
                {
                    _tarjetaContext.TarjetasInformativas.Update(tarjeta);

                    var programa = await ObtenerProgramaPatrullajePorIdAsync(tarjeta.IdPrograma);

                    if (programa != null)
                    {
                        programa.IdUsuario = usuarioId;
                        programa.IdEstadoPatrullaje = idEstadoPatrullaje;
                        programa.Termino = tarjeta.Termino;
                        programa.Observaciones = tarjeta.Observaciones;
                        programa.IdPuntoResponsable = idPuntoResponsable;
                        programa.UltimaActualizacion = tarjeta.UltimaActualizacion;

                        _tarjetaContext.Programas.Update(programa);
                    }

                    await _tarjetaContext.SaveChangesAsync();
                    transaction.Commit();
                }
                catch (Exception)
                {
                    transaction.Rollback();
                    throw;
                }
            }
        }

        public async Task<List<TarjetaInformativaVista>> ObtenerTarjetasPorRegionAsync(string tipo, string region, int anio, int mes) 
        {
            try
            {
                string sqlQuery = @"SELECT a.id_nota, a.id_programa, b.fechapatrullaje, b.id_ruta, c.regionssf, c.id_tipopatrullaje, a.ultimaactualizacion,
                                       a.id_usuario, a.inicio, a.termino, a.tiempovuelo, a.calzoacalzo, a.observaciones, d.id_estadopatrullaje,
                                       d.descripcionestadopatrullaje, a.kmrecorrido, a.comandantesinstalacionssf, a.personalmilitarsedenaoficial,
                                       a.id_estadotarjetainformativa, a.personalmilitarsedenatropa, a.linieros, a.comandantesturnossf, a.oficialesssf,
                                       a.personalnavalsemaroficial,a.personalnavalsemartropa, a.fechaTermino, a.idresultadopatrullaje, 
									   rp.descripcion resultadopatrullaje, a.lineaestructurainstalacion, a.responsablevuelo, a.fuerzareaccion, b.id_puntoresponsable,
                                       COALESCE((SELECT STRING_AGG(CAST(g.ubicacion as nvarchar(MAX)),'-') WITHIN GROUP (ORDER BY f.posicion ASC) 
                                                FROM ssf.itinerario f join ssf.puntospatrullaje g on f.id_punto=g.id_punto
                                                WHERE f.id_ruta=b.id_ruta),'') as itinerario,
                                       COALESCE((SELECT STRING_AGG(CAST(CONCAT('Linea',h.clave,'Estructura',g.nombre,'Incidencia',f.incidencia,'SSFPATRULLAJECR') as nvarchar(MAX)),':') 
                                                        WITHIN GROUP (ORDER BY h.clave, g.nombre ASC) 
                                                FROM ssf.reporteestructuras f
                                               JOIN ssf.estructura g ON f.id_estructura=g.id_estructura
                                               JOIN ssf.linea h ON g.id_linea=h.id_linea
                                                WHERE f.id_nota=a.id_nota),'') as incidenciaenestructura,
                                       COALESCE((SELECT STRING_AGG(CAST(CONCAT('Instalacion',f.ubicacion,'Incidencia',g.incidencia,'SSFPATRULLAJECR') as nvarchar(MAX)),':') 
                                                        WITHIN GROUP (ORDER BY f.ubicacion ASC) 
                                                FROM ssf.puntospatrullaje f
                                               JOIN ssf.reportepunto g ON f.id_punto = g.id_punto
                                                WHERE g.id_nota=a.id_nota),'') as incidenciaeninstalacion,
                                       COALESCE((SELECT STRING_AGG(CAST(CONCAT(g.matricula,'SSFPATRULLAJECR') as nvarchar(MAX)),' ') WITHIN GROUP (ORDER BY g.matricula ASC) 
                                                FROM ssf.usovehiculo f
                                               JOIN ssf.vehiculos g ON f.id_vehiculo=g.id_vehiculo
                                                WHERE f.id_programa=a.id_programa),'') as matriculas,
                                       COALESCE((SELECT STRING_AGG(CAST(CONCAT(CAST(f.kminicio AS VARCHAR(10)),', ',CAST(f.kmFin AS VARCHAR(10)),'SSFPATRULLAJECR') as nvarchar(MAX)),' ') WITHIN GROUP (ORDER BY g.matricula ASC) 
                                                FROM ssf.usovehiculo f
                                               JOIN ssf.vehiculos g ON f.id_vehiculo=g.id_vehiculo
                                                WHERE f.id_programa=a.id_programa),'') as odometros,
                                       COALESCE((SELECT STRING_AGG(CAST(CONCAT(CAST(f.kmfin-f.kminicio AS VARCHAR(10)),'SSFPATRULLAJECR') as nvarchar(MAX)),' ') WITHIN GROUP (ORDER BY g.matricula ASC) 
                                                FROM ssf.usovehiculo f
                                               JOIN ssf.vehiculos g ON f.id_vehiculo=g.id_vehiculo
                                                WHERE f.id_programa=a.id_programa),'') as kmrecorridos
                                FROM ssf.tarjetainformativa a
                                JOIN ssf.programapatrullajes b ON a.id_programa=b.id_programa
                                JOIN ssf.rutas c ON b.id_ruta=c.id_ruta 
                                JOIN ssf.estadopatrullaje d ON b.id_estadopatrullaje=d.id_estadopatrullaje
                                JOIN ssf.tipopatrullaje e ON c.id_tipopatrullaje=e.id_tipopatrullaje
								JOIN ssf.resultadopatrullaje rp ON a.idresultadopatrullaje=rp.idresultadopatrullaje
                                WHERE e.descripcion=@pTipo AND c.regionssf= @pRegion
                                AND MONTH(b.fechapatrullaje)=@pMes AND YEAR(b.fechapatrullaje)=@pAnio 
                                ORDER BY b.fechapatrullaje, c.regionssf, a.inicio";

                object[] parametros = new object[]
                {
                new SqlParameter("@pTipo", tipo),
                new SqlParameter("@pRegion", region),
                new SqlParameter("@pAnio", anio),
                new SqlParameter("@pMes", mes)
                 };


                return await _tarjetaContext.TarjetasInformativasVista.FromSqlRaw(sqlQuery, parametros).ToListAsync();
            }
            catch (Exception ex)
            {
                var m = ex.Message;
                throw;
            }

        }

        public async Task<List<TarjetaInformativaVista>> ObtenerParteNovedadesPorDiaAsync(string tipo, int anio, int mes, int dia)
        {
            string sqlQuery = @"SELECT a.id_nota, a.id_programa, b.fechapatrullaje, b.id_ruta, c.regionssf, c.id_tipopatrullaje, a.ultimaactualizacion,
                                       a.id_usuario, a.inicio, a.termino, a.tiempovuelo, a.calzoacalzo, a.observaciones, d.id_estadopatrullaje,
                                       d.descripcionestadopatrullaje, a.kmrecorrido, a.comandantesinstalacionssf, a.personalmilitarsedenaoficial,
                                       a.id_estadotarjetainformativa, a.personalmilitarsedenatropa, a.linieros, a.comandantesturnossf, a.oficialesssf,
                                       a.personalnavalsemaroficial,a.personalnavalsemartropa, a.fechaTermino, a.idresultadopatrullaje, 
									   rp.descripcion resultadopatrullaje, a.lineaestructurainstalacion, a.responsablevuelo, a.fuerzareaccion, b.id_puntoresponsable,
                                       COALESCE((SELECT STRING_AGG(CAST(g.ubicacion as nvarchar(MAX)),'-') WITHIN GROUP (ORDER BY f.posicion ASC) 
                                                FROM ssf.itinerario f join ssf.puntospatrullaje g on f.id_punto=g.id_punto
                                                WHERE f.id_ruta=b.id_ruta),'') as itinerario,
                                       COALESCE((SELECT STRING_AGG(CAST(CONCAT('Linea',h.clave,'Estructura',g.nombre,'Incidencia',f.incidencia,'SSFPATRULLAJECR') as nvarchar(MAX)),':') 
                                                        WITHIN GROUP (ORDER BY h.clave, g.nombre ASC) 
                                                FROM ssf.reporteestructuras f
                                               JOIN ssf.estructura g ON f.id_estructura=g.id_estructura
                                               JOIN ssf.linea h ON g.id_linea=h.id_linea
                                                WHERE f.id_nota=a.id_nota),'') as incidenciaenestructura,
                                       COALESCE((SELECT STRING_AGG(CAST(CONCAT('Instalacion',f.ubicacion,'Incidencia',g.incidencia,'SSFPATRULLAJECR') as nvarchar(MAX)),':') 
                                                        WITHIN GROUP (ORDER BY f.ubicacion ASC) 
                                                FROM ssf.puntospatrullaje f
                                               JOIN ssf.reportepunto g ON f.id_punto = g.id_punto
                                                WHERE g.id_nota=a.id_nota),'') as incidenciaeninstalacion,
                                       COALESCE((SELECT STRING_AGG(CAST(CONCAT(g.matricula,'SSFPATRULLAJECR') as nvarchar(MAX)),' ') WITHIN GROUP (ORDER BY g.matricula ASC) 
                                                FROM ssf.usovehiculo f
                                               JOIN ssf.vehiculos g ON f.id_vehiculo=g.id_vehiculo
                                                WHERE f.id_programa=a.id_programa),'') as matriculas,
                                       COALESCE((SELECT STRING_AGG(CAST(CONCAT(CAST(f.kminicio AS VARCHAR(10)),', ',CAST(f.kmFin AS VARCHAR(10)),'SSFPATRULLAJECR') as nvarchar(MAX)),' ') WITHIN GROUP (ORDER BY g.matricula ASC) 
                                                FROM ssf.usovehiculo f
                                               JOIN ssf.vehiculos g ON f.id_vehiculo=g.id_vehiculo
                                                WHERE f.id_programa=a.id_programa),'') as odometros,
                                       COALESCE((SELECT STRING_AGG(CAST(CONCAT(CAST(f.kmfin-f.kminicio AS VARCHAR(10)),'SSFPATRULLAJECR') as nvarchar(MAX)),' ') WITHIN GROUP (ORDER BY g.matricula ASC) 
                                                FROM ssf.usovehiculo f
                                               JOIN ssf.vehiculos g ON f.id_vehiculo=g.id_vehiculo
                                                WHERE f.id_programa=a.id_programa),'') as kmrecorridos
                                FROM ssf.tarjetainformativa a
                                JOIN ssf.programapatrullajes b ON a.id_programa=b.id_programa
                                JOIN ssf.rutas c ON b.id_ruta=c.id_ruta 
                                JOIN ssf.estadopatrullaje d ON b.id_estadopatrullaje=d.id_estadopatrullaje
                                JOIN ssf.tipopatrullaje e ON c.id_tipopatrullaje=e.id_tipopatrullaje
								JOIN ssf.resultadopatrullaje rp ON a.idresultadopatrullaje=rp.idresultadopatrullaje
                                WHERE e.descripcion=@pTipo 
                                AND DAY(b.fechapatrullaje)=@pDia AND MONTH(b.fechapatrullaje)=@pMes AND YEAR(b.fechapatrullaje)=@pAnio 
                                ORDER BY b.fechapatrullaje, c.regionssf, a.inicio";

            object[] parametros = new object[]
            {
                new SqlParameter("@pTipo", tipo),
                new SqlParameter("@pAnio", anio),
                new SqlParameter("@pMes", mes),
                new SqlParameter("@pDia", dia)
             };

            return await _tarjetaContext.TarjetasInformativasVista.FromSqlRaw(sqlQuery, parametros).ToListAsync();
        }

        public async Task<List<TarjetaInformativaVista>> ObtenerMonitoreoAsync(string tipo, int idUsuario, int anio, int mes, int dia)
        {
            string sqlQuery = @"SELECT a.id_nota, a.id_programa, b.fechapatrullaje, b.id_ruta, c.regionssf, c.id_tipopatrullaje, a.ultimaactualizacion,
                                       a.id_usuario, a.inicio, a.termino, a.tiempovuelo, a.calzoacalzo, a.observaciones, d.id_estadopatrullaje,
                                       d.descripcionestadopatrullaje, a.kmrecorrido, a.comandantesinstalacionssf, a.personalmilitarsedenaoficial,
                                       a.id_estadotarjetainformativa, a.personalmilitarsedenatropa, a.linieros, a.comandantesturnossf, a.oficialesssf,
                                       a.personalnavalsemaroficial,a.personalnavalsemartropa, a.fechaTermino, a.idresultadopatrullaje, 
									   rp.descripcion resultadopatrullaje, a.lineaestructurainstalacion, a.responsablevuelo, a.fuerzareaccion, b.id_puntoresponsable,
                                       COALESCE((SELECT STRING_AGG(CAST(g.ubicacion as nvarchar(MAX)),'-') WITHIN GROUP (ORDER BY f.posicion ASC) 
                                                FROM ssf.itinerario f join ssf.puntospatrullaje g on f.id_punto=g.id_punto
                                                WHERE f.id_ruta=b.id_ruta),'') as itinerario,
                                       COALESCE((SELECT STRING_AGG(CAST(CONCAT('Linea',h.clave,'Estructura',g.nombre,'Incidencia',f.incidencia,'SSFPATRULLAJECR') as nvarchar(MAX)),':') 
                                                        WITHIN GROUP (ORDER BY h.clave, g.nombre ASC) 
                                                FROM ssf.reporteestructuras f
                                               JOIN ssf.estructura g ON f.id_estructura=g.id_estructura
                                               JOIN ssf.linea h ON g.id_linea=h.id_linea
                                                WHERE f.id_nota=a.id_nota),'') as incidenciaenestructura,
                                       COALESCE((SELECT STRING_AGG(CAST(CONCAT('Instalacion',f.ubicacion,'Incidencia',g.incidencia,'SSFPATRULLAJECR') as nvarchar(MAX)),':') 
                                                        WITHIN GROUP (ORDER BY f.ubicacion ASC) 
                                                FROM ssf.puntospatrullaje f
                                               JOIN ssf.reportepunto g ON f.id_punto = g.id_punto
                                                WHERE g.id_nota=a.id_nota),'') as incidenciaeninstalacion,
                                       COALESCE((SELECT STRING_AGG(CAST(CONCAT(g.matricula,'SSFPATRULLAJECR') as nvarchar(MAX)),' ') WITHIN GROUP (ORDER BY g.matricula ASC) 
                                                FROM ssf.usovehiculo f
                                               JOIN ssf.vehiculos g ON f.id_vehiculo=g.id_vehiculo
                                                WHERE f.id_programa=a.id_programa),'') as matriculas,
                                       COALESCE((SELECT STRING_AGG(CAST(CONCAT(CAST(f.kminicio AS VARCHAR(10)),', ',CAST(f.kmFin AS VARCHAR(10)),'SSFPATRULLAJECR') as nvarchar(MAX)),' ') WITHIN GROUP (ORDER BY g.matricula ASC) 
                                                FROM ssf.usovehiculo f
                                               JOIN ssf.vehiculos g ON f.id_vehiculo=g.id_vehiculo
                                                WHERE f.id_programa=a.id_programa),'') as odometros,
                                       COALESCE((SELECT STRING_AGG(CAST(CONCAT(CAST(f.kmfin-f.kminicio AS VARCHAR(10)),'SSFPATRULLAJECR') as nvarchar(MAX)),' ') WITHIN GROUP (ORDER BY g.matricula ASC) 
                                                FROM ssf.usovehiculo f
                                               JOIN ssf.vehiculos g ON f.id_vehiculo=g.id_vehiculo
                                                WHERE f.id_programa=a.id_programa),'') as kmrecorridos
                                FROM ssf.tarjetainformativa a
                                JOIN ssf.programapatrullajes b ON a.id_programa=b.id_programa
                                JOIN ssf.rutas c ON b.id_ruta=c.id_ruta 
                                JOIN ssf.estadopatrullaje d ON b.id_estadopatrullaje=d.id_estadopatrullaje
                                JOIN ssf.tipopatrullaje e ON c.id_tipopatrullaje=e.id_tipopatrullaje
								JOIN ssf.resultadopatrullaje rp ON a.idresultadopatrullaje=rp.idresultadopatrullaje
                                WHERE e.descripcion=@pTipo 
                                AND c.regionssf IN (SELECT id_comandancia FROM ssf.usuariocomandancia WHERE id_usuario=@pIdUsuario)
                                AND DAY(b.fechapatrullaje)=@pDia AND MONTH(b.fechapatrullaje)=@pMes AND YEAR(b.fechapatrullaje)=@pAnio 
                                ORDER BY b.fechapatrullaje, c.regionssf, a.inicio";

            object[] parametros = new object[]
            {
                new SqlParameter("@pTipo", tipo),
                new SqlParameter("@pIdUsuario", idUsuario),
                new SqlParameter("@pAnio", anio),
                new SqlParameter("@pMes", mes),
                new SqlParameter("@pDia", dia)
             };

            return await _tarjetaContext.TarjetasInformativasVista.FromSqlRaw(sqlQuery, parametros).ToListAsync();
        }

        public async Task<TarjetaInformativaIdVista?> ObtenerTarjetaCompletaPorIdAsync(int idTarjeta)
        {
                string sqlQuery = @"SELECT a.id_nota,a.id_programa,b.fechapatrullaje,a.ultimaactualizacion
                                      ,a.id_usuario,a.inicio,a.termino,a.tiempovuelo,a.calzoacalzo,a.observaciones
                                      ,a.kmrecorrido,a.comandantesinstalacionssf,a.personalmilitarsedenaoficial
                                      ,a.id_estadotarjetainformativa,a.personalmilitarsedenatropa,a.linieros,a.comandantesturnossf,a.oficialesssf
                                      ,a.personalnavalsemaroficial,a.personalnavalsemartropa,a.fechaTermino,a.idresultadopatrullaje
                                      ,a.lineaestructurainstalacion,a.responsablevuelo,a.fuerzareaccion
                                      ,b.id_ruta,b.id_puntoresponsable,c.regionssf,c.id_tipopatrullaje
                                      ,d.id_estadopatrullaje,d.descripcionestadopatrullaje
                                      ,rp.descripcion resultadopatrullaje
									  ,i.ubicacion instalacion,j.nombre municipio,k.nombre estado,l.nombre,l.apellido1,l.apellido2
                                      ,COALESCE((SELECT STRING_AGG(CAST(g.ubicacion as nvarchar(MAX)),'-') WITHIN GROUP (ORDER BY f.posicion ASC) 
                                                FROM ssf.itinerario f join ssf.puntospatrullaje g on f.id_punto=g.id_punto
                                                WHERE f.id_ruta=b.id_ruta),'') as itinerario
                                       ,COALESCE((SELECT STRING_AGG(CAST(CONCAT('Linea',h.clave,'Estructura',g.nombre,'Incidencia',f.incidencia,'SSFPATRULLAJECR') as nvarchar(MAX)),':') 
                                                        WITHIN GROUP (ORDER BY h.clave, g.nombre ASC) 
                                                FROM ssf.reporteestructuras f
                                               JOIN ssf.estructura g ON f.id_estructura=g.id_estructura
                                               JOIN ssf.linea h ON g.id_linea=h.id_linea
                                                WHERE f.id_nota=a.id_nota),'') as incidenciaenestructura
                                        ,COALESCE((SELECT STRING_AGG(CAST(CONCAT('Instalacion',f.ubicacion,'Incidencia',g.incidencia,'SSFPATRULLAJECR') as nvarchar(MAX)),':') 
                                                         WITHIN GROUP (ORDER BY f.ubicacion ASC) 
                                                 FROM ssf.puntospatrullaje f
                                                JOIN ssf.reportepunto g ON f.id_punto = g.id_punto
                                                 WHERE g.id_nota=a.id_nota),'') as incidenciaeninstalacion
                                         ,COALESCE((SELECT STRING_AGG(CAST(CONCAT(g.matricula,'SSFPATRULLAJECR') as nvarchar(MAX)),' ') WITHIN GROUP (ORDER BY g.matricula ASC) 
                                                  FROM ssf.usovehiculo f
                                                 JOIN ssf.vehiculos g ON f.id_vehiculo=g.id_vehiculo
                                                  WHERE f.id_programa=a.id_programa),'') as matriculas
                                         ,COALESCE((SELECT STRING_AGG(CAST(CONCAT(CAST(f.kminicio as VARCHAR(10)),', ',CAST(f.kmfin AS VARCHAR(10)),'SSFPATRULLAJECR') as nvarchar(MAX)),' ') WITHIN GROUP (ORDER BY g.matricula ASC) 
                                                   FROM ssf.usovehiculo f
                                                  JOIN ssf.vehiculos g ON f.id_vehiculo=g.id_vehiculo
                                                   WHERE f.id_programa=a.id_programa),'') as odometros
                                         ,COALESCE((SELECT STRING_AGG(CAST(CONCAT(CAST((f.kmfin-f.kminicio) as VARCHAR(10)),'SSFPATRULLAJECR') as nvarchar(MAX)),' ') WITHIN GROUP (ORDER BY g.matricula ASC) 
                                                  FROM ssf.usovehiculo f
                                                 JOIN ssf.vehiculos g ON f.id_vehiculo=g.id_vehiculo
                                                  WHERE f.id_programa=a.id_programa),'') as kmrecorridos
                                FROM ssf.tarjetainformativa a
                                JOIN ssf.programapatrullajes b ON a.id_programa=b.id_programa
                                JOIN ssf.rutas c ON b.id_ruta=c.id_ruta
                                JOIN ssf.estadopatrullaje d ON b.id_estadopatrullaje=d.id_estadopatrullaje
                                JOIN ssf.tipopatrullaje e ON c.id_tipopatrullaje=e.id_tipopatrullaje
                                JOIN ssf.resultadopatrullaje rp ON a.idresultadopatrullaje=rp.idresultadopatrullaje
                                JOIN ssf.comandanciasregionales h ON c.regionssf=h.id_comandancia
                                JOIN ssf.puntospatrullaje i ON h.id_punto=i.id_punto
                                JOIN ssf.municipios j ON i.id_municipio=j.id_municipio
                                JOIN ssf.estadospais k ON j.id_estado=k.id_estado
                                JOIN ssf.usuarios l ON h.id_usuario=l.id_usuario
                                WHERE a.id_nota= @pIdTarjeta";

     
                object[] parametros = new object[]
                {
                new SqlParameter("@pIdTarjeta", idTarjeta)
                 };

                return await _tarjetaContext.TarjetaInformativaIdVista.FromSqlRaw(sqlQuery, parametros).FirstOrDefaultAsync();
 
        }

        public async Task<TarjetaInformativaIdVista?> ObtenerTarjetaCompletaPorIdProgramaAsync(int idPrograma)
        {
            string sqlQuery = @"SELECT a.id_nota,a.id_programa,b.fechapatrullaje,b.id_ruta,c.regionssf,c.id_tipopatrullaje,a.ultimaactualizacion
                                      ,a.id_usuario,a.inicio,a.termino,a.tiempovuelo,a.calzoacalzo,a.observaciones,d.id_estadopatrullaje
                                      ,d.descripcionestadopatrullaje,a.kmrecorrido,a.comandantesinstalacionssf,a.personalmilitarsedenaoficial
                                      ,a.id_estadotarjetainformativa,a.personalmilitarsedenatropa,a.linieros,a.comandantesturnossf,a.oficialesssf
                                      ,a.personalnavalsemaroficial,a.personalnavalsemartropa,a.fechaTermino,a.idresultadopatrullaje
                                      ,rp.descripcion resultadopatrullaje,a.lineaestructurainstalacion,a.responsablevuelo,a.fuerzareaccion,b.id_puntoresponsable
                                      ,i.ubicacion instalacion,j.nombre municipio,k.nombre estado,l.nombre,l.apellido1,l.apellido2
                                      ,COALESCE((SELECT STRING_AGG(CAST(g.ubicacion as nvarchar(MAX)),'-') WITHIN GROUP (ORDER BY f.posicion ASC)
                                          FROM ssf.itinerario f
                                      	JOIN ssf.puntospatrullaje g ON f.id_punto=g.id_punto
                                          WHERE f.id_ruta=b.id_ruta ),'') as itinerario
                                      ,COALESCE((SELECT STRING_AGG(CAST(CONCAT('Linea',h.clave,'Estructura',g.nombre,'Incidencia',f.incidencia,'SSFPATRULLAJECR') as nvarchar(MAX)),':') 
                                                       WITHIN GROUP (ORDER BY h.clave, g.nombre ASC)
                                          FROM ssf.reporteestructuras f
                                      	JOIN ssf.estructura g ON f.id_estructura=g.id_estructura
                                      	JOIN ssf.linea h ON g.id_linea=h.id_linea
                                          WHERE f.id_nota=a.id_nota),'') as incidenciaenestructura
                                      ,COALESCE((SELECT STRING_AGG(CAST(CONCAT('Instalacion',f.ubicacion,'Incidencia',g.incidencia,'SSFPATRULLAJECR') as nvarchar(MAX)),':')
                                                      WITHIN GROUP (ORDER BY f.ubicacion ASC)
                                          FROM ssf.puntospatrullaje f
                                      	JOIN ssf.reportepunto g ON f.id_punto = g.id_punto
                                          WHERE g.id_nota=a.id_nota),'') as incidenciaeninstalacion
                                      ,COALESCE((SELECT STRING_AGG(CAST(CONCAT(g.matricula,'SSFPATRULLAJECR') as nvarchar(MAX)),' ')
                                                      WITHIN GROUP (ORDER BY g.matricula ASC)
                                          FROM ssf.usovehiculo f
                                      	JOIN ssf.vehiculos g ON f.id_vehiculo=g.id_vehiculo
                                          WHERE f.id_programa=a.id_programa),'') as matriculas
                                      ,COALESCE((SELECT STRING_AGG(CAST(CONCAT(CAST(f.kminicio AS VARCHAR(10)),', ',CAST(f.kmFin AS VARCHAR(10)),'SSFPATRULLAJECR') as nvarchar(MAX)),' ')
                                                      WITHIN GROUP (ORDER BY g.matricula ASC)
                                          FROM ssf.usovehiculo f
                                      	JOIN ssf.vehiculos g ON  f.id_vehiculo=g.id_vehiculo
                                          WHERE f.id_programa=a.id_programa),'') as odometros
                                      ,COALESCE((SELECT STRING_AGG(CAST(CONCAT(CAST((f.kmfin-f.kminicio) AS VARCHAR(10)),'SSFPATRULLAJECR') as nvarchar(MAX)),' ')
                                                      WITHIN GROUP (ORDER BY g.matricula ASC)
                                          FROM ssf.usovehiculo f
                                      	JOIN ssf.vehiculos g ON f.id_vehiculo=g.id_vehiculo
                                          WHERE f.id_programa=a.id_programa ),'') as kmrecorridos
                                FROM ssf.tarjetainformativa a
                                JOIN ssf.programapatrullajes b ON a.id_programa=b.id_programa
                                JOIN ssf.rutas c ON b.id_ruta=c.id_ruta
                                JOIN ssf.estadopatrullaje d ON b.id_estadopatrullaje=d.id_estadopatrullaje
                                JOIN ssf.tipopatrullaje e ON c.id_tipopatrullaje=e.id_tipopatrullaje
                                JOIN ssf.resultadopatrullaje rp ON a.idresultadopatrullaje=rp.idresultadopatrullaje
                                JOIN ssf.comandanciasregionales h ON c.regionssf=h.id_comandancia
                                JOIN ssf.puntospatrullaje i ON h.id_punto=i.id_punto
                                JOIN ssf.municipios j ON i.id_municipio=j.id_municipio
                                JOIN ssf.estadospais k ON j.id_estado=k.id_estado
                                JOIN ssf.usuarios l ON h.id_usuario=l.id_usuario
                                WHERE a.id_programa= @pIdTarjeta";

            object[] parametros = new object[]
            {
                new SqlParameter("@pIdTarjeta", idPrograma)
             };

            return await _tarjetaContext.TarjetaInformativaIdVista.FromSqlRaw(sqlQuery, parametros).FirstOrDefaultAsync();
        }

        public async Task<TarjetaInformativa?> ObtenerTarjetaPorIdNotaAsync(int idNota) 
        { 
            return await _tarjetaContext.TarjetasInformativas.Where(x => x.IdNota== idNota).FirstOrDefaultAsync();
        }

        public async Task<int> NumeroDeTarjetasPorProgamaAsync(int idPrograma)
        {
            return await _tarjetaContext.TarjetasInformativas.Where(x => x.IdPrograma == idPrograma).CountAsync();
        }

        private async Task<ProgramaPatrullaje?> ObtenerProgramaPatrullajePorIdAsync(int idPrograma)
        {
            return await _tarjetaContext.Programas.Where(x => x.IdPrograma == idPrograma).FirstOrDefaultAsync();
        }
    }
}
