using Domain.DTOs;
using Domain.Entities;
using Domain.Entities.Vistas;
using Domain.Enums;
using Domain.Ports.Driven;
using Domain.Ports.Driven.Repositories;
using Domain.Ports.Driving;

namespace DomainServices.DomServ
{
    public class ProgramasService : IProgramaService
    {

        private readonly IProgramaPatrullajeRepo _repo;
        private readonly IUsuariosParaValidacionQuery _user;

        public ProgramasService(IProgramaPatrullajeRepo repo, IUsuariosParaValidacionQuery u)
        {
            _repo = repo;
            _user = u;
        }

        public async Task<List<PatrullajeDto>> ObtenerPorFiltroAsync(string usuario, string tipo, int region, string clase, int anio, int mes, int dia = 1, FiltroProgramaOpcion opcion = FiltroProgramaOpcion.ExtraordinariosyProgramados, PeriodoOpcion periodo = PeriodoOpcion.UnDia)
        {
            string estadoPropuesta;

            List<PatrullajeVista> patrullajes = new List<PatrullajeVista>();
            List<PatrullajeDto> patrullajesDto = new List<PatrullajeDto>();

            var user = await _user.ObtenerUsuarioPorUsuarioNomAsync(usuario);

            if (user != null)
            {
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

                    case FiltroProgramaOpcion.PropuestasAnioMesZona:
                        if (clase == "EXTRAORDINARIO")
                        {
                            patrullajes = await _repo.ObtenerPropuestasExtraordinariasPorAnioMesZonaAsync(tipo, region, anio, mes, clase, (int)periodo);
                        }
                        else
                        {
                            patrullajes = await _repo.ObtenerPropuestasPorAnioMesZonaAsync(tipo, region, anio, mes, clase, (int)periodo);
                        }

                        break;
                }

                foreach (PatrullajeVista p in patrullajes)
                {
                    var pDto = ConviertePatrullajeDominioToPatrullajeDto(p);
                    patrullajesDto.Add(pDto);
                }
            }

            return patrullajesDto;
        }

        public async Task<PatrullajeSoloDto> ObtenerProgramaPorIdAsync(int idPrograma, string usuario)
        {
            var user = await _user.ObtenerUsuarioPorUsuarioNomAsync(usuario);
            var ret = new PatrullajeSoloDto();
            if (user != null)
            {
                var prog = await _repo.ObtenerProgramaPorIdAsync(idPrograma);

                if (prog != null && prog.Count > 0)
                {
                    ret = ConvierteProgramaSoloVistaToDto(prog[0]);
                }
            }

            return ret;
        }

