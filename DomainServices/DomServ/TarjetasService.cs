using Domain.DTOs;
using Domain.Entities;
using Domain.Entities.Vistas;
using Domain.Enums;
using Domain.Ports.Driven;
using Domain.Ports.Driven.Repositories;
using Domain.Ports.Driving;

namespace DomainServices.DomServ
{
    public class TarjetasService : ITarjetaService
    {
        private readonly ITarjetaInformativaRepo _repo;
        private readonly IUsuariosParaValidacionQuery _user;

        public TarjetasService(ITarjetaInformativaRepo repo, IUsuariosParaValidacionQuery u)
        {
            _repo = repo;
            _user = u;
        }

        public async Task AgregaTarjetaTransaccionalAsync(TarjetaDtoForCreate tarjeta) {

            var user = await _user.ObtenerUsuarioConfiguradorPorNombreAsync(tarjeta.strIdUsuario);

            if (user != null)
            {
                if (await ExisteTarjetaParaElProgramaAsync(tarjeta.intIdPrograma)) 
                {
                    return;
                }

                var t = ConvierteTarjetaDtoForCreateToDomain(tarjeta, user.IdUsuario);

                await _repo.AgregaTransaccionalAsync(t, tarjeta.intIdEstadoPatrullaje, user.IdUsuario);
            }
        }

        public async Task UpdateAsync(TarjetaDto tarjeta, string usuario) 
        {
            var user = await _user.ObtenerUsuarioConfiguradorPorNombreAsync(usuario);

            if (user != null)
            {
                var t = await _repo.ObtenerTarjetaPorIdNotaAsync(tarjeta.intIdNota);

                if (t == null)
                {
                    return;
                }

                t.UltimaActualizacion = DateTime.UtcNow;
                t.Inicio = TimeSpan.Parse(tarjeta.strInicio); // ":00" al final  representa segundos al final ??
                t.Termino = TimeSpan.Parse(tarjeta.strTermino); // ":00" al final
                t.TiempoVuelo = TimeSpan.Parse(tarjeta.strTiempoVuelo);
                t.CalzoAcalzo = TimeSpan.Parse(tarjeta.strCalzoCalzo);
                t.Observaciones = tarjeta.strObservaciones;
                t.ComandantesInstalacionSsf = tarjeta.intComandantesInstalacionSSF;
                t.PersonalMilitarSedenaoficial = tarjeta.intPersonalMilitarSEDENAOficial;
                t.KmRecorrido = tarjeta.intKmRecorrido;
                //TODO revisar fecha de patrullaje (format)
                //t.FechaPatrullaje = tarjeta.FechaPatrullaje; // En qué formato viene??
                t.PersonalMilitarSedenatropa = tarjeta.intPersonalMilitarSEDENATropa;
                t.Linieros = tarjeta.intLinieros;
                t.ComandantesTurnoSsf = tarjeta.intComandantesTurnoSSF;
                t.OficialesSsf = tarjeta.intOficialesSSF;
                t.PersonalNavalSemaroficial = tarjeta.intPersonalNavalSEMAROficial;
                t.PersonalNavalSemartropa = tarjeta.intPersonalNavalSEMARTropa;

                await _repo.UpdateTarjetaAndProgramaTransaccionalAsync(t, tarjeta.intIdEstadoPatrullaje, user.IdUsuario, tarjeta.intIdInstalacionResponsable);
            }    
        }

        public async Task<List<TarjetaDto>> ObtenerPorOpcionAsync(int opcion, string tipo, string region, int anio, int mes, int dia, string usuario)
        {
            var regreso = new List<TarjetaDto>();
            var user = await _user.ObtenerUsuarioPorUsuarioNomAsync(usuario);

            var laOpcion = (TarjetaInformativaOpcion)opcion;

            if (user != null)
            {
                var tarjetas = new List<TarjetaInformativaVista>();
                switch (laOpcion)
                {
                    case TarjetaInformativaOpcion.TarjetaPorRegion:
                        tarjetas = await _repo.ObtenerTarjetasPorRegionAsync(tipo, region, anio, mes);
                        break;
                    case TarjetaInformativaOpcion.ParteNovedades:
                        tarjetas = await _repo.ObtenerParteNovedadesPorDiaAsync(tipo, anio, mes, dia);
                        break;
                    case TarjetaInformativaOpcion.Monitoreo:
                        tarjetas = await _repo.ObtenerMonitoreoAsync(tipo, user.IdUsuario, anio, mes, dia);
                        break;
                }

                foreach (var t in tarjetas)
                {
                    var tiv = ConvierteTarjetaVistaDomainToDto(t);
                    regreso.Add(tiv);
                }
            }

            return regreso;
        }

