using Domain.DTOs;
using Domain.Entities;
using Domain.Entities.Vistas;
using Domain.Ports.Driven.Repositories;
using Domain.Ports.Driving;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using SqlServerAdapter.Data;
using System;
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
                item.IdRuta = r.IdRuta;
            }

            _rutaContext.Itinerarios.AddRange(itin); 
            _rutaContext.SaveChanges();
        }

        /// <summary>
        /// Método <c>Update</c> Implementa la interfaz para actualizar una ruta junto con sus itinerarios
        /// </summary>
        public void Update(Ruta pp)
        {
            _rutaContext.Rutas.Remove(pp);
            _rutaContext.SaveChanges();
        }

        /// <summary>
        /// Método <c>Delete</c> Implementa la interfaz para eliminar una ruta indicada que no está bloqueada
        /// </summary>
        public void Delete(int id)
        {
            var r = _rutaContext.Rutas.Where(x => x.IdRuta == id).First();

            if (r.Bloqueado == 0)
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
            var t = _rutaContext.TiposPatrullaje.Where(x => x.IdTipoPatrullaje == tipoPatrullaje).First();

            return t.Descripcion;
        }

        /// <summary>
        /// Método <c>ObtenerNumeroItinerariosConfiguradosPorZonasRuta</c> implementa la interfaz para obtener el número de itinerarios configurados para la combinación de parámetros indicados
        /// </summary>
        public int ObtenerNumeroItinerariosConfiguradosPorZonasRuta(int tipoPatrullaje, string regionSsf, string regionMilitar, int zonaMilitar, string ruta)
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

            return _rutaContext.Itinerarios.FromSqlRaw(sqlQuery, parametros).Count();
        }

        /// <summary>
        /// Método <c>ObtenerNumeroItinerariosConfiguradosEnOtraRuta</c> implementa la interfaz para obtener el número de itinerarios configurados en una ruta diferente a la indicada
        /// </summary>
        public int ObtenerNumeroItinerariosConfiguradosEnOtraRuta(int tipoPatrullaje, string regionSsf, string regionMilitar, int zonaMilitar, int ruta, string rutaItinerario)
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

            return _rutaContext.Itinerarios.FromSqlRaw(sqlQuery, parametros).Count();
        }

        /// <summary>
        /// Método <c>ObtenerNumeroRutasPorFiltro</c> implementa la interfaz para obtener el número de rutas por clave y ruta
        /// </summary>
        public int ObtenerNumeroRutasPorFiltro(string clave, int idRuta)
        {
            return _rutaContext.Rutas.Where(x => x.Clave == clave && x.IdRuta == idRuta).Count();
        }

        /// <summary>
        /// Método <c>ObtenerNumeroRutasPorTipoAndRegionMilitar</c> implementa la interfaz para obtener el número de rutas por tipo de patrullaje y región militar
        /// </summary>
        public int ObtenerNumeroRutasPorTipoAndRegionMilitar(int tipoPatrullaje, string regionMilitar)
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
           return _rutaContext.Usuarios.Where(x => x.UsuarioNom == usuario && x.Configurador == 1).FirstOrDefault();
        }

        /// <summary>
        /// Método <c>ObtenerRutasPorRegionSsf</c> implementa la interfaz para obtener rutas filtradas por tipo y región SSF
        /// </summary>
        public List<RutaVista> ObtenerRutasPorRegionSsf(string tipo, int regionSsf)
        {
            string sqlQuery = @"SELECT a.id_ruta, a.clave, a.regionmilitarsdn, a.regionssf,
                   a.zonamilitarsdn,a.observaciones, a.consecutivoregionmilitarsdn, a.totalrutasregionmilitarsdn,
                   a.bloqueado,a.habilitado,
                   COALESCE((SELECT STRING_AGG(CAST(ubicacion as nvarchar(MAX)), '-') WITHIN GROUP(ORDER BY f.posicion ASC)
                             FROM ssf.itinerario f join ssf.puntospatrullaje g on f.id_punto = g.id_punto
                             WHERE f.id_ruta = a.id_ruta),'') as itinerarioruta
            FROM ssf.rutas a
            join ssf.tipopatrullaje t on a.id_tipoPatrullaje = t.id_tipoPatrullaje
            WHERE a.habilitado = 1 AND a.regionssf = @parRegionSsf AND t.descripcion = @parDescripcion
            ORDER BY regionssf,regionmilitarsdn,zonamilitarsdn,consecutivoregionmilitarsdn";

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
            string sqlQuery = @"SELECT a.id_ruta, a.clave, a.regionmilitarsdn, a.regionssf,
                   a.zonamilitarsdn,a.observaciones, a.consecutivoregionmilitarsdn, a.totalrutasregionmilitarsdn,
                   a.bloqueado,a.habilitado,
                   COALESCE((SELECT STRING_AGG(CAST(ubicacion as nvarchar(MAX)), '-') WITHIN GROUP(ORDER BY f.posicion ASC)
                             FROM ssf.itinerario f join ssf.puntospatrullaje g on f.id_punto = g.id_punto
                             WHERE f.id_ruta = a.id_ruta),'') as itinerarioruta
            FROM ssf.rutas a
            join ssf.tipopatrullaje t on a.id_tipoPatrullaje = t.id_tipoPatrullaje
            WHERE a.habilitado = 1 AND a.regionmilitarsdn = @parRegionMilitar AND t.descripcion = @parDescripcion
            ORDER BY regionssf,regionmilitarsdn,zonamilitarsdn,consecutivoregionmilitarsdn";

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

            string sqlQuery = @"SELECT a.id_ruta, a.clave, a.regionmilitarsdn, a.regionssf,
                   a.zonamilitarsdn,a.observaciones, a.consecutivoregionmilitarsdn, a.totalrutasregionmilitarsdn,
                   a.bloqueado,a.habilitado,
                   COALESCE((SELECT STRING_AGG(CAST(ubicacion as nvarchar(MAX)), '-') WITHIN GROUP(ORDER BY f.posicion ASC)
                             FROM ssf.itinerario f join ssf.puntospatrullaje g on f.id_punto = g.id_punto
                             WHERE f.id_ruta = a.id_ruta),'') as itinerarioruta
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

            return _rutaContext.RutasVista.FromSqlRaw(sqlQuery, parametros).ToList();
        }

        /// <summary>
        /// Método <c>ObtenerPropuestasPorRegionMilitarAndRegionSsf</c> implementa la interfaz para obtener propuestas de patrullaje (rutas) filtradas tipo, región militar y región SSF
        /// </summary>
        public List<RutaVista> ObtenerPropuestasPorRegionMilitarAndRegionSsf(string tipo, string regionMilitar, int regionSsf)
        {
            string sqlQuery = @"SELECT a.id_ruta, a.clave, a.regionmilitarsdn, a.regionssf,
                   a.zonamilitarsdn,a.observaciones, a.consecutivoregionmilitarsdn, a.totalrutasregionmilitarsdn,
                   a.bloqueado,a.habilitado,
                   COALESCE((SELECT STRING_AGG(CAST(ubicacion as nvarchar(MAX)), '-') WITHIN GROUP(ORDER BY f.posicion ASC)
                             FROM ssf.itinerario f join ssf.puntospatrullaje g on f.id_punto = g.id_punto
                             WHERE f.id_ruta = a.id_ruta),'') as itinerarioruta
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

            return _rutaContext.RutasVista.FromSqlRaw(sqlQuery, parametros).ToList();
        }

        /// <summary>
        /// Método <c>ObtenerPropuestasPorCombinacionFiltrosConRegionSsf</c> implementa la interfaz para obtener propuestas de patrullaje (rutas) filtradas tipo, región SSF,  clave, observaciones e itinerario
        /// </summary>
        public List<RutaVista> ObtenerPropuestasPorCombinacionFiltrosConRegionSsf(string tipo, string criterio, int regionSsf)
        {
            criterio = "%" + criterio + "%";

            string sqlQuery = @"SELECT a.id_ruta, a.clave, a.regionmilitarsdn, a.regionssf,
                   a.zonamilitarsdn,a.observaciones, a.consecutivoregionmilitarsdn, a.totalrutasregionmilitarsdn,
                   a.bloqueado,a.habilitado,
                   COALESCE((SELECT STRING_AGG(CAST(ubicacion as nvarchar(MAX)), '-') WITHIN GROUP(ORDER BY f.posicion ASC)
                             FROM ssf.itinerario f join ssf.puntospatrullaje g on f.id_punto = g.id_punto
                             WHERE f.id_ruta = a.id_ruta),'') as itinerarioruta
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

            return _rutaContext.RutasVista.FromSqlRaw(sqlQuery, parametros).ToList();
        }
    }
}
