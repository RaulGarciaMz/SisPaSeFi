using Domain.DTOs;
using Domain.Entities;
using Domain.Entities.Vistas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Ports.Driven
{
    public interface IProgramaCommand
    {
        Task AgregaPropuestasComoProgramasActualizaPropuestasAsync(List<ProgramaPatrullaje> programas, int usuarioId);
        Task AgregaPropuestaExtraordinariaAsync(PropuestaExtraordinariaAdd pp, string clase, int usuarioId);
        Task AgregaPropuestasFechasMultiplesAsync(PropuestaPatrullaje pp, List<DateTime> fechas, string clase, int usuarioId);
        Task AgregaProgramaFechasMultiplesAsync(ProgramaPatrullaje pp, List<DateTime> fechas, int usuarioId);
        Task ActualizaProgramaPorCambioDeRutaAsync(int idPrograma, int idRuta, int usuarioId);
        Task ActualizaPropuestasAgregaProgramasAsync(List<ProgramaPatrullaje> programas);
        Task ActualizaProgramasPorInicioPatrullajeAsync(int idPrograma, int idRiesgo, int idUsuario, int idEstadoPatrullaje, TimeSpan inicio);
        Task ActualizaPropuestaToAutorizadaAsync(int idPropuesta);
        Task ActualizaPropuestaToAprobadaComandanciaRegionalAsync(int idPropuesta);
        Task ActualizaProgramaRegistraSolicitudOficioComisionAsync(int idPrograma, string oficio);
        Task ActualizaPropuestaRegistraSolicitudOficioAutorizacionAsync(int idPropuesta, string oficio);
        Task ActualizaProgramaRegistraOficioComisionAsync(int idPrograma, string oficio);
        Task ActualizaPropuestaRegistraOficioAutorizacionAsync(int idPropuesta, string oficio);

        Task ActualizaPropuestasAutorizadaToRechazadaAsync(List<int> propuestas, int usuarioId);
        Task ActualizaPropuestasAprobadaPorComandanciaToPendientoDeAprobacionComandanciaAsync(List<int> propuestas, int usuarioId);
        Task ActualizaPropuestasAutorizadaToPendientoDeAutorizacionSsfAsync(List<int> propuestas, int usuarioId);        
        Task DeletePropuestaAsync(int id);
    }

    public interface IProgramaQuery
    {
        //Caso 0 Extraordinario  --Propuestas extraordinarias
        Task<List<PatrullajeVista>> ObtenerPropuestasExtraordinariasPorAnioMesDiaAsync(string tipo, int region, int anio, int mes, int dia);
        //Caso 5 Ordinario  - Propuestas pendientes de autorizar
        Task<List<PatrullajeVista>> ObtenerPropuestasPendientesPorAutorizarPorFiltroAsync(string tipo, int region, int anio, int mes, string clase);
        //Caso 5 ExtraordinarioOrdinario  - 
        Task<List<PatrullajeVista>> ObtenerPropuestasExtraordinariasPorFiltroAsync(string tipo, int region, int anio, int mes, string clase);
        //Dan servicio a las opciones 6,7,8,9  Ordinario  -- Propuestas
        Task<List<PatrullajeVista>> ObtenerPropuestasPorFiltroEstadoAsync(string tipo, int region, int anio, int mes, string clase, string estadoPropuesta);
        //Caso 6,7,8,9 Extraordinario  - 
        Task<List<PatrullajeVista>> ObtenerPropuestasExtraordinariasPorFiltroEstadoAsync(string tipo, int region, int anio, int mes, string clase, string estadoPropuesta);
        //Caso 10 Patrullajes de una ruta y fecha específica
        Task<List<PatrullajeVista>> ObtenerPatrullajesEnRutaAndFechaEspecificaAsync(int ruta, int anio, int mes, int dia);
        //Caso 1 Programas EN PROGRESO Periodo 1 - Un día
        Task<List<PatrullajeVista>> ObtenerProgramasEnProgresoPorDiaAsync(string tipo, int region, int anio, int mes, int dia);
        //Caso 1 Programas EN PROGRESO Periodo 2 - Un mes
        Task<List<PatrullajeVista>> ObtenerProgramasEnProgresoPorMesAsync(string tipo, int region, int anio, int mes);
        //Caso 1 Programas EN PROGRESO Periodo 3 - todos
        Task<List<PatrullajeVista>> ObtenerProgramasEnProgresoAsync(string tipo, int region);
        //Caso 2 Programas CONCLUIDOS Periodo 1 - Un día
        Task<List<PatrullajeVista>> ObtenerProgramasConcluidosPorDiaAsync(string tipo, int region, int anio, int mes, int dia);
        //Caso 2 Programas CONCLUIDOS Periodo 2 - Un mes
        Task<List<PatrullajeVista>> ObtenerProgramasConcluidosPorMesAsync(string tipo, int region, int anio, int mes);
        //Caso 2 Programas CONCLUIDOS Periodo 3 - todos
        Task<List<PatrullajeVista>> ObtenerProgramasConcluidosAsync(string tipo, int region);
        //Caso 3 Programas CANCELADOS Periodo 1 - Un día
        Task<List<PatrullajeVista>> ObtenerProgramasCanceladosPorDiaAsync(string tipo, int region, int anio, int mes, int dia);
        //Caso 3 Programas CANCELADOS Periodo 2 - Un mes
        Task<List<PatrullajeVista>> ObtenerProgramasCanceladosPorMesAsync(string tipo, int region, int anio, int mes);
        //Caso 3 Programas CANCELADOS Periodo 3 - todos
        Task<List<PatrullajeVista>> ObtenerProgramasCanceladosAsync(string tipo, int region);
        //Caso 4 Programas Periodo 1 - Un día
        Task<List<PatrullajeVista>> ObtenerProgramasPorDiaAsync(string tipo, int region, int anio, int mes, int dia);
        //Caso 4 Programas Periodo 2 - Un mes  --- Aplica también para el Caso 0 Ordinario
        Task<List<PatrullajeVista>> ObtenerProgramasPorMesAsync(string tipo, int region, int anio, int mes);
        //Caso 4 Programas Periodo 3 - Todos
        Task<List<PatrullajeVista>> ObtenerProgramasAsync(string tipo, int region);
    }
}
