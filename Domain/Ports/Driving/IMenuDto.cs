using Domain.DTOs;

namespace Domain.Ports.Driving
{
    public interface IMenuDtoCommand
    {
        Task AgregaMenuAsync(string opcion, MenuDto menu, string usuario);
        Task ActualizaMenuAsync(MenuDto menu, string usuario);
    }

    public interface IMenuDtoQuery
    {
        Task<List<MenuDto>> ObtenerMenuPorOpcionAsync(string opcion, int padre, string usuario);
    }
}
