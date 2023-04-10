using Domain.Entities;
using Domain.Entities.Vistas;
using Domain.Ports.Driven;
using Domain.Ports.Driven.Repositories;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using SqlServerAdapter.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace SqlServerAdapter
{
    public class EstructurasRepository : IEstructurasRepo
    {
        protected readonly EstructurasContext _estructuraContext;

        public EstructurasRepository(EstructurasContext estructuraContext)
        {
            _estructuraContext = estructuraContext ?? throw new ArgumentNullException(nameof(estructuraContext));
        }


        public async Task ActualizaUbicacionAsync(int idEstructura, string nombre, int idMunicipio, string latitud, string longitud)
        {
            var estructura = await _estructuraContext.Estructuras.Where(x => x.IdEstructura == idEstructura).FirstOrDefaultAsync();

            if (estructura != null) 
            {
                estructura.Latitud = latitud;
                estructura.Longitud = longitud;
                estructura.Coordenadas = latitud + "," + longitud;
                estructura.Nombre = nombre;
                estructura.IdMunicipio = idMunicipio;

                _estructuraContext.Estructuras.Update(estructura);

                await _estructuraContext.SaveChangesAsync();
            }
        }
        public async Task AgregaAsync(int idLinea, string nombre, int idMunicipio, string latitud, string longitud)
        {
            var estruct = new Estructura()
            { 
                Nombre= nombre,
                IdLinea = idLinea,
                IdMunicipio= idMunicipio,
                Latitud = latitud,
                Longitud = longitud,
                Coordenadas = latitud + "," + longitud
            };

            _estructuraContext.Estructuras.Add(estruct);
            await _estructuraContext.SaveChangesAsync();
        }

        public async Task<List<EstructurasVista>> ObtenerEstructuraPorLineaAsync(int idLinea)
        {
            string sqlQuery = @"SELECT a.id_estructura, a.nombre,  a.id_procesoresponsable, a.id_gerenciadivision,
                                       a.latitud, a.longitud, a.coordenadas, a.id_municipio,
                                       b.id_linea, b.clave, b.descripcion, c.nombre municipio,
                                       d.id_estado, d.nombre estado                                       
                                FROM ssf.estructura a
                                JOIN ssf.linea b ON a.id_linea = b.id_linea
                                JOIN ssf.municipios c ON a.id_municipio = c.id_municipio
                                JOIN ssf.estadospais d ON c.id_estado = d.id_estado
                                WHERE a.id_linea = @pLinea";

            object[] parametros = new object[]
            {
                new SqlParameter("@pLinea", idLinea)
            };

            return await _estructuraContext.EstructurasVistas.FromSqlRaw(sqlQuery, parametros).ToListAsync();
        }

        public async Task<List<EstructurasVista>> ObtenerEstructuraPorLineaEnRutaAsync(int idLinea, int idRuta)
        {
            string sqlQuery = @"SELECT a.id_estructura, a.nombre, a.id_procesoresponsable,a.id_gerenciadivision
                                       a.latitud, a.longitud, a.coordenadas, a.id_municipio,
                                       b.id_linea, b.clave, b.descripcion, c.nombre municipio,
                                       d.id_estado,d.nombre estado
                                FROM ssf.estructura a
                                JOIN ssf.linea b ON a.id_linea = b.id_linea
                                JOIN ssf.municipios c ON a.id_municipio = c.id_municipio
                                JOIN ssf.estadospais d ON c.id_estado = d.id_estado,
                                (
                                    SELECT MAX(TRY_CAST(c.latitud as float)) + 0.05 maxLatitud, 
                                           MIN(TRY_CAST(c.latitud as float)) - 0.05 minLatitud, 
                                	       MAX(TRY_CAST(c.longitud as float)) - 0.01 minLongitud, 
                                           MIN(TRY_CAST(c.longitud as float)) + 0.01 maxLongitud
                                    FROM ssf.rutas a                                
                                    JOIN ssf.itinerario b ON a.id_ruta = b.id_ruta                                
                                    JOIN ssf.puntospatrullaje c ON b.id_punto = c.id_punto                                
                                    WHERE a.id_ruta = @pRuta
                                ) cuadrante
                                WHERE
                                a.id_linea = @pLinea
                                 AND a.latitud BETWEEN cuadrante.minLatitud AND cuadrante.maxLatitud
                                AND a.longitud BETWEEN cuadrante.minLongitud AND cuadrante.maxLongitud";

            object[] parametros = new object[]
            {
                new SqlParameter("@pRuta", idRuta),
                new SqlParameter("@pLinea", idLinea)
            };

            return await _estructuraContext.EstructurasVistas.FromSqlRaw(sqlQuery, parametros).ToListAsync();
        }

        public async Task<List<EstructurasVista>> ObtenerEstructuraAlrededorDeCoordenadaAsync(float coordX, float coordY)
        {
            string sqlQuery = @"SELECT a.id_estructura, a.nombre, a.id_procesoresponsable, a.id_gerenciadivision
                                       a.latitud, a.longitud, a.coordenadas, a.id_municipio,
                                       b.id_linea, b.clave, b.descripcion , c.nombre municipio,
                                       d.id_estado, d.nombre estado    
                                FROM ssf.estructura a
                                JOIN ssf.linea b ON a.id_linea = b.id_linea
                                JOIN ssf.municipios c ON a.id_municipio = c.id_municipio
                                JOIN ssf.estadospais d ON c.id_estado = d.id_estado,
                                (
                                SELECT @pLatitud+0.05 maxLatitud, @pLongitud-0.05 minLatitud, @pLatitud-0.01 minLongitud, @pLatitud+0.01 maxLongitud 
                                ) cuadrante
                                WHERE a.latitud BETWEEN cuadrante.minLatitud AND cuadrante.maxLatitud
                                AND a.longitud BETWEEN cuadrante.minLongitud AND cuadrante.maxLongitud";

            object[] parametros = new object[]
            {
                new SqlParameter("@pRuta", coordX),
                new SqlParameter("@pLinea", coordY)
            };

            return await _estructuraContext.EstructurasVistas.FromSqlRaw(sqlQuery, parametros).ToListAsync();
        }

        public async Task<EstructurasVista?> ObtenerEstructuraPorIdAsync(int idEstructura)
        {
            string sqlQuery = @"SELECT a.id_estructura, a.nombre, a.id_procesoresponsable, a.id_gerenciadivision
                                       a.latitud, a.longitud, a.coordenadas, a.id_municipio,
                                       b.id_linea, b.clave, b.descripcion , c.nombre municipio,
                                       d.id_estado, d.nombre estado    
                                FROM ssf.estructura a
                                JOIN ssf.linea b ON a.id_linea = b.id_linea
                                JOIN ssf.municipios c ON a.id_municipio = c.id_municipio
                                JOIN ssf.estadospais d ON c.id_estado = d.id_estado
                                WHERE a.id_estructura = @pEstructura";

            object[] parametros = new object[]
            {

                new SqlParameter("@pEstructura", idEstructura)
            };

            return await _estructuraContext.EstructurasVistas.FromSqlRaw(sqlQuery, parametros).SingleOrDefaultAsync();
        }

        public async Task<List<Estructura>> ObtenerEstructurasEnCoordenadasPorId(int estructura, string coordenadas)
        {
            return await _estructuraContext.Estructuras.Where(x => x.IdEstructura == estructura && x.Coordenadas != coordenadas).ToListAsync();
        }

        public async Task<List<Estructura>> ObtenerEstructurasEnCoordenadas(string coordenadas)
        {
            return await _estructuraContext.Estructuras.Where(x => x.Coordenadas == coordenadas).ToListAsync();
        }

        public async Task<bool> SaveChangesAsync()
        {
            return (await _estructuraContext.SaveChangesAsync() >= 0);
        }
    }
}
