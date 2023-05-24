using Domain.DTOs;
using Domain.Enums;

namespace Domain.Ports.Driving
{
    public interface IPuntosDtoCommand
    {
        Task AgregaAsync(PuntoDtoForCreate pp, string usuario);
        Task UpdateAsync(PuntoDtoForUpdate pp);
        Task DeleteAsync(int id, string usuario);
    }

    public interface IPuntosDtoQuery
    {
        Task<List<PuntoDto>> ObtenerPorOpcionAsync(FiltroPunto opcion, string valor, string usuario);
    }
}
