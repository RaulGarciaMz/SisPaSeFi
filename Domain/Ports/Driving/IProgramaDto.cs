using Domain.DTOs;
using Domain.Entities;
using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Ports.Driving
{
    public interface IProgramaDtoCommand
    {
        Task AgregaPrograma(string opcion, string clase, ProgramaDto p, string usuario);
        Task AgregaPropuestasComoProgramas(List<ProgramaDto> p, string usuario);

        Task ActualizaProgramasOrPropuestasPorOpcion(ProgramaDtoForUpdatePorOpcion p, string opcion, string usuario);
        Task ActualizaPropuestasComoProgramasActualizaPropuestas(List<ProgramaDto> p, string opcion, int accion, string usuario);
        Task ActualizaProgramaPorCambioDeRuta(ProgramaDtoForUpdateRuta p, string usuario);
        Task ActualizaProgramasPorInicioPatrullajeAsync(ProgramaDtoForUpdateInicio p, string usuario);
        Task DeletePropuesta(int id, string usuario);
    }
    public interface IProgramaDtoQuery
    {
        Task<List<PatrullajeDto>> ObtenerPorFiltro(string tipo, int region, string clase,int anio, int mes, int dia=1, FiltroProgramaOpcion opcion = FiltroProgramaOpcion.ExtraordinariosyProgramados, PeriodoOpcion periodo = PeriodoOpcion.UnDia);      
    }
}
