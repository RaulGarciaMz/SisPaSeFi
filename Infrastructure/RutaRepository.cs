using Domain.DTOs;
using Domain.Entities;
using Domain.Entities.Vistas;
using Domain.Ports.Driven.Repositories;
using Domain.Ports.Driving;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using SqlServerAdapter.Data;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Intrinsics.X86;
using System.Text;
using System.Threading.Tasks;

namespace SqlServerAdapter
{
    public class RutaRepository : IRutasRepo
    {
        protected readonly RutaContext _rutaContext;

        public RutaRepository(RutaContext rutaContext)
        {
            _rutaContext = rutaContext;
        }

        /// <summary>
        /// Método <c>Agrega</c> Implementa la interfaz para agregar una ruta junto con sus itinerarios
        /// </summary>
        public void Agrega(Ruta r, List<Itinerario> itin)
        {
            //TODO Tratar de implementar transacción
           _rutaContext.Rutas.Add(r);

            foreach (var item in itin)
            {
                item.IdRuta = r.Id;
            }

            _rutaContext.Itinerarios.AddRange(itin); 
            _rutaContext.SaveChanges();
        }


        /// <summary>
        /// Método <c>Agrega</c> Implementa la interfaz para agregar una ruta junto con sus itinerarios, que deben venir en el objeto ruta
        /// </summary>
        public void Agrega2(Ruta r)
        {
            //TODO Tratar de implementar transacción
            _rutaContext.Rutas.Add(r);
            _rutaContext.SaveChanges();
        }


        /// <summary>
        /// Método <c>Update</c> Implementa la interfaz para actualizar una ruta junto con sus itinerarios
        /// </summary>
        public void Update(Ruta pp)
        {
            _rutaContext.Rutas.Update(pp);
            _rutaContext.SaveChanges();
        }

        /// <summary>
        /// Método <c>Delete</c> Implementa la interfaz para eliminar una ruta indicada que no está bloqueada
        /// </summary>
        public void Delete(int id)
        {
            var r = _rutaContext.Rutas.Where(x => x.Id == id).First();

            if (r.Bloqueado == false)
            {
                _rutaContext.Rutas.Remove(r);
                _rutaContext.SaveChanges();
            }
        }

        /// <summary>
        /// Método <c>Delete</c> Implementa la interfaz para obtener rutas filtradas
        /// </summary>
        public List<Ruta> ObtenerPorFiltro(int opcion, string tipo, string criterio, string actividad)
        {
             throw new NotImplementedException();
        }

        /// <summary>
        /// Método <c>ObtenerDescripcionTipoPatrullaje</c> implementa la interfaz para obtener la descripcion de un tipo de patrullaje indicado
        /// </summary>
        public string ObtenerDescripcionTipoPatrullaje(int tipoPatrullaje)
        {
            var t = _rutaContext.TiposPatrullaje.Where(x => x.Id == tipoPatrullaje).First();

            return t.Nombre;
        }

        /// <summary>
        /// Método <c>ObtenerNumeroItinerariosConfiguradosPorZonasRuta</c> implementa la interfaz para obtener el número de itinerarios configurados para la combinación de parámetros indicados
        /// </summary>
        public int ObtenerNumeroItinerariosConfiguradosPorZonasRuta(int tipoPatrullaje, string regionSsf, string regionMilitar, int zonaMilitar, string ruta)
        {
            string sqlQuery = @"SELECT itinerarioruta FROM (
                     SELECT 
                            COALESCE((SELECT STRING_AGG(CAST(p.ubicacion as nvarchar(MAX)), '-') WITHIN GROUP(ORDER BY i.posicion ASC)
                                     FROM dmn.Itinerario i JOIN dmn.Punto_Patrullaje p ON i.id_punto = p.id
                                     WHERE i.id_ruta = a.id), '') as itinerarioruta
                    FROM dmn.Ruta a 
                    WHERE a.id_tipo_patrullaje@parTipoPatrullaje AND a.id_comandancia_regional_SSF=@parRegionSsf  
                    AND a.region_militar_SDN=@parRegionMilitar AND a.zona_militar_SDN=@parZonaMilitar
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

            return _rutaContext.Itinerarios.FromSqlRaw(sqlQuery, parametros).Count();
        }