        public async Task AgregaProgramaAsync(string opcion, string clase, ProgramaDtoForCreateWithListas p) 
        {
            var user = await _user.ObtenerUsuarioConfiguradorPorNombreAsync(p.strUsuario);

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

        public async Task AgregaPropuestasComoProgramasAsync(List<ProgramaDtoForCreate> p, string usuario)
        {
            var userId = await _user.ObtenerIdUsuarioPorUsuarioNomAsync(usuario);

            if (userId != null)
            {
                if (EsUsuarioRegistrado(userId.Value))
                {
                    var programas = new List<ProgramaPatrullaje>();
                    foreach (ProgramaDtoForCreate prog in p)
                    {
                        programas.Add(ConvierteProgramaDtoToProgramaDominio(prog));
                    }

                    await _repo.AgregaPropuestasComoProgramasActualizaPropuestasAsync(programas, userId.Value);
                }
            }
        }

        public async Task ActualizaPropuestasOrProgramasPorOpcionAndAccionAsync(List<PropuestaDtoForListaUpdate> p, string opcion, int accion, string usuario)
        {
            var usuarioId = await _user.ObtenerIdUsuarioPorUsuarioNomAsync(usuario);

            if (usuarioId != null)
            {
                var userId = usuarioId.Value;

                if (EsUsuarioRegistrado(userId))
                {
                    switch (opcion)
                    {
                        case "Programa":
                            var lstProgramas = new List<ProgramaPatrullaje>();

                            foreach (PropuestaDtoForListaUpdate prog in p)
                            {
                                var a = ConvierteProgramaDtoForUpdateToProgramaDominioForCreate(prog);
                                lstProgramas.Add(a);
                            }
                            await _repo.ActualizaPropuestasAgregaProgramasAsync(lstProgramas);

                            break;

                        case "Propuesta":

                            var lstIdsPropuestas = new List<int>();

                            foreach (PropuestaDtoForListaUpdate prog in p)
                            {
                                lstIdsPropuestas.Add(prog.intidpropuestapatrullaje);
                            }

                            switch (accion)
                            {
                                case 2:
                                    await _repo.ActualizaPropuestasAutorizadaToRechazadaAsync(lstIdsPropuestas, userId);
                                    break;
                                case 3:
                                    await _repo.ActualizaPropuestasAprobadaPorComandanciaToPendientoDeAprobacionComandanciaAsync(lstIdsPropuestas, userId);
                                    break;
                                case 4:
                                    await _repo.ActualizaPropuestasAutorizadaToPendientoDeAutorizacionSsfAsync(lstIdsPropuestas, userId);
                                    break;
                            }
                            break;
                    }
                }
            }

        }

        public async Task ActualizaProgramasOrPropuestasPorOpcionAsync(ProgramaDtoForUpdatePorOpcion p, string opcion, string usuario)
        {
            var usuarioId = await _user.ObtenerIdUsuarioPorUsuarioNomAsync(usuario);

            if (usuarioId != null)
            {
                var userId = usuarioId.Value;

                if (EsUsuarioRegistrado(userId))
                {
                    switch (opcion)
                    {
                        case "CambioRuta":
                            await _repo.ActualizaProgramaPorCambioDeRutaAsync(p.intIdPrograma, p.intIdRuta, DateTime.Parse(p.strFechaPatrullaje), userId);
                            break;

                        case "InicioPatrullaje":
                            TimeSpan ini = TimeSpan.Parse(p.strInicio);
                            await _repo.ActualizaProgramasPorInicioPatrullajeAsync(p.intIdPrograma, p.intIdRiesgoPatrullaje, userId, p.intIdEstadoPatrullaje, ini);
                            break;

                        case "AutorizaPropuesta":
                            await _repo.ActualizaPropuestaToAutorizadaAsync(p.intidpropuestapatrullaje);
                            break;

                        case "RegionalApruebaPropuesta":
                            await _repo.ActualizaPropuestaToAprobadaComandanciaRegionalAsync(p.intidpropuestapatrullaje);
                            break;

                        case "RegistrarSolicitudOficioComision":
                            await _repo.ActualizaProgramaRegistraSolicitudOficioComisionAsync(p.intIdPrograma, p.strSolicitudOficio);
                            break;

                        case "RegistrarSolicitudOficioAutorizacion":
                            await _repo.ActualizaPropuestaRegistraSolicitudOficioAutorizacionAsync(p.intidpropuestapatrullaje, p.strSolicitudOficio);
                            break;

                        case "RegistrarOficioComision":
                            await _repo.ActualizaProgramaRegistraOficioComisionAsync(p.intIdPrograma, p.strOficio);
                            break;

                        case "RegistrarOficioAutorizacion":
                            await _repo.ActualizaPropuestaRegistraOficioAutorizacionAsync(p.intidpropuestapatrullaje, p.strOficio);
                            break;
                        case "ModificarFechaPatrullaje":
                            var fechaPatrullaje = DateTime.Parse(p.strFechaPatrullaje);
                            await _repo.ActualizaFechaPatrullajeEnProgramaAndTarjetaAsync(p.intIdPrograma, fechaPatrullaje);
                            break;
                    }
                }
            }

        }

        public async Task ActualizaProgramaPorCambioDeRutaAsync(ProgramaDtoForUpdateRuta p, string usuario)
        {
            var usuarioId = await _user.ObtenerIdUsuarioPorUsuarioNomAsync(usuario);

            if (usuarioId != null)
            {
                var userId = usuarioId.Value;
                if (EsUsuarioRegistrado(userId))
                {
                    await _repo.ActualizaProgramaPorCambioDeRutaAsync(p.IdPrograma, p.IdRuta, userId);
                }
            }
        }

        public async Task ActualizaProgramasPorInicioPatrullajeAsync(ProgramaDtoForUpdateInicio p, string usuario)
        {
            var usuarioId = await _user.ObtenerIdUsuarioPorUsuarioNomAsync(usuario);

            if (usuarioId != null)
            {
                var userId = usuarioId.Value;
                if (EsUsuarioRegistrado(userId))
                {
                    TimeSpan ini = TimeSpan.Parse(p.Inicio);
                    await _repo.ActualizaProgramasPorInicioPatrullajeAsync(p.IdPrograma, p.IdRiesgoPatrullaje, p.IdUsuario, p.IdEstadoPatrullaje, ini);
                }
            }

        }

        public async Task DeletePorOpcionAsync(string opcion, int id, string usuario)
        {
            var usuarioId = await _user.ObtenerIdUsuarioPorUsuarioNomAsync(usuario);

            if (usuarioId != null)
            {
                var userId = usuarioId.Value;
                if (EsUsuarioRegistrado(userId))
                {
                    switch (opcion)
                    {
                        case "Programa":
                            await _repo.DeleteProgramaAsync(id);
                            break;
                        case "Propuesta":
                            await _repo.DeletePropuestaAsync(id);
                            break;
                    }
                    
                }
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
       
            var pd = new PatrullajeDto(){
                intIdPrograma = p.id,
                intIdRuta = p.id_ruta,
                strClave = p.clave,
                strDescripcionEstadoPatrullaje = p.descripcionestadopatrullaje,
                strDescripcionNivelRiesgo = p.descripcionnivel,
                strFechaPatrullaje = p.fechapatrullaje.ToString("yyyy-MM-dd"),
                strFechaTermino = p.fechatermino.ToString("yyyy-MM-dd"),
                intIdPuntoResponsable = p.id_puntoresponsable,
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

            if (p.id_apoyopatrullaje.HasValue)
                pd.intApoyoPatrullaje = p.id_apoyopatrullaje.Value;
                    
            return pd;
        }

        private PatrullajeSoloDto ConvierteProgramaSoloVistaToDto(ProgramaSoloVista p)
        {

            var pd = new PatrullajeSoloDto()
            {
                intIdPrograma = p.id,
                intIdRuta = p.id_ruta,
                strFechaPatrullaje = p.fechapatrullaje.ToString("yyyy-MM-dd"),
                strInicio = p.inicio.ToString(),
                intIdPuntoResponsable = p.id_puntoresponsable,
                strClave = p.clave,
                intRegionMilitarSDN = Int32.Parse(p.regionmilitarsdn),
                intRegionSSF = Int32.Parse(p.regionssf),
                strObservacionesRuta = p.observacionesruta,
                strDescripcionEstadoPatrullaje = p.descripcionestadopatrullaje,
                strObservacionesPrograma = p.observaciones,
                intIdRiesgoPatrullaje = p.riesgopatrullaje,
                strSolicitudOficio = p.solicitudoficiocomision,
                strOficio = p.oficiocomision,
                strDescripcionNivelRiesgo = p.descripcionnivel,
                strItinerario = p.itinerario,
                strUltimaActualizacion = p.ultimaactualizacion.ToString("yyyy-MM-dd"),
                intIdUsuario = p.id_usuario,
                intUsuarioResponsablePatrullaje = p.id_usuarioresponsablepatrullaje,
                intApoyoPatrullaje = p.id_apoyopatrullaje.Value,
                intidrutaoriginal = p.id_ruta_original,
                strInstalacion = p.ubicacion,
                strMunicipio = p.municipio,
                strEstado = p.estado,
                strComandanteRegional = p.nombre + " " + p.apellido1 + " " + p.apellido2

            };

            return pd;
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

        private ProgramaPatrullaje ConvierteProgramaDtoForUpdateToProgramaDominioForCreate(PropuestaDtoForListaUpdate p)
        {
            return new ProgramaPatrullaje()
            {
                UltimaActualizacion = DateTime.UtcNow,
                IdRuta = p.intIdRuta,
                IdUsuario = p.intIdUsuario,
                IdPuntoResponsable = p.intIdPuntoResponsable,
                IdPropuestaPatrullaje = p.intidpropuestapatrullaje,
                RiesgoPatrullaje = p.intIdRiesgoPatrullaje,
                IdRutaOriginal = p.intidrutaoriginal,
                FechaPatrullaje = DateTime.Parse(p.strFechaPatrullaje)
            };
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
