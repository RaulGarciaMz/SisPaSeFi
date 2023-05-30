using Domain.DTOs;
using Domain.Entities;
using Domain.Entities.Vistas;
using Domain.Ports.Driven.Repositories;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using SqlServerAdapter.Data;

namespace SqlServerAdapter
{
    public class IncidenciasRepository : IIncidenciasRepo
    {
        protected readonly IncidenciaContext _incidenciaContext;

        public IncidenciasRepository(IncidenciaContext incidenciaContext)
        {
            _incidenciaContext = incidenciaContext ?? throw new ArgumentNullException(nameof(incidenciaContext));
        }

        public async Task<List<IncidenciaGeneralVista>> ObtenerIncidenciasAbiertasEnInstalacionAsync(int idActivo)
        {

            string sqlQuery = @"SELECT a.id_reportepunto id_reporte, a.id_nota, b.ubicacion, '' as estructura, 
                                       b.coordenadas, b.id_procesoresponsable, b.id_gerenciadivision, a.incidencia,
                                       a.estadoincidencia, d.descripcionestado, a.ultimaactualizacion, a.prioridadincidencia,
                                       a.id_clasificacionincidencia, 'INSTALACION' as tiporeporte, e.descripcionnivel, h.descripcion
                                FROM ssf.reportepunto a
                                JOIN ssf.puntospatrullaje b ON a.id_punto= b.id_punto
                                JOIN ssf.estadosincidencias d ON a.estadoincidencia= d.id_estadoincidencia
                                JOIN ssf.niveles e ON a.prioridadincidencia=e.id_nivel
                                JOIN ssf.clasificacionincidencia h ON a.id_clasificacionincidencia = h.id_clasificacionincidencia
                                WHERE a.estadoincidencia<>(SELECT id_estadoincidencia FROM ssf.estadosincidencias WHERE descripcionestado= 'concluido')
                                AND a.id_punto= @pIdActivo
                                ORDER BY a.ultimaactualizacion DESC";

            object[] parametros = new object[]
            {
                new SqlParameter("@pIdActivo", idActivo)
            };

            return await _incidenciaContext.IncidenciasGenerales.FromSqlRaw(sqlQuery, parametros).ToListAsync();
        }

        public async Task<List<IncidenciaGeneralVista>> ObtenerIncidenciasAbiertasEnEstructuraAsync(int idEstructura)
        {
            string sqlQuery = @"SELECT a.id_reporte, a.id_nota, b.clave ubicacion, c.nombre estructura, c.coordenadas, c.id_procesoresponsable, 
                                       c.id_gerenciadivision, a.incidencia, a.estadoincidencia, d.descripcionestado, a.ultimaactualizacion,
                                       a.prioridadincidencia, a.id_clasificacionincidencia, 'ESTRUCTURA' as tiporeporte,e.descripcionnivel,
                                       h.descripcion
                                FROM ssf.reporteestructuras a 
                                JOIN ssf.estructura c ON a.id_estructura=c.id_estructura
                                JOIN ssf.linea b ON c.id_linea=b.id_linea
                                JOIN ssf.estadosincidencias d ON a.estadoincidencia=d.id_estadoincidencia
                                JOIN ssf.niveles e ON a.prioridadincidencia=e.id_nivel
                                JOIN ssf.clasificacionincidencia h ON a.id_clasificacionincidencia = h.id_clasificacionincidencia
                                WHERE a.estadoincidencia<>(SELECT id_estadoincidencia FROM ssf.estadosincidencias WHERE descripcionestado='concluido')
                                AND a.id_estructura=@pIdActivo
                                ORDER BY a.ultimaactualizacion DESC";

            object[] parametros = new object[]
            {
                new SqlParameter("@pIdActivo", idEstructura)
            };

            return await _incidenciaContext.IncidenciasGenerales.FromSqlRaw(sqlQuery, parametros).ToListAsync();
        }

