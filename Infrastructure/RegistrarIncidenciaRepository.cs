using Domain.DTOs;
using Domain.Entities;
using Domain.Entities.Vistas;
using Domain.Ports.Driven.Repositories;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using SqlServerAdapter.Data;

namespace SqlServerAdapter
{
    public class RegistrarIncidenciaRepository : IRegistroIncidenteRepo
    {
        protected readonly RegistrarIncidenciaContext _regIncidenciaContext;

        public RegistrarIncidenciaRepository(RegistrarIncidenciaContext regIncidenciaContext)
        {
            _regIncidenciaContext = regIncidenciaContext ?? throw new ArgumentNullException(nameof(regIncidenciaContext));
        }

        public async Task<ProgramaRegionVista> ObtenerProgramaAndRegionAsync(int idRuta, DateTime fecha)
        {
            string sqlQuery = @"SELECT a.id_programa, a.riesgopatrullaje, b.regionSSF
                                FROM ssf.programapatrullajes a
                                JOIN ssf.rutas b ON a.id_ruta = b.id_ruta
                                WHERE a.id_ruta = @pIdRuta
                                AND a.fechapatrullaje = @pFecha
                                AND a.id_estadopatrullaje < (SELECT id_estadopatrullaje FROM ssf.estadopatrullaje WHERE descripcionestadopatrullaje = 'Concluido')";

            object[] parametros = new object[]
            {
                new SqlParameter("@pIdRuta", idRuta),
                new SqlParameter("@pFecha", fecha)
             };

            return await _regIncidenciaContext.ProgramasRegionesVista.FromSqlRaw(sqlQuery, parametros).FirstAsync();
        }

        public async Task<int> ObtenerIdTarjetaInformativaPorProgramaAsync(int idPrograma)
        {
            var t = await _regIncidenciaContext.TarjetasInformativas.Where(x => x.IdPrograma == idPrograma).FirstAsync();
            return t.IdNota;
        }

        public async Task<int> ObtenerNumeroDeReportesEstructurasConcluidosPorIdAndClasificacionAsync(int idEstructura, int idClasificacion)
        {
            string sqlQuery = @"SELECT * FROM reporteestructuras
                                WHERE id_estructura = @pIdEstructura
                                AND id_clasificacionincidencia = @pIdClasificacion
                                AND estadoincidencia<(SELECT id_estadoincidencia FROM estadosincidencias WHERE descripcionestado = 'concluido')";

            object[] parametros = new object[]
            {
                new SqlParameter("@pIdEstructura", idEstructura),
                new SqlParameter("@pIdClasificacion", idClasificacion)
             };

            return await _regIncidenciaContext.ReportesEstructuras.FromSqlRaw(sqlQuery, parametros).CountAsync();
        }

        public async Task<ReportePunto> ObtenerReporteInstalacionPorPuntoAndClasificacionAsync(int idPunto, int idClasificacion)
        {
            var edo = await _regIncidenciaContext.EstadosIncidencia.Where(x => x.DescripcionEstado == "concluido").FirstAsync();

            return await _regIncidenciaContext.ReportesInstalacion.Where(x => x.IdPunto == idPunto && x.IdClasificacionIncidencia == idClasificacion && x.EstadoIncidencia == edo.IdEstadoIncidencia).SingleAsync();
        }

        public void AgregaReporteEstructurasEnMemoriaAsync(int idNota, int idEstructura, string descripcion, int prioridad, int idClasificacion)
        {
            var r = new ReporteEstructura()
            {
                IdNota = idNota,
            IdEstructura = idEstructura,
            Incidencia= descripcion,
            EstadoIncidencia= 5,
            PrioridadIncidencia = prioridad,
            UltimaActualizacion = DateTime.UtcNow,
            IdClasificacionIncidencia = idClasificacion
            };

            _regIncidenciaContext.ReportesEstructuras.Add(r);
        }

        public async void ActualizaReporteEstructurasEnMemoriaAsync(int idEstructura, int idClasificacion, int prioridad, string descripcion)
        {
            var edoIncidencia = await _regIncidenciaContext.EstadosIncidencia.Where(x => x.DescripcionEstado == "concluido").SingleAsync();

            var r = await _regIncidenciaContext.ReportesEstructuras.
                Where(x => x.IdEstructura == idEstructura &&
                           x.IdClasificacionIncidencia == idClasificacion &&
                           x.EstadoIncidencia < edoIncidencia.IdEstadoIncidencia).SingleAsync();

            var txIncidencia = 
            r.UltimaActualizacion = DateTime.UtcNow;
            r.PrioridadIncidencia = prioridad;
            r.Incidencia = r.Incidencia + " / " + descripcion;

            _regIncidenciaContext.ReportesEstructuras.Update(r);
        }

        public async void ActualizaReporteInstalacionEnMemoriaAsync(int idActivo, int idClasificacion, int prioridad, string descripcion)
        {
            var edoIncidencia = await _regIncidenciaContext.EstadosIncidencia.Where(x => x.DescripcionEstado == "concluido").SingleAsync();

            var r = await _regIncidenciaContext.ReportesInstalacion.
                Where(x => x.IdPunto == idActivo &&
                           x.IdClasificacionIncidencia == idClasificacion &&
                           x.EstadoIncidencia < edoIncidencia.IdEstadoIncidencia).SingleAsync();

            var txIncidencia =
            r.UltimaActualizacion = DateTime.UtcNow;
            r.PrioridadIncidencia = prioridad;
            r.Incidencia = r.Incidencia + " / " + descripcion;

            _regIncidenciaContext.ReportesInstalacion.Update(r);
        }

        public void AgregaReporteInstalacionEnMemoriaAsync(int idNota, int idActivo, string descripcion, int prioridad, int idClasificacion)
        {
            var r = new ReportePunto()
            {
                IdNota = idNota,
                IdPunto = idActivo,
                Incidencia = descripcion,
                EstadoIncidencia = 5,
                PrioridadIncidencia = prioridad,
                UltimaActualizacion = DateTime.UtcNow,
                IdClasificacionIncidencia = idClasificacion
            };

            _regIncidenciaContext.ReportesInstalacion.Add(r);
        }

        public void AgregaEvidenciaIncidenciaEnMemoriaAsync(int idReporte, string ruta, string nombreArchivo, string descripcion)
        {
            var r = new EvidenciaIncidencia()
            {
                IdReporte = idReporte,
                RutaArchivo = ruta,
                NombreArchivo = nombreArchivo,
                Descripcion = descripcion
            };

            _regIncidenciaContext.EvidenciasIncidencia.Add(r);
        }

        public async void AgregaListaDeAfectacionesIncidenciaEnMemoriaAsync(int idReporte, string tipoIncidencia, List<AfectacionIncidenciaMovilDto> afectaciones)
        {
            var lstAfectaciones = new List<AfectacionIncidencia>();

            var tipo = await _regIncidenciaContext.TiposReporte.Where(x => x.Descripcion == tipoIncidencia).SingleAsync();

            foreach (var item in afectaciones)
            {
                var r = new AfectacionIncidencia()
                {
                    IdIncidencia = idReporte,
                    IdConceptoAfectacion = item.intIdAfectacion,
                    Cantidad = item.intTotalAfectaciones,
                    PrecioUnitario = 0,
                    TipoIncidencia = tipo.Idtiporeporte
                };

                lstAfectaciones.Add(r);
            }
   
            _regIncidenciaContext.AfectacionesIncidencia.AddRange(lstAfectaciones);
        }

        public async Task SaveTransactionAsync()
        {
            await _regIncidenciaContext.SaveChangesAsync();
        }
    }
}
