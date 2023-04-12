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
    public class ProgramasService : IProgramaService
    {

        IProgramaPatrullajeRepo _repo;
        public ProgramasService(IProgramaPatrullajeRepo repo)
        {
            _repo= repo;
        }

        public async Task<List<PatrullajeDto>> ObtenerPorFiltro(string tipo, int region, string clase, int anio, int mes, int dia = 1, FiltroProgramaOpcion opcion = FiltroProgramaOpcion.ExtraordinariosyProgramados, PeriodoOpcion periodo = PeriodoOpcion.UnDia)
        {
            string estadoPropuesta;

            List<PatrullajeVista> patrullajes = new List<PatrullajeVista>();
            List<PatrullajeDto> patrullajesDto = new List<PatrullajeDto>();

            switch (opcion)
            {

                case FiltroProgramaOpcion.ExtraordinariosyProgramados:

                    if (clase == "EXTRAORDINARIO")
                    {
                        patrullajes = await _repo.ObtenerPropuestasExtraordinariasPorAnioMesDiaAsync(tipo, region, anio, mes, dia);
                    }
                    else
                    {
                        patrullajes = await _repo.ObtenerProgramasPorMesAsync(tipo, region, anio, mes);
                    }
                    break;
                case FiltroProgramaOpcion.PatrullajesEnProgreso:
                    switch (periodo)
                    {
                        case PeriodoOpcion.UnDia:
                            patrullajes = await _repo.ObtenerProgramasEnProgresoPorDiaAsync(tipo, region, anio, mes, dia);
                            break;
                        case PeriodoOpcion.UnMes:
                            patrullajes = await _repo.ObtenerProgramasEnProgresoPorMesAsync(tipo, region, anio, mes);
                            break;
                        case PeriodoOpcion.Todos:
                            patrullajes = await _repo.ObtenerProgramasEnProgresoAsync(tipo, region);
                            break;
                    }
                    break;
                case FiltroProgramaOpcion.PatrullajesConcluidos:
                    switch (periodo)
                    {
                        case PeriodoOpcion.UnDia:
                            patrullajes = await _repo.ObtenerProgramasConcluidosPorDiaAsync(tipo, region, anio, mes, dia);
                            break;
                        case PeriodoOpcion.UnMes:
                            patrullajes = await _repo.ObtenerProgramasConcluidosPorMesAsync(tipo, region, anio, mes);
                            break;
                        case PeriodoOpcion.Todos:
                            patrullajes = await _repo.ObtenerProgramasConcluidosAsync(tipo, region);
                            break;
                    }
                    break;
                case FiltroProgramaOpcion.PatrullajesCancelados:
                    switch (periodo)
                    {
                        case PeriodoOpcion.UnDia:
                            patrullajes = await _repo.ObtenerProgramasCanceladosPorDiaAsync(tipo, region, anio, mes, dia);
                            break;
                        case PeriodoOpcion.UnMes:
                            patrullajes = await _repo.ObtenerProgramasCanceladosPorMesAsync(tipo, region, anio, mes);
                            break;
                        case PeriodoOpcion.Todos:
                            patrullajes = await _repo.ObtenerProgramasCanceladosAsync(tipo, region);
                            break;
                    }
                    break;
                case FiltroProgramaOpcion.PatrullajeTodos:
                    switch (periodo)
                    {
                        case PeriodoOpcion.UnDia:
                            patrullajes = await _repo.ObtenerProgramasPorDiaAsync(tipo, region, anio, mes, dia);
                            break;
                        case PeriodoOpcion.UnMes:
                            patrullajes = await _repo.ObtenerProgramasPorMesAsync(tipo, region, anio, mes);
                            break;
                        case PeriodoOpcion.Todos:
                            patrullajes = await _repo.ObtenerProgramasAsync(tipo, region);
                            break;
                    }
                    break;
                case FiltroProgramaOpcion.PropuestaTodas:
                    estadoPropuesta = "Rechazada";
                    if (clase == "EXTRAORDINARIO")
                    {
                        patrullajes = await _repo.ObtenerPropuestasExtraordinariasPorFiltroEstadoAsync(tipo, region, anio, mes, clase, estadoPropuesta);
                    }
                    else
                    {
                        patrullajes = await _repo.ObtenerPropuestasPorFiltroEstadoAsync(tipo, region, anio, mes, clase, estadoPropuesta);
                    }
                    break;
                case FiltroProgramaOpcion.PropuestasPendientes:
                    if (clase == "EXTRAORDINARIO")
                    {
                        patrullajes = await _repo.ObtenerPropuestasExtraordinariasPorFiltroAsync(tipo, region, anio, mes, clase);
                    }
                    else
                    {
                        patrullajes = await _repo.ObtenerPropuestasPendientesPorAutorizarPorFiltroAsync(tipo, region, anio, mes, clase);
                    }
                    break;
                case FiltroProgramaOpcion.PropuestasAutorizadas:
                    estadoPropuesta = "Pendiente de autorizacion por la SSF";
                    if (clase == "EXTRAORDINARIO")
                    {
                        patrullajes = await _repo.ObtenerPropuestasExtraordinariasPorFiltroEstadoAsync(tipo, region, anio, mes, clase, estadoPropuesta);
                    }
                    else
                    {
                        patrullajes = await _repo.ObtenerPropuestasPorFiltroEstadoAsync(tipo, region, anio, mes, clase, estadoPropuesta);
                    }
                    break;
                case FiltroProgramaOpcion.PropuestasRechazadas:
                    estadoPropuesta = "Autorizada";
                    if (clase == "EXTRAORDINARIO")
                    {
                        patrullajes = await _repo.ObtenerPropuestasExtraordinariasPorFiltroEstadoAsync(tipo, region, anio, mes, clase, estadoPropuesta);
                    }
                    else
                    {
                        patrullajes = await _repo.ObtenerPropuestasPorFiltroEstadoAsync(tipo, region, anio, mes, clase, estadoPropuesta);
                    }
                    break;

                case FiltroProgramaOpcion.PropuestasEnviadas:
                    estadoPropuesta = "Aprobada por comandancia regional";
                    if (clase == "EXTRAORDINARIO")
                    {
                        patrullajes = await _repo.ObtenerPropuestasExtraordinariasPorFiltroEstadoAsync(tipo, region, anio, mes, clase, estadoPropuesta);
                    }
                    else
                    {
                        patrullajes = await _repo.ObtenerPropuestasPorFiltroEstadoAsync(tipo, region, anio, mes, clase, estadoPropuesta);
                    }
                    break;

                case FiltroProgramaOpcion.PatrullajesEnRutaFechaEspecifica:
                    patrullajes = await _repo.ObtenerPatrullajesEnRutaAndFechaEspecificaAsync(region, anio, mes, dia);
                    break;
            }

            foreach (PatrullajeVista p in patrullajes)
            {
                var pDto = ConviertePatrullajeDominioToPatrullajeDto(p);
                patrullajesDto.Add(pDto);
            }

            return patrullajesDto;
        }

        public async Task AgregaPrograma(string opcion, string clase, ProgramaDto p, string usuario) 
        {
            var user = await _repo.ObtenerUsuarioConfiguradorAsync(usuario);

            if (EsUsuarioConfigurador(user))
            {
                switch (opcion)
                {
                    case "Propuesta":
                        switch (clase)
                        {
                            case "EXTRAORDINARIO":

                                var pp = new PropuestaExtraordinariaAdd()
                                {
                                    Propuesta = ConvierteProgramaDtoToPropuestaDominio(p),
                                    Vehiculos = ConvierteListaVehiculosDtoToVehiculosDomain(p),
                                    Lineas = ConvierteListaLineasDtoToLineasDomain(p)
                                };
                                await _repo.AgregaPropuestaExtraordinariaAsync(pp, clase, user.IdUsuario);

                                break;

                            default:

                                await _repo.AgregaPropuestasFechasMultiplesAsync(ConvierteProgramaDtoToPropuestaDominio(p),
                                                                      ConvierteStringFechaToDateTime(p.LstPropuestasPatrullajesFechas),
                                                                      clase, user.IdUsuario);
                                break;
                        }
                        break;
                    case "Programa":

                        var prog = ConvierteProgramaDtoToProgramaDominio(p);
                        var fechas = ConvierteStringFechaToDateTime(p.LstPropuestasPatrullajesFechas);
                        await _repo.AgregaProgramaFechasMultiplesAsync(prog, fechas, user.IdUsuario);

                        break;
                }
            }
        }

        public async Task AgregaPropuestasComoProgramas(List<ProgramaDto> p, string usuario)
        {
            var userId = await _repo.ObtenerIdUsuarioAsync(usuario);

            if (EsUsuarioRegistrado(userId))
            {
                var programas = new List<ProgramaPatrullaje>();
                foreach (ProgramaDto prog in p)
                {
                    programas.Add(ConvierteProgramaDtoToProgramaDominio(prog));
                }

                await _repo.AgregaPropuestasComoProgramasActualizaPropuestasAsync(programas, userId);
            }
        }

        public async Task ActualizaPropuestasComoProgramasActualizaPropuestas(List<ProgramaDto> p, string opcion, int accion, string usuario)
        {
            var userId = await _repo.ObtenerIdUsuarioAsync(usuario);

            if (EsUsuarioRegistrado(userId))
            {
                switch (opcion)
                {
                    case "Programa":
                        var lstProgramas = new List<ProgramaPatrullaje>();

                        foreach (ProgramaDto prog in p)
                        {
                            var a = ConvierteProgramaDtoToProgramaDominio(prog);
                            lstProgramas.Add(a);
                        }
                        await _repo.ActualizaProgramasConPropuestasAsync(lstProgramas);

                        break;

                    case "Propuesta":

                        var lstPropuestas = new List<PropuestaPatrullaje>();

                        foreach (ProgramaDto prog in p)
                        {
                            var a = ConvierteProgramaDtoToPropuestaDominio(prog);
                            lstPropuestas.Add(a);
                        }

                        switch (accion)
                        {
                            case 2:
                                await _repo.ActualizaPropuestasAutorizadaToRechazadaAsync(lstPropuestas, userId);
                                break;
                            case 3:
                                await _repo.ActualizaPropuestasAprobadaPorComandanciaToPendientoDeAprobacionComandanciaAsync(lstPropuestas, userId);
                                break;
                            case 4:
                                await _repo.ActualizaPropuestasAutorizadaToPendientoDeAutorizacionSsfAsync(lstPropuestas, userId);
                                break;
                        }
                        break;
                }
            }
        }

        public async Task ActualizaProgramasOrPropuestasPorOpcion(ProgramaDtoForUpdatePorOpcion p, string opcion, string usuario)
        {
            var userId = await _repo.ObtenerIdUsuarioAsync(usuario);

            if (EsUsuarioRegistrado(userId))
            {
                switch (opcion)
                {
                    case "CambioRuta":
                        await _repo.ActualizaProgramaPorCambioDeRutaAsync(p.IdPrograma, p.IdRuta, userId);
                        break;

                    case "InicioPatrullaje":
                        TimeSpan ini = TimeSpan.Parse(p.Inicio);
                        await _repo.ActualizaProgramasPorInicioPatrullajeAsync(p.IdPrograma, p.IdRiesgoPatrullaje, p.IdUsuario, p.IdEstadoPatrullaje, ini);
                        break;

                    case "AutorizaPropuesta":
                        await _repo.ActualizaPropuestaToAutorizadaAsync(p.IdPrograma);
                        break;

                    case "RegionalApruebaPropuesta":
                        await _repo.ActualizaPropuestaToAprobadaComandanciaRegionalAsync(p.IdPrograma);
                        break;

                    case "RegistrarSolicitudOficioComision":
                        await _repo.ActualizaProgramaRegistraSolicitudOficioComisionAsync(p.IdPrograma, p.SolicitudOficioComision);
                        break;

                    case "RegistrarSolicitudOficioAutorizacion":
                        await _repo.ActualizaPropuestaRegistraSolicitudOficioAutorizacionAsync(p.IdPrograma, p.SolicitudOficioComision);
                        break;

                    case "RegistrarOficioComision":
                        await _repo.ActualizaProgramaRegistraOficioComisionAsync(p.IdPrograma, p.OficioComision);
                        break;

                    case "RegistrarOficioAutorizacion":
                        await _repo.ActualizaPropuestaRegistraOficioAutorizacionAsync(p.IdPrograma, p.OficioComision);
                        break;
                }
            }
        }

        public async Task ActualizaProgramaPorCambioDeRuta(ProgramaDtoForUpdateRuta p, string usuario)
        {
            var user =await  _repo.ObtenerIdUsuarioAsync(usuario);

            if (EsUsuarioRegistrado(user))
            {
                await _repo.ActualizaProgramaPorCambioDeRutaAsync(p.IdPrograma, p.IdRuta, user);
            }                
        }

        public async Task ActualizaProgramasPorInicioPatrullajeAsync(ProgramaDtoForUpdateInicio p, string usuario)
        {
            var user = await _repo.ObtenerIdUsuarioAsync(usuario);

            if (EsUsuarioRegistrado(user))
            {
                TimeSpan ini = TimeSpan.Parse(p.Inicio);
                await _repo.ActualizaProgramasPorInicioPatrullajeAsync(p.IdPrograma, p.IdRiesgoPatrullaje, p.IdUsuario,p.IdEstadoPatrullaje, ini);
            }
        }

        public async Task DeletePropuesta(int id, string usuario)
        {
            var user = await _repo.ObtenerIdUsuarioAsync(usuario);

            if (EsUsuarioRegistrado(user))
            {
                await _repo.DeletePropuestaAsync(id);
            }                
        }

        private bool EsUsuarioConfigurador(Usuario? user) 
        {
            return user != null;
        }

        private bool EsUsuarioRegistrado(int user)
        {
            return user >= 0;
        }

      
        private PatrullajeDto ConviertePatrullajeDominioToPatrullajeDto(PatrullajeVista p) 
        {
            return new PatrullajeDto(){
                IdPrograma = p.id,
                IdRuta = p.id_ruta,
                Clave = p.clave,
                DescripcionEstadoPatrullaje = p.descripcionestadopatrullaje,
                DescripcionNivelRiesgo = p.descripcionnivel,
                FechaPatrullaje = p.fechapatrullaje.ToString("yyyy-MM-dd"),
                FechaTermino = p.fechatermino.ToString("yyyy-MM-dd"),
                IdPuntoResponsable= p.id_puntoresponsable,
                IdUsuario= p.id_usuario,
                Itinerario=p.itinerario,
                ObservacionesPrograma= p.observaciones,
                ObservacionesRuta= p.observacionesruta,
                OficioComision=p.oficiocomision,
                RegionMilitarSDN= Int32.Parse(p.regionmilitarsdn),
                RegionSSF= Int32.Parse(p.regionssf),
                SolicitudOficioComision=p.solicitudoficiocomision,
                UltimaActualizacion= p.ultimaactualizacion.ToString("yyyy-MM-dd"),
                UsuarioResponsablePatrullaje=p.id_usuarioresponsablepatrullaje,
                Inicio= p.inicio.ToString()
            };
        }

        private ProgramaPatrullaje ConvierteProgramaDtoToProgramaDominio(ProgramaDto p) 
        {
            var pp = new ProgramaPatrullaje()
            {
                UltimaActualizacion = DateTime.UtcNow,
                IdRuta = p.IdRuta,            
                IdUsuario = p.IdUsuario,
                IdPuntoResponsable = p.IdPuntoResponsable,
                Observaciones = p.ObservacionesPrograma,
                IdApoyoPatrullaje = p.ApoyoPatrullaje,
                RiesgoPatrullaje = Int32.Parse(p.IdRiesgoPatrullaje),
                IdPropuestaPatrullaje = p.IdPropuestaPatrullaje                
            };

            DateTime dateValue;
            if (DateTime.TryParse(p.FechaPatrullaje, out dateValue))
            {
                pp.FechaPatrullaje = dateValue;
            }

            return pp;
        }

        private List<PropuestaPatrullajeVehiculo> ConvierteListaVehiculosDtoToVehiculosDomain(ProgramaDto p) 
        {
        
            var lstVehiculos = new List<PropuestaPatrullajeVehiculo>();

            foreach (var item in p.LstPropuestasPatrullajesVehiculos)
            {
                var pv = new PropuestaPatrullajeVehiculo() 
                { 
                    IdVehiculo = item,
                };

                lstVehiculos.Add(pv);
            }

            return lstVehiculos;
        }

        private List<PropuestaPatrullajeLinea> ConvierteListaLineasDtoToLineasDomain(ProgramaDto p)
        {

            var lstLineas = new List<PropuestaPatrullajeLinea>();

            foreach (var item in p.LstPropuestasPatrullajesLineas)
            {
                var pv = new PropuestaPatrullajeLinea()
                {
                    IdLinea = item,
                };

                lstLineas.Add(pv);
            }

            return lstLineas;
        }
        
        private PropuestaPatrullaje ConvierteProgramaDtoToPropuestaDominio(ProgramaDto p)
        {
            var pp = new PropuestaPatrullaje()
            {
                UltimaActualizacion = DateTime.UtcNow,
                IdRuta = p.IdRuta,
                IdUsuario = p.IdUsuario,
                IdPuntoResponsable = p.IdPuntoResponsable,
                Observaciones = p.ObservacionesPrograma,
                IdApoyoPatrullaje = p.ApoyoPatrullaje,
                RiesgoPatrullaje = Int32.Parse(p.IdRiesgoPatrullaje),
            };

            DateTime dateValue;
            if (DateTime.TryParse(p.FechaPatrullaje, out dateValue))
            {
                pp.FechaPatrullaje = dateValue;
            }

            return pp;
        }

        private List<DateTime> ConvierteStringFechaToDateTime(List<string> fechas) 
        {
        var lstFechas = new List<DateTime>();

            foreach (var f in fechas) 
            {
                var nf =  Convert.ToDateTime(f);
                lstFechas.Add(nf);
            }
            return lstFechas;
        }
    }
}