        public async Task<List<IncidenciaGeneralVista>> ObtenerIncidenciasNoAtendidasPorDiasEnEstructurasAsync(int numeroDias)
        {
            string sqlQuery = @" SELECT a.id_reporte, a.id_nota, b.clave ubicacion, c.nombre estructura, c.coordenadas, c.id_procesoresponsable, 
                                        c.id_gerenciadivision, a.incidencia,a.estadoincidencia, d.descripcionestado, a.ultimaactualizacion,
                                        a.prioridadincidencia, a.id_clasificacionincidencia, 'ESTRUCTURA' as tiporeporte, e.descripcionnivel,
                                        h.descripcion
                                 FROM ssf.reporteestructuras a
                                 JOIN ssf.estructura c ON a.id_estructura=c.id_estructura
                                 JOIN ssf.linea b ON c.id_linea=b.id_linea
                                 JOIN ssf.estadosincidencias d ON a.estadoincidencia=d.id_estadoincidencia
                                 JOIN ssf.niveles e ON a.prioridadincidencia=e.id_nivel
                                JOIN ssf.clasificacionincidencia h ON a.id_clasificacionincidencia = h.id_clasificacionincidencia
                                 WHERE a.estadoincidencia<>(SELECT id_estadoincidencia FROM ssf.estadosincidencias WHERE descripcionestado='concluido')
                                 AND a.ultimoregistroenbitacora < DATEADD(day, @pDias, GETDATE()) 
                                 ORDER BY a.ultimaactualizacion DESC";

            //numeroDias = -numeroDias;

            object[] parametros = new object[]
            {
                new SqlParameter("@pDias", numeroDias)
            };

            return await _incidenciaContext.IncidenciasGenerales.FromSqlRaw(sqlQuery, parametros).ToListAsync();
        }

        public async Task<List<IncidenciaGeneralVista>> ObtenerIncidenciasReportadasEnProgramaEnInstalacionAsync(int programa)
        {
            string sqlQuery = @"SELECT a.id_reportepunto id_reporte, a.id_nota, b.ubicacion, '' as estructura, b.coordenadas, b.id_procesoresponsable, 
                                       b.id_gerenciadivision, a.incidencia,a.estadoincidencia, d.descripcionestado, a.ultimaactualizacion,
                                       a.prioridadincidencia, a.id_clasificacionincidencia, 'INSTALACION' as tiporeporte, e.descripcionnivel,
                                       h.descripcion
                                FROM ssf.reportepunto a
                                JOIN ssf.puntospatrullaje b ON a.id_punto= b.id_punto
                                JOIN ssf.estadosincidencias c ON a.estadoincidencia= c.id_estadoincidencia
                                JOIN ssf.niveles d ON a.prioridadincidencia= d.id_nivel
                                JOIN ssf.tarjetainformativareporte e ON a.id_reportepunto= e.idreporte
                                JOIN ssf.tarjetainformativa f ON e.idtarjeta= f.id_nota
                                JOIN ssf.clasificacionincidencia g ON a.id_clasificacionincidencia= g.id_clasificacionincidencia
                                JOIN ssf.tiporeporte h ON e.idtiporeporte = h.idtiporeporte
                                WHERE h.descripcion= 'INSTALACION' AND f.id_programa= @pPrograma
                                ORDER BY a.ultimaactualizacion DESC";

            object[] parametros = new object[]
            {
                new SqlParameter("@pPrograma", programa)
            };

            return await _incidenciaContext.IncidenciasGenerales.FromSqlRaw(sqlQuery, parametros).ToListAsync();
        }

