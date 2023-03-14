using Domain.Entities;
using Domain.Entities.Vistas;
using Domain.Ports.Driven.Repositories;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using SqlServerAdapter.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SqlServerAdapter
{
    public class VehiculoPatrullajeRepository : IVehiculosPatrullajeRepo
    {
        protected readonly VehiculosPatrullajeContext _vehiculoPatContext;

        public VehiculoPatrullajeRepository(VehiculosPatrullajeContext vehiculoContext)
        {
            _vehiculoPatContext = vehiculoContext ?? throw new ArgumentNullException(nameof(vehiculoContext));
        }

        public async Task ActualizaAsync(int idVehiculo, string matricula, string numeroEconomico, int habilitado, int tipoPatrullaje, int tipoVehiculo)
        {
                        var veh = await _vehiculoPatContext.Vehiculos.Where(x => x.IdVehiculo == idVehiculo).SingleOrDefaultAsync();

            if (veh != null) 
            {
                veh.IdTipoPatrullaje = tipoPatrullaje;
                veh.IdTipoVehiculo = tipoVehiculo;
                veh.Habilitado = habilitado;
                veh.Matricula = matricula;
                veh.NumeroEconomico = numeroEconomico;

                _vehiculoPatContext.Vehiculos.Update(veh);

                await _vehiculoPatContext.SaveChangesAsync();
            }
        }

        public async Task AgregaAsync(string matricula, string numeroEconomico, int habilitado, int tipoPatrullaje, int tipoVehiculo, int comandancia)
        {
            var v = new Vehiculo() 
            {
                Matricula = matricula,
                NumeroEconomico = numeroEconomico,
                Habilitado = habilitado,
                IdComandancia = comandancia,
                IdTipoPatrullaje = tipoPatrullaje,
                IdTipoVehiculo = tipoVehiculo,
            };   

            _vehiculoPatContext.Vehiculos.Add(v);

            await _vehiculoPatContext.SaveChangesAsync();
        }


        public async Task<List<VehiculoPatrullajeVista>> ObtenerVehiculosPorRegionAsync(int region)
        {
            string sqlQuery = @"SELECT a.id_vehiculo, a.id_tipopatrullaje, a.matricula, a.id_comandancia, a.id_tipovehiculo,
                                       CASE  WHEN a.numeroeconomico IS NULL THEN '' ELSE a.numeroeconomico END numeroeconomico,
                                       a.habilitado, b.descripcion, c.descripciontipovehiculo
                                FROM ssf.vehiculos a
                                JOIN ssf.tipopatrullaje b ON a.id_tipopatrullaje = b.id_tipopatrullaje
                                JOIN ssf.tipovehiculo c ON a.id_tipovehiculo=c.id_tipovehiculo
                                WHERE a.id_comandancia = @pRegion";

            object[] parametros = new object[]
            {
                new SqlParameter("@pRegion", region)
            };

            return await _vehiculoPatContext.VehiculosPatrullajeVistas.FromSqlRaw(sqlQuery, parametros).ToListAsync();
        }

        public async Task<List<VehiculoPatrullajeVista>> ObtenerVehiculosPorRegionCriterioAsync(int region, string criterio)
        {
            string sqlQuery = @"SELECT a.id_vehiculo, a.id_tipopatrullaje, a.matricula, a.id_comandancia, a.id_tipovehiculo,
                                       CASE  WHEN a.numeroeconomico IS NULL THEN '' ELSE a.numeroeconomico END numeroeconomico,
                                       a.habilitado, b.descripcion, c.descripciontipovehiculo
                                FROM ssf.vehiculos a
                                JOIN ssf.tipopatrullaje b ON a.id_tipopatrullaje = b.id_tipopatrullaje
                                JOIN ssf.tipovehiculo c ON a.id_tipovehiculo=c.id_tipovehiculo
                                WHERE a.id_comandancia = @pRegion
                                AND (a.matricula like @pCriterio OR a.numeroeconomico like @pCriterio)";

            object[] parametros = new object[]
            {
                new SqlParameter("@pRegion", region),
                new SqlParameter("@pCriterio", criterio)
            };

            return await _vehiculoPatContext.VehiculosPatrullajeVistas.FromSqlRaw(sqlQuery, parametros).ToListAsync();
        }

        public async Task<List<VehiculoPatrullajeVista>> ObtenerVehiculosPorRegionParaPatrullajeAereoAsync(int region)
        {
            string sqlQuery = @"SELECT a.id_vehiculo, a.id_tipopatrullaje, a.matricula, a.id_comandancia, a.id_tipovehiculo,
                                       CASE  WHEN a.numeroeconomico IS NULL THEN '' ELSE a.numeroeconomico END numeroeconomico,
                                       a.habilitado, b.descripcion, c.descripciontipovehiculo
                                FROM ssf.vehiculos a
                                JOIN ssf.tipopatrullaje b ON a.id_tipopatrullaje = b.id_tipopatrullaje
                                JOIN ssf.tipovehiculo c ON a.id_tipovehiculo=c.id_tipovehiculo
                                WHERE b.descripcion = 'AEREO' and a.id_comandancia = @pRegion";

            object[] parametros = new object[]
            {
                new SqlParameter("@pRegion", region)
            };

            return await _vehiculoPatContext.VehiculosPatrullajeVistas.FromSqlRaw(sqlQuery, parametros).ToListAsync();
        }

        public async Task<List<VehiculoPatrullajeVista>> ObtenerVehiculosPorRegionCriterioParaPatrullajeAereoAsync(int region, string criterio)
        {
            string sqlQuery = @"SELECT a.id_vehiculo, a.id_tipopatrullaje, a.matricula, a.id_comandancia, a.id_tipovehiculo,
                                       CASE  WHEN a.numeroeconomico IS NULL THEN '' ELSE a.numeroeconomico END numeroeconomico,
                                       a.habilitado, b.descripcion, c.descripciontipovehiculo
                                FROM ssf.vehiculos a
                                JOIN ssf.tipopatrullaje b ON a.id_tipopatrullaje = b.id_tipopatrullaje
                                JOIN ssf.tipovehiculo c ON a.id_tipovehiculo=c.id_tipovehiculo
                                WHERE b.descripcion = 'AEREO' and a.id_comandancia = @pRegion
                                AND (a.matricula like @pCriterio OR a.numeroeconomico like @pCriterio)";

            object[] parametros = new object[]
            {
                new SqlParameter("@pRegion", region),
                new SqlParameter("@pCriterio", criterio)
            };

            return await _vehiculoPatContext.VehiculosPatrullajeVistas.FromSqlRaw(sqlQuery, parametros).ToListAsync();
        }

        public async Task<List<VehiculoPatrullajeVista>> ObtenerVehiculosPorRegionParaPatrullajeTerrestreAsync(int region)
        {
            string sqlQuery = @"SELECT a.id_vehiculo, a.id_tipopatrullaje, a.matricula, a.id_comandancia, a.id_tipovehiculo,
                                       CASE  WHEN a.numeroeconomico IS NULL THEN '' ELSE a.numeroeconomico END numeroeconomico,
                                       a.habilitado, b.descripcion, c.descripciontipovehiculo
                                FROM ssf.vehiculos a
                                JOIN ssf.tipopatrullaje b ON a.id_tipopatrullaje = b.id_tipopatrullaje
                                JOIN ssf.tipovehiculo c ON a.id_tipovehiculo=c.id_tipovehiculo
                                WHERE b.descripcion = 'TERRESTRE' and a.id_comandancia = @pRegion";

            object[] parametros = new object[]
            {
                new SqlParameter("@pRegion", region)
            };

            return await _vehiculoPatContext.VehiculosPatrullajeVistas.FromSqlRaw(sqlQuery, parametros).ToListAsync();
        }

        public async Task<List<VehiculoPatrullajeVista>> ObtenerVehiculosPorRegionCriterioParaPatrullajeTerrestreAsync(int region, string criterio)
        {
            string sqlQuery = @"SELECT a.id_vehiculo, a.id_tipopatrullaje, a.matricula, a.id_comandancia, a.id_tipovehiculo,
                                       CASE  WHEN a.numeroeconomico IS NULL THEN '' ELSE a.numeroeconomico END numeroeconomico,
                                       a.habilitado, b.descripcion, c.descripciontipovehiculo
                                FROM ssf.vehiculos a
                                JOIN ssf.tipopatrullaje b ON a.id_tipopatrullaje = b.id_tipopatrullaje
                                JOIN ssf.tipovehiculo c ON a.id_tipovehiculo=c.id_tipovehiculo
                                WHERE b.descripcion = 'TERRESTRE' and a.id_comandancia = @pRegion
                                AND (a.matricula like @pCriterio OR a.numeroeconomico like @pCriterio)";

            object[] parametros = new object[]
            {
                new SqlParameter("@pRegion", region),
                new SqlParameter("@pCriterio", criterio)
            };

            return await _vehiculoPatContext.VehiculosPatrullajeVistas.FromSqlRaw(sqlQuery, parametros).ToListAsync();
        }

        public async Task<List<VehiculoPatrullajeVista>> ObtenerVehiculosPatrullajeExtraordinarioPorDescripcionAsync(int idPropuesta, string descripcion)
        {
            string sqlQuery = @"SELECT a.id_vehiculo, a.id_tipopatrullaje, a.matricula, a.id_comandancia, a.id_tipovehiculo,
                                       CASE  WHEN a.numeroeconomico IS NULL THEN '' ELSE a.numeroeconomico END numeroeconomico,
                                       a.habilitado, b.descripcion, c.descripciontipovehiculo
                                FROM ssf.vehiculos a
                                JOIN ssf.tipopatrullaje b ON a.id_tipopatrullaje = b.id_tipopatrullaje
                                JOIN ssf.tipovehiculo c ON a.id_tipovehiculo=c.id_tipovehiculo
                                JOIN ssf.propuestaspatrullajesvehiculos d ON a.id_vehiculo=d.id_vehiculo
                                WHERE b.descripcion = @pDescripcion and d.id_propuestapatrullaje = @pPropuesta";

            object[] parametros = new object[]
            {
                new SqlParameter("@pDescripcion", descripcion),
                new SqlParameter("@pPropuesta", idPropuesta)
            };

            return await _vehiculoPatContext.VehiculosPatrullajeVistas.FromSqlRaw(sqlQuery, parametros).ToListAsync();
        }
    }
}
