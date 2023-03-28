using Domain.Entities;
using Domain.Entities.Vistas;
using Domain.Ports.Driven;
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
    public class LineasRepository : ILineasRepo
    {
        protected readonly LineasContext _lineaContext;

        public LineasRepository(LineasContext lineaContext)
        {
            _lineaContext = lineaContext ?? throw new ArgumentNullException(nameof(lineaContext));
        }

        public async Task<List<LineaVista>> ObtenerLineaPorClaveAsync(string clave)
        {
            string sqlQuery = @"SELECT a.id_linea, a.clave, a.descripcion, a.id_punto_inicio, b.ubicacion ubicacion_punto_inicio,
                                       d.nombre municipio_punto_inicio, f.nombre estado_punto_inicio, a.id_punto_fin,c.ubicacion ubicacion_punto_fin,
	                                   e.nombre municipio_punto_fin,g.nombre estado_punto_fin
                                FROM ssf.linea a
                                JOIN ssf.puntospatrullaje b ON a.id_punto_inicio=b.id_punto
                                JOIN ssf.puntospatrullaje c ON a.id_punto_fin=c.id_punto
                                JOIN ssf.municipios d ON  b.id_municipio=d.id_municipio
                                JOIN ssf.municipios e ON c.id_municipio=e.id_municipio
                                JOIN ssf.estadospais f ON d.id_estado=f.id_estado
                                JOIN ssf.estadospais g ON e.id_estado=g.id_estado
                                WHERE a.clave like @pCriterio ";

            object[] parametros = new object[]
            {
                new SqlParameter("@pCriterio", clave)
            };

            return await _lineaContext.LineasVista.FromSqlRaw(sqlQuery, parametros).ToListAsync();
        }

        public async Task<List<LineaVista>> ObtenerLineaPorUbicacionDePuntoInicioAsync(string ubicacion)
        {

            var ubiLike = "%" + ubicacion + "%";

            string sqlQuery = @"SELECT a.id_linea, a.clave, a.descripcion, a.id_punto_inicio, b.ubicacion ubicacion_punto_inicio,
                                       d.nombre municipio_punto_inicio, f.nombre estado_punto_inicio, a.id_punto_fin,c.ubicacion ubicacion_punto_fin,
	                                   e.nombre municipio_punto_fin,g.nombre estado_punto_fin
                                FROM ssf.linea a
                                JOIN ssf.puntospatrullaje b ON a.id_punto_inicio=b.id_punto
                                JOIN ssf.puntospatrullaje c ON a.id_punto_fin=c.id_punto
                                JOIN ssf.municipios d ON  b.id_municipio=d.id_municipio
                                JOIN ssf.municipios e ON c.id_municipio=e.id_municipio
                                JOIN ssf.estadospais f ON d.id_estado=f.id_estado
                                JOIN ssf.estadospais g ON e.id_estado=g.id_estado
                                WHERE b.ubicacion like @pCriterio ";

            object[] parametros = new object[]
            {
                new SqlParameter("@pCriterio", ubiLike)
            };

            return await _lineaContext.LineasVista.FromSqlRaw(sqlQuery, parametros).ToListAsync();
        }

        public async Task<List<LineaVista>> ObtenerLineaPorUbicacionDePuntoFinalAsync(string ubicacion)
        {

            var ubiLike = "%" + ubicacion + "%";

            string sqlQuery = @"SELECT a.id_linea, a.clave, a.descripcion, a.id_punto_inicio, b.ubicacion ubicacion_punto_inicio,
                                       d.nombre municipio_punto_inicio, f.nombre estado_punto_inicio, a.id_punto_fin,c.ubicacion ubicacion_punto_fin,
	                                   e.nombre municipio_punto_fin,g.nombre estado_punto_fin
                                FROM ssf.linea a
                                JOIN ssf.puntospatrullaje b ON a.id_punto_inicio=b.id_punto
                                JOIN ssf.puntospatrullaje c ON a.id_punto_fin=c.id_punto
                                JOIN ssf.municipios d ON  b.id_municipio=d.id_municipio
                                JOIN ssf.municipios e ON c.id_municipio=e.id_municipio
                                JOIN ssf.estadospais f ON d.id_estado=f.id_estado
                                JOIN ssf.estadospais g ON e.id_estado=g.id_estado
                                WHERE c.ubicacion like @pCriterio ";

            object[] parametros = new object[]
            {
                new SqlParameter("@pCriterio", ubiLike)
            };

            return await _lineaContext.LineasVista.FromSqlRaw(sqlQuery, parametros).ToListAsync();
        }

        public async Task<List<LineaVista>> ObtenerLineaDentroDeRecorridoDeRutaAsync(string recorrido)
        {
            string sqlQuery = @"SELECT DISTINCT b.id_linea, b.clave, b.descripcion, b.id_punto_inicio,c.ubicacion ubicacion_punto_inicio,
                                                e.nombre municipio_punto_inicio, g.nombre estado_punto_inicio, b.id_punto_fin,
                                				d.ubicacion ubicacion_punto_fin, f.nombre municipio_punto_fin, h.nombre estado_punto_fin
                                FROM ssf.estructura a 
                                JOIN ssf.linea b ON a.id_linea = b.id_linea
                                JOIN ssf.puntospatrullaje c ON b.id_punto_inicio=c.id_punto
                                JOIN ssf.puntospatrullaje d ON b.id_punto_fin=d.id_punto
                                JOIN ssf.municipios e ON c.id_municipio=e.id_municipio
                                JOIN ssf.municipios f ON d.id_municipio=f.id_municipio
                                JOIN ssf.estadospais g ON e.id_estado=g.id_estado
                                JOIN ssf.estadospais h ON f.id_estado=h.id_estado
                                ,(
                                	SELECT MAX(TRY_CAST(c.latitud AS FLOAT))+0.05 maxLatitud, MIN(TRY_CAST(c.latitud AS FLOAT))-0.05 minLatitud, 
                                	       MAX(TRY_CAST(c.longitud AS FLOAT))-0.01 minLongitud, MIN(TRY_CAST(c.longitud AS FLOAT))+0.01 maxLongitud 
                                	FROM ssf.rutas a
                                	JOIN ssf.itinerario b ON a.id_ruta=b.id_ruta
                                	JOIN ssf.puntospatrullaje c ON b.id_punto=c.id_punto
                                	WHERE a.id_ruta = @pCriterio 
                                ) cuadrante
                                WHERE a.latitud BETWEEN cuadrante.minLatitud AND cuadrante.maxLatitud
                                AND a.longitud BETWEEN cuadrante.minLongitud AND cuadrante.maxLongitud";

            object[] parametros = new object[]
            {
                new SqlParameter("@pCriterio", recorrido)
            };

            return await _lineaContext.LineasVista.FromSqlRaw(sqlQuery, parametros).ToListAsync();
        }

        public async Task<List<LineaVista>> ObtenerLineasDentroDeRadio5KmDeUnPuntoAsync(int idPunto)
        {
            string sqlQuery = @"SELECT DISTINCT b.id_linea, b.clave, b.descripcion, b.id_punto_inicio,c.ubicacion ubicacion_punto_inicio,
                                                e.nombre municipio_punto_inicio, g.nombre estado_punto_inicio, b.id_punto_fin,
                                				d.ubicacion ubicacion_punto_fin, f.nombre municipio_punto_fin, h.nombre estado_punto_fin
                                FROM ssf.estructura a 
                                JOIN ssf.linea b ON a.id_linea = b.id_linea
                                JOIN ssf.puntospatrullaje c ON b.id_punto_inicio=c.id_punto
                                JOIN ssf.puntospatrullaje d ON b.id_punto_fin=d.id_punto
                                JOIN ssf.municipios e ON c.id_municipio=e.id_municipio
                                JOIN ssf.municipios f ON d.id_municipio=f.id_municipio
                                JOIN ssf.estadospais g ON e.id_estado=g.id_estado
                                JOIN ssf.estadospais h ON f.id_estado=h.id_estado
                                ,(
                                	SELECT MAX(TRY_CAST(c.latitud AS FLOAT))+0.05 maxLatitud, MIN(TRY_CAST(c.latitud AS FLOAT))-0.05 minLatitud, 
                                	       MAX(TRY_CAST(c.longitud AS FLOAT))-0.01 minLongitud, MIN(TRY_CAST(c.longitud AS FLOAT))+0.01 maxLongitud 
                                	FROM ssf.rutas a
                                	JOIN ssf.itinerario b ON a.id_ruta=b.id_ruta
                                	JOIN ssf.puntospatrullaje c ON b.id_punto=c.id_punto
                                	WHERE c.id_punto = @pCriterio 
                                ) cuadrante
                                WHERE a.latitud BETWEEN cuadrante.minLatitud AND cuadrante.maxLatitud
                                AND a.longitud BETWEEN cuadrante.minLongitud AND cuadrante.maxLongitud";

            object[] parametros = new object[]
            {
                new SqlParameter("@pCriterio", idPunto)
            };

            return await _lineaContext.LineasVista.FromSqlRaw(sqlQuery, parametros).ToListAsync();
        }

        public async Task<List<LineaVista>> ObtenerLineasDentroDeRadio5KmDeUnasCoordenadasAsync(string latitud, string longitud)
        {
            string sqlQuery = @"SELECT DISTINCT b.id_linea, b.clave, b.descripcion, b.id_punto_inicio,c.ubicacion ubicacion_punto_inicio,
                                                e.nombre municipio_punto_inicio, g.nombre estado_punto_inicio, b.id_punto_fin,
                                				d.ubicacion ubicacion_punto_fin, f.nombre municipio_punto_fin, h.nombre estado_punto_fin
                                FROM ssf.estructura a 
                                JOIN ssf.linea b ON a.id_linea = b.id_linea
                                JOIN ssf.puntospatrullaje c ON b.id_punto_inicio=c.id_punto
                                JOIN ssf.puntospatrullaje d ON b.id_punto_fin=d.id_punto
                                JOIN ssf.municipios e ON c.id_municipio=e.id_municipio
                                JOIN ssf.municipios f ON d.id_municipio=f.id_municipio
                                JOIN ssf.estadospais g ON e.id_estado=g.id_estado
                                JOIN ssf.estadospais h ON f.id_estado=h.id_estado
                                ,(
                                  SELECT CAST(@pLatitud AS FLOAT)+0.05 maxLatitud,  CAST(@pLatitud AS FLOAT)-0.05 minLatitud, 
                                         CAST(@pLongitud AS FLOAT)-0.01 minLongitud, CAST(@pLongitud AS FLOAT)+0.01 maxLongitud 
                                ) cuadrante
                                WHERE a.latitud BETWEEN cuadrante.minLatitud AND cuadrante.maxLatitud
                                AND a.longitud BETWEEN cuadrante.minLongitud AND cuadrante.maxLongitud";

            object[] parametros = new object[]
            {
                new SqlParameter("@pLatitud", latitud),
                new SqlParameter("@pLongitud", longitud)
            };

            return await _lineaContext.LineasVista.FromSqlRaw(sqlQuery, parametros).ToListAsync();
        }

        public async Task AgregarAsync(string clave, string descripcion, int idPuntoInicio, int idPuntoFin)
        {

            var l = new Linea() 
            {
                Clave = clave,
                Descripcion = descripcion,
                IdPuntoInicio = idPuntoInicio,
                IdPuntoFin = idPuntoFin
            };

            _lineaContext.Add(l);

            await _lineaContext.SaveChangesAsync();
        }

        public async Task ActualizaAsync(int idLinea, string clave, string descripcion, int idPuntoInicio, int idPuntoFin, int idUsuario, int bloqueado)
        {

            var l = await _lineaContext.Lineas.Where(x => x.IdLinea == idLinea).SingleOrDefaultAsync();

            if (l != null)
            { 
                l.Clave = clave;
                l.Descripcion= descripcion;
                l.IdPuntoInicio= idPuntoInicio;
                l.IdPuntoFin= idPuntoFin;
                l.IdUsuario= idUsuario;
                l.Bloqueado= bloqueado;

                _lineaContext.Update(l);
                await _lineaContext.SaveChangesAsync();
            }
        }

        public async Task BorraAsync(int idLinea)
        {

            var l = await _lineaContext.Lineas.Where(x => x.IdLinea == idLinea).SingleOrDefaultAsync();

            _lineaContext.Remove(l);

            await _lineaContext.SaveChangesAsync();
        }
    }
}