        public async Task<List<IncidenciaGeneralVista>> ObtenerIncidenciasReportadasEnProgramaEnEstructuraAsync(int programa)
        {
            string sqlQuery = @"SELECT a.id_reporte, a.id_nota, b.clave ubicacion, c.nombre estructura, c.coordenadas, c.id_procesoresponsable, 
                                       c.id_gerenciadivision, a.incidencia,a.estadoincidencia, d.descripcionestado, a.ultimaactualizacion,
                                       a.prioridadincidencia, a.id_clasificacionincidencia, 'ESTRUCTURA' as tiporeporte, e.descripcionnivel, 
                                       h.descripcion
                                FROM ssf.reporteestructuras a
                                JOIN ssf.estructura b ON a.id_estructura=b.id_estructura
                                JOIN ssf.linea c ON b.id_linea= c.id_linea
                                JOIN ssf.estadosincidencias d ON a.estadoincidencia=d.id_estadoincidencia
                                JOIN ssf.niveles e ON a.prioridadincidencia=e.id_nivel
                                JOIN ssf.tarjetainformativareporte f ON a.id_reporte=f.idreporte
                                JOIN ssf.tarjetainformativa g ON f.idtarjeta=g.id_nota
                                JOIN ssf.clasificacionincidencia h ON a.id_clasificacionincidencia=h.id_clasificacionincidencia
                                JOIN ssf.tiporeporte i ON f.idtiporeporte = i.idtiporeporte
                                WHERE i.descripcion='ESTRUCTURA' AND g.id_programa=@pPrograma
                                ORDER BY a.ultimaactualizacion DESC";

            object[] parametros = new object[]
            {
                new SqlParameter("@pPrograma", programa)
            };

            return await _incidenciaContext.IncidenciasGenerales.FromSqlRaw(sqlQuery, parametros).ToListAsync();
        }

        public async Task<List<IncidenciaGeneralVista>> ObtenerIncidenciasInstalacionPorUbicacionOrIncidenciaAsync(string criterio)
        {
            string sqlQuery = @"SELECT a.id_reportepunto id_reporte, a.id_nota, b.ubicacion, '' as estructura, b.coordenadas, b.id_procesoresponsable, 
                                       b.id_gerenciadivision, a.incidencia, a.estadoincidencia, d.descripcionestado, a.ultimaactualizacion, 
                                       a.prioridadincidencia, a.id_clasificacionincidencia, 'INSTALACION' as tiporeporte, e.descripcionnivel,
                                       h.descripcion
                               FROM ssf.reportepunto a
                               JOIN ssf.puntospatrullaje b ON a.id_punto=b.id_punto
                               JOIN ssf.estadosincidencias d ON a.estadoincidencia=d.id_estadoincidencia
                               JOIN ssf.niveles e ON a.prioridadincidencia=e.id_nivel
                               JOIN ssf.clasificacionincidencia h ON a.id_clasificacionincidencia=h.id_clasificacionincidencia
                               WHERE (b.ubicacion like @pCriterio OR a.incidencia like @pCriterio)
                               ORDER BY a.ultimaactualizacion DESC";

            var crit = "%" + criterio + "%";

            object[] parametros = new object[]
            {
                new SqlParameter("@pCriterio", crit)
            };

            return await _incidenciaContext.IncidenciasGenerales.FromSqlRaw(sqlQuery, parametros).ToListAsync();
        }

        public async Task<List<IncidenciaGeneralVista>> ObtenerIncidenciasEstructuraPorUbicacionOrIncidenciaAsync(string criterio)
        {
            string sqlQuery = @" SELECT a.id_reporte, a.id_nota, A.ID_ESTRUCTURA, b.clave ubicacion, c.nombre estructura, c.coordenadas, c.id_procesoresponsable, 
                                        c.id_gerenciadivision, a.incidencia, a.estadoincidencia, d.descripcionestado, a.ultimaactualizacion, 
                                        a.prioridadincidencia, a.id_clasificacionincidencia, 'ESTRUCTURA' as tiporeporte, e.descripcionnivel, 
                                        h.descripcion
                                 FROM ssf.reporteestructuras a
                                 JOIN ssf.estructura c ON a.id_estructura = c.id_estructura
                                 JOIN ssf.linea b ON c.id_linea = b.id_linea
                                 JOIN ssf.estadosincidencias d ON a.estadoincidencia=d.id_estadoincidencia
                                 JOIN ssf.niveles e ON a.prioridadincidencia=e.id_nivel
                                 JOIN ssf.clasificacionincidencia h ON a.id_clasificacionincidencia=h.id_clasificacionincidencia 
                                 WHERE (b.clave like @pCriterio OR a.incidencia like @pCriterio)
                                 ORDER BY a.ultimaactualizacion DESC";

            var crit = "%" + criterio + "%";

            object[] parametros = new object[]
            {
                new SqlParameter("@pCriterio", crit)
            };

            return await _incidenciaContext.IncidenciasGenerales.FromSqlRaw(sqlQuery, parametros).ToListAsync();
        }

