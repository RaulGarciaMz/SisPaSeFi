using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Ports.Driving
{
    public interface IPermisoEdicionConduccionDtoCommand
    {
        Task AgregarPorOpcionAsync(int region, int anio, int mes, string usuario);
        Task BorraPorOpcionAsync(int region, int anio, int mes, string usuario);
    }

    public interface IPermisoEdicionConduccionDtoQuery
    {
        Task<List<Permisosedicionprocesoconduccion>> ObtenerPermisosAsync(string usuario);
        Task<Permisosedicionprocesoconduccion?> ObtenerPermisosPorOpcionAsync(int region, int anio, int mes, string usuario);
    }
}
