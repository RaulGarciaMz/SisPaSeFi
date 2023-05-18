using Domain.Entities;
using Domain.Entities.Vistas;
using Domain.Ports.Driven.Repositories;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using SqlServerAdapter.Data;

namespace SqlServerAdapter
{
    public class EvidenciasRepository : IEvidenciasRepo
    {

        protected readonly EvidenciasContext _evidenciaContext;

        public EvidenciasRepository(EvidenciasContext evidenciaContext)
        {
            _evidenciaContext = evidenciaContext ?? throw new ArgumentNullException(nameof(evidenciaContext));
        }

        public async Task AgregarEvidenciaDeEstructuraAsync(int idReporte, string rutaArchivo, string nombreArchivo, string descripcion)
        {
            var evidencia = new EvidenciaIncidencia() 
            {
                IdReporte = idReporte,
                RutaArchivo = rutaArchivo,
                NombreArchivo = nombreArchivo,
                Descripcion = descripcion,
                //Campos no nulos
                UltimaActualizacion = DateTime.UtcNow
            };

            _evidenciaContext.EvidenciasEstructura.Add(evidencia);

            await _evidenciaContext.SaveChangesAsync();
        }

        public async Task AgregarEvidenciaDeInstalacionAsync(int idReporte, string rutaArchivo, string nombreArchivo, string descripcion)
        {
            var evidencia = new EvidenciaIncidenciaPunto()
            {
                IdReportePunto = idReporte,
                RutaArchivo = rutaArchivo,
                NombreArchivo = nombreArchivo,
                Descripcion = descripcion,
                //Campo no nulos
                UltimaActualizacion = DateTime.UtcNow
            };

            _evidenciaContext.EvidenciasInstalacion.Add(evidencia);

            await _evidenciaContext.SaveChangesAsync();
        }

        public async Task AgregarEvidenciaSeguimientoDeEstructuraAsync(int idReporte, string rutaArchivo, string nombreArchivo, string descripcion)
        {
            var evidencia = new EvidenciaSeguimientoIncidencia()
            {
                IdBitacoraSeguimientoIncidencia = idReporte,
                RutaArchivo = rutaArchivo,
                NombreArchivo = nombreArchivo,
                Descripcion = descripcion,
                //Campos no nuls
                UltimaActualizacion= DateTime.UtcNow
            };

            _evidenciaContext.EvidenciasSeguimientoEstructura.Add(evidencia);

            await _evidenciaContext.SaveChangesAsync();
        }

        public async Task AgregarEvidenciaSeguimientoDeInstalacionAsync(int idReporte, string rutaArchivo, string nombreArchivo, string descripcion)
        {
            var evidencia = new EvidenciaSeguimientoIncidenciaPunto()
            {
                IdBitacoraSeguimientoIncidenciaPunto = idReporte,
                RutaArchivo = rutaArchivo,
                NombreArchivo = nombreArchivo,
                Descripcion = descripcion,
                //Campos no nulos
                UltimaActualizacion = DateTime.UtcNow
            };

            _evidenciaContext.EvidenciasSeguimientoInstalacion.Add(evidencia);

            await _evidenciaContext.SaveChangesAsync();
        }

        public async Task BorrarEvidenciaDeEstructuraAsync(int idEvidencia)
        {
            var ev = await _evidenciaContext.EvidenciasEstructura.Where(x => x.IdEvidenciaIncidencia == idEvidencia).FirstOrDefaultAsync();

            if (ev != null)
            {
                _evidenciaContext.EvidenciasEstructura.Remove(ev);
                await _evidenciaContext.SaveChangesAsync();
            }
        }

        public async Task BorrarEvidenciaDeInstalacionAsync(int idEvidencia)
        {
            var ev = await _evidenciaContext.EvidenciasInstalacion.Where(x => x.IdEvidenciaIncidenciaPunto == idEvidencia).FirstOrDefaultAsync();

            if (ev != null) 
            {
                _evidenciaContext.EvidenciasInstalacion.Remove(ev);
                await _evidenciaContext.SaveChangesAsync();
            }         
        }

        public async Task BorrarEvidenciaSeguimientoDeEstructuraAsync(int idEvidencia)
        {
            var ev = await _evidenciaContext.EvidenciasSeguimientoEstructura.Where(x => x.IdBitacoraSeguimientoIncidencia == idEvidencia).FirstOrDefaultAsync();

            if (ev != null)
            {
                _evidenciaContext.EvidenciasSeguimientoEstructura.Remove(ev);
                await _evidenciaContext.SaveChangesAsync();
            }
        }

