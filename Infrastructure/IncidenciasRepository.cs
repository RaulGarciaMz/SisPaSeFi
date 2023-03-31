using Domain.DTOs.catalogos;
using Domain.Entities;
using Domain.Entities.Vistas;
using Domain.Ports.Driven;
using Domain.Ports.Driven.Repositories;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using SqlServerAdapter.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace SqlServerAdapter
{
    public class IncidenciasRepository : IIncidenciasRepo
    {
        protected readonly IncidenciaContext _incidenciaContext;

        public IncidenciasRepository(IncidenciaContext incidenciaContext)
        {
            _incidenciaContext = incidenciaContext ?? throw new ArgumentNullException(nameof(incidenciaContext));
        }
       
        public async Task<List<IncidenciaInstalacionVista>> ObtenerIncidenciasAbiertasEnInstalacionAsync(int idActivo)
        {
            string sqlQuery = @"SELECT a.id_reportepunto id_reporte, a.id_nota, a.id_punto, b.ubicacion, 
                                       a.incidencia, a.estadoincidencia, 
                                       a.prioridadincidencia, a.id_clasificacionincidencia, a.ultimaactualizacion,
                                       b.coordenadas, b.id_procesoresponsable, b.id_gerenciadivision, 
                                       d.descripcionestado, e.descripcionnivel,'INSTALACION' as tiporeporte
                                FROM ssf.reportepunto a
                                JOIN ssf.puntospatrullaje b ON a.id_punto= b.id_punto
                                JOIN ssf.estadosincidencias d ON a.estadoincidencia= d.id_estadoincidencia
                                JOIN ssf.niveles e ON a.prioridadincidencia=e.id_nivel
                                WHERE a.estadoincidencia<>(SELECT id_estadoincidencia FROM ssf.estadosincidencias WHERE descripcionestado= 'concluido')
                                AND a.id_punto= @pIdActivo
                                ORDER BY a.ultimaactualizacion DESC";

            object[] parametros = new object[]
            {
                new SqlParameter("@pIdActivo", idActivo)
            };

            return await _incidenciaContext.IncidenciasInstalaciones.FromSqlRaw(sqlQuery, parametros).ToListAsync();
        }

        public async Task<List<IncidenciaEstructuraVista>> ObtenerIncidenciasAbiertasEnEstructuraAsync(int idEstructura) 
        {
            string sqlQuery = @"SELECT a.id_reporte, a.id_nota, a.id_estructura, a.incidencia, a.estadoincidencia, 
                                       a.prioridadincidencia, a.id_clasificacionincidencia, a.ultimaactualizacion,
                                       b.clave, c.nombre, c.coordenadas, c.id_procesoresponsable, c.id_gerenciadivision,
                                       d.descripcionestado,e.descripcionnivel, 'ESTRUCTURA' as tiporeporte
                                FROM ssf.reporteestructuras a 
                                JOIN ssf.estructura c ON a.id_estructura=c.id_estructura
                                JOIN ssf.linea b ON c.id_linea=b.id_linea
                                JOIN ssf.estadosincidencias d ON a.estadoincidencia=d.id_estadoincidencia
                                JOIN ssf.niveles e ON a.prioridadincidencia=e.id_nivel
                                WHERE a.estadoincidencia<>(SELECT id_estadoincidencia FROM ssf.estadosincidencias WHERE descripcionestado='concluido')
                                AND a.id_estructura=@pIdActivo
                                ORDER BY a.ultimaactualizacion DESC";

            object[] parametros = new object[]
            {
                new SqlParameter("@pIdActivo", idEstructura)
            };

            return await _incidenciaContext.IncidenciasEstructuras.FromSqlRaw(sqlQuery, parametros).ToListAsync();
        }

        public async Task<List<IncidenciaEstructuraVista>> ObtenerIncidenciasNoAtendidasPorDiasEnEstructurasAsync(int numeroDias) 
        {
            string sqlQuery = @" SELECT a.id_reporte, a.id_nota, a.id_estructura,a.incidencia, a.estadoincidencia, 
                                        a.prioridadincidencia, a.id_clasificacionincidencia, a.ultimaactualizacion,
                                        b.clave, c.nombre, c.coordenadas, c.id_procesoresponsable, c.id_gerenciadivision,
                                		d.descripcionestado, e.descripcionnivel, 'ESTRUCTURA' as tiporeporte
                                 FROM ssf.reporteestructuras a
                                 JOIN ssf.estructura c ON a.id_estructura=c.id_estructura
                                 JOIN ssf.linea b ON c.id_linea=b.id_linea
                                 JOIN ssf.estadosincidencias d ON a.estadoincidencia=d.id_estadoincidencia
                                 JOIN ssf.niveles e ON a.prioridadincidencia=e.id_nivel
                                 WHERE a.estadoincidencia<>(SELECT id_estadoincidencia FROM ssf.estadosincidencias WHERE descripcionestado='concluido')
                                 AND a.ultimoregistroenbitacora < DATEADD(day, @pDias, GETDATE()) 
                                 ORDER BY a.ultimaactualizacion DESC";

            numeroDias = -numeroDias;

            object[] parametros = new object[]
            {
                new SqlParameter("@pDias", numeroDias)
            };

            return await _incidenciaContext.IncidenciasEstructuras.FromSqlRaw(sqlQuery, parametros).ToListAsync();
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

        public async Task ActualizaReporteEnInstalacionAsync(int idReporte, string incidencia, int prioridad, int clasificacion, int estado)
        {
            var reporte = await _incidenciaContext.ReportesInstalaciones.Where(x => x.IdReportePunto == idReporte).SingleOrDefaultAsync();   

            if (reporte != null) 
            {
                reporte.Incidencia = incidencia;
                reporte.PrioridadIncidencia = prioridad;
                reporte.IdClasificacionIncidencia = clasificacion;
                reporte.EstadoIncidencia = estado;
                reporte.UltimaActualizacion = DateTime.UtcNow;

                _incidenciaContext.ReportesInstalaciones.Update(reporte);
                await _incidenciaContext.SaveChangesAsync();
            }
        }

        public async Task ActualizaReporteEnEstructuraPorIncidenciaExistenteAsync(int idReporte, string incidencia, int prioridad)
        {
            var reporte = await _incidenciaContext.ReportesEstructuras.Where(x => x.IdReporte == idReporte).SingleOrDefaultAsync();

            if (reporte != null)
            {
                reporte.Incidencia = incidencia;
                reporte.PrioridadIncidencia = prioridad;
                reporte.UltimaActualizacion = DateTime.UtcNow;

                _incidenciaContext.ReportesEstructuras.Update(reporte);
                await _incidenciaContext.SaveChangesAsync();
            }
        }

        public async Task ActualizaReporteEnEstructuraAsync(int idReporte, string incidencia, int prioridad, int clasificacion, int estado)
        {
            var reporte = await _incidenciaContext.ReportesEstructuras.Where(x => x.IdReporte == idReporte).SingleOrDefaultAsync();

            if (reporte != null)
            {
                reporte.Incidencia = incidencia;
                reporte.PrioridadIncidencia = prioridad;
                reporte.IdClasificacionIncidencia = clasificacion;
                reporte.EstadoIncidencia = estado;
                reporte.UltimaActualizacion = DateTime.UtcNow;

                _incidenciaContext.ReportesEstructuras.Update(reporte);
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
                UltimaActualizacion= DateTime.UtcNow
            };

            _incidenciaContext.ReportesInstalaciones.Add(reporte);

            await _incidenciaContext.SaveChangesAsync();

            return reporte;
        }

        public async Task<ReporteEstructura> AgregaReporteEstructuraAsync(int idEstructura, int idNota, string incidencia, int edoIncidencia, int prioridad, int clasificacion)
        {
            var reporte = new ReporteEstructura()
            {
                IdEstructura = idEstructura,
                IdNota = idNota,
                Incidencia = incidencia,
                EstadoIncidencia = edoIncidencia,
                PrioridadIncidencia = prioridad,
                IdClasificacionIncidencia = clasificacion,
                UltimaActualizacion = DateTime.UtcNow
            };

            _incidenciaContext.ReportesEstructuras.Add(reporte);

            await _incidenciaContext.SaveChangesAsync();

            return reporte;
        }


        //TODO Revisar funcionalidad porque la consulta original está rara
        public async Task AgregaTarjetaInformativaReporteAsync(int idTarjeta, int idReporte, string tipoIncidencia)
        {
            var tr = await _incidenciaContext.TiposReporte.Where(x => x.Descripcion == tipoIncidencia).FirstOrDefaultAsync();

            if (tr != null)
            {
                var existe = await _incidenciaContext.TarjetaInformativaReportes.AnyAsync(x => x.Idtarjeta == idTarjeta && x.Idreporte == idReporte && x.Idtiporeporte == tr.Idtiporeporte);

                if (!existe)
                {
                    var tir = new TarjetaInformativaReporte() 
                    {
                        Idtarjeta = idTarjeta,
                        Idreporte = idReporte,
                        Idtiporeporte = tr.Idtiporeporte
                    };

                    _incidenciaContext.TarjetaInformativaReportes.Add(tir);

                    await _incidenciaContext.SaveChangesAsync();
                }
            }
        }
    }
}
