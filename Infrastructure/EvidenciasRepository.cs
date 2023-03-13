using Domain.Entities;
using Domain.Entities.Vistas;
using Domain.Ports.Driven.Repositories;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using SqlServerAdapter.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
                Descripcion = descripcion
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
                Descripcion = descripcion
            };

            _evidenciaContext.EvidenciasInstalacion.Add(evidencia);

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
    }
}







