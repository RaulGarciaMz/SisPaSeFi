using Domain.DTOs;
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
                var pDto = ConvierteDominioToDto(p);
                patrullajesDto.Add(pDto);
            }

            return patrullajesDto;
        }

        private PatrullajeDto ConvierteDominioToDto(PatrullajeVista p) 
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
    }
}