        public async Task<List<IncidenciaGeneralVista>> ObtenerIncidenciasNoConcluidasInstalacionAsync(string criterio)
        {
            string sqlQuery = @"SELECT a.id_reportepunto id_reporte, a.id_nota, A.ID_PUNTO, b.ubicacion, '' as estructura, b.coordenadas, b.id_procesoresponsable, 
                                       b.id_gerenciadivision, a.incidencia, a.estadoincidencia, d.descripcionestado, a.ultimaactualizacion, 
                                       a.prioridadincidencia, a.id_clasificacionincidencia, 'INSTALACION' as tiporeporte, e.descripcionnivel, 
                                       h.descripcion
                                FROM ssf.reportepunto a
                                JOIN ssf.puntospatrullaje b ON a.id_punto=b.id_punto
                                JOIN ssf.estadosincidencias d ON a.estadoincidencia=d.id_estadoincidencia
                                JOIN ssf.niveles e ON a.prioridadincidencia=e.id_nivel
                                JOIN ssf.clasificacionincidencia h ON a.id_clasificacionincidencia=h.id_clasificacionincidencia
                                WHERE  (b.ubicacion like @pCriterio OR a.incidencia like @pCriterio)
                                AND a.estadoincidencia <> (SELECT id_estadoincidencia FROM ssf.estadosincidencias WHERE descripcionestado='concluido')
                                ORDER BY a.ultimaactualizacion DESC";

            var crit = "%" + criterio + "%";

            object[] parametros = new object[]
            {
                new SqlParameter("@pCriterio", crit)
            };

            return await _incidenciaContext.IncidenciasGenerales.FromSqlRaw(sqlQuery, parametros).ToListAsync();
        }

        public async Task<List<IncidenciaGeneralVista>> ObtenerIncidenciasNoConcluidasEstructuraAsync(string criterio)
        {
            string sqlQuery = @"SELECT a.id_reporte, a.id_nota, A.ID_ESTRUCTURA, b.clave ubicacion, c.nombre estructura, c.coordenadas, c.id_procesoresponsable, 
                                       c.id_gerenciadivision, a.incidencia, a.estadoincidencia, d.descripcionestado, a.ultimaactualizacion, a.prioridadincidencia, 
                                       a.id_clasificacionincidencia, 'ESTRUCTURA' as tiporeporte, e.descripcionnivel, 
                                       h.descripcion
                                FROM ssf.reporteestructuras a
                                JOIN ssf.estructura c ON a.id_estructura=c.id_estructura
                                JOIN ssf.linea b ON c.id_linea=b.id_linea
                                JOIN ssf.estadosincidencias d ON a.estadoincidencia=d.id_estadoincidencia
                                JOIN ssf.niveles e ON a.prioridadincidencia=e.id_nivel
                                JOIN ssf.clasificacionincidencia h ON a.id_clasificacionincidencia=h.id_clasificacionincidencia
                                WHERE (b.clave like @pCriterio OR a.incidencia like @pCriterio)
                                AND a.estadoincidencia <> (SELECT id_estadoincidencia FROM ssf.estadosincidencias WHERE descripcionestado='concluido')
                                ORDER BY a.ultimaactualizacion DESC";

            var crit = "%" + criterio + "%";

            object[] parametros = new object[]
            {
                new SqlParameter("@pCriterio", crit)
            };

            return await _incidenciaContext.IncidenciasGenerales.FromSqlRaw(sqlQuery, parametros).ToListAsync();
        }

