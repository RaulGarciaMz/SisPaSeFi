using Domain.Entities;

namespace Domain.Ports.Driven.Repositories
{
    public interface IRolesCommand
    {
        Task AgregaRolAsync(string nombre, string descripcion, int idMenu);
        Task ActualizaRolAsync(int idRol, string nombre, string descripcion, int idMenu);
    }
    public interface IRolesQuery
    {
        Task<List<Rol>> ObtenerRolesAsync();
        Task<Rol?> ObtenerRolPorNombreAsync(string nombre);
        Task<Rol?> ObtenerRolPorNombreAndIdAsync(string nombre, int idRol);
    }
}
