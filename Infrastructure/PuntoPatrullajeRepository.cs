using Domain.Entities;
using Domain.Ports.Driven.Repositories;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using SqlServerAdapter.Data;

namespace SqlServerAdapter
{
    public class PuntoPatrullajeRepository : IPuntoPatrullajeRepo
    {
        protected readonly PatrullajeContext _patrullajeContext;

        public PuntoPatrullajeRepository(PatrullajeContext patrullajeContext) 
        {
            _patrullajeContext = patrullajeContext ?? throw new ArgumentNullException(nameof(patrullajeContext));
        }

        /// <summary>
        /// Método <c>ObtenerPorEstado</c> Obtiene puntos de patrullaje pertenecientes al estado indicado
        /// </summary>
        public async Task<List<PuntoPatrullaje>> ObtenerPorEstadoAsync(int id_estado)
        {
            return await _patrullajeContext.puntospatrullaje
                .Include(m => m.IdMunicipioNavigation.IdEstadoNavigation)
                .Where(c => c.IdMunicipioNavigation.IdEstado == id_estado)
                .ToListAsync();
        }

        /// <summary>
        /// Método <c>ObtenerPorUbicacion</c> Obtiene puntos de patrullaje cuya ubicación (nombre) coincida con el parámetro
        /// </summary>
        public async Task<List<PuntoPatrullaje>> ObtenerPorUbicacionAsync(string ubicacion)
        {
            return await _patrullajeContext.puntospatrullaje
                .Include(m => m.IdMunicipioNavigation.IdEstadoNavigation)
                .Where(e => e.Ubicacion == ubicacion)
                .ToListAsync();
        }

        /// <summary>
        /// Método <c>ObtenerPorRutaAsync</c> Obtiene puntos de patrullaje cuya ruta coincida con el parámetro. se limita a  sólo instalaciones
        /// </summary>
        public async Task<List<PuntoPatrullaje>> ObtenerPorRutaAsync(int ruta)
        {
            string sqlQuery = @" SELECT a.*
                                 FROM ssf.puntospatrullaje a
                                 JOIN ssf.municipios b ON a.id_municipio=b.id_municipio
                                 JOIN ssf.estadospais c on b.id_estado=c.id_estado
                                  ,(
                                  SELECT MAX(TRY_CAST(c.latitud as DECIMAL(10,5)))+0.15 maxLatitud, MIN(TRY_CAST(c.latitud as DECIMAL(10,5)))-0.15 minLatitud, 
                                         MIN(TRY_CAST(c.longitud as DECIMAL(10,5)))-0.11 minLongitud, MAX(TRY_CAST(c.longitud as DECIMAL(10,5)))+0.11 maxLongitud 
                                  FROM ssf.rutas a
                                  JOIN ssf.itinerario b ON a.id_ruta=b.id_ruta
                                  JOIN ssf.puntospatrullaje c ON b.id_punto=c.id_punto
                                  WHERE a.id_ruta = @pRuta 
                                  ) cuadrante
                                 WHERE a.esInstalacion=1
                                  AND TRY_CAST(a.latitud AS FLOAT) BETWEEN cuadrante.minLatitud AND cuadrante.maxLatitud
                                  AND TRY_CAST(a.longitud AS FLOAT) BETWEEN cuadrante.minLongitud AND cuadrante.maxLongitud
                                 ORDER BY a.ubicacion";

            object[] parametros = new object[]
            {
                new SqlParameter("@pRuta", ruta)
            };


            return await _patrullajeContext.puntospatrullaje.FromSqlRaw(sqlQuery, parametros).ToListAsync();
        }

        /// <summary>
        /// Método <c>ObtenerPorRegionAsync</c> Obtiene puntos de patrullaje cuya comandancia coincida con el parámetro. se limita a sólo instalaciones estratégicas
        /// </summary>
        public async Task<List<PuntoPatrullaje>> ObtenerPorRegionAsync(int region, int nivel)
        {
            string sqlQuery = @"SELECT a.*
                                FROM ssf.puntospatrullaje a
                                JOIN ssf.municipios b ON a.id_municipio=b.id_municipio
                                JOIN ssf.estadospais c ON b.id_estado=c.id_estado
                                WHERE  a.esInstalacion=1 AND a.id_comandancia= @pRegion
                                AND a.id_nivelriesgo=@pNivel
                                ORDER BY a.ubicacion";

            object[] parametros = new object[]
            {
                new SqlParameter("@pRegion", region),
                new SqlParameter("@pNivel", nivel)
            };

            return await _patrullajeContext.puntospatrullaje.FromSqlRaw(sqlQuery, parametros).ToListAsync();
        }

        /// <summary>
        /// Método <c>Agrega</c> implementa la interface para registrar puntos de patrullaje
        /// </summary>
        public async Task Agrega(PuntoPatrullaje pp)
        {
            _patrullajeContext.Add(pp);
            await _patrullajeContext.SaveChangesAsync();
        }

        /// <summary>
        /// Método <c>Update</c> implementa la interface para actualizar puntos de patrullaje.
        /// </summary>
        public async Task Update(PuntoPatrullaje pp)
        {
            _patrullajeContext.Update(pp);
            await _patrullajeContext.SaveChangesAsync();
        }

        /// <summary>
        /// Método <c>Delete</c> implementa la interface para eliminar el punto de patrullaje indicado, siempre y cuando no esté bloqueado
        /// </summary>
        public async Task Delete(int id)
        {
            var pp = _patrullajeContext.puntospatrullaje
                .Where(x => x.IdPunto == id && x.Bloqueado == 0)
                .FirstOrDefault();

            if (pp != null)
            {
                _patrullajeContext.Remove(pp);
                await _patrullajeContext.SaveChangesAsync();
            }            
        }

        public async Task<int> ObtenerItinerariosPorPuntoAsync(int id)
        {
            return await _patrullajeContext.Itinerarios.Where(x => x.IdPunto == id).CountAsync();
        }

        public async Task<int> ObtenerIdUsuarioConfiguradorAsync(string usuario_nom)
        {
            var user = await _patrullajeContext.Usuarios.Where(x => x.UsuarioNom == usuario_nom && x.Configurador == 1).Select(x => x.IdUsuario).ToListAsync();

            if (user.Count == 0)
            {
                return -1;
            }
            else
            {
                return user[0];
            }
        }

        public async Task<bool> SaveChangesAsync() 
        {
            return (await _patrullajeContext.SaveChangesAsync() >= 0 );
        }
    }











}