        public async Task<TarjetaDto> ObtenerPorIdAndOpcionAsync(int id, string usuario, string opcion)
        {
            var regreso = new TarjetaDto();
            var ti = new TarjetaInformativaIdVista();

            var userId = await _user.ObtenerIdUsuarioPorUsuarioNomAsync(usuario);
            if (userId != null)
            {
                switch (opcion) 
                {
                    case "TARJETA":
                        ti = await _repo.ObtenerTarjetaCompletaPorIdAsync(id);
                        break;
                    case "PROGRAMA":
                        ti = await _repo.ObtenerTarjetaCompletaPorIdProgramaAsync(id);
                        break;
                }

                if (ti != null)
                {
                    regreso = ConvierteTarjetaIdVistaDomainToDto(ti);
                }
            }

            return regreso;
        }

        private async Task<bool> ExisteTarjetaParaElProgramaAsync(int idPrograma)
        {
            if (await _repo.NumeroDeTarjetasPorProgamaAsync(idPrograma) > 0)
            {
                return true;
            }

            return false;
        }

        private TarjetaDto ConvierteTarjetaIdVistaDomainToDto(TarjetaInformativaIdVista t) 
        { 
            var r = new TarjetaDto() 
            { 
                intIdNota= t.id_nota,
                intIdPrograma= t.id_programa,
                strFechaPatrullaje = t.fechapatrullaje.Value.ToString("yyyy-MM-dd"),
                intIdRuta = t.id_ruta,
                intRegion = Int32.Parse(t.regionssf),
                intIdTipoPatullaje = t.id_tipopatrullaje,
                strUltimaActualizacion = t.ultimaactualizacion.ToString("yyyy-MM-dd HH:mm:ss"), 
                strIdUsuario = t.id_usuario.ToString(),
                strInicio = t.inicio.ToString(),
                strTermino = t.termino.ToString(),
                strTiempoVuelo = t.tiempovuelo.ToString(),
                strCalzoCalzo = t.calzoacalzo.ToString(),
                strObservaciones= t.observaciones,
                intComandantesInstalacionSSF = t.comandantesinstalacionssf,
                intPersonalMilitarSEDENAOficial = t.personalmilitarsedenaoficial,
                intKmRecorrido = t.kmrecorrido,
                intIdEstadoTarjetaInformativa = t.id_estadotarjetainformativa,
                intPersonalMilitarSEDENATropa = t.personalmilitarsedenatropa,
                intLinieros= t.linieros,
                intComandantesTurnoSSF = t.comandantesturnossf,
                intOficialesSSF = t.oficialesssf,
                intPersonalNavalSEMAROficial = t.personalnavalsemaroficial,
                intPersonalNavalSEMARTropa = t.personalnavalsemartropa,
                intIdEstadoPatrullaje = t.id_estadopatrullaje,
                strDescripcionEstadoPatrullaje = t.descripcionestadopatrullaje,
                strItinerarios = t.itinerario,
                strReportes = t.incidenciaenestructura + t.incidenciaeninstalacion,
                strMatriculas = t.matriculas,
                strOdometros = t.odometros,
                strKmVehiculos = t.kmrecorridos,
                strFechaTermino = t.fechaTermino.ToString("yyyy-MM-dd"),
                intIdResultadoPatrullaje = t.idresultadopatrullaje,
                strResultadoPatrullaje = t.resultadopatrullaje,
                strLineaEstructuraInstalacion = t.lineaestructurainstalacion,
                strResponsableVuelo = t.responsablevuelo,
                intFuerzaDeReaccion = t.fuerzareaccion,
                intIdInstalacionResponsable = t.id_puntoresponsable,
                strInstalacion = t.instalacion,
                strComandanteRegional = t.nombre + " " + t.apellido1 + " " + t.apellido2,
                strEstado = t.estado,
                strMunicipio = t.municipio
            };

            return r;
        }

