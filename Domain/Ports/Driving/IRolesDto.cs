using Domain.DTOs;

namespace Domain.Ports.Driving
{
    public interface IRolesDtoCommand
    {
        Task AgregaRolAsync(string nombre, string descripcion, int idMenu, string usuario);
        Task ActualizaRolPorOpcionAsync(int idRol, string nombre, string descripcion, int idMenu, string dato, string opcion, string usuario);
    }

    public interface IRolesDtoQuery
    {
        Task<List<RolDto>> ObtenerRolesAsync(string usuario);
    }
}
