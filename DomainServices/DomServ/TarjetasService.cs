using Domain.DTOs;
using Domain.Entities;
using Domain.Entities.Vistas;
using Domain.Enums;
using Domain.Ports.Driven.Repositories;
using Domain.Ports.Driving;

namespace DomainServices.DomServ
{
    public class TarjetasService : ITarjetaService
    {
        private readonly ITarjetaInformativaRepo _repo;

        public TarjetasService(ITarjetaInformativaRepo repo)
        {
            _repo = repo;
        }

        public async Task Agrega(TarjetaDto tarjeta, string usuario) {

            var userId = await _repo.ObtenerIdUsuarioConfiguradorAsync(usuario);

            if (EsUsuarioRegistrado(userId))
            {
                if (await ExisteTarjetaParaElPrograma(tarjeta.intIdPrograma)) 
                {
                    return;
                }

                var t = ConvierteTarjetaDtoToDomain(tarjeta);

                await _repo.AgregaAsync(t, tarjeta.intIdEstadoPatrullaje, userId);
            }
        }

        public async Task Update(TarjetaDto tarjeta, string usuario) 
        {
            var userId = await _repo.ObtenerIdUsuarioConfiguradorAsync(usuario);

            if (EsUsuarioRegistrado(userId))
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

                await _repo.UpdateTarjetaAndProgramaAsync(t, tarjeta.intIdEstadoPatrullaje, userId, tarjeta.intIdInstalacionResponsable);
            }    
        }

        public async Task<List<TarjetaDto>> ObtenerPorOpcion(int opcion, string tipo, string region, int anio, int mes, int dia, string usuario)
        {
            var regreso = new List<TarjetaDto>();
            var userId = await _repo.ObtenerIdUsuarioRegistradoAsync(usuario);
            var laOpcion = (TarjetaInformativaOpcion)opcion;

            if (EsUsuarioRegistrado(userId))
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
                        tarjetas = await _repo.ObtenerMonitoreoAsync(tipo, userId, anio, mes, dia);
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

        public async Task<TarjetaDto> ObtenerPorId(int idTarjeta, string usuario)
        {
            var regreso = new TarjetaDto();
            var userId = await _repo.ObtenerIdUsuarioRegistradoAsync(usuario);  

            if (EsUsuarioRegistrado(userId))
            {
                var ti = await _repo.ObtenerPorIdAsync(idTarjeta);

                regreso = ConvierteTarjetaVistaDomainToDto(ti);
            }

            return regreso;
        }

        private bool EsUsuarioRegistrado(int user)
        {
            return user >= 0;
        }

        private async Task<bool> ExisteTarjetaParaElPrograma(int idPrograma)
        {
            if (await _repo.NumeroDeTarjetasPorProgamaAsync(idPrograma) > 0)
            {
                return true;
            }

            return false;
        }

        private TarjetaDto ConvierteTarjetaVistaDomainToDto(TarjetaInformativaVista t) 
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
                intKmRecorrido = t.kmrecorridos,
                intIdEstadoTarjetaInformativa = t.id_estadotarjetainformativa,
                intPersonalMilitarSEDENATropa = t.personalmilitarsedenatropa,
                intLinieros= t.linieros,
                intComandantesTurnoSSF = t.comandantesturnossf,
                intOficialesSSF = t.oficialesssf,
                intPersonalNavalSEMAROficial = t.personalnavalsemaroficial,
                intPersonalNavalSEMARTropa = t.personalnavalsemartropa,
                intIdEstadoPatrullaje = t.id_estadopatrullaje,
                strDescripcionEstadoPatrullaje = t.descripcionestadopatrullaje,
                strMatriculas = t.matriculas,
                strItinerarios = t.itinerario,
                strReportes = t.incidenciaenestructura + t.incidenciaeninstalacion,
                strOdometros = t.odometros,
                strKmVehiculos = t.KmVehiculos,
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

        private TarjetaInformativa ConvierteTarjetaDtoToDomain(TarjetaDto t)
        {
            var r = new TarjetaInformativa() 
            { 
                IdPrograma= t.intIdPrograma,
                UltimaActualizacion = DateTime.UtcNow,
                Inicio = TimeSpan.Parse(t.strInicio), // ":00" al final  representa segundos al final ??
                Termino = TimeSpan.Parse(t.strTermino), // ":00" al final
                TiempoVuelo = TimeSpan.Parse(t.strTiempoVuelo),
                CalzoAcalzo = TimeSpan.Parse(t.strCalzoCalzo),
                Observaciones= t.strObservaciones,
                ComandantesInstalacionSsf = t.intComandantesInstalacionSSF,
                PersonalMilitarSedenaoficial = t.intPersonalMilitarSEDENAOficial,
                KmRecorrido= t.intKmRecorrido,
                //FechaPatrullaje = t.FechaPatrullaje, // En qué formato viene??
                PersonalMilitarSedenatropa = t.intPersonalMilitarSEDENATropa,
                Linieros = t.intLinieros,
                ComandantesTurnoSsf = t.intComandantesTurnoSSF,
                OficialesSsf = t.intOficialesSSF,
                PersonalNavalSemaroficial = t.intPersonalNavalSEMAROficial,
                PersonalNavalSemartropa = t.intPersonalNavalSEMARTropa
                //FechaTermino = t.FechaTermino
            };

            DateTime dateValue;
            if (DateTime.TryParse(t.strFechaPatrullaje, out dateValue))
            {
                r.FechaPatrullaje = dateValue;
            }

            return r;
        }


    }
}
