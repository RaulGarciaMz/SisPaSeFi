using Domain.DTOs;
using Domain.Entities;
using Domain.Entities.Vistas;
using Domain.Enums;
using Domain.Ports.Driven.Repositories;
using Domain.Ports.Driving;

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

        public async Task AgregaPrograma(string opcion, string clase, ProgramaDtoForCreateWithListas p) 
        {
            var user = await _repo.ObtenerUsuarioConfiguradorAsync(p.strUsuario);

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
                                                                      ConvierteStringFechaToDateTime(p.lstPropuestasPatrullajesFechas),
                                                                      clase, user.IdUsuario);
                                break;
                        }
                        break;
                    case "Programa":

                        var prog = ConvierteProgramaDtoToProgramaDominio(p);
                        var fechas = ConvierteStringFechaToDateTime(p.lstPropuestasPatrullajesFechas);
                        await _repo.AgregaProgramaFechasMultiplesAsync(prog, fechas, user.IdUsuario);

                        break;
                }
            }
        }

        public async Task AgregaPropuestasComoProgramas(List<ProgramaDtoForCreate> p, string usuario)
        {
            var userId = await _repo.ObtenerIdUsuarioAsync(usuario);

            if (EsUsuarioRegistrado(userId))
            {
                var programas = new List<ProgramaPatrullaje>();
                foreach (ProgramaDtoForCreate prog in p)
                {
                    programas.Add(ConvierteProgramaDtoToProgramaDominio(prog));
                }

                await _repo.AgregaPropuestasComoProgramasActualizaPropuestasAsync(programas, userId);
            }
        }

        public async Task ActualizaPropuestasOrProgramasPorOpcionAndAccion(List<ProgramaDto> p, string opcion, int accion, string usuario)
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
                            var a = ConvierteProgramaDtoForUpdateToProgramaDominio(prog);
                            lstProgramas.Add(a);
                        }
                        await _repo.ActualizaProgramasConPropuestasAsync(lstProgramas);

                        break;

                    case "Propuesta":

                        var lstPropuestas = new List<PropuestaPatrullaje>();

                        foreach (ProgramaDto prog in p)
                        {
                            var a = ConvierteProgramaDtoForUpdateToPropuestaDominio(prog);
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
                        await _repo.ActualizaProgramaPorCambioDeRutaAsync(p.intIdPrograma, p.intIdRuta, userId);
                        break;

                    case "InicioPatrullaje":
                        TimeSpan ini = TimeSpan.Parse(p.strInicio);
                        await _repo.ActualizaProgramasPorInicioPatrullajeAsync(p.intIdPrograma, p.intIdRiesgoPatrullaje, p.IdUsuario, p.intIdEstadoPatrullaje, ini);
                        break;

                    case "AutorizaPropuesta":
                        await _repo.ActualizaPropuestaToAutorizadaAsync(p.intIdPrograma);
                        break;

                    case "RegionalApruebaPropuesta":
                        await _repo.ActualizaPropuestaToAprobadaComandanciaRegionalAsync(p.intIdPrograma);
                        break;

                    case "RegistrarSolicitudOficioComision":
                        await _repo.ActualizaProgramaRegistraSolicitudOficioComisionAsync(p.intIdPrograma, p.strSolicitudOficio);
                        break;

                    case "RegistrarSolicitudOficioAutorizacion":
                        await _repo.ActualizaPropuestaRegistraSolicitudOficioAutorizacionAsync(p.intIdPrograma, p.strSolicitudOficio);
                        break;

                    case "RegistrarOficioComision":
                        await _repo.ActualizaProgramaRegistraOficioComisionAsync(p.intIdPrograma, p.strOficio);
                        break;

                    case "RegistrarOficioAutorizacion":
                        await _repo.ActualizaPropuestaRegistraOficioAutorizacionAsync(p.intIdPrograma, p.strOficio);
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
                intIdPrograma = p.id,
                intIdRuta = p.id_ruta,
                strClave = p.clave,
                strDescripcionEstadoPatrullaje = p.descripcionestadopatrullaje,
                strDescripcionNivelRiesgo = p.descripcionnivel,
                strFechaPatrullaje = p.fechapatrullaje.ToString("yyyy-MM-dd"),
                strFechaTermino = p.fechatermino.ToString("yyyy-MM-dd"),
                intIdPuntoResponsable= p.id_puntoresponsable,
                intIdUsuario= p.id_usuario,
                strItinerario=p.itinerario,
                strObservacionesPrograma= p.observaciones,
                strObservacionesRuta= p.observacionesruta,
                strOficio=p.oficiocomision,
                intRegionMilitarSDN= Int32.Parse(p.regionmilitarsdn),
                intRegionSSF= Int32.Parse(p.regionssf),
                strSolicitudOficio=p.solicitudoficiocomision,
                strUltimaActualizacion= p.ultimaactualizacion.ToString("yyyy-MM-dd"),
                intUsuarioResponsablePatrullaje=p.id_usuarioresponsablepatrullaje,
                strInicio= p.inicio.ToString()
            };
        }

        private ProgramaPatrullaje ConvierteProgramaDtoToProgramaDominio(ProgramaDtoForCreate p) 
        {
            var pp = new ProgramaPatrullaje()
            {
                UltimaActualizacion = DateTime.UtcNow,
                IdRuta = p.intIdRuta,            
                //IdUsuario = p.IdUsuario,
                IdPuntoResponsable = p.intIdPuntoResponsable,
                //Observaciones = p.ObservacionesPrograma,
                IdApoyoPatrullaje = p.intApoyoPatrullaje,
                RiesgoPatrullaje = Int32.Parse(p.intIdRiesgoPatrullaje),
                IdPropuestaPatrullaje = p.intidpropuestapatrullaje                
            };

            DateTime dateValue;
            if (DateTime.TryParse(p.strFechaPatrullaje, out dateValue))
            {
                pp.FechaPatrullaje = dateValue;
            }

            return pp;
        }

        private ProgramaPatrullaje ConvierteProgramaDtoToProgramaDominio(ProgramaDtoForCreateWithListas p)
        {
            var pp = new ProgramaPatrullaje()
            {
                UltimaActualizacion = DateTime.UtcNow,
                IdRuta = p.intIdRuta,
                //IdUsuario = p.IdUsuario,
                IdPuntoResponsable = p.intIdPuntoResponsable,
                //Observaciones = p.ObservacionesPrograma,
                IdApoyoPatrullaje = p.intApoyoPatrullaje,
                RiesgoPatrullaje = p.intIdRiesgoPatrullaje,
                IdPropuestaPatrullaje = p.intidpropuestapatrullaje
            };

            DateTime dateValue;
            if (DateTime.TryParse(p.strFechaPatrullaje, out dateValue))
            {
                pp.FechaPatrullaje = dateValue;
            }

            return pp;
        }

        private ProgramaPatrullaje ConvierteProgramaDtoForUpdateToProgramaDominio(ProgramaDto p)
        {
            var pp = new ProgramaPatrullaje()
            {
                UltimaActualizacion = DateTime.UtcNow,
                IdRuta = p.intIdRuta,
                IdUsuario = p.IdUsuario,
                IdPuntoResponsable = p.intIdPuntoResponsable,
                Observaciones = p.ObservacionesPrograma,
                IdApoyoPatrullaje = p.intApoyoPatrullaje,
                RiesgoPatrullaje = Int32.Parse(p.intIdRiesgoPatrullaje),
                IdPropuestaPatrullaje = p.intidpropuestapatrullaje
            };

            DateTime dateValue;
            if (DateTime.TryParse(p.strFechaPatrullaje, out dateValue))
            {
                pp.FechaPatrullaje = dateValue;
            }

            return pp;
        }

        private List<PropuestaPatrullajeVehiculo> ConvierteListaVehiculosDtoToVehiculosDomain(ProgramaDtoForCreateWithListas p) 
        {
        
            var lstVehiculos = new List<PropuestaPatrullajeVehiculo>();

            foreach (var item in p.lstPropuestasPatrullajesVehiculos)
            {
                var pv = new PropuestaPatrullajeVehiculo() 
                { 
                    IdVehiculo = item,
                };

                lstVehiculos.Add(pv);
            }

            return lstVehiculos;
        }

        private List<PropuestaPatrullajeLinea> ConvierteListaLineasDtoToLineasDomain(ProgramaDtoForCreateWithListas p)
        {

            var lstLineas = new List<PropuestaPatrullajeLinea>();

            foreach (var item in p.lstPropuestasPatrullajesLineas)
            {
                var pv = new PropuestaPatrullajeLinea()
                {
                    IdLinea = item,
                };

                lstLineas.Add(pv);
            }

            return lstLineas;
        }
        
        private PropuestaPatrullaje ConvierteProgramaDtoToPropuestaDominio(ProgramaDtoForCreateWithListas p)
        {
            var pp = new PropuestaPatrullaje()
            {
                UltimaActualizacion = DateTime.UtcNow,
                IdRuta = p.intIdRuta,
//                IdUsuario = p.IdUsuario,
                IdPuntoResponsable = p.intIdPuntoResponsable,
                //Observaciones = p.ObservacionesPrograma,
                IdApoyoPatrullaje = p.intApoyoPatrullaje,
                RiesgoPatrullaje = p.intIdRiesgoPatrullaje,
            };

            DateTime dateValue;
            if (DateTime.TryParse(p.strFechaPatrullaje, out dateValue))
            {
                pp.FechaPatrullaje = dateValue;
            }

            return pp;
        }

        private PropuestaPatrullaje ConvierteProgramaDtoForUpdateToPropuestaDominio(ProgramaDto p)
        {
            var pp = new PropuestaPatrullaje()
            {
                UltimaActualizacion = DateTime.UtcNow,
                IdRuta = p.intIdRuta,
                IdUsuario = p.IdUsuario,
                IdPuntoResponsable = p.intIdPuntoResponsable,
                Observaciones = p.ObservacionesPrograma,
                IdApoyoPatrullaje = p.intApoyoPatrullaje,
                RiesgoPatrullaje = Int32.Parse(p.intIdRiesgoPatrullaje),
            };

            DateTime dateValue;
            if (DateTime.TryParse(p.strFechaPatrullaje, out dateValue))
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