        /// <summary>
        /// Método <c>ObtenerNumeroItinerariosConfiguradosEnOtraRuta</c> implementa la interfaz para obtener el número de itinerarios configurados en una ruta diferente a la indicada
        /// </summary>
        public int ObtenerNumeroItinerariosConfiguradosEnOtraRuta(int tipoPatrullaje, string regionSsf, string regionMilitar, int zonaMilitar, int ruta, string rutaItinerario)
        {
            string sqlQuery = @"SELECT itinerarioruta FROM (
                     SELECT 
                            COALESCE((SELECT STRING_AGG(CAST(p.ubicacion as nvarchar(MAX)), '-') WITHIN GROUP(ORDER BY i.posicion ASC)
                                     FROM dmn.Itinerario i JOIN dmn.Punto_Patrullaje p ON i.id_punto = p.id
                                     WHERE i.id_ruta = a.id), '') as itinerarioruta
                    FROM dmn.Ruta a 
                    WHERE a.id_tipo_patrullaje@parTipoPatrullaje AND a.id_comandancia_regional_SSF=@parRegionSsf  
                    AND a.region_militar_SDN=@parRegionMilitar AND a.zona_militar_SDN=@parZonaMilitar
                    AND a.id <> @parRuta
            ) as resultado
            WHERE itinerarioruta=@parRuta";

            object[] parametros = new object[]
            {
                new SqlParameter("@parTipoPatrullaje", tipoPatrullaje),
                new SqlParameter("@parRegionSsf", regionSsf),
                new SqlParameter("@parRegionMilitar", regionMilitar),
                new SqlParameter("@parZonaMilitar", zonaMilitar),
                new SqlParameter("@parRuta", ruta),
                new SqlParameter("@parRutaItinerario", rutaItinerario)
            };

            return _rutaContext.Itinerarios.FromSqlRaw(sqlQuery, parametros).Count();
        }

        /// <summary>
        /// Método <c>ObtenerNumeroRutasPorFiltro</c> implementa la interfaz para obtener el número de rutas por clave y ruta
        /// </summary>
        public int ObtenerNumeroRutasPorFiltro(string clave, int idRuta)
        {
            return _rutaContext.Rutas.Where(x => x.Clave == clave && x.Id == idRuta).Count();
        }

        /// <summary>
        /// Método <c>ObtenerNumeroRutasPorTipoAndRegionMilitar</c> implementa la interfaz para obtener el número de rutas por tipo de patrullaje y región militar
        /// </summary>
        public int ObtenerNumeroRutasPorTipoAndRegionMilitar(int tipoPatrullaje, short regionMilitar)
        {
            return _rutaContext.Rutas.Where(x => x.IdTipoPatrullaje == tipoPatrullaje && x.RegionMilitarSdn == regionMilitar).Count();
        }

        /// <summary>
        /// Método <c>ObtenerNumeroProgramasPorRuta</c> implementa la interfaz para obtener el número de programas de patrullaje para una ruta indicada
        /// </summary>
        public int ObtenerNumeroProgramasPorRuta(int idRuta)
        {
            return _rutaContext.Programas.Where(x => x.IdRuta == idRuta).Count();
        }

        /// <summary>
        /// Método <c>ObtenerNumeroPropuestasPorRuta</c> implementa la interfaz para obtener el número de propuestas de patrullaje para una ruta indicada
        /// </summary>
        public int ObtenerNumeroPropuestasPorRuta(int idRuta)
        {
            return _rutaContext.Propuestas.Where(x => x.IdRuta == idRuta).Count();
        }

        /// <summary>
        /// Método <c>ObtenerUsuarioConfigurador</c> implementa la interfaz para obtener al usuario indicado sólo si es configurador configurador, 
        /// </summary>
        public Usuario? ObtenerUsuarioConfigurador(string usuario) 
        {
           return _rutaContext.Usuarios.Where(x => x.Nombre == usuario && x.EsConfigurador == true).FirstOrDefault();
        }

        /// <summary>
        /// Método <c>ObtenerRutasPorRegionSsf</c> implementa la interfaz para obtener rutas filtradas por tipo y región SSF
        /// </summary>
        public List<RutaVista> ObtenerRutasPorRegionSsf(string tipo, int regionSsf)
        {
            string sqlQuery = @"SELECT a.id, a.clave, a.region_militar_SDN, a.id_comandancia_regional_SSF,
                   a.zona_militar_SDN, a.observaciones, a.consecutivo_region_militar_SDN,
                   a.bloqueado, a.habilitado,
                   COUNT(a.id) OVER(PARTITION BY a.region_militar_SDN) total_rutas_region,
                   COALESCE((SELECT STRING_AGG(CAST(p.ubicacion as nvarchar(MAX)), '-') WITHIN GROUP(ORDER BY i.posicion ASC)
                            FROM dmn.Itinerario i JOIN dmn.Punto_Patrullaje p ON i.id_punto = p.id
                            WHERE i.id_ruta = a.id),'') as itinerarioruta
            FROM dmn.Ruta a
            JOIN cat.Tipo_Patrullaje t ON a.id_tipo_patrullaje = t.id
            WHERE a.habilitado = 1 AND a.id_comandancia_regional_SSF = @parRegionSsf AND t.nombre = @parDescripcion
            ORDER BY a.id_comandancia_regional_SSF, a.region_militar_SDN, a.zona_militar_SDN, a.consecutivo_region_militar_SDN";

            object[] parametros = new object[]
            {
                new SqlParameter("@parRegionSsf", regionSsf),
                new SqlParameter("@parDescripcion", tipo)
            };

            return _rutaContext.RutasVista.FromSqlRaw(sqlQuery, parametros).ToList();
        }

