using Domain.Entities;
using Domain.Entities.Vistas;
using Domain.Ports.Driven.Repositories;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using SqlServerAdapter.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace SqlServerAdapter
{
    public class PermisosEdicionConduccionRepository : IPermisosConduccionRepo
    {
        protected readonly PermisosEdicionConduccionContext _permisosContext;
        public PermisosEdicionConduccionRepository(PermisosEdicionConduccionContext permisosContext)
        {
            _permisosContext = permisosContext ?? throw new ArgumentNullException(nameof(permisosContext));
        }

        public async Task<List<Permisosedicionprocesoconduccion>> ObtenerPermisosAsync()
        {
            return await _permisosContext.PermisosEdicionConduccion.OrderBy(x => x.Regionssf).ThenBy(x => x.Anio).ThenBy(x => x.Mes).ToListAsync();
        }

        public async Task<Permisosedicionprocesoconduccion?> ObtenerPermisosPorOpcionAsync(int region, int anio, int mes)
        {
            return await _permisosContext.PermisosEdicionConduccion.Where(x => x.Regionssf == region && x.Anio == anio && x.Mes == mes ).SingleOrDefaultAsync();
        }

        public async Task AgregarPorOpcionAsync(int region, int anio, int mes)
        {
            var d = await ObtenerPermisosPorOpcionAsync(region, anio, mes);

            if (d == null)
            {
                var nvo = new Permisosedicionprocesoconduccion() 
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