        public async Task<List<IncidenciaGeneralVista>> ObtenerIncidenciasInstalacionEstadoEspecificoAsync(string criterio, int complemento)
        {
            string sqlQuery = @"SELECT a.id_reportepunto id_reporte, a.id_nota, A.ID_PUNTO, b.ubicacion, '' as estructura, b.coordenadas, b.id_procesoresponsable, 
                                       b.id_gerenciadivision, a.incidencia, a.estadoincidencia, d.descripcionestado, a.ultimaactualizacion, 
                                       a.prioridadincidencia, a.id_clasificacionincidencia, 'INSTALACION' as tiporeporte, e.descripcionnivel, 
                                       h.descripcion
                                FROM ssf.reportepunto a
                                JOIN ssf.puntospatrullaje b ON a.id_punto=b.id_punto
                                JOIN ssf.estadosincidencias d ON a.estadoincidencia=d.id_estadoincidencia
                                JOIN ssf.niveles e ON a.prioridadincidencia=e.id_nivel
                                JOIN ssf.clasificacionincidencia h ON a.id_clasificacionincidencia=h.id_clasificacionincidencia
                                WHERE (b.ubicacion like @pCriterio OR a.incidencia like @pCriterio)
                                AND a.estadoincidencia= @pComplemento
                                ORDER BY a.ultimaactualizacion DESC";

            var crit = "%" + criterio + "%";

            object[] parametros = new object[]
            {
                new SqlParameter("@pCriterio", crit),
                new SqlParameter("@pComplemento", complemento),
            };

            return await _incidenciaContext.IncidenciasGenerales.FromSqlRaw(sqlQuery, parametros).ToListAsync();
        }

        public async Task<List<IncidenciaGeneralVista>> ObtenerIncidenciasEstructuraEstadoEspecificoAsync(string criterio, int complemento)
        {
            string sqlQuery = @"SELECT a.id_reporte, a.id_nota, A.ID_ESTRUCTURA, b.clave ubicacion, c.nombre estructura, c.coordenadas, c.id_procesoresponsable, 
                                       c.id_gerenciadivision, a.incidencia, a.estadoincidencia, d.descripcionestado, a.ultimaactualizacion, 
                                       a.prioridadincidencia, a.id_clasificacionincidencia, 'ESTRUCTURA' as tiporeporte, e.descripcionnivel, 
                                       h.descripcion
                                FROM ssf.reporteestructuras a
                                JOIN ssf.estructura c ON a.id_estructura=c.id_estructura
                                JOIN ssf.linea b ON c.id_linea=b.id_linea
                                JOIN ssf.estadosincidencias d ON a.estadoincidencia=d.id_estadoincidencia
                                JOIN ssf.niveles e ON a.prioridadincidencia=e.id_nivel
                                JOIN ssf.clasificacionincidencia h ON a.id_clasificacionincidencia=h.id_clasificacionincidencia
                                WHERE (b.clave like @pCriterio OR a.incidencia like @pCriterio)
                                AND a.estadoincidencia=@pComplemento
                                ORDER BY a.ultimaactualizacion DESC";

            var crit = "%" + criterio + "%";

            object[] parametros = new object[]
            {
                new SqlParameter("@pCriterio", crit),
                new SqlParameter("@pComplemento", complemento),
            };

            return await _incidenciaContext.IncidenciasGenerales.FromSqlRaw(sqlQuery, parametros).ToListAsync();
        }

        public async Task<List<ReporteIncidenciaAbierto>> ObtenerReportesAbiertosPorInstalacionAsync(int idActivo, int claseIncidencia)
        {
            string sqlQuery = @"SELECT a.id_reportePunto id_reporte, a.incidencia 
                                FROM ssf.reportePunto a
                                JOIN ssf.estadosincidencias b ON a.estadoincidencia=b.id_estadoincidencia
                                WHERE b.id_estadoincidencia <> (SELECT id_estadoincidencia FROM ssf.estadosincidencias WHERE descripcionestado='concluido')
                                AND a.id_punto= @pIdActivo
                                AND a.id_clasificacionincidencia= @pClaseIncidencia";

            object[] parametros = new object[]
            {
                new SqlParameter("@pIdActivo", idActivo),
                new SqlParameter("@pClaseIncidencia", claseIncidencia)
            };

            return await _incidenciaContext.ReportesIncidenciasAbiertos.FromSqlRaw(sqlQuery, parametros).ToListAsync();
        }

