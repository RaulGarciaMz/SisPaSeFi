using Domain.Entities;
using Domain.Entities.Vistas;
using Domain.Ports.Driven.Repositories;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using SqlServerAdapter.Data;

namespace SqlServerAdapter
{
    public class BitacoraIncidenciaRepository : IBitacoraRepo
    {
        protected readonly BitacoraIncidenciaContext _bitacoraContext;

        public BitacoraIncidenciaRepository(BitacoraIncidenciaContext bitacoraContext)
        {
            _bitacoraContext = bitacoraContext ?? throw new ArgumentNullException(nameof(bitacoraContext));
        }

        public async Task<List<BitacoraSeguimientoVista>> ObtenerBitacoraInstalacionPorReporteAsync(int idReporte)
        {
            string sqlQuery = @"SELECT a.id_bitacoraseguimientoincidenciapunto id_bitacora, a.id_reportepunto id_reporte, a.ultimaactualizacion, a.id_usuario, 
                                       b.nombre, b.apellido1, b.apellido2, a.descripcion, a.id_estadoincidencia, c.descripcionestado, 'INSTALACION' as tipoincidencia
                                FROM ssf.bitacoraseguimientoincidenciapunto a
                                JOIN ssf.usuarios b ON a.id_usuario = b.id_usuario
                                JOIN ssf.estadosincidencias c ON a.id_estadoincidencia = c.id_estadoincidencia
                                WHERE id_reportepunto = @pIdReporte";

            object[] parametros = new object[]
            {
                new SqlParameter("@pIdReporte", idReporte)
            };

            return await _bitacoraContext.BitacoraSeguimientosVista.FromSqlRaw(sqlQuery, parametros).ToListAsync();

        }

        public async Task<List<BitacoraSeguimientoVista>> ObtenerBitacoraEstructuraPorReporteAsync(int idReporte)
        {
            string sqlQuery = @"SELECT a.id_bitacoraseguimientoincidencia id_bitacora, a.id_reporte, a.ultimaactualizacion, a.id_usuario, 
                                       b.nombre, b.apellido1, b.apellido2, a.descripcion, a.id_estadoincidencia, c.descripcionestado,'ESTRUCTURA' as tipoincidencia
                                FROM ssf.bitacoraseguimientoincidencia a
                                JOIN ssf.usuarios b ON a.id_usuario=b.id_usuario
                                JOIN ssf.estadosincidencias c ON a.id_estadoincidencia=c.id_estadoincidencia
                                WHERE id_reporte= @pIdReporte";

            object[] parametros = new object[]
            {
                new SqlParameter("@pIdReporte", idReporte)
            };

            return await _bitacoraContext.BitacoraSeguimientosVista.FromSqlRaw(sqlQuery, parametros).ToListAsync();

        }

        public async Task AgregaBitacoraEstructuraAsync(int idReporte, int idEstado, int idUsuario, string descripcion)
        {
            var horaActualizacion = DateTime.UtcNow;

            var bitacora = new BitacoraSeguimientoIncidencia()
            {
                IdReporte = idReporte,
                IdUsuario = idUsuario,
                IdEstadoIncidencia = idEstado,
                Descripcion = descripcion,
                UltimaActualizacion = horaActualizacion
            };

            _bitacoraContext.BitacorasIncidenciaEstructura.Add(bitacora);

            var reporte = await _bitacoraContext.ReportesEstructura.Where(x => x.IdReporte == idReporte).SingleOrDefaultAsync();

            if (reporte != null) 
            {
                reporte.UltimaActualizacion = horaActualizacion;
                reporte.EstadoIncidencia = idEstado;
                _bitacoraContext.ReportesEstructura.Update(reporte);
            }

            await _bitacoraContext.SaveChangesAsync();
        }

        public async Task AgregaBitacoraInstalacionAsync(int idReporte, int idEstado, int idUsuario, string descripcion)
        {
            var horaActualizacion = DateTime.UtcNow;

            var bitacora = new BitacoraSeguimientoIncidenciaPunto()
            {
                IdReportePunto = idReporte,
                IdUsuario = idUsuario,
                IdEstadoIncidencia = idEstado,
                Descripcion = descripcion,
                UltimaActualizacion = horaActualizacion
            };

            _bitacoraContext.BitacorasIncidenciaInstalacion.Add(bitacora);

            var reporte = await _bitacoraContext.ReportesInstalacion.Where(x => x.IdReportePunto == idReporte).SingleOrDefaultAsync();

            if (reporte != null)
            {
                reporte.UltimaActualizacion = horaActualizacion;
                reporte.EstadoIncidencia = idEstado;
                _bitacoraContext.ReportesInstalacion.Update(reporte);
            }

            await _bitacoraContext.SaveChangesAsync();
        }
    }
}
