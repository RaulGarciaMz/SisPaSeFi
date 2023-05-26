using Domain.Entities;
using Domain.Ports.Driven.Repositories;
using Microsoft.EntityFrameworkCore;
using SqlServerAdapter.Data;

namespace SqlServerAdapter
{
    public class PermisosEdicionConduccionRepository : IPermisosConduccionRepo
    {
        protected readonly PermisosEdicionConduccionContext _permisosContext;
        public PermisosEdicionConduccionRepository(PermisosEdicionConduccionContext permisosContext)
        {
            _permisosContext = permisosContext ?? throw new ArgumentNullException(nameof(permisosContext));
        }

        public async Task<List<PermisoEdicionProcesoConduccion>> ObtenerPermisosAsync()
        {
            return await _permisosContext.PermisosEdicionConduccion.OrderBy(x => x.Regionssf).ThenBy(x => x.Anio).ThenBy(x => x.Mes).ToListAsync();
        }

        public async Task<PermisoEdicionProcesoConduccion?> ObtenerPermisosPorOpcionAsync(int region, int anio, int mes)
        {
            return await _permisosContext.PermisosEdicionConduccion.Where(x => x.Regionssf == region && x.Anio == anio && x.Mes == mes).SingleOrDefaultAsync();
        }

        public async Task<int> ObtenerNumeroPermisosPorOpcionAsync(int region, int anio, int mes)
        {
            var r = await _permisosContext.PermisosEdicionConduccion.Where(x => x.Regionssf == region && x.Anio == anio && x.Mes == mes).ToListAsync();

            return r.Count;
        }

        public async Task AgregarPorOpcionAsync(int region, int anio, int mes)
        {
            var d = await ObtenerPermisosPorOpcionAsync(region, anio, mes);

            if (d == null)
            {
                var nvo = new PermisoEdicionProcesoConduccion() 
                { 
                    Regionssf = region,
                    Anio = anio,
                    Mes = mes
                };
                _permisosContext.PermisosEdicionConduccion.Add(nvo);
                await _permisosContext.SaveChangesAsync();
            }
        }

        public async Task BorraPorOpcionAsync(int region, int anio, int mes)
        {
            var d = await ObtenerPermisosPorOpcionAsync(region, anio, mes);

            if (d != null)
            {
                _permisosContext.PermisosEdicionConduccion.Remove(d);
                await _permisosContext.SaveChangesAsync();
            }
        }
    }
}