        public async Task<List<ReporteIncidenciaAbierto>> ObtenerReportesAbiertosPorEstructuraAsync(int idEstructura, int claseIncidencia)
        {
            string sqlQuery = @"SELECT id_reporte ,incidencia 
                                FROM ssf.reporteEstructuras a
                                JOIN ssf.estadosincidencias b ON a.estadoincidencia=b.id_estadoincidencia
                                WHERE b.id_estadoincidencia <> (SELECT id_estadoincidencia FROM ssf.estadosincidencias WHERE descripcionestado='concluido')
                                AND a.id_estructura= @pIdActivo AND a.id_clasificacionincidencia=@pClaseIncidencia";

            object[] parametros = new object[]
            {
                new SqlParameter("@pIdActivo", idEstructura),
                new SqlParameter("@pClaseIncidencia", claseIncidencia)
            };

            return await _incidenciaContext.ReportesIncidenciasAbiertos.FromSqlRaw(sqlQuery, parametros).ToListAsync();
        }

        public async Task ActualizaReporteEnInstalacionPorIncidenciaExistenteAsync(int idReporte, string incidencia, int prioridad)
        {
            var reporte = await _incidenciaContext.ReportesInstalaciones.Where(x => x.IdReportePunto == idReporte).SingleOrDefaultAsync();

            if (reporte != null)
            {
                reporte.Incidencia = incidencia;
                reporte.PrioridadIncidencia = prioridad;
                reporte.UltimaActualizacion = DateTime.UtcNow;

                _incidenciaContext.ReportesInstalaciones.Update(reporte);
                await _incidenciaContext.SaveChangesAsync();
            }
        }
        
        public async Task ActualizaReporteEnEstructuraPorIncidenciaExistenteAsync(int idReporte, string incidencia, int prioridad, int idTarjeta, string tipo)
        {
            var reporte = await _incidenciaContext.ReportesEstructuras.Where(x => x.IdReporte == idReporte).SingleOrDefaultAsync();

            if (reporte != null)
            {
                reporte.Incidencia = incidencia;
                reporte.PrioridadIncidencia = prioridad;
                reporte.UltimaActualizacion = DateTime.UtcNow;

                _incidenciaContext.ReportesEstructuras.Update(reporte);

                var up = new IncidenciasDtoForUpdate()
                {
                    intIdReporte = idReporte,
                    intIdTarjeta = idTarjeta,
                    strTipoIncidencia = tipo
                };

                await AgregaTarjetaInformativaReporteEnMemoriaAsync(up);

                await _incidenciaContext.SaveChangesAsync();
            }
        }

        public async Task ActualizaReporteEnInstalacionAsync(IncidenciasDtoForUpdate i)
        {
            var reporte = await _incidenciaContext.ReportesInstalaciones.Where(x => x.IdReportePunto == i.intIdReporte).SingleOrDefaultAsync();

            if (reporte != null)
            {
                reporte.Incidencia = i.strDescripcionIncidencia;
                reporte.PrioridadIncidencia = i.intIdPrioridadIncidencia;
                reporte.IdClasificacionIncidencia = i.intIdClasificacionIncidencia;
                reporte.EstadoIncidencia = i.intIdEstadoIncidencia;
                reporte.UltimaActualizacion = DateTime.UtcNow;

                _incidenciaContext.ReportesInstalaciones.Update(reporte);
                await AgregaTarjetaInformativaReporteEnMemoriaAsync(i);

                await _incidenciaContext.SaveChangesAsync();
            }
        }

