using Domain.Entities;
using Domain.Entities.Vistas;

namespace Domain.Ports.Driven
{
    public interface IMenuCommand
    {
        Task AgregarMenuAsync(string desplegado, string liga, string descripcion, int idPadre, int posicion);
        Task ActualizarMenuAsync(int idMenu, string desplegado, string liga, string descripcion, int idPadre, int posicion);
    }

    public interface IMenuQuery
    {
        Task<List<MenuVista>> ObtenerMenuPorCriterioPadreAsync(int padre);
        Task<List<MenuVista>> ObtenerMenuPorUsuarioAndCriterioPadreAsync(int idUsuario, int padre);
        Task<List<Menu>> ObtenerMenuPorDesplegadoAsync(string desplegado);
        Task<List<Menu>> ObtenerMenusConNombreIgualAlDelMenuIdAsync(string desplegado, int idMenu);
    }
}
