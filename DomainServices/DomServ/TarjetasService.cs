using Domain.DTOs;
using Domain.Entities;
using Domain.Entities.Vistas;
using Domain.Enums;
using Domain.Ports.Driven.Repositories;
using Domain.Ports.Driving;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
                if (await ExisteTarjetaParaElPrograma(tarjeta.IdPrograma)) 
                {
                    return;
                }

                var t = ConvierteTarjetaDtoToDomain(tarjeta);

                await _repo.AgregaAsync(t, tarjeta.IdEstadoPatrullaje, userId);
            }
        }

        public async  Task Update(TarjetaDto tarjeta, string usuario) 
        {
            var userId = await _repo.ObtenerIdUsuarioConfiguradorAsync(usuario);

            if (EsUsuarioRegistrado(userId))
            {
                var t = await _repo.ObtenerTarjetaPorIdNotaAsync(tarjeta.IdNota);

                if (t == null)
                {
                    return;
                }

                t.UltimaActualizacion = DateTime.UtcNow;
                t.Inicio = TimeSpan.Parse(tarjeta.Inicio); // ":00" al final  representa segundos al final ??
                t.Termino = TimeSpan.Parse(tarjeta.Termino); // ":00" al final
                t.TiempoVuelo = TimeSpan.Parse(tarjeta.TiempoVuelo);
                t.CalzoAcalzo = TimeSpan.Parse(tarjeta.CalzoCalzo);
                t.Observaciones = tarjeta.Observaciones;
                t.ComandantesInstalacionSsf = tarjeta.ComandantesInstalacionSSF;
                t.PersonalMilitarSedenaoficial = tarjeta.PersonalMilitarSEDENAOficial;
                t.KmRecorrido = tarjeta.KmRecorrido;
                //FechaPatrullaje = tarjeta.FechaPatrullaje; // En qué formato viene??
                t.PersonalMilitarSedenatropa = tarjeta.PersonalMilitarSEDENATropa;
                t.Linieros = tarjeta.Linieros;
                t.ComandantesTurnoSsf = tarjeta.ComandantesTurnoSSF;
                t.OficialesSsf = tarjeta.OficialesSSF;
                t.PersonalNavalSemaroficial = tarjeta.PersonalNavalSEMAROficial;
                t.PersonalNavalSemartropa = tarjeta.PersonalNavalSEMARTropa;

                await _repo.UpdateAsync(t, tarjeta.IdEstadoPatrullaje, userId);
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

        public async Task<List<TarjetaDto>> ObtenerPorId(int idTarjeta, string usuario)
        {
            var regreso = new List<TarjetaDto>();
            var userId = await _repo.ObtenerIdUsuarioRegistradoAsync(usuario);  

            if (EsUsuarioRegistrado(userId))
            {
                var tarjetas = await _repo.ObtenerPorIdAsync(idTarjeta);
    
                foreach (var t in tarjetas)
                {
                    var tiv = ConvierteTarjetaVistaDomainToDto(t);
                    regreso.Add(tiv);
                }
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
                IdNota= t.id_nota,
                IdPrograma= t.id_programa,
                FechaPatrullaje = t.fechapatrullaje.Value.ToString("yyyy-MM-dd"), 
                IdRuta = t.id_ruta,
                Region = Int32.Parse(t.regionssf),
                IdTipoPatrullaje = t.id_tipopatrullaje,
                UltimaActualizacion = t.ultimaactualizacion.ToString("yyyy-MM-dd HH:mm:ss"), 
                IdUsuario = t.id_usuario.ToString(),
                Inicio = t.inicio.ToString(),
                Termino = t.termino.ToString(),
                TiempoVuelo = t.tiempovuelo.ToString(),
                CalzoCalzo = t.calzoacalzo.ToString(),
                Observaciones= t.observaciones,
                ComandantesInstalacionSSF = t.comandantesinstalacionssf,
                PersonalMilitarSEDENAOficial = t.personalmilitarsedenaoficial,
                KmRecorrido = t.kmrecorridos,
                IdEstadoTarjetaInformativa = t.id_estadotarjetainformativa,
                PersonalMilitarSEDENATropa = t.personalmilitarsedenatropa,
                Linieros= t.linieros,
                ComandantesTurnoSSF = t.comandantesturnossf,
                OficialesSSF = t.oficialesssf,
                PersonalNavalSEMAROficial = t.personalnavalsemaroficial,
                PersonalNavalSEMARTropa = t.personalnavalsemartropa,
                IdEstadoPatrullaje = t.id_estadopatrullaje,
                DescripcionEstadoPatrullaje = t.descripcionestadopatrullaje,
                Matriculas = t.matriculas,
                Itinerarios = t.itinerario,
                Reportes = t.incidenciaenestructura + t.incidenciaeninstalacion,
                Odometros = t.odometros,
                KmVehiculos = t.KmVehiculos,
                FechaTermino = t.fechaTermino.ToString("yyyy-MM-dd"),
                IdResultadoPatrullaje = t.idresultadopatrullaje,
                ResultadoPatrullaje = t.resultadopatrullaje,
                LineaEstructuraInstalacion = t.lineaestructurainstalacion,
                ResponsableVuelo = t.responsablevuelo,
                FuerzaReaccion = t.fuerzareaccion
            };

            return r;
        }

        private TarjetaInformativa ConvierteTarjetaDtoToDomain(TarjetaDto t)
        {
            var r = new TarjetaInformativa() 
            { 
                IdPrograma= t.IdPrograma,
                UltimaActualizacion = DateTime.UtcNow,
                Inicio = TimeSpan.Parse(t.Inicio), // ":00" al final  representa segundos al final ??
                Termino = TimeSpan.Parse(t.Termino), // ":00" al final
                TiempoVuelo = TimeSpan.Parse(t.TiempoVuelo),
                CalzoAcalzo = TimeSpan.Parse(t.CalzoCalzo),
                Observaciones= t.Observaciones,
                ComandantesInstalacionSsf = t.ComandantesInstalacionSSF,
                PersonalMilitarSedenaoficial = t.PersonalMilitarSEDENAOficial,
                KmRecorrido= t.KmRecorrido,
                //FechaPatrullaje = t.FechaPatrullaje, // En qué formato viene??
                PersonalMilitarSedenatropa = t.PersonalMilitarSEDENATropa,
                Linieros = t.Linieros,
                ComandantesTurnoSsf = t.ComandantesTurnoSSF,
                OficialesSsf = t.OficialesSSF,
                PersonalNavalSemaroficial = t.PersonalNavalSEMAROficial,
                PersonalNavalSemartropa = t.PersonalNavalSEMARTropa
                //FechaTermino = t.FechaTermino
            };

            DateTime dateValue;
            if (DateTime.TryParse(t.FechaPatrullaje, out dateValue))
            {
                r.FechaPatrullaje = dateValue;
            }

            return r;
        }


    }
}
