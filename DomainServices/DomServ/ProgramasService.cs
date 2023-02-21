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

        public void AgregaPrograma(string opcion, string clase, ProgramaDto p, string usuario) 
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
                            _repo.AgregaPropuestaExtraordinaria(pp, clase, usuario);

                            break;

                        default:


                            _repo.AgregaPropuestasFechasMultiples(ConvierteProgramaDtoToPropuestaDominio(p), 
                                                                  ConvierteStringFechaToDateTime(p.LstPropuestasPatrullajesFechas),  
                                                                  clase, usuario);

                            break;
                    }
                    break;
                case "Programa":

                    var prog = ConvierteProgramaDtoToProgramaDominio(p);
                    var fechas = ConvierteStringFechaToDateTime(p.LstPropuestasPatrullajesFechas);
                    _repo.AgregaProgramaFechasMultiples(prog, fechas, usuario);

                    break;
            }
        }

        public void AgregaPropuestasComoProgramas(List<ProgramaDto> p, string usuario)
        {
            var programas = new List<ProgramaPatrullaje>();
            foreach (ProgramaDto prog in p) 
            {
                programas.Add(ConvierteProgramaDtoToProgramaDominio(prog));
            }  

            _repo.AgregaPropuestasComoProgramasActualizaPropuestas(programas, usuario);     
        }

        public void ActualizaPropuestasComoProgramasActualizaPropuestas(List<ProgramaDto> p, string opcion, int accion, string usuario)
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
                    _repo.ActualizaProgramasConPropuestas(lstProgramas, usuario);
                    
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
                            _repo.ActualizaPropuestasAutorizadaToRechazada(lstPropuestas, usuario);
                            break;
                        case 3:
                            _repo.ActualizaPropuestasAprobadaPorComandanciaToPendientoDeAprobacionComandancia(lstPropuestas, usuario);
                            break;
                        case 4:
                            _repo.ActualizaPropuestasAutorizadaToPendientoDeAutorizacionSsf(lstPropuestas, usuario);
                            break;
                    }
                    break;
            }
        }

        public void ActualizaProgramaPorCambioDeRuta(ProgramaDto p, string usuario)
        {
            _repo.ActualizaProgramaPorCambioDeRuta(p.IdPrograma, p.IdRuta, usuario);
        }

        public void DeletePropuesta(int id)
        {
            _repo.DeletePropuesta(id);
        }

        public List<PatrullajeDto> ObtenerPorFiltro(string tipo, int region, string clase, int anio, int mes, int dia = 1, FiltroProgramaOpcion opcion = FiltroProgramaOpcion.ExtraordinariosyProgramados, PeriodoOpcion periodo = PeriodoOpcion.UnDia)
        {
            string estadoPropuesta;

            List<PatrullajeVista> patrullajes = new List<PatrullajeVista>();
            List<PatrullajeDto> patrullajesDto = new List<PatrullajeDto>();

            switch (opcion) {

                case FiltroProgramaOpcion.ExtraordinariosyProgramados:

                    if (clase == "EXTRAORDINARIO") 
                    {
                        patrullajes = _repo.ObtenerPropuestasExtraordinariasPorAnioMesDia(tipo, region, anio, mes, dia);
                    } 
                    else 
                    {
                        patrullajes = _repo.ObtenerProgramasPorMes(tipo, region, anio, mes);
                    }
                    break;
                case FiltroProgramaOpcion.PatrullajesEnProgreso:
                    switch (periodo) {
                        case PeriodoOpcion.UnDia:
                            patrullajes = _repo.ObtenerProgramasEnProgresoPorDia(tipo, region, anio, mes,dia);
                            break;
                        case PeriodoOpcion.UnMes:
                            patrullajes = _repo.ObtenerProgramasEnProgresoPorMes(tipo, region, anio, mes);
                            break;
                        case PeriodoOpcion.Todos:
                            patrullajes = _repo.ObtenerProgramasEnProgreso(tipo, region);
                            break;
                    }
                    break;
                case FiltroProgramaOpcion.PatrullajesConcluidos:
                    switch (periodo)
                    {
                        case PeriodoOpcion.UnDia:
                            patrullajes = _repo.ObtenerProgramasConcluidosPorDia(tipo, region, anio, mes, dia);
                            break;
                        case PeriodoOpcion.UnMes:
                            patrullajes = _repo.ObtenerProgramasConcluidosPorMes(tipo, region, anio, mes);
                            break;
                        case PeriodoOpcion.Todos:
                            patrullajes = _repo.ObtenerProgramasConcluidos(tipo, region);
                            break;
                    }
                    break;
                case FiltroProgramaOpcion.PatrullajesCancelados:
                    switch (periodo)
                    {
                        case PeriodoOpcion.UnDia:
                            patrullajes = _repo.ObtenerProgramasCanceladosPorDia(tipo, region, anio, mes, dia);
                            break;
                        case PeriodoOpcion.UnMes:
                            patrullajes = _repo.ObtenerProgramasCanceladosPorMes(tipo, region, anio, mes);
                            break;
                        case PeriodoOpcion.Todos:
                            patrullajes = _repo.ObtenerProgramasCancelados(tipo, region);
                            break;
                    }
                    break;
                case FiltroProgramaOpcion.PatrullajeTodos:
                    switch (periodo)
                    {
                        case PeriodoOpcion.UnDia:
                            patrullajes = _repo.ObtenerProgramasPorDia(tipo, region, anio, mes, dia);
                            break;
                        case PeriodoOpcion.UnMes:
                            patrullajes = _repo.ObtenerProgramasPorMes(tipo, region, anio, mes);
                            break;
                        case PeriodoOpcion.Todos:
                            patrullajes = _repo.ObtenerProgramas(tipo, region);
                            break;
                    }
                    break;
                case FiltroProgramaOpcion.PropuestasPendientes:
                    if (clase == "EXTRAORDINARIO")
                    {
                        patrullajes = _repo.ObtenerPropuestasExtraordinariasPorFiltro(tipo, region, anio, mes, clase);
                    }
                    else
                    {
                        patrullajes = _repo.ObtenerPropuestasPendientesPorAutorizarPorFiltro(tipo, region, anio, mes, clase);
                    }
                    break;
                case FiltroProgramaOpcion.PropuestasAutorizadas:
                    estadoPropuesta = "Pendiente de autorizacion por la SSF";
                    if (clase == "EXTRAORDINARIO")
                    {
                        patrullajes = _repo.ObtenerPropuestasExtraordinariasPorFiltroEstado(tipo, region, anio, mes, clase, estadoPropuesta);
                    }
                    else
                    {
                        patrullajes = _repo.ObtenerPropuestasPorFiltroEstado(tipo, region, anio, mes, clase, estadoPropuesta);
                    }
                    break;
                case FiltroProgramaOpcion.PropuestasRechazadas:
                    estadoPropuesta = "Autorizada";
                    if (clase == "EXTRAORDINARIO")
                    {
                        patrullajes = _repo.ObtenerPropuestasExtraordinariasPorFiltroEstado(tipo, region, anio, mes, clase, estadoPropuesta);
                    }
                    else
                    {
                        patrullajes = _repo.ObtenerPropuestasPorFiltroEstado(tipo, region, anio, mes, clase, estadoPropuesta);
                    }
                    break;
                case FiltroProgramaOpcion.PropuestaTodas:
                    estadoPropuesta = "Rechazada";
                    if (clase == "EXTRAORDINARIO")
                    {
                        patrullajes = _repo.ObtenerPropuestasExtraordinariasPorFiltroEstado(tipo, region, anio, mes, clase, estadoPropuesta);
                    }
                    else
                    {
                        patrullajes = _repo.ObtenerPropuestasPorFiltroEstado(tipo, region, anio, mes, clase, estadoPropuesta);
                    }
                    break;
                case FiltroProgramaOpcion.PropuestasEnviadas:
                    estadoPropuesta = "Aprobada por comandancia regional";
                    if (clase == "EXTRAORDINARIO")
                    {
                        patrullajes = _repo.ObtenerPropuestasExtraordinariasPorFiltroEstado(tipo, region, anio, mes, clase, estadoPropuesta);
                    }
                    else
                    {
                        patrullajes = _repo.ObtenerPropuestasPorFiltroEstado(tipo, region, anio, mes, clase, estadoPropuesta);
                    }
                    break;
            }

            foreach (PatrullajeVista p in patrullajes)
            {
                var pDto = ConviertePatrullajeDominioToPatrullajeDto(p);
                patrullajesDto.Add(pDto);
            }

            return patrullajesDto;
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

/*        private PatrullajeDto ConvierteDtoToPropuestaAddDomain(PatrullajeDto p)
        {
*//*
            var propuesta = ConvierteDtoPropuestaToDominio(p);
            var propuestaVehiculos = ConvierteDtoPropuestaVehiculoToDominio(p);
            var propuestaLineas = ConvierteDtoPropuestaLineaToDominio(p);
*//*
            var pp = new PropuestaExtraordinariaAdd()
            { 
                Propuesta = null,
                Vehiculos= null,
                Lineas = null
            };


            pp, string clase, List< PropuestaPatrullajeVehiculo > vehiculos, List<PropuestaPatrullajeLinea> lineas

            return new PatrullajeDto()
            {
                IdPrograma = p.id,
                IdRuta = p.id_ruta,
                Clave = p.clave,
                DescripcionEstadoPatrullaje = p.descripcionestadopatrullaje,
                DescripcionNivelRiesgo = p.descripcionnivel,
                FechaPatrullaje = p.fechapatrullaje.ToString("yyyy-MM-dd"),
                FechaTermino = p.fechatermino.ToString("yyyy-MM-dd"),
                IdPuntoResponsable = p.id_puntoresponsable,
                IdUsuario = p.id_usuario,
                Itinerario = p.itinerario,
                ObservacionesPrograma = p.observaciones,
                ObservacionesRuta = p.observacionesruta,
                OficioComision = p.oficiocomision,
                RegionMilitarSDN = Int32.Parse(p.regionmilitarsdn),
                RegionSSF = Int32.Parse(p.regionssf),
                SolicitudOficioComision = p.solicitudoficiocomision,
                UltimaActualizacion = p.ultimaactualizacion.ToString("yyyy-MM-dd"),
                UsuarioResponsablePatrullaje = p.id_usuarioresponsablepatrullaje,
                Inicio = p.inicio.ToString()
            };
        }*/

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
