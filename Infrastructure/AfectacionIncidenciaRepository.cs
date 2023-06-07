using Domain.Entities;
using Domain.Entities.Vistas;
using Domain.Ports.Driven.Repositories;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using SqlServerAdapter.Data;

namespace SqlServerAdapter
{
    public class AfectacionIncidenciaRepository : IAfectacionesRepo
    {
        protected readonly AfectacionIncidenciasContext _afectacionContext;

        public AfectacionIncidenciaRepository(AfectacionIncidenciasContext afectacionContext)
        {
            _afectacionContext = afectacionContext ?? throw new ArgumentNullException(nameof(afectacionContext));
        }

        public async Task<List<AfectacionIncidenciaVista>> ObtenerAfectacionIncidenciaPorOpcionAsync(int idReporte, string tipo)
        {
            string sqlQuery = @"SELECT a.id_afectacionincidencia, a.id_incidencia, a.id_conceptoafectacion, a.cantidad, 
                                       a.preciounitario, a.tipo_incidencia, b.descripcion, b.unidades
                                FROM ssf.afectacionincidencia a
                                JOIN ssf.conceptosafectacion b ON a.id_conceptoafectacion=b.id_conceptoafectacion
                                JOIN ssf.tiporeporte c ON a.tipo_incidencia = c.idtiporeporte
                                WHERE c.descripcion= @pTipo
                                AND a.id_incidencia= @pIdReporte";

            object[] parametros = new object[]
            {
                new SqlParameter("@pIdReporte", idReporte),
                new SqlParameter("@pTipo", tipo)
            };

                return await _afectacionContext.AfectacionesIncidenciasVista.FromSqlRaw(sqlQuery, parametros).ToListAsync();

        }

        public async Task AgregaAsync(int idIncidencia, int idConcepto, int cantidad, float precio, string tipo)
        {
            var t = await _afectacionContext.TiposReporte.Where(x => x.Descripcion == tipo).SingleOrDefaultAsync();

            if (t != null)
            {
                var afect = new AfectacionIncidencia()
                {
                    IdIncidencia = idIncidencia,
                    IdConceptoAfectacion = idConcepto,
                    Cantidad = cantidad,
                    PrecioUnitario = precio,
                    TipoIncidencia = t.Idtiporeporte
                };

                _afectacionContext.AfectacionesIncidencias.Add(afect);
                await _afectacionContext.SaveChangesAsync();
            }
        }

        public async Task ActualizaAsync(int idIncidencia, int cantidad, float precio)
        {
            var afect = await _afectacionContext.AfectacionesIncidencias.Where(x => x.IdAfectacionIncidencia == idIncidencia).SingleOrDefaultAsync();

            if (afect != null)
            { 
                afect.Cantidad = cantidad;
                afect.PrecioUnitario = precio;

                _afectacionContext.AfectacionesIncidencias.Update(afect);
                await _afectacionContext.SaveChangesAsync();
            } 
        }

        public async Task<List<AfectacionIncidencia>> ObtenerAfectacionPorIncidenciaAndTipoAndConceptoAsync(int idIncidencia, int idConcepto, string tipo)
        {
            string sqlQuery = @"SELECT a.*
                                FROM ssf.afectacionincidencia a
                                JOIN ssf.conceptosafectacion b ON a.id_conceptoafectacion=b.id_conceptoafectacion
                                JOIN ssf.tiporeporte c ON a.tipo_incidencia = c.idtiporeporte
                                WHERE c.descripcion= @pTipo
                                AND a.id_incidencia= @pIdIncidencia
                                AND a.id_conceptoAfectacion = @pConcepto";

            object[] parametros = new object[]
            {
                new SqlParameter("@pIdIncidencia", idIncidencia),
                new SqlParameter("@pTipo", tipo),
                new SqlParameter("@pConcepto", idConcepto)
            };

             return await _afectacionContext.AfectacionesIncidencias.FromSqlRaw(sqlQuery, parametros).ToListAsync();
        }



    }
}
