﻿using Domain.DTOs;
using Domain.Entities;
using Domain.Entities.Vistas;
using Domain.Ports.Driven.Repositories;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using SqlServerAdapter.Data;

namespace SqlServerAdapter
{
    public class RutaRepository : IRutasRepo
    {
        protected readonly RutaContext _rutaContext;

        public RutaRepository(RutaContext rutaContext)
        {
            _rutaContext = rutaContext ?? throw new ArgumentNullException(nameof(rutaContext));
        }

        /// <summary>
        /// Método <c>AgregaAsync</c> Implementa la interfaz para agregar una ruta junto con sus itinerarios
        /// </summary>
        public async Task AgregaTransacionalAsync(Ruta r, List<Itinerario> itin)
        {
            using (var transaction = _rutaContext.Database.BeginTransaction())
            {
                try
                {
                    _rutaContext.Rutas.Add(r);
                    await _rutaContext.SaveChangesAsync();

                    foreach (var item in itin)
                    {
                        item.IdRuta = r.IdRuta;

                    }

                    _rutaContext.Itinerarios.AddRange(itin);
                    await _rutaContext.SaveChangesAsync();

                    transaction.Commit();
                }
                catch (Exception)
                {
                    transaction.Rollback();
                    throw;
                }
            }
        }

        /// <summary>
        /// Método <c>UpdateAsync</c> Implementa la interfaz para actualizar una ruta junto con sus itinerarios
        /// </summary>
        public async Task UpdateAsync(RutaDtoForUpdate r)
        {
            var ruta = await _rutaContext.Rutas.Where(x => x.IdRuta == r.intIdRuta).SingleOrDefaultAsync();

            if (ruta != null)
            {
                ruta.Clave = r.strClave;
                ruta.RegionMilitarSdn = r.intRegionMilitarSDN.ToString();
                ruta.RegionSsf = r.intRegionSSF.ToString();
                ruta.ZonaMilitarSdn = r.intZonaMilitarSDN;
                ruta.Observaciones = r.strObservaciones;
                ruta.Bloqueado = r.intBloqueado;
                ruta.Habilitado = r.intHabilitado;
                ruta.UltimaActualizacion = DateTime.UtcNow;

                _rutaContext.Rutas.Update(ruta);

                var itinerario = await ObtenerItinerariosPorRutaAsync(r.intIdRuta);

                if (itinerario != null && itinerario.itinerarioruta != r.strItinerario)
                {
                    var i = await _rutaContext.Itinerarios.Where(x => x.IdRuta == r.intIdRuta).ToListAsync();
                    if (i.Count > 0) 
                    {
                        _rutaContext.Itinerarios.RemoveRange(i);

                        foreach (var item in r.objRecorridoRuta)
                        {
                            var nIti = new Itinerario()
                            { 
                                IdRuta = r.intIdRuta,
                                IdPunto = item.intIdPunto,
                                Posicion = item.intPosicion,
                                UltimaActualizacion = DateTime.UtcNow
                            };

                            _rutaContext.Itinerarios.Add(nIti);
                        }
                    }
                }

                await _rutaContext.SaveChangesAsync();
            }
        }

        /// <summary>
        /// Método <c>UpdateAsync</c> Implementa la interfaz para actualizar una ruta junto con sus itinerarios
        /// </summary>
        public async Task ReiniciaRegionMilitarAsync(string regionMilitar, string tipoPatrullaje)
        {
            string sqlQuery = @"SELECT a.*
											FROM ssf.rutas a
                                            JOIN ssf.tipopatrullaje b ON a.id_tipoPatrullaje = b.id_tipoPatrullaje
                                            WHERE a.regionMilitarSDN = @pRegion
                                            AND b.descripcion = @pTipo
                                            CHARINDEX(a.clave,'_') = 0";

            object[] parametros = new object[]
            {
                new SqlParameter("@pRegion", regionMilitar),
                new SqlParameter("@pTipo", tipoPatrullaje),
            };

            var toUpdate =  await _rutaContext.Rutas.FromSqlRaw(sqlQuery, parametros).ToListAsync();

            if (toUpdate.Count > 0) 
            {
                foreach (var item in toUpdate) 
                {
                    item.Clave = item.Clave + "_" + DateTime.UtcNow.Year.ToString();
                }

                _rutaContext.Rutas.UpdateRange(toUpdate);

                await _rutaContext.SaveChangesAsync();
            }
        }

