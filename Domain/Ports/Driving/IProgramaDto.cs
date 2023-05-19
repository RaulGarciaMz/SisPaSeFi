using Domain.DTOs;
using Domain.Enums;

namespace Domain.Ports.Driving
{
    public interface IProgramaDtoCommand
    {
        Task AgregaProgramaAsync(string opcion, string clase, ProgramaDtoForCreateWithListas p);
        Task AgregaPropuestasComoProgramasAsync(List<ProgramaDtoForCreate> p, string usuario);
        Task ActualizaProgramasOrPropuestasPorOpcionAsync(ProgramaDtoForUpdatePorOpcion p, string opcion, string usuario);
        Task ActualizaPropuestasOrProgramasPorOpcionAndAccionAsync(List<PropuestaDtoForListaUpdate> p, string opcion, int accion, string usuario);
        Task ActualizaProgramaPorCambioDeRutaAsync(ProgramaDtoForUpdateRuta p, string usuario);
        Task ActualizaProgramasPorInicioPatrullajeAsync(ProgramaDtoForUpdateInicio p, string usuario);
        Task DeletePorOpcionAsync(string opcion, int id, string usuario);
    }
    public interface IProgramaDtoQuery
    {
        Task<List<PatrullajeDto>> ObtenerPorFiltroAsync(string usuario, string tipo, int region, string clase,int anio, int mes, int dia=1, FiltroProgramaOpcion opcion = FiltroProgramaOpcion.ExtraordinariosyProgramados, PeriodoOpcion periodo = PeriodoOpcion.UnDia);
        Task<PatrullajeSoloDto> ObtenerProgramaPorIdAsync(int idPrograma, string usuario);
    }
}
