using Domain.DTOs;
using Domain.Enums;

namespace Domain.Ports.Driving
{
    public interface IProgramaDtoCommand
    {
        Task AgregaPrograma(string opcion, string clase, ProgramaDtoForCreateWithListas p);
        Task AgregaPropuestasComoProgramas(List<ProgramaDtoForCreate> p, string usuario);

        Task ActualizaProgramasOrPropuestasPorOpcion(ProgramaDtoForUpdatePorOpcion p, string opcion, string usuario);
        Task ActualizaPropuestasOrProgramasPorOpcionAndAccion(List<ProgramaDto> p, string opcion, int accion, string usuario);
        Task ActualizaProgramaPorCambioDeRuta(ProgramaDtoForUpdateRuta p, string usuario);
        Task ActualizaProgramasPorInicioPatrullajeAsync(ProgramaDtoForUpdateInicio p, string usuario);
        Task DeletePropuesta(int id, string usuario);
    }
    public interface IProgramaDtoQuery
    {
        Task<List<PatrullajeDto>> ObtenerPorFiltro(string tipo, int region, string clase,int anio, int mes, int dia=1, FiltroProgramaOpcion opcion = FiltroProgramaOpcion.ExtraordinariosyProgramados, PeriodoOpcion periodo = PeriodoOpcion.UnDia);      
    }
}