        private async Task<ItinerarioRutaVista?> ObtenerItinerariosPorRutaAsync(int idRuta)
        {
            string sqlQuery = @"SELECT COALESCE((SELECT STRING_AGG(CAST(g.ubicacion as nvarchar(MAX)), '-') WITHIN GROUP(ORDER BY f.posicion ASC) 
                                                 FROM ssf.itinerario f
                                				 JOIN ssf.puntospatrullaje g ON f.id_punto=g.id_punto
                                                 WHERE f.id_ruta=a.id_ruta),'') as itinerarioruta
                                FROM ssf.rutas a
                                WHERE a.id_ruta= @pIdRuta";

            object[] parametros = new object[]
            {
                new SqlParameter("@pIdRuta", idRuta)
            };

            return await _rutaContext.ItinerariosRutasVista.FromSqlRaw(sqlQuery, parametros).FirstOrDefaultAsync();
        }

        /// <summary>
        /// Método <c>DeleteAsync</c> Implementa la interfaz para eliminar una ruta indicada que no está bloqueada
        /// </summary>
        public async Task DeleteAsync(int id)
        {
            var r = await _rutaContext.Rutas.Where(x => x.IdRuta == id).FirstOrDefaultAsync();

            if (r != null)
            {
                if (r.Bloqueado == 0)
                {
                    _rutaContext.Rutas.Remove(r);
                    await _rutaContext.SaveChangesAsync();
                }
            }
        }

        /// <summary>
        /// Método <c>ObtenerDescripcionTipoPatrullajeAsync</c> implementa la interfaz para obtener la descripcion de un tipo de patrullaje indicado
        /// </summary>
        public async Task<string> ObtenerDescripcionTipoPatrullajeAsync(int tipoPatrullaje)
        {
            var t = await _rutaContext.TiposPatrullaje.Where(x => x.IdTipoPatrullaje == tipoPatrullaje).FirstOrDefaultAsync();

            if (t == null) throw new Exception("No se encontró tipo de patrullaje para el identificador indicado");
            return t.Descripcion;
        }

        /// <summary>
        /// Método <c>ObtenerNumeroItinerariosConfiguradosPorZonasRutaAsync</c> implementa la interfaz para obtener el número de itinerarios configurados para la combinación de parámetros indicados
        /// </summary>
        public async Task<int> ObtenerNumeroItinerariosConfiguradosPorZonasRutaAsync(int tipoPatrullaje, string regionSsf, string regionMilitar, int zonaMilitar, string ruta)
        {
            string sqlQuery = @"SELECT itinerarioruta FROM (
                     SELECT a.id_ruta,a.clave,a.regionmilitarsdn as regiondelasdn,a.regionssf as regiondelassf,
                            a.zonamilitarsdn,a.observaciones,a.consecutivoregionmilitarsdn,
                            a.totalrutasregionmilitarsdn,a.bloqueado,a.habilitado,
                            COALESCE((SELECT STRING_AGG(CAST(ubicacion as nvarchar(MAX)),'-') WITHIN GROUP (ORDER BY f.posicion ASC) 
                                     FROM ssf.itinerario f join ssf.puntospatrullaje g on f.id_punto=g.id_punto
                                     WHERE f.id_ruta=a.id_ruta),'') as itinerarioruta
                    FROM ssf.rutas a 
                    WHERE a.id_tipopatrullaje=@parTipoPatrullaje AND a.regionssf=@parRegionSsf  
                    AND a.regionmilitarsdn=@parRegionMilitar AND a.zonamilitarsdn=@parZonaMilitar
            ) as resultado
            WHERE itinerarioruta=@parRuta";

            object[] parametros = new object[]
            {
                new SqlParameter("@parTipoPatrullaje", tipoPatrullaje),
                new SqlParameter("@parRegionSsf", regionSsf),
                new SqlParameter("@parRegionMilitar", regionMilitar),
                new SqlParameter("@parZonaMilitar", zonaMilitar),
                new SqlParameter("@parRuta", ruta),
            };

            return await _rutaContext.Itinerarios.FromSqlRaw(sqlQuery, parametros).CountAsync();
        }

        /// <summary>
        /// Método <c>ObtenerNumeroItinerariosConfiguradosEnOtraRutaAsync</c> implementa la interfaz para obtener el número de itinerarios configurados en una ruta diferente a la indicada
        /// </summary>
        public async Task<int> ObtenerNumeroItinerariosConfiguradosEnOtraRutaAsync(int tipoPatrullaje, string regionSsf, string regionMilitar, int zonaMilitar, int ruta, string rutaItinerario)
        {
            string sqlQuery = @"SELECT itinerarioruta FROM (
                     SELECT a.id_ruta,a.clave,a.regionmilitarsdn as regiondelasdn,a.regionssf as regiondelassf,
                            a.zonamilitarsdn,a.observaciones,a.consecutivoregionmilitarsdn,
                            a.totalrutasregionmilitarsdn,a.bloqueado,a.habilitado,
                            COALESCE((SELECT STRING_AGG(CAST(ubicacion as nvarchar(MAX)),'-') WITHIN GROUP (ORDER BY f.posicion ASC) 
                                     FROM ssf.itinerario f join ssf.puntospatrullaje g on f.id_punto=g.id_punto
                                     WHERE f.id_ruta=a.id_ruta),'') as itinerarioruta
                    FROM ssf.rutas a 
                    WHERE a.id_tipopatrullaje=@parTipoPatrullaje AND a.regionssf=@parRegionSsf  
                    AND a.regionmilitarsdn=@parRegionMilitar AND a.zonamilitarsdn=@parZonaMilitar
                    AND a.id_ruta <> @parRuta
            ) as resultado
            WHERE itinerarioruta=@parRutaItinerario";

            object[] parametros = new object[]
            {
                new SqlParameter("@parTipoPatrullaje", tipoPatrullaje),
                new SqlParameter("@parRegionSsf", regionSsf),
                new SqlParameter("@parRegionMilitar", regionMilitar),
                new SqlParameter("@parZonaMilitar", zonaMilitar),
                new SqlParameter("@parRuta", ruta),
                new SqlParameter("@parRutaItinerario", rutaItinerario)
            };

            return await _rutaContext.Itinerarios.FromSqlRaw(sqlQuery, parametros).CountAsync();
        }

        /// <summary>
        /// Método <c>ObtenerNumeroRutasPorFiltroAsync</c> implementa la interfaz para obtener el número de rutas por clave y ruta
        /// </summary>
        public async Task<int> ObtenerNumeroRutasConMismaClaveAsync(string clave, int idRuta)
        {
            return await _rutaContext.Rutas.Where(x => x.Clave == clave && x.IdRuta != idRuta).CountAsync();
        }

        /// <summary>
        /// Método <c>ObtenerNumeroRutasPorTipoAndRegionMilitarAsync</c> implementa la interfaz para obtener el número de rutas por tipo de patrullaje y región militar
        /// </summary>
        public async Task<int> ObtenerNumeroRutasPorTipoAndRegionMilitarAsync(int tipoPatrullaje, string regionMilitar)
        {
            return await _rutaContext.Rutas.Where(x => x.IdTipoPatrullaje == tipoPatrullaje && x.RegionMilitarSdn == regionMilitar).CountAsync();
        }

        /// <summary>
        /// Método <c>ObtenerNumeroProgramasPorRutaAsync</c> implementa la interfaz para obtener el número de programas de patrullaje para una ruta indicada
        /// </summary>
        public async Task<int> ObtenerNumeroProgramasPorRutaAsync(int idRuta)
        {
            return await _rutaContext.Programas.Where(x => x.IdRuta == idRuta).CountAsync();
        }

        /// <summary>
        /// Método <c>ObtenerNumeroPropuestasPorRutaAsync</c> implementa la interfaz para obtener el número de propuestas de patrullaje para una ruta indicada
        /// </summary>
        public async Task<int> ObtenerNumeroPropuestasPorRutaAsync(int idRuta)
        {
            return await _rutaContext.Propuestas.Where(x => x.IdRuta == idRuta).CountAsync();
        }

        /// <summary>
        /// Método <c>ObtenerUsuarioConfiguradorAsync</c> implementa la interfaz para obtener al usuario indicado sólo si es configurador configurador, 
        /// </summary>
        public async Task<Usuario?> ObtenerUsuarioConfiguradorAsync(string usuario)
        {
            return await _rutaContext.Usuarios.Where(x => x.UsuarioNom == usuario && x.Configurador == 1).FirstOrDefaultAsync();
        }

        /// <summary>
        /// Método <c>ObtenerRutasPorRegionSsfAsync</c> implementa la interfaz para obtener rutas filtradas por tipo y región SSF
        /// </summary>
        public async Task<List<RutaVista>> ObtenerRutasPorRegionSsfAsync(string tipo, int regionSsf)
        {
            string sqlQuery = @"SELECT a.id_ruta, a.clave, a.regionmilitarsdn, a.regionssf,
                   a.zonamilitarsdn,a.observaciones, a.consecutivoregionmilitarsdn, a.totalrutasregionmilitarsdn,
                   a.bloqueado,a.habilitado, a.id_tipopatrullaje,
                   COALESCE((SELECT STRING_AGG(CAST(ubicacion as nvarchar(MAX)), ' - ') WITHIN GROUP(ORDER BY f.posicion ASC)
                             FROM ssf.itinerario f join ssf.puntospatrullaje g on f.id_punto = g.id_punto
                             WHERE f.id_ruta = a.id_ruta),'') as itinerarioruta,
                   COALESCE((SELECT STRING_AGG(CAST(CONCAT(f.posicion,'[!:!]',g.ubicacion,'[!:!]',g.coordenadas,'[!:!]', f.id_itinerario,'[!:!]',g.id_punto) as nvarchar(MAX)), '¦') 
                               WITHIN GROUP(ORDER BY f.posicion ASC)
                             FROM ssf.itinerario f JOIN ssf.puntospatrullaje g on f.id_punto = g.id_punto
                             WHERE f.id_ruta = a.id_ruta),'') as itinerariorutapatrullaje
            FROM ssf.rutas a
            join ssf.tipopatrullaje t on a.id_tipoPatrullaje = t.id_tipoPatrullaje
            WHERE a.regionssf = @parRegionSsf AND t.descripcion = @parDescripcion 
            ORDER BY regionssf,regionmilitarsdn,zonamilitarsdn,consecutivoregionmilitarsdn";

            object[] parametros = new object[]
            {
                new SqlParameter("@parRegionSsf", regionSsf),
                new SqlParameter("@parDescripcion", tipo)
            };

            return await _rutaContext.RutasVista.FromSqlRaw(sqlQuery, parametros).ToListAsync();

        }

        /// <summary>
        /// Método <c>ObtenerRutasPorRegionMilitarAsync</c> implementa la interfaz para obtener rutas filtradas por Región Militar
        /// </summary>
        public async Task<List<RutaVista>> ObtenerRutasPorRegionMilitarAsync(string tipo, string regionMilitar)
        {
            string sqlQuery = @"SELECT a.id_ruta, a.clave, a.regionmilitarsdn, a.regionssf,
                   a.zonamilitarsdn,a.observaciones, a.consecutivoregionmilitarsdn, a.totalrutasregionmilitarsdn,
                   a.bloqueado,a.habilitado, a.id_tipopatrullaje,
                   COALESCE((SELECT STRING_AGG(CAST(ubicacion as nvarchar(MAX)), ' - ') WITHIN GROUP(ORDER BY f.posicion ASC)
                             FROM ssf.itinerario f join ssf.puntospatrullaje g on f.id_punto = g.id_punto
                             WHERE f.id_ruta = a.id_ruta),'') as itinerarioruta,
                   COALESCE((SELECT STRING_AGG(CAST(CONCAT(f.posicion,'[!:!]',g.ubicacion,'[!:!]',g.coordenadas,'[!:!]', f.id_itinerario,'[!:!]',g.id_punto) as nvarchar(MAX)), '¦') 
                               WITHIN GROUP(ORDER BY f.posicion ASC)
                             FROM ssf.itinerario f join ssf.puntospatrullaje g on f.id_punto = g.id_punto
                             WHERE f.id_ruta = a.id_ruta),'') as itinerariorutapatrullaje
            FROM ssf.rutas a
            join ssf.tipopatrullaje t on a.id_tipoPatrullaje = t.id_tipoPatrullaje
            WHERE a.habilitado = 1 AND a.regionmilitarsdn = @parRegionMilitar AND t.descripcion = @parDescripcion
            ORDER BY regionssf,regionmilitarsdn,zonamilitarsdn,consecutivoregionmilitarsdn";

            object[] parametros = new object[]
            {
                new SqlParameter("@parRegionMilitar", regionMilitar),
                new SqlParameter("@parDescripcion", tipo)
            };

            return await _rutaContext.RutasVista.FromSqlRaw(sqlQuery, parametros).ToListAsync();
        }

        /// <summary>
        /// Método <c>ObtenerRutasPorCombinacionFiltrosAsync</c> implementa la interfaz para obtener rutas filtradas tipo, clave, observaciones e itinerario
        /// </summary>
        public async Task<List<RutaVista>> ObtenerRutasPorCombinacionFiltrosAsync(string tipo, string criterio)
        {
            criterio = "%" + criterio + "%";

            string sqlQuery = @"SELECT a.id_ruta, a.clave, a.regionmilitarsdn, a.regionssf,
                   a.zonamilitarsdn,a.observaciones, a.consecutivoregionmilitarsdn, a.totalrutasregionmilitarsdn,
                   a.bloqueado,a.habilitado, a.id_tipopatrullaje,
                   COALESCE((SELECT STRING_AGG(CAST(ubicacion as nvarchar(MAX)), ' - ') WITHIN GROUP(ORDER BY f.posicion ASC)
                             FROM ssf.itinerario f join ssf.puntospatrullaje g on f.id_punto = g.id_punto
                             WHERE f.id_ruta = a.id_ruta),'') as itinerarioruta,
                   COALESCE((SELECT STRING_AGG(CAST(CONCAT(f.posicion,'[!:!]',g.ubicacion,'[!:!]',g.coordenadas,'[!:!]', f.id_itinerario,'[!:!]',g.id_punto) as nvarchar(MAX)), '¦') 
                               WITHIN GROUP(ORDER BY f.posicion ASC)
                             FROM ssf.itinerario f join ssf.puntospatrullaje g on f.id_punto = g.id_punto
                             WHERE f.id_ruta = a.id_ruta),'') as itinerariorutapatrullaje
            FROM ssf.rutas a
            join ssf.tipopatrullaje t on a.id_tipoPatrullaje = t.id_tipoPatrullaje
            WHERE a.habilitado = 1 AND t.descripcion = @parDescripcion   
            AND ( a.clave like @parCriterio OR a.observaciones like @parCriterio OR 
                  COALESCE((SELECT STRING_AGG(CAST(ubicacion as nvarchar(MAX)), '-') WITHIN GROUP(ORDER BY f.posicion ASC)
                            FROM ssf.itinerario f join ssf.puntospatrullaje g on f.id_punto = g.id_punto
                            WHERE f.id_ruta = a.id_ruta),'') like @parCriterio )
            ORDER BY regionssf,regionmilitarsdn,zonamilitarsdn,consecutivoregionmilitarsdn";

            object[] parametros = new object[]
            {
                new SqlParameter("@parCriterio", criterio),
                new SqlParameter("@parDescripcion", tipo)
            };

            return await _rutaContext.RutasVista.FromSqlRaw(sqlQuery, parametros).ToListAsync();
        }

        /// <summary>
        /// Método <c>ObtenerRutasTodasPorTipoAndCriterioAsync</c> implementa la interfaz para obtener rutas filtradas tipo, clave, observaciones e itinerario
        /// </summary>
        public async Task<List<RutaVista>> ObtenerRutasTodasPorTipoAndCriterioAsync(string tipo, string criterio)
        {
            criterio = "%" + criterio + "%";

            string sqlQuery = @"SELECT a.id_ruta ,a.clave, a.regionmilitarsdn, a.regionssf
                                       ,a.zonamilitarsdn,a.observaciones,a.consecutivoregionmilitarsdn,a.totalrutasregionmilitarsdn
                                       ,a.bloqueado,a.habilitado,a.id_tipopatrullaje
                                       ,COALESCE((SELECT STRING_AGG(CAST(ubicacion as nvarchar(MAX)), ' - ') WITHIN GROUP(ORDER BY f.posicion ASC)
                                                FROM ssf.itinerario f join ssf.puntospatrullaje g on f.id_punto = g.id_punto
                                                WHERE f.id_ruta = a.id_ruta),'') as itinerarioruta
                                       ,COALESCE((SELECT STRING_AGG(CAST(CONCAT(f.posicion,'[!:!]',g.ubicacion,'[!:!]',g.coordenadas,'[!:!]', f.id_itinerario,'[!:!]',g.id_punto) as nvarchar(MAX)), '¦') 
                                                    WITHIN GROUP(ORDER BY f.posicion ASC)
                                                  FROM ssf.itinerario f JOIN ssf.puntospatrullaje g on f.id_punto = g.id_punto
                                                  WHERE f.id_ruta = a.id_ruta),'') as itinerariorutapatrullaje
                                FROM ssf.rutas a 
                                JOIN ssf.tipopatrullaje t on a.id_tipoPatrullaje = t.id_tipoPatrullaje
                                WHERE t.descripcion = @parDescripcion
                                AND (a.clave like @parCriterio OR observaciones like @parCriterio
                                    OR COALESCE((SELECT STRING_AGG(CAST(ubicacion as nvarchar(MAX)), '-') WITHIN GROUP(ORDER BY f.posicion ASC) as recorrido 
                                               FROM ssf.itinerario f JOIN ssf.puntospatrullaje g ON f.id_punto=g.id_punto
                                               WHERE f.id_ruta=a.id_ruta  ),'') like @parCriterio )
                                ORDER BY a.regionssf, a.regionmilitarsdn, a.zonamilitarsdn, a.consecutivoregionmilitarsdn";

            object[] parametros = new object[]
            {
                new SqlParameter("@parCriterio", criterio),
                new SqlParameter("@parDescripcion", tipo)
            };

            return await _rutaContext.RutasVista.FromSqlRaw(sqlQuery, parametros).ToListAsync();
        }

        /// <summary>
        /// Método <c>ObtenerRutasTodasPorTipoAndCriterioAsync</c> implementa la interfaz para obtener rutas filtradas tipo, clave, observaciones e itinerario
        /// </summary>
        public async Task<List<RutaVista>> ObtenerRutasTodasPorRegionMilitarAsync(string tipo, string regionMilitar)
        {
            string sqlQuery = @"SELECT a.id_ruta ,a.clave, a.regionmilitarsdn, a.regionssf
                                       ,a.zonamilitarsdn,a.observaciones,a.consecutivoregionmilitarsdn,a.totalrutasregionmilitarsdn
                                       ,a.bloqueado,a.habilitado,a.id_tipopatrullaje
                                       ,COALESCE((SELECT STRING_AGG(CAST(ubicacion as nvarchar(MAX)), ' - ') WITHIN GROUP(ORDER BY f.posicion ASC)
                                                FROM ssf.itinerario f join ssf.puntospatrullaje g on f.id_punto = g.id_punto
                                                WHERE f.id_ruta = a.id_ruta),'') as itinerarioruta
                                       ,COALESCE((SELECT STRING_AGG(CAST(CONCAT(f.posicion,'[!:!]',g.ubicacion,'[!:!]',g.coordenadas,'[!:!]', f.id_itinerario,'[!:!]',g.id_punto) as nvarchar(MAX)), '¦') 
                                                    WITHIN GROUP(ORDER BY f.posicion ASC)
                                                  FROM ssf.itinerario f JOIN ssf.puntospatrullaje g on f.id_punto = g.id_punto
                                                  WHERE f.id_ruta = a.id_ruta),'') as itinerariorutapatrullaje
                                FROM ssf.rutas a 
                                JOIN ssf.tipopatrullaje t on a.id_tipoPatrullaje = t.id_tipoPatrullaje
                                WHERE t.descripcion = @parDescripcion
                                AND a.regionmilitarsdn = @parRegionMilitar
                                ORDER BY a.regionmilitarsdn, a.zonamilitarsdn, a.consecutivoregionmilitarsdn, a.regionssf";

            object[] parametros = new object[]
            {
                new SqlParameter("@parRegionMilitar", regionMilitar),
                new SqlParameter("@parDescripcion", tipo)
            };

            return await _rutaContext.RutasVista.FromSqlRaw(sqlQuery, parametros).ToListAsync();
        }

        /// <summary>
        /// Método <c>ObtenerPropuestasPorRegionMilitarAndRegionSsfAsync</c> implementa la interfaz para obtener propuestas de patrullaje (rutas) filtradas tipo, región militar y región SSF
        /// </summary>
        public async Task<List<RutaVista>> ObtenerPropuestasPorRegionMilitarAndRegionSsfAsync(string tipo, string regionMilitar, int regionSsf)
        {
            string sqlQuery = @"SELECT a.id_ruta, a.clave, a.regionmilitarsdn, a.regionssf,
                   a.zonamilitarsdn,a.observaciones, a.consecutivoregionmilitarsdn, a.totalrutasregionmilitarsdn,
                   a.bloqueado,a.habilitado, a.id_tipopatrullaje,
                   COALESCE((SELECT STRING_AGG(CAST(ubicacion as nvarchar(MAX)), ' - ') WITHIN GROUP(ORDER BY f.posicion ASC)
                             FROM ssf.itinerario f join ssf.puntospatrullaje g on f.id_punto = g.id_punto
                             WHERE f.id_ruta = a.id_ruta),'') as itinerarioruta,
                   COALESCE((SELECT STRING_AGG(CAST(CONCAT(f.posicion,'[!:!]',g.ubicacion,'[!:!]',g.coordenadas,'[!:!]', f.id_itinerario,'[!:!]',g.id_punto) as nvarchar(MAX)), '¦') 
                               WITHIN GROUP(ORDER BY f.posicion ASC)
                             FROM ssf.itinerario f join ssf.puntospatrullaje g on f.id_punto = g.id_punto
                             WHERE f.id_ruta = a.id_ruta),'') as itinerariorutapatrullaje
            FROM ssf.rutas a
            join ssf.tipopatrullaje t on a.id_tipoPatrullaje = t.id_tipoPatrullaje
            WHERE a.habilitado = 1 AND a.regionssf = @parRegionSsf AND t.descripcion = @parDescripcion AND a.regionmilitarsdn = @parRegionMilitar
            ORDER BY regionssf,regionmilitarsdn,zonamilitarsdn,consecutivoregionmilitarsdn";

            object[] parametros = new object[]
            {
                new SqlParameter("@parRegionMilitar", regionMilitar),
                new SqlParameter("@parRegionSsf", regionSsf),
                new SqlParameter("@parDescripcion", tipo)
            };

            return await _rutaContext.RutasVista.FromSqlRaw(sqlQuery, parametros).ToListAsync();
        }

        /// <summary>
        /// Método <c>ObtenerPropuestasPorCombinacionFiltrosConRegionSsfAsync</c> implementa la interfaz para obtener propuestas de patrullaje (rutas) filtradas tipo, región SSF,  clave, observaciones e itinerario
        /// </summary>
        public async Task<List<RutaVista>> ObtenerPropuestasPorCombinacionFiltrosConRegionSsfAsync(string tipo, string criterio, int regionSsf)
        {
            criterio = "%" + criterio + "%";

            string sqlQuery = @"SELECT a.id_ruta, a.clave, a.regionmilitarsdn, a.regionssf,
                   a.zonamilitarsdn,a.observaciones, a.consecutivoregionmilitarsdn, a.totalrutasregionmilitarsdn,
                   a.bloqueado,a.habilitado, a.id_tipopatrullaje,
                   COALESCE((SELECT STRING_AGG(CAST(ubicacion as nvarchar(MAX)), ' - ') WITHIN GROUP(ORDER BY f.posicion ASC)
                             FROM ssf.itinerario f join ssf.puntospatrullaje g on f.id_punto = g.id_punto
                             WHERE f.id_ruta = a.id_ruta),'') as itinerarioruta,
                   COALESCE((SELECT STRING_AGG(CAST(CONCAT(f.posicion,'[!:!]',g.ubicacion,'[!:!]',g.coordenadas,'[!:!]', f.id_itinerario,'[!:!]',g.id_punto) as nvarchar(MAX)), '¦') 
                               WITHIN GROUP(ORDER BY f.posicion ASC)
                             FROM ssf.itinerario f join ssf.puntospatrullaje g on f.id_punto = g.id_punto
                             WHERE f.id_ruta = a.id_ruta),'') as itinerariorutapatrullaje
            FROM ssf.rutas a
            join ssf.tipopatrullaje t on a.id_tipoPatrullaje = t.id_tipoPatrullaje
            WHERE a.habilitado = 1 AND t.descripcion = @parDescripcion AND a.regionssf = @parRegionSsf  
            AND ( a.clave like @parCriterio OR a.observaciones like @parCriterio OR 
                  COALESCE((SELECT STRING_AGG(CAST(ubicacion as nvarchar(MAX)), '-') WITHIN GROUP(ORDER BY f.posicion ASC)
                            FROM ssf.itinerario f join ssf.puntospatrullaje g on f.id_punto = g.id_punto
                            WHERE f.id_ruta = a.id_ruta),'') like @parCriterio )
            ORDER BY regionssf,regionmilitarsdn,zonamilitarsdn,consecutivoregionmilitarsdn";

            object[] parametros = new object[]
            {
                new SqlParameter("@parCriterio", criterio),
                new SqlParameter("@parRegionSsf", regionSsf),
                new SqlParameter("@parDescripcion", tipo)
            };

            return await _rutaContext.RutasVista.FromSqlRaw(sqlQuery, parametros).ToListAsync();
        }

        public async Task<List<RutaDisponibleVista>> ObtenerRutasDisponiblesParaCambioDeRutaAsync(string region, DateTime fecha)
        {
            string sqlQuery = @"SELECT a.id_ruta, a.clave, a.regionMilitarSDN, a.regionSSF, a.zonaMilitarSDN, a.Observaciones  
                                      ,(SELECT STRING_AGG(CAST(g.ubicacion as nvarchar(MAX)),'-') WITHIN GROUP (ORDER BY f.posicion ASC) as ruta 
                                       FROM ssf.itinerario f
                                       JOIN ssf.puntospatrullaje g ON f.id_punto=g.id_punto
                                       WHERE f.id_ruta=a.id_ruta ) as itinerario
                                FROM ssf.rutas a
                                WHERE a.regionSSF = @pRegion
                                AND a.id_ruta NOT IN(
                                    SELECT b.id_ruta
                                    FROM ssf.programapatrullajes b
                                    WHERE b.fechaPatrullaje = @pFecha
                                )";

            object[] parametros = new object[]
            {
                new SqlParameter("@pRegion", region),
                new SqlParameter("@pFecha", fecha)
            };

            return await _rutaContext.RutasDisponibleVista.FromSqlRaw(sqlQuery, parametros).AsNoTracking().ToListAsync();
        }

        public async Task<bool> SaveChangesAsync()
        {
            return (await _rutaContext.SaveChangesAsync() >= 0);
        }
    }
}
