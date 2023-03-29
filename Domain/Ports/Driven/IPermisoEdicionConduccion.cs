using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Ports.Driven
{
    public interface IPermisoEdicionConduccionCommand
    {
        Task AgregarPorOpcionAsync(int region, int anio, int mes);
        Task BorraPorOpcionAsync(int region, int anio, int mes);
    }

    public interface IPermisoEdicionConduccionQuery
    {
        Task<List<Permisosedicionprocesoconduccion>> ObtenerPermisosAsync();
        Task<Permisosedicionprocesoconduccion?> ObtenerPermisosPorOpcionAsync(int region, int anio, int mes);
    }
}
