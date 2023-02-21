using Domain.DTOs;
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
        void AgregaPrograma(string opcion, string clase, ProgramaDto p, string usuario);
        void AgregaPropuestasComoProgramas(List<ProgramaDto> p, string usuario);

        void ActualizaPropuestasComoProgramasActualizaPropuestas(List<ProgramaDto> p, string opcion, int accion, string usuario);
        void ActualizaProgramaPorCambioDeRuta(ProgramaDto p, string usuario);

        void DeletePropuesta(int id);
    }
    public interface IProgramaDtoQuery
    {
        List<PatrullajeDto> ObtenerPorFiltro(string tipo, int region, string clase,int anio, int mes, int dia=1, FiltroProgramaOpcion opcion = FiltroProgramaOpcion.ExtraordinariosyProgramados, PeriodoOpcion periodo = PeriodoOpcion.UnDia);      
    }
}
