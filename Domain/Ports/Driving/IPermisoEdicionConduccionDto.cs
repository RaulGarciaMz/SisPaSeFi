using Domain.Entities;

namespace Domain.Ports.Driving
{
    public interface IPermisoEdicionConduccionDtoCommand
    {
        Task AgregarPorOpcionAsync(int region, int anio, int mes, string usuario);
        Task BorraPorOpcionAsync(int region, int anio, int mes, string usuario);
    }

    public interface IPermisoEdicionConduccionDtoQuery
    {
        Task<List<PermisoEdicionProcesoConduccion>> ObtenerPermisosAsync(string usuario);
        Task<int> ObtenerNumeroDePermisosEspecificoPorOpcionAsync(int region, int anio, int mes, string usuario);
    }
}