        /// <summary>
        /// Método <c>ObtenerRutasPorRegionMilitar</c> implementa la interfaz para obtener rutas filtradas por Región Militar
        /// </summary>
        public List<RutaVista> ObtenerRutasPorRegionMilitar(string tipo, string regionMilitar)
        {
            string sqlQuery = @"SELECT a.id, a.clave, a.region_militar_SDN, a.id_comandancia_regional_SSF,
                   a.zona_militar_SDN, a.observaciones, a.consecutivo_region_militar_SDN,
                   a.bloqueado, a.habilitado,
	               COUNT(a.id) OVER(PARTITION BY a.region_militar_SDN) totalrutasregionmilitarsdn,
                   COALESCE((SELECT STRING_AGG(CAST(p.ubicacion as nvarchar(MAX)), '-') WITHIN GROUP(ORDER BY i.posicion ASC)
                            FROM dmn.Itinerario i JOIN dmn.Punto_Patrullaje p ON i.id_punto = p.id
                            WHERE i.id_ruta = a.id),'') as itinerarioruta
                   FROM dmn.Ruta a
                   JOIN cat.Tipo_Patrullaje t ON a.id_tipo_patrullaje = t.id
            WHERE a.habilitado = 1 AND a.region_militar_SDN = @parRegionMilitar AND t.nombre = @parDescripcion
            ORDER BY a.id_comandancia_regional_SSF, a.region_militar_SDN, a.zona_militar_SDN, a.consecutivo_region_militar_SDN";

            object[] parametros = new object[]
            {
                new SqlParameter("@parRegionMilitar", regionMilitar),
                new SqlParameter("@parDescripcion", tipo)
            };

            return _rutaContext.RutasVista.FromSqlRaw(sqlQuery, parametros).ToList();
        }

        /// <summary>
        /// Método <c>ObtenerRutasPorCombinacionFiltros</c> implementa la interfaz para obtener rutas filtradas tipo, clave, observaciones e itinerario
        /// </summary>
        public List<RutaVista> ObtenerRutasPorCombinacionFiltros(string tipo, string criterio)
        {
            criterio = "%" + criterio + "%";

            string sqlQuery = @"SELECT a.id, a.clave, a.region_militar_SDN, a.id_comandancia_regional_SSF,
                   a.zona_militar_SDN, a.observaciones, a.consecutivo_region_militar_SDN,
                   a.bloqueado, a.habilitado,
	               COUNT(a.id) OVER(PARTITION BY a.region_militar_SDN) totalrutasregionmilitarsdn,
                   COALESCE((SELECT STRING_AGG(CAST(p.ubicacion as nvarchar(MAX)), '-') WITHIN GROUP(ORDER BY i.posicion ASC)
                            FROM dmn.Itinerario i JOIN dmn.Punto_Patrullaje p ON i.id_punto = p.id
                            WHERE i.id_ruta = a.id),'') as itinerarioruta
            FROM dmn.Ruta a
            JOIN cat.Tipo_Patrullaje t ON a.id_tipo_patrullaje = t.id
            WHERE a.habilitado = 1 AND t.nombre = @parDescripcion   
            AND(a.clave like like @parCriterio OR a.observaciones like @parCriterio OR
            COALESCE((SELECT STRING_AGG(CAST(p.ubicacion as nvarchar(MAX)), '-') WITHIN GROUP(ORDER BY i.posicion ASC)
                      FROM dmn.Itinerario i JOIN dmn.Punto_Patrullaje p ON i.id_punto = p.id
                      WHERE i.id_ruta = a.id), '') like @parCriterio )
            ORDER BY a.id_comandancia_regional_SSF, a.region_militar_SDN, a.zona_militar_SDN, a.consecutivo_region_militar_SDN";

            object[] parametros = new object[]
            {
                new SqlParameter("@parCriterio", criterio),
                new SqlParameter("@parDescripcion", tipo)
            };

            return _rutaContext.RutasVista.FromSqlRaw(sqlQuery, parametros).ToList();
        }

