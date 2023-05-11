using Domain.DTOs;
using Domain.Entities;
using Domain.Entities.Vistas;
using Domain.Ports.Driven;
using Domain.Ports.Driven.Repositories;
using Domain.Ports.Driving;

namespace DomainServices.DomServ
{
    public class MonitoreoMovilService : IMonitoreoService
    {
        private readonly IMonitoreoMovilRepo _repo;
        private readonly IUsuariosParaValidacionQuery _user;

        public MonitoreoMovilService(IMonitoreoMovilRepo repo, IUsuariosParaValidacionQuery u)
        {
            _repo = repo;
            _user = u;
        }

        public async Task<MonitoreoMovilDto> ObtenerMonitoreoMovil(string usuario)
        {
            var monitoreo = new MonitoreoMovilDto();
            var user = await _user.ObtenerUsuarioConfiguradorPorNombreAsync(usuario);

            if (user != null)
            {
                var terrPre = await _repo.ObtenerProgramasEnEstadoPreConcluidoPorTipoAsync("TERRESTRE", user.IdUsuario);
                var iniciados = await LlenaInformacionTerrestreAsync(terrPre);
                monitoreo.listaMMPatrullajesTerrestresIniciados = iniciados;

                var terrConc = await _repo.ObtenerProgramasConcluidosPorTipoAsync("TERRESTRE", user.IdUsuario);
                var terminados = await LlenaInformacionTerrestreAsync(terrConc);
                monitoreo.listaMMPatrullajesTerrestresTerminados = terminados;

                var terrPost = await _repo.ObtenerProgramasEnEstadoPostConcluidoPorTipoAsync("TERRESTRE", user.IdUsuario);
                var concluidos = await LlenaInformacionTerrestreAsync(terrPost);
                monitoreo.listaMMPatrullajesTerrestresCancelados = concluidos;

                var aerPre = await _repo.ObtenerProgramasEnEstadoPreConcluidoPorTipoAsync("AEREO", user.IdUsuario);
                var iniciadosA = await LlenaInformacionAereo(aerPre);
                monitoreo.listaMMPatrullajesAereosIniciados = iniciadosA;

                var aerConc = await _repo.ObtenerProgramasConcluidosPorTipoAsync("AEREO", user.IdUsuario);
                var terminadosA = await LlenaInformacionAereo(aerConc);
                monitoreo.listaMMPatrullajesAereosTerminados = terminadosA;

                var aerPost = await _repo.ObtenerProgramasEnEstadoPostConcluidoPorTipoAsync("AEREO", user.IdUsuario);
                var concluidosA = await LlenaInformacionAereo(aerPost);
                monitoreo.listaMMPatrullajesAereosCancelados = concluidosA;
            }

            return monitoreo;
        }

        private async Task<List<PatrullajesTerrestresMovilDto>> LlenaInformacionTerrestreAsync(List<MonitoreoVista> terrestres)
        { 
            var pt = new List<PatrullajesTerrestresMovilDto>();
            foreach (var m in terrestres) 
            {
                var patrullaje = new PatrullajesTerrestresMovilDto();

                patrullaje.objMMDatosGenerales = ConvierteMonitoreoMovilToDatosGeneralesDto(m);

                var puntos = await _repo.ObtenerPuntosEnRutaAsync(m.id_ruta);
                if (puntos != null && puntos.Count > 0)
                {
                    var itinerarios = new List<ItinerarioRutaMovilDto>();
                    foreach (var p in puntos)
                    {
                        var pto = ConviertePuntoEnRutaVistaToItinerarioRutaMovilDto(p);
                        itinerarios.Add(pto);
                    }

                    patrullaje.listaMMItinerarioRuta = itinerarios;
                }

                var tarjetas = await _repo.ObtenerTarjetasInformativasPorProgramaAsync(m.id_programa);
                if (tarjetas != null && tarjetas.Count > 0)
                {
                    patrullaje.objMMTarjetaInformativa = ConvierteTarjetaInformativaTerrestreToMovilDto(tarjetas[0]);
                    patrullaje.intKmRecorrido = tarjetas[0].KmRecorrido;

                    var incEstructura = await _repo.ObtenerIncidenciasEnEstructuraPorTarjetaAsync(tarjetas[0].IdNota);
                    if (incEstructura != null && incEstructura.Count > 0)
                    {
                        var repsEstructura = new List<ReporteEstructuraMovilDto>();
                        foreach (var item in incEstructura) 
                        {
                            var rep = ConvierteIncidenciaTarjetaToReporteEstructuraMovilDto(item);
                            repsEstructura.Add(rep);
                        }
                        patrullaje.listaMMReporteEstructura = repsEstructura;
                    }


                    var incInstalacion = await _repo.ObtenerIncidenciasEnInstalacionPorTarjetaAsync(tarjetas[0].IdNota);
                    if (incInstalacion != null && incInstalacion.Count > 0)
                    {
                        var repsInstalacion = new List<ReporteInstalacionMovilDto>();
                        foreach (var item in incInstalacion)
                        {
                            var rep = ConvierteIncidenciaTarjetaToReporteInstalacionMovilDto(item);
                            repsInstalacion.Add(rep);
                        }
                        patrullaje.listaMMReporteInstalacion = repsInstalacion;
                    }


                    var incUsoVehiculo = await _repo.ObtenerUsoVehiculoPorProgramaAsync(tarjetas[0].IdPrograma);
                    if (incUsoVehiculo != null && incUsoVehiculo.Count > 0)
                    {
                        var repsUsoV = new List<VehiculoMovilDto>();
                        foreach (var item in incUsoVehiculo)
                        {
                            var rep = ConvierteUsoVehiculoToVehiculoMovilDto(item);
                            repsUsoV.Add(rep);
                        }
                        patrullaje.listaMMVehiculos = repsUsoV;
                    }
                }

                pt.Add(patrullaje);
            }

            return pt;
        }

