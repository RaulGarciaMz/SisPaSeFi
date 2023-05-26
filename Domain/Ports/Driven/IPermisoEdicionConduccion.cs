using Domain.Entities;

namespace Domain.Ports.Driven
{
    public interface IPermisoEdicionConduccionCommand
    {
        Task AgregarPorOpcionAsync(int region, int anio, int mes);
        Task BorraPorOpcionAsync(int region, int anio, int mes);
    }

    public interface IPermisoEdicionConduccionQuery
    {
        Task<List<PermisoEdicionProcesoConduccion>> ObtenerPermisosAsync();
        Task<PermisoEdicionProcesoConduccion?> ObtenerPermisosPorOpcionAsync(int region, int anio, int mes);
        Task<int> ObtenerNumeroPermisosPorOpcionAsync(int region, int anio, int mes);
    }
}