        public async Task ActualizaReporteEnEstructuraAsync(IncidenciasDtoForUpdate i)
        {
            var reporte = await _incidenciaContext.ReportesEstructuras.Where(x => x.IdReporte == i.intIdReporte).SingleOrDefaultAsync();

            if (reporte != null)
            {
                reporte.Incidencia = i.strDescripcionIncidencia;
                reporte.PrioridadIncidencia = i.intIdPrioridadIncidencia;
                reporte.IdClasificacionIncidencia = i.intIdClasificacionIncidencia;
                reporte.EstadoIncidencia = i.intIdEstadoIncidencia;
                reporte.UltimaActualizacion = DateTime.UtcNow;

                _incidenciaContext.ReportesEstructuras.Update(reporte);
                await AgregaTarjetaInformativaReporteEnMemoriaAsync(i);

                await _incidenciaContext.SaveChangesAsync();
            }
        }

        public async Task<ReportePunto> AgregaReporteInstalacionAsync(int idPunto, int idNota, string incidencia, int edoIncidencia, int prioridad, int clasificacion)
        {
            var reporte = new ReportePunto() 
            { 
                IdPunto = idPunto,
                IdNota = idNota,
                Incidencia = incidencia,
                EstadoIncidencia = edoIncidencia,
                PrioridadIncidencia= prioridad,
                IdClasificacionIncidencia = clasificacion,
                UltimaActualizacion= DateTime.UtcNow,
                //Campos no nulos
                UltimoRegistroEnBitacora = DateTime.UtcNow
            };

            _incidenciaContext.ReportesInstalaciones.Add(reporte);

            await _incidenciaContext.SaveChangesAsync();

            return reporte;
        }

        public async Task<ReporteEstructura> AgregaReporteEstructuraAsync(IncidenciasDtoForCreate i)
        {
            var reporte = new ReporteEstructura()
            {
                IdEstructura = i.intIdActivo,
                IdNota = i.intIdTarjeta,
                Incidencia = i.strDescripcionIncidencia,
                EstadoIncidencia = i.intIdEstadoIncidencia,
                PrioridadIncidencia = i.intIdPrioridadIncidencia,
                IdClasificacionIncidencia = i.intIdClasificacionIncidencia,
                UltimaActualizacion = DateTime.UtcNow,
                //Campos no nulos
                UltimoRegistroEnBitacora = DateTime.UtcNow
            };

            _incidenciaContext.ReportesEstructuras.Add(reporte);

            var up = new IncidenciasDtoForUpdate() 
            {
                intIdReporte = i.intIdReporte,
                intIdTarjeta = i.intIdTarjeta,
                strTipoIncidencia = i.strTipoIncidencia,
            };

            await AgregaTarjetaInformativaReporteEnMemoriaAsync(up);

            await _incidenciaContext.SaveChangesAsync();

            return reporte;
        }

        private async Task<bool> ExisteTarjetaInformativaReporte(int idTarjeta, int idReporte, int idTipo)
        {
            return await _incidenciaContext.TarjetaInformativaReportes.Where(x => x.Idtarjeta == idTarjeta && x.Idreporte == idReporte && x.Idtiporeporte == idTipo).AnyAsync();
        }

        private async Task AgregaTarjetaInformativaReporteEnMemoriaAsync(IncidenciasDtoForUpdate i)
        {
            if (i.intIdReporte > 0 && i.intIdTarjeta > 0)
            {
                var t = await _incidenciaContext.TiposReporte.Where(x => x.Descripcion == i.strTipoIncidencia).FirstOrDefaultAsync();

                if (t != null)
                {
                    var existe = await ExisteTarjetaInformativaReporte(i.intIdTarjeta, i.intIdReporte, t.Idtiporeporte);

                    if (!existe)
                    {
                        var tir = new TarjetaInformativaReporte()
                        {
                            Idtarjeta = i.intIdTarjeta,
                            Idreporte = i.intIdReporte,
                            Idtiporeporte = t.Idtiporeporte
                        };

                        _incidenciaContext.TarjetaInformativaReportes.Add(tir);
                    }
                }
            }
        }
    }
}