        private async Task<List<PatrullajesAereosMovilDto>> LlenaInformacionAereo(List<MonitoreoVista> aereos)
        {
            var pa = new List<PatrullajesAereosMovilDto>();

            foreach (var m in aereos)
            {
                var patrullaje = new PatrullajesAereosMovilDto();
                patrullaje.objMMDatosGenerales = ConvierteMonitoreoMovilToDatosGeneralesDto(m);

                var puntos = await _repo.ObtenerPuntosEnRutaAsync(m.id_ruta);
                if (puntos != null && puntos.Count > 0)
                {
                    var itinerarios = new List<ItinerarioRutaMovilDto>();
                    foreach (var p in puntos)
                    {
                        var pto = ConviertePuntoEnRutaVistaToItinerarioRutaMovilDto(p);
                        itinerarios.Add(pto);
                    }

                    patrullaje.listaMMItinerarioRuta = itinerarios;
                }

                var tarjetas = await _repo.ObtenerTarjetasInformativasPorProgramaAsync(m.id_programa);
                if (tarjetas != null && tarjetas.Count > 0)
                {
                    patrullaje.objMMTarjetaInformativa = ConvierteTarjetaInformativaTerrestreToMovilDto(tarjetas[0]);
                    patrullaje.strTiempoVuelo = tarjetas[0].TiempoVuelo.ToString();
                    patrullaje.strCalzoCalzo = tarjetas[0].CalzoAcalzo.ToString();

                    var incEstructura = await _repo.ObtenerIncidenciasEnEstructuraPorTarjetaAsync(tarjetas[0].IdNota);
                    if (incEstructura != null && incEstructura.Count > 0)
                    {
                        var repsEstructura = new List<ReporteEstructuraMovilDto>();
                        foreach (var item in incEstructura)
                        {
                            var rep = ConvierteIncidenciaTarjetaToReporteEstructuraMovilDto(item);
                            repsEstructura.Add(rep);
                        }
                        patrullaje.listaMMReporteEstructura = repsEstructura;
                    }

                    var incInstalacion = await _repo.ObtenerIncidenciasEnInstalacionPorTarjetaAsync(tarjetas[0].IdNota);
                    if (incInstalacion != null && incInstalacion.Count > 0)
                    {
                        var repsInstalacion = new List<ReporteInstalacionMovilDto>();
                        foreach (var item in incInstalacion)
                        {
                            var rep = ConvierteIncidenciaTarjetaToReporteInstalacionMovilDto(item);
                            repsInstalacion.Add(rep);
                        }
                        patrullaje.listaMMReporteInstalacion = repsInstalacion;
                    }

                    var incUsoVehiculo = await _repo.ObtenerUsoVehiculoPorProgramaAsync(tarjetas[0].IdPrograma);
                    if (incUsoVehiculo != null && incUsoVehiculo.Count > 2)
                    {
                        patrullaje.intIdVehiculo = incUsoVehiculo[0].id_vehiculo;
                        patrullaje.strMatricula = incUsoVehiculo[0].matricula;
                    }
                }

            }

            return pa;
        }
        private DatosGeneralesMovilDto ConvierteMonitoreoMovilToDatosGeneralesDto(MonitoreoVista m)
        {
            var r = new DatosGeneralesMovilDto()
            {
                intIdPrograma = m.id_programa,
                intIdRuta = m.id_ruta,
                strFechaPatrullaje = m.fechapatrullaje.Value.ToString("yyyy-MM-dd"),
                strInicio = m.inicio.ToString(),
                strClave = m.clave,
                intRegionMilitarSDN = Int32.Parse(m.regionmilitarsdn),
                intRegionSSF = Int32.Parse(m.regionssf),
                strObservacionesRuta = m.observaciones,
                strDescripcionEstadoPatrullaje = m.descripcionestadopatrullaje,
                intIdRiesgoPatrullaje = m.riesgopatrullaje,
                strDescripcionRiesgoPatrullaje = m.descripcionnivel,
                intIdPuntoResponsable = m.id_puntoresponsable,
                intIdRutaOriginal = m.id_ruta_original,
                strDescripcionApoyoPatrullaje = m.descripcion

            };

            return r;
        }

