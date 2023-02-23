using Domain.DTOs;
using Domain.Entities;
using Domain.Entities.Vistas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Ports.Driving
{
    public interface IProgramaCommand
    {
        void AgregaPropuestasComoProgramasActualizaPropuestas(List<ProgramaPatrullaje> programas, int usuarioId);
        void AgregaPropuestaExtraordinaria(PropuestaExtraordinariaAdd pp, string clase, int usuarioId);
        void AgregaPropuestasFechasMultiples(PropuestaPatrullaje pp, List<DateTime> fechas, string clase, int usuarioId);
        void AgregaProgramaFechasMultiples(ProgramaPatrullaje pp, List<DateTime> fechas, int usuarioId);
        void ActualizaProgramaPorCambioDeRuta(int idPrograma, int idRuta, int usuarioId);
        void ActualizaProgramasConPropuestas(List<ProgramaPatrullaje> programas);
        void ActualizaPropuestasAutorizadaToRechazada(List<PropuestaPatrullaje> propuestas, int usuarioId);
        void ActualizaPropuestasAprobadaPorComandanciaToPendientoDeAprobacionComandancia(List<PropuestaPatrullaje> propuestas, int usuarioId);
        void ActualizaPropuestasAutorizadaToPendientoDeAutorizacionSsf(List<PropuestaPatrullaje> propuestas, int usuarioId);

        void DeletePropuesta(int id);
    }

    public interface IProgramaQuery
    {
        //Caso 0 Extraordinario  --Propuestas extraordinarias
        List<PatrullajeVista> ObtenerPropuestasExtraordinariasPorAnioMesDia(string tipo, int region, int anio, int mes, int dia);
        //Caso 5 Ordinario  - Propuestas pendientes de autorizar
        public List<PatrullajeVista> ObtenerPropuestasPendientesPorAutorizarPorFiltro(string tipo, int region, int anio, int mes, string clase);
        //Caso 5 ExtraordinarioOrdinario  - 
        public List<PatrullajeVista> ObtenerPropuestasExtraordinariasPorFiltro(string tipo, int region, int anio, int mes, string clase);
        //Dan servicio a las opciones 6,7,8,9  Ordinario  -- Propuestas
        public List<PatrullajeVista> ObtenerPropuestasPorFiltroEstado(string tipo, int region, int anio, int mes, string clase, string estadoPropuesta);
        //Caso 6,7,8,9 Extraordinario  - 
        public List<PatrullajeVista> ObtenerPropuestasExtraordinariasPorFiltroEstado(string tipo, int region, int anio, int mes, string clase, string estadoPropuesta);
        //Caso 1 Programas EN PROGRESO Periodo 1 - Un día
        public List<PatrullajeVista> ObtenerProgramasEnProgresoPorDia(string tipo, int region, int anio, int mes, int dia);
        //Caso 1 Programas EN PROGRESO Periodo 2 - Un mes
        public List<PatrullajeVista> ObtenerProgramasEnProgresoPorMes(string tipo, int region, int anio, int mes);
        //Caso 1 Programas EN PROGRESO Periodo 3 - todos
        public List<PatrullajeVista> ObtenerProgramasEnProgreso(string tipo, int region);
        //Caso 2 Programas CONCLUIDOS Periodo 1 - Un día
        public List<PatrullajeVista> ObtenerProgramasConcluidosPorDia(string tipo, int region, int anio, int mes, int dia);
        //Caso 2 Programas CONCLUIDOS Periodo 2 - Un mes
        public List<PatrullajeVista> ObtenerProgramasConcluidosPorMes(string tipo, int region, int anio, int mes);
        //Caso 2 Programas CONCLUIDOS Periodo 3 - todos
        public List<PatrullajeVista> ObtenerProgramasConcluidos(string tipo, int region);
        //Caso 3 Programas CANCELADOS Periodo 1 - Un día
        public List<PatrullajeVista> ObtenerProgramasCanceladosPorDia(string tipo, int region, int anio, int mes, int dia);
        //Caso 3 Programas CANCELADOS Periodo 2 - Un mes
        public List<PatrullajeVista> ObtenerProgramasCanceladosPorMes(string tipo, int region, int anio, int mes);
        //Caso 3 Programas CANCELADOS Periodo 3 - todos
        public List<PatrullajeVista> ObtenerProgramasCancelados(string tipo, int region);
        //Caso 4 Programas Periodo 1 - Un día
        public List<PatrullajeVista> ObtenerProgramasPorDia(string tipo, int region, int anio, int mes, int dia);
        //Caso 4 Programas Periodo 2 - Un mes  --- Aplica también para el Caso 0 Ordinario
        public List<PatrullajeVista> ObtenerProgramasPorMes(string tipo, int region, int anio, int mes);
        //Caso 4 Programas Periodo 3 - Todos
        public List<PatrullajeVista> ObtenerProgramas(string tipo, int region);

        public Usuario? ObtenerUsuarioConfigurador(string usuario);
        public int ObtenerIdUsuario(string usuario);
    }
}
