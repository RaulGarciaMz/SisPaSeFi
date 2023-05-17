using Domain.DTOs;
using Domain.Entities.Vistas;
using Domain.Ports.Driven;
using Domain.Ports.Driven.Repositories;
using Domain.Ports.Driving;

namespace DomainServices.DomServ
{
    public class ReporteServicioMensualService : IReporteServicioMensualService
    {
        private readonly IReporteServicioMensualRepo _repo;
        private readonly IUsuariosParaValidacionQuery _user;

        public ReporteServicioMensualService(IReporteServicioMensualRepo repo, IUsuariosParaValidacionQuery u)
        {
            _repo = repo;
            _user = u;
        }

        public async Task<ReporteServicioMensualDto> ObtenerReporteDeServicioMensualPorOpcionAsync(int anio, int mes, string tipo, string usuario)
        {
            var retorno = new ReporteServicioMensualDto();
            var user = await _user.ObtenerUsuarioConfiguradorPorNombreAsync(usuario);

            if (user != null)
            {
                var reporte = new List<ReporteServicioMensualVista>();
                var resumen = new DetalleReporteServicioMensualVista();

                switch (tipo) 
                {
                    case "AEREO":
                        reporte = await _repo.ObtenerReporteAereoPorOpcionAsync(anio,mes,tipo);
                        resumen = await _repo.ObtenerResumenParaReporteAereoPorOpcionAsync(anio, mes, tipo);
                        break;
                    case "TERRESTRESDN":
                        reporte = await _repo.ObtenerReporteTerrestreSedenaPorAnioAnMesAsync(anio, mes);
                        resumen = await _repo.ObtenerResumenParaReporteTerrestreSedenaPorAnioAnMesAsync(anio, mes);
                        break;
                    case "TERRESTRESSF":
                        reporte = await _repo.ObtenerReporteTerrestreRegionPorAnioAnMesAsync(anio, mes);
                        resumen = await _repo.ObtenerResumenParaReporteTerrestreRegionPorAnioAnMesAsync(anio, mes);
                        break;
                }

                var rpte = new ReporteServicioMensualDto() 
                {
                    intTotalPatrullajes = resumen.total,
                    strTiempoVueloKmRecorrido = resumen.dato1.ToString(),
                    intComandantesInstalacion = resumen.dato2,
                    intPersonalMilitarSEDENAoficial = resumen.dato3,
                    intPersonalMilitarSEDENAtropa = resumen.dato4,
                    intConductores = resumen.dato5,
                    intComandantesTurnoSSF = resumen.dato6,
                    intOficialesSSF = resumen.dato7
                };

                foreach (var rep in reporte) 
                {
                    var r = new DetalleReporteServicioMensualDto() 
                    {
                        intIdTarjeta = rep.id_nota,
                        intIdPrograma = rep.id_programa,
                        strFechaPatrullaje = rep.fechaPatrullaje.Value.ToString("yyyy-MM-dd"),
                        intIdRuta = rep.id_ruta,
                        intRegionSSF = Int32.Parse(rep.regionSSF),
                        intIdTipoPatrullaje = rep.id_tipoPatrullaje,
                        strUltimaActualizacion = rep.ultimaActualizacion.ToString("yyyy-MM-dd HH:mm:ss"),
                        intIdUsuario = rep.id_usuario,
                        strInicio = rep.inicio.ToString(),
                        strTermino = rep.termino.ToString(),
                        strTiempoVuelo = rep.tiempoVuelo.ToString(),
                        strCalzoCalzo = rep.calzoAcalzo.ToString(),
                        strObservaciones = rep.observaciones,
                        strDescripcionEstadoPatrullaje = rep.descripcionEstadoPatrullaje,
                        strKmRecorridos = rep.kmRecorrido.ToString(),
                        intComandantesInstalacionSSF = rep.comandantesInstalacionSSF,
                        intPersonalMilitarSEDENAoficial = rep.personalMilitarSEDENAOficial,
                        intIdEstadoTarjetaInformativa = rep.id_estadoTarjetaInformativa,
                        intPersonalMilitarSEDENAtropa = rep.personalMilitarSEDENATropa,
                        intConductores = rep.linieros,
                        intComandantesTurnoSSF = rep.comandantesTurnoSSF,
                        intOficialesSSF = rep.oficialesSSF,
                        intPersonalNavalSEMARoficial = rep.personalNavalSEMAROficial,
                        intPersonalNavalSEMARtropa = rep.personalNavalSEMARTropa,
                        strItinerarioRuta = rep.itinerariorutas,
                        strIncidenciaEnEstructura = rep.incidenciaenestructura,
                        strIncidenciaEnInstalacion = rep.incidenciaeninstalacion,
                        strMatriculas = rep.matriculas,
                        strOdometros = rep.odometros,
                        strKmRecorridosVehiculos = rep.kmrecorridos
                    };

                    rpte.listaDetalles.Add(r);
                }

                retorno = rpte;
            }

            return retorno;
        }
    }
}