        private ItinerarioRutaMovilDto ConviertePuntoEnRutaVistaToItinerarioRutaMovilDto(PuntoEnRutaVista m)
        {
            var r = new ItinerarioRutaMovilDto()
            {
                strUbicacion = m.ubicacion,
                strLatitud = m.latitud,
                strLongitud = m.longitud
            };

            return r;
        }

        private TarjetaInformativaTerrestreMovilDto ConvierteTarjetaInformativaTerrestreToMovilDto(TarjetaInformativa m)
        {
            var r = new TarjetaInformativaTerrestreMovilDto()
            {
                intIdTarjetaInformativa = m.IdNota,
                strUltimaActualizacion = m.UltimaActualizacion.ToString("yyyy-MM-dd HH:mm:ss"),
                intIdUsuario = m.IdUsuario,
                strInicio = m.Inicio.ToString(),
                strTermino = m.Termino.ToString(),
                strObservaciones = m.Observaciones,
                intComandantesInstalacionSSF = m.ComandantesInstalacionSsf,
                intPersonalMilitarOficialSDN = m.PersonalMilitarSedenaoficial,
                intIdEstadoTarjetaInformativa = m.IdEstadoTarjetaInformativa,
                intPersonalMilitarTropaSDN = m.PersonalMilitarSedenatropa,
                intConductoresSSF = m.Linieros,
                intComandantesTurnoSSF = m.ComandantesTurnoSsf,
                intOficialesSSF = m.OficialesSsf,
                intPersonalNavalOficialSEMAR = m.PersonalNavalSemaroficial,
                intPersonalNavalTropaSEMAR = m.PersonalNavalSemartropa
            };

            return r;
        }

 /*       private TarjetaInformativaAereaMovilDto ConvierteTarjetaInformativaAereaToMovilDto(TarjetaInformativa m)
        {
            var r = new TarjetaInformativaAereaMovilDto()
            {
                intIdTarjetaInformativa = m.IdNota,
                strUltimaActualizacion = m.UltimaActualizacion.ToString("yyyy-MM-dd HH:mm:ss"),
                intIdUsuario = m.IdUsuario,
                strInicio = m.Inicio.ToString(),
                strTermino = m.Termino.ToString(),
                strObservaciones = m.Observaciones,
                intComandantesInstalacionSSF = m.ComandantesInstalacionSsf,
                intPersonalMilitarOficialSDN = m.PersonalMilitarSedenaoficial,
                intIdEstadoTarjetaInformativa = m.IdEstadoTarjetaInformativa,
                intPersonalMilitarTropaSDN = m.PersonalMilitarSedenatropa,
                intConductoresSSF = m.Linieros,
                intComandantesTurnoSSF = m.ComandantesTurnoSsf,
                intOficialesSSF = m.OficialesSsf,
                intPersonalNavalOficialSEMAR = m.PersonalNavalSemaroficial,
                intPersonalNavalTropaSEMAR = m.PersonalNavalSemartropa,
                strTiempoVuelo = m.TiempoVuelo.ToString(),
                strCalzoCalzo = m.CalzoAcalzo.ToString()
                
            };

            return r;
        }
*/
        private ReporteEstructuraMovilDto ConvierteIncidenciaTarjetaToReporteEstructuraMovilDto(IncidenciaTarjetaVista m)
        {
            var r = new ReporteEstructuraMovilDto()
            {
                strIncidencia = m.incidencia,
                strNombreEstructura = m.nombre,
                strClaveLinea = m.clave
            };

            return r;
        }

        private ReporteInstalacionMovilDto ConvierteIncidenciaTarjetaToReporteInstalacionMovilDto(IncidenciaTarjetaVista m)
        {
            var r = new ReporteInstalacionMovilDto()
            {
                strIncidencia = m.incidencia,
                strUbicacion = m.nombre
            };

            return r;
        }

        private VehiculoMovilDto ConvierteUsoVehiculoToVehiculoMovilDto(UsoVehiculoMonitoreoVista m)
        {
            var r = new VehiculoMovilDto()
            {
                intIdVehiculo = m.id_vehiculo,
                intKmInicio = m.kminicio,
                intKmFin = m.kmfin,
                intConsumoCombustible = m.consumocombustible,
                strEstadoVehiculo = m.estadovehiculo,
                strNumeroEconomico = m.numeroeconomico,
                strMatricula = m.matricula
            };

            return r;
        }
    }
}