        /// <summary>
        /// Método <c>ObtenerPropuestasPorRegionMilitarAndRegionSsf</c> implementa la interfaz para obtener propuestas de patrullaje (rutas) filtradas tipo, región militar y región SSF
        /// </summary>
        public List<RutaVista> ObtenerPropuestasPorRegionMilitarAndRegionSsf(string tipo, string regionMilitar, int regionSsf)
        {
            string sqlQuery = @"SELECT r.id, r.clave, r.region_militar_SDN, r.id_comandancia_regional_SSF,
                                 r.zona_militar_SDN, r.observaciones, r.consecutivo_region_militar_SDN,
	                             r.bloqueado, r.habilitado,
	                             COUNT(r.id) OVER(PARTITION BY r.region_militar_SDN) totalrutasregionmilitarsdn,
                                 COALESCE((SELECT STRING_AGG(CAST(p.ubicacion as nvarchar(MAX)), '-') WITHIN GROUP(ORDER BY i.posicion ASC)
                                          FROM dmn.Itinerario i JOIN dmn.Punto_Patrullaje p ON i.id_punto = p.id
                                          WHERE i.id_ruta = r.id),'') as itinerarioruta
                                 FROM dmn.Propuesta_Patrullaje_Ruta_Contenedor p
                                 JOIN dmn.Ruta r ON p.id_ruta = r.id
                                 JOIN cat.Tipo_Patrullaje t ON r.id_tipo_patrullaje = t.id
                                 WHERE r.habilitado = 1 AND r.id_comandancia_regional_SSF = @parRegionSsf
                                 AND r.region_militar_SDN = @parRegionMilitar t.nombre = @parDescripcion
                                 ORDER BY r.id_comandancia_regional_SSF, r.region_militar_SDN, r.zona_militar_SDN, r.consecutivo_region_militar_SDN";

            object[] parametros = new object[]
            {
                new SqlParameter("@parRegionMilitar", regionMilitar),
                new SqlParameter("@parRegionSsf", regionSsf),
                new SqlParameter("@parDescripcion", tipo)
            };

            return _rutaContext.RutasVista.FromSqlRaw(sqlQuery, parametros).ToList();
        }

        /// <summary>
        /// Método <c>ObtenerPropuestasPorCombinacionFiltrosConRegionSsf</c> implementa la interfaz para obtener propuestas de patrullaje (rutas) filtradas tipo, región SSF,  clave, observaciones e itinerario
        /// </summary>
        public List<RutaVista> ObtenerPropuestasPorCombinacionFiltrosConRegionSsf(string tipo, string criterio, int regionSsf)
        {
            criterio = "%" + criterio + "%";

            string sqlQuery = @"SELECT r.id, r.clave, r.region_militar_SDN, r.id_comandancia_regional_SSF,
                                       r.zona_militar_SDN, r.observaciones, r.consecutivo_region_militar_SDN,
	                                   r.bloqueado, r.habilitado,
	                                   COUNT(r.id) OVER(PARTITION BY r.region_militar_SDN) totalrutasregionmilitarsdn,
                                       COALESCE((SELECT STRING_AGG(CAST(p.ubicacion as nvarchar(MAX)), '-') WITHIN GROUP(ORDER BY i.posicion ASC)
                                                FROM dmn.Itinerario i JOIN dmn.Punto_Patrullaje p ON i.id_punto = p.id
                                                WHERE i.id_ruta = r.id),'') as itinerarioruta
                                FROM dmn.Propuesta_Patrullaje_Ruta_Contenedor p
                                JOIN dmn.Ruta r ON p.id_ruta = r.id
                                JOIN cat.Tipo_Patrullaje t ON r.id_tipo_patrullaje = t.id
                                WHERE r.habilitado = 1 AND r.id_comandancia_regional_SSF = @parRegionSsf
                                AND t.nombre = @parDescripcion
                                AND(r.clave like like @parCriterio OR r.observaciones like @parCriterio OR
                                COALESCE((SELECT STRING_AGG(CAST(p.ubicacion as nvarchar(MAX)), '-') WITHIN GROUP(ORDER BY i.posicion ASC)
                                                FROM dmn.Itinerario i JOIN dmn.Punto_Patrullaje p ON i.id_punto = p.id
                                                WHERE i.id_ruta = a.id), '') like @parCriterio )
                                ORDER BY r.id_comandancia_regional_SSF, r.region_militar_SDN, r.zona_militar_SDN, r.consecutivo_region_militar_SDN";

            object[] parametros = new object[]
            {
                new SqlParameter("@parCriterio", criterio),
                new SqlParameter("@parRegionSsf", regionSsf),
                new SqlParameter("@parDescripcion", tipo)
            };

            return _rutaContext.RutasVista.FromSqlRaw(sqlQuery, parametros).ToList();
        }
    }
}
