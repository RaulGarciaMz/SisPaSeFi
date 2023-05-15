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

        public async Task AgregaIncidenciaTransaccionalAsync(RegistrarIncidenciaDto i, int idUsuario)
        {
            int idReporte = -1;
            var fechaPatrullaje = DateTime.Parse(i.FechaPatrullaje);
            var programa = await ObtenerProgramaAndRegionPorRutaAndFechaAsync(i.IdRuta, fechaPatrullaje);

            if (programa != null)
            {
                var idTarjeta = await ObtenerIdTarjetaInformativaPorProgramaAsync(programa.id_programa);


                using (var transaction = _regIncidenciaContext.Database.BeginTransaction())
                {
                    try
                    {
                        switch (i.TipoIncidencia)
                        {
                            case "ESTRUCTURA":
                                if (i.IdActivo == 999999)
                                {
                                    AgregaReporteEstructurasEnMemoria(idTarjeta, i.IdActivo, i.DescripcionIncidencia, i.IdPrioridad, i.IdClasificacion);
                                }
                                else
                                {
                                    var rptes = await ObtenerReporteEstructuraPorEstructuraAndClasificacionAsync(i.IdActivo, i.IdClasificacion);
                                    if (rptes.Count > 0)
                                    {
                                        await ActualizaReporteEstructurasEnMemoriaAsync(i.IdActivo, i.IdClasificacion, i.IdPrioridad, i.DescripcionIncidencia);
                                    }
                                    else
                                    {
                                        AgregaReporteEstructurasEnMemoria(idTarjeta, i.IdActivo, i.DescripcionIncidencia, i.IdPrioridad, i.IdClasificacion);
                                    }
                                }

                                await _regIncidenciaContext.SaveChangesAsync();

                                var nvosRptesE = await ObtenerReporteEstructuraPorEstructuraAndClasificacionAsync(i.IdActivo, i.IdClasificacion);
                                idReporte = nvosRptesE[0].IdReporte;

                                break;

                            default: // Reportar por instalación
                                var ri = await ObtenerReporteInstalacionPorPuntoAndClasificacionAsync(i.IdActivo, i.IdClasificacion);
                                if (ri != null && ri.Count > 0)
                                {
                                    await ActualizaReporteInstalacionEnMemoriaAsync(i.IdActivo, i.IdClasificacion, i.IdPrioridad, i.DescripcionIncidencia);
                                }
                                else
                                {
                                    AgregaReporteInstalacionEnMemoria(idTarjeta, i.IdActivo, i.DescripcionIncidencia, i.IdPrioridad, i.IdClasificacion);
                                }

                                await _regIncidenciaContext.SaveChangesAsync();

                                var nvosRptesI = await ObtenerReporteInstalacionPorPuntoAndClasificacionAsync(i.IdActivo, i.IdClasificacion);
                                idReporte = nvosRptesI[0].IdReportePunto;

                                break;
                        }

                        if (idReporte > -1)
                        {
                            AgregaListaDeEvidenciasEnMemoriaConCopiaDeArchivoAsync(idReporte, programa.regionSSF, i.TipoIncidencia, i.listaEvidencia);
                            AgregaListaDeAfectacionesIncidenciaEnMemoriaAsync(idReporte, i.TipoIncidencia, i.listaAfectaciones);
                        }

                        transaction.Commit();
                    }
                    catch (Exception)
                    {
                        transaction.Rollback();
                        throw;
                    }
                }
            }
        }

        private async Task<ProgramaRegionVista?> ObtenerProgramaAndRegionPorRutaAndFechaAsync(int idRuta, DateTime fecha)
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

            return await _regIncidenciaContext.ProgramasRegionesVista.FromSqlRaw(sqlQuery, parametros).FirstOrDefaultAsync();
        }

        private async Task<int> ObtenerIdTarjetaInformativaPorProgramaAsync(int idPrograma)
        {
            var t = await _regIncidenciaContext.TarjetasInformativas.Where(x => x.IdPrograma == idPrograma).FirstAsync();
            return t.IdNota;
        }

        private async Task<List<ReportePunto>> ObtenerReporteInstalacionPorPuntoAndClasificacionAsync(int idPunto, int idClasificacion)
        {
            var edo = await _regIncidenciaContext.EstadosIncidencia.Where(x => x.DescripcionEstado == "concluido").FirstAsync();

            return await _regIncidenciaContext.ReportesInstalacion.Where(x => x.IdPunto == idPunto && x.IdClasificacionIncidencia == idClasificacion && x.EstadoIncidencia == edo.IdEstadoIncidencia).ToListAsync();
        }

        private async Task<List<ReporteEstructura>> ObtenerReporteEstructuraPorEstructuraAndClasificacionAsync(int idEstructura, int idClasificacion)
        {
            var edo = await _regIncidenciaContext.EstadosIncidencia.Where(x => x.DescripcionEstado == "concluido").FirstAsync();

            return await _regIncidenciaContext.ReportesEstructuras.Where(x => x.IdEstructura == idEstructura && x.IdClasificacionIncidencia == idClasificacion && x.EstadoIncidencia == edo.IdEstadoIncidencia).ToListAsync();
        }

        private void AgregaReporteEstructurasEnMemoria(int idNota, int idEstructura, string descripcion, int prioridad, int idClasificacion)
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

        private async Task ActualizaReporteEstructurasEnMemoriaAsync(int idEstructura, int idClasificacion, int prioridad, string descripcion)
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

        private async Task ActualizaReporteInstalacionEnMemoriaAsync(int idActivo, int idClasificacion, int prioridad, string descripcion)
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

        private void AgregaReporteInstalacionEnMemoria(int idNota, int idActivo, string descripcion, int prioridad, int idClasificacion)
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

        private async void AgregaListaDeAfectacionesIncidenciaEnMemoriaAsync(int idReporte, string tipoIncidencia, List<AfectacionIncidenciaMovilDto> afectaciones)
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

        private void AgregaListaDeEvidenciasEnMemoriaConCopiaDeArchivoAsync(int idReporte,string region, string tipoIncidencia, List<EvidenciaIncidenciaMovilDto> evidencias)
        {
            var rom = ConvierteCadenaNumericaARomano(region);
            var now = DateTime.Now;
            var cadWebPath = rom + "/" + now.Year.ToString() + "/" + now.Month.ToString("D2") + "/" + now.Day.ToString("D2") + "/";
            var cadPath = cadWebPath.Replace("/", "\\");
            var wepageUbicacion = "~/Principal/ImagenesCargadas/Incidencias/" + cadWebPath;
            var filesystemUbicacion = "C:\\inetpub\\wwwroot\\SSF\\Principal\\ImagenesCargadas\\Incidencias\\" + cadPath;

            foreach (var item in evidencias)
            {
                var nombreArchivo = Path.GetFileName(item.strNombreArchivo);

                switch (tipoIncidencia)
                {
                    case "ESTRUCTURA":
                        var eie = new EvidenciaIncidencia()
                        {
                            IdReporte = idReporte,
                            RutaArchivo = wepageUbicacion,
                            NombreArchivo = nombreArchivo,
                            Descripcion = item.strDescripcion,
                        };
                        _regIncidenciaContext.EvidenciasIncidencia.Add(eie);
                        break;
                    default:
                        var eii = new EvidenciaIncidenciaPunto()
                        {
                            IdReportePunto = idReporte,
                            RutaArchivo = wepageUbicacion,
                            NombreArchivo = nombreArchivo,
                            Descripcion = item.strDescripcion,
                        };
                        _regIncidenciaContext.EvidenciasIncidenciaInstalacion.Add(eii);
                        break;
                }

                var pthOrigen = "c:\\inetpub\\ftproot\\Seguridad\\" + nombreArchivo;
                var pthDestino = filesystemUbicacion + nombreArchivo;
                if (!Directory.Exists(filesystemUbicacion))
                {
                    Directory.CreateDirectory(filesystemUbicacion);
                }
                File.Copy(pthOrigen, pthDestino);
            }
        }

        private string ConvierteCadenaNumericaARomano(string num)
        {

            var toRoman = new Dictionary<string, string>() {
                { "1","I"},
                { "2","II"},
                { "3","III"},
                { "4","IV"},
                { "5","V"},
                { "6","VI"},
                { "7","VII"},
                { "8","VIII"},
                { "9","IX"},
                { "10","X"},
                { "11","XI"},
                { "12","XII"},
                { "13","XIII"},
                { "14","XIV"},
                { "15","XV"},
                { "16","XVI"},
                { "17","XVII"},
                { "18","XVIII"},
                { "19","XIX"},
                { "20","XX"},
            };

            return toRoman[num];
        }
    }
}