        private TarjetaDto ConvierteTarjetaVistaDomainToDto(TarjetaInformativaVista t)
        {
            var r = new TarjetaDto()
            {
                intIdNota = t.id_nota,
                intIdPrograma = t.id_programa,
                strFechaPatrullaje = t.fechapatrullaje.Value.ToString("yyyy-MM-dd"),
                intIdRuta = t.id_ruta,
                intRegion = Int32.Parse(t.regionssf),
                intIdTipoPatullaje = t.id_tipopatrullaje,
                strUltimaActualizacion = t.ultimaactualizacion.ToString("yyyy-MM-dd HH:mm:ss"),
                strIdUsuario = t.id_usuario.ToString(),
                strInicio = t.inicio.ToString(),
                strTermino = t.termino.ToString(),
                strTiempoVuelo = t.tiempovuelo.ToString(),
                strCalzoCalzo = t.calzoacalzo.ToString(),
                strObservaciones = t.observaciones,
                intComandantesInstalacionSSF = t.comandantesinstalacionssf,
                intPersonalMilitarSEDENAOficial = t.personalmilitarsedenaoficial,
                intKmRecorrido = t.kmrecorrido,
                intIdEstadoTarjetaInformativa = t.id_estadotarjetainformativa,
                intPersonalMilitarSEDENATropa = t.personalmilitarsedenatropa,
                intLinieros = t.linieros,
                intComandantesTurnoSSF = t.comandantesturnossf,
                intOficialesSSF = t.oficialesssf,
                intPersonalNavalSEMAROficial = t.personalnavalsemaroficial,
                intPersonalNavalSEMARTropa = t.personalnavalsemartropa,
                intIdEstadoPatrullaje = t.id_estadopatrullaje,
                strDescripcionEstadoPatrullaje = t.descripcionestadopatrullaje,
                strItinerarios = t.itinerario,
                strReportes = t.incidenciaenestructura + t.incidenciaeninstalacion,
                strMatriculas = t.matriculas,
                strOdometros = t.odometros,
                strKmVehiculos = t.kmrecorridos,
                strFechaTermino = t.fechaTermino.ToString("yyyy-MM-dd"),
                intIdResultadoPatrullaje = t.idresultadopatrullaje,
                strResultadoPatrullaje = t.resultadopatrullaje,
                strLineaEstructuraInstalacion = t.lineaestructurainstalacion,
                strResponsableVuelo = t.responsablevuelo,
                intFuerzaDeReaccion = t.fuerzareaccion,
                intIdInstalacionResponsable = t.id_puntoresponsable
            };

            return r;
        }

        private TarjetaInformativa ConvierteTarjetaDtoForCreateToDomain(TarjetaDtoForCreate t, int idUsuario)
        {
            var r = new TarjetaInformativa() 
            { 
                IdPrograma= t.intIdPrograma,
                IdUsuario = idUsuario,
                UltimaActualizacion = DateTime.UtcNow,
                Inicio = ConvierteToTimeSpanSinSegundo(t.strInicio), 
                Termino = ConvierteToTimeSpanSinSegundo(t.strTermino),
                TiempoVuelo = TimeSpan.Parse(t.strTiempoVuelo),
                CalzoAcalzo = TimeSpan.Parse(t.strCalzoCalzo),
                Observaciones= t.strObservaciones,
                ComandantesInstalacionSsf = t.intComandantesInstalacionSSF,
                PersonalMilitarSedenaoficial = t.intPersonalMilitarSEDENAOficial,
                KmRecorrido= t.intKmRecorrido,
                FechaPatrullaje = DateTime.Parse(t.strFechaPatrullaje),
                PersonalMilitarSedenatropa = t.intPersonalMilitarSEDENATropa,
                Linieros = t.intLinieros,
                ComandantesTurnoSsf = t.intComandantesTurnoSSF,
                OficialesSsf = t.intOficialesSSF,
                PersonalNavalSemaroficial = t.intPersonalNavalSEMAROficial,
                PersonalNavalSemartropa = t.intPersonalNavalSEMARTropa,
                FechaTermino = DateTime.Parse(t.strFechaTermino),
                Idresultadopatrullaje = t.intIdResultadoPatrullaje,
                Lineaestructurainstalacion = t.strLineaEstructuraInstalacion,
                //Campos no nulos
                IdEstadoTarjetaInformativa = 1,
                Responsablevuelo="",
                Fuerzareaccion=0
                
            };

            return r;
        }

        private TimeSpan ConvierteToTimeSpanSinSegundo(string valor)
        {
            var t = TimeSpan.Parse(valor);

            return new TimeSpan(t.Hours, t.Minutes, 0);
        }
    }
}