        public async Task BorrarEvidenciaSeguimientoDeInstalacionAsync(int idEvidencia)
        {
            var ev = await _evidenciaContext.EvidenciasSeguimientoInstalacion.Where(x => x.IdBitacoraSeguimientoIncidenciaPunto == idEvidencia).FirstOrDefaultAsync();

            if (ev != null)
            {
                _evidenciaContext.EvidenciasSeguimientoInstalacion.Remove(ev);
                await _evidenciaContext.SaveChangesAsync();
            }
        }

        public async Task<List<EvidenciaVista>> ObtenerEvidenciaDeEstructuraAsync(int idReporte)
        {
            string sqlQuery = @"SELECT b.id_evidenciaincidencia IdEvidencia, b.id_reporte IdReporte, b.rutaarchivo, 
                                       b.nombrearchivo, b.descripcion, 'ESTRUCTURA' as tiporeporte
                                FROM ssf.reporteestructuras a
                                JOIN ssf.evidenciaincidencias b ON a.id_reporte=b.id_reporte
                                WHERE a.id_reporte= @pReporte";

            object[] parametros = new object[]
            {
                new SqlParameter("@pReporte", idReporte),
            };

            return await _evidenciaContext.EvidenciasEstructuraVista.FromSqlRaw(sqlQuery, parametros).ToListAsync();
        }

        public async Task<List<EvidenciaVista>> ObtenerEvidenciaDeInstalacionAsync(int idReporte)
        {
            string sqlQuery = @"SELECT b.id_evidenciaincidenciapunto IdEvidencia, b.id_reportepunto IdReporte, b.rutaarchivo, 
                                       b.nombrearchivo, b.descripcion, 'INSTALACION' as tiporeporte
                                FROM ssf.reportepunto a
                                JOIN ssf.evidenciaincidenciaspunto b ON a.id_reportepunto=b.id_reportepunto
                                WHERE a.id_reportepunto= @pReporte";

            object[] parametros = new object[]
            {
                new SqlParameter("@pReporte", idReporte),
            };

            return await _evidenciaContext.EvidenciasEstructuraVista.FromSqlRaw(sqlQuery, parametros).ToListAsync();
        }

        public async Task<List<EvidenciaVista>> ObtenerEvidenciaSeguimientoDeEstructuraAsync(int idReporte)
        {
            string sqlQuery = @"SELECT b.id_evidenciaseguimientoincidencia IdEvidencia, b.id_bitacoraseguimientoincidencia IdReporte, b.rutaarchivo, 
                                       b.nombrearchivo, b.descripcion,'SeguimientoESTRUCTURA' as tiporeporte
                                FROM ssf.bitacoraseguimientoincidencia a
                                JOIN ssf.evidenciaseguimientoincidencia b ON a.id_bitacoraseguimientoincidencia = b.id_bitacoraseguimientoincidencia
                                WHERE a.id_bitacoraseguimientoincidencia = @pReporte";
            
            object[] parametros = new object[]
            {
                new SqlParameter("@pReporte", idReporte),
            };

            return await _evidenciaContext.EvidenciasEstructuraVista.FromSqlRaw(sqlQuery, parametros).ToListAsync();
        }

        public async Task<List<EvidenciaVista>> ObtenerEvidenciaSeguimientoDeInstalacionAsync(int idReporte)
        {
            string sqlQuery = @"SELECT b.id_evidenciaseguimientoincidenciapunto IdEvidencia, b.id_bitacoraseguimientoincidenciapunto IdReporte, b.rutaarchivo, 
                                       b.nombrearchivo, b.descripcion, 'SeguimientoINSTALACION' as tiporeporte
                                FROM ssf.bitacoraseguimientoincidenciapunto a
                                JOIN ssf.evidenciaseguimientoincidenciapunto b ON a.id_bitacoraseguimientoincidenciapunto = b.id_bitacoraseguimientoincidenciapunto
                                WHERE a.id_bitacoraseguimientoincidenciapunto = @pReporte";

            object[] parametros = new object[]
            {
                new SqlParameter("@pReporte", idReporte),
            };

            return await _evidenciaContext.EvidenciasEstructuraVista.FromSqlRaw(sqlQuery, parametros).ToListAsync();
        }
    }
}







