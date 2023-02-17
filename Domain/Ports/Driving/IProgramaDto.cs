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
        void AgregaProgramaFechasMultiples(ProgramaDto p);
        void AgregaPropuestasParaPrograma();
        void AgregaPropuestasFechasMultiples();
        void AgregaPropuestaExtraordinaria();

        void ActualizaPrograma();
        void ActualizaPropuestasDelPrograma();
        void DeletePropuesta(int id);
    }
    public interface IProgramaDtoQuery
    {
        List<PatrullajeDto> ObtenerPorFiltro(string tipo, int region, string clase,int anio, int mes, int dia=1, FiltroProgramaOpcion opcion = FiltroProgramaOpcion.ExtraordinariosyProgramados, PeriodoOpcion periodo = PeriodoOpcion.UnDia);      
    }
}
