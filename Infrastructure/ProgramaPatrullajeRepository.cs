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
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace SqlServerAdapter
{
    public class ProgramaPatrullajeRepository : IProgramaPatrullajeRepo
    {

        protected readonly ProgramaContext _programaContext;

        public ProgramaPatrullajeRepository(ProgramaContext programaContext)
        {
            _programaContext = programaContext; 
        }
        
        //Caso 0 Extraordinario  --Propuestas extraordinarias
        public List<PatrullajeVista> ObtenerPropuestasExtraordinariasPorAnioMesDia(string tipo, int region, int anio, int mes, int dia)
        {
            string sqlQuery = @"SELECT a.id_propuestapatrullaje id, a.id_ruta,a.fechapatrullaje, a.id_puntoresponsable, 
                   a.ultimaactualizacion,a.id_usuario, b.clave, b.regionmilitarsdn,b.regionssf, b.observaciones observacionesruta, 
                   d.descripcionnivel, e.fechatermino, 0 id_usuarioresponsablepatrullaje, '' observaciones, 0 riesgopatrullaje,
                   COALESCE(a.solicitudoficioautorizacion, '') solicitudoficiocomision,
                   COALESCE(a.oficioautorizacion, '') as oficiocomision,
                   COALESCE((SELECT STRING_AGG(CAST(ubicacion as nvarchar(MAX)), '-') WITHIN GROUP(ORDER BY f.posicion ASC)
                             FROM ssf.itinerario f join ssf.puntospatrullaje g on f.id_punto = g.id_punto
                             WHERE f.id_ruta = a.id_ruta),'') as itinerario,
                   COALESCE((SELECT top 1 f.inicio FROM ssf.programapatrullajes f
                             WHERE f.id_propuestapatrullaje = a.id_propuestapatrullaje
                             AND YEAR(f.fechapatrullaje) = YEAR(getutcdate()) 
                             AND MONTH(f.fechapatrullaje) = MONTH(getutcdate()) 
                             AND DAY(f.fechapatrullaje) = DAY(getutcdate())),'00:00:00') inicio,
                   COALESCE((SELECT top 1 g.descripcionestadopatrullaje 
                             FROM ssf.programapatrullajes f
                             JOIN ssf.estadopatrullaje g ON f.id_estadopatrullaje = g.id_estadopatrullaje
                             WHERE f.id_propuestapatrullaje = a.id_propuestapatrullaje
                             AND YEAR(f.fechapatrullaje) = YEAR(getutcdate()) 
                             AND MONTH(f.fechapatrullaje) = MONTH(getutcdate()) 
                             AND DAY(f.fechapatrullaje) = DAY(getutcdate())),c.descripcionestadopropuesta)	descripcionestadopatrullaje
            FROM ssf.propuestaspatrullajes a
            JOIN ssf.rutas b ON a.id_ruta = b.id_ruta                        
            JOIN ssf.estadopropuesta c ON a.id_estadopropuesta = c.id_estadopropuesta
            JOIN ssf.niveles d ON a.riesgopatrullaje=d.id_nivel
            JOIN ssf.propuestaspatrullajescomplementossf e ON a.id_propuestapatrullaje=e.id_propuestapatrullaje
            JOIN ssf.tipopatrullaje k ON b.id_tipopatrullaje = k.id_tipopatrullaje
            WHERE CONVERT(datetime,a.fechapatrullaje) <= DATETIMEFROMPARTS(@pAnio,@pMes,@pDia,0,0,0,0)
            AND DATEADD(hour, 23, DATEADD(minute, 59, DATEADD(second, 59, CONVERT(datetime,e.fechatermino)))) >= DATETIMEFROMPARTS(@pAnio,@pMes,@pDia,0,0,0,0)
            AND k.descripcion = @pTipo
            AND b.regionssf = @pRegion
            ORDER BY a.fechapatrullaje";

            object[] parametros = new object[]
            {
                new SqlParameter("@pTipo", tipo),
                new SqlParameter("@pRegion", region),
                new SqlParameter("@pAnio", anio),
                new SqlParameter("@pMes", mes),
                new SqlParameter("@pDia", dia)
             };

            return _programaContext.PatrullajesVista.FromSqlRaw(sqlQuery, parametros).ToList();
        }

        //Caso 5 Ordinario  - Propuestas pendientes de autorizar
        public List<PatrullajeVista> ObtenerPropuestasPendientesPorAutorizarPorFiltro(string tipo, int region, int anio, int mes, string clase)
        {
            string sqlQuery = @"SELECT a.id_propuestapatrullaje id, a.id_ruta,a.fechapatrullaje, a.fechapatrullaje fechatermino, a.id_puntoresponsable, b.clave,
                   b.regionmilitarsdn,b.regionssf,b.observaciones observacionesruta, a.ultimaactualizacion,a.id_usuario,d.descripcionnivel,0 id_usuarioresponsablepatrullaje,
                   '' observaciones, 0 riesgopatrullaje,
                   COALESCE(a.solicitudoficioautorizacion, '') solicitudoficiocomision,
                   COALESCE(a.oficioautorizacion, '') as oficiocomision, 
                   COALESCE((SELECT STRING_AGG(CAST(ubicacion as nvarchar(MAX)), '-') WITHIN GROUP(ORDER BY f.posicion ASC)
                             FROM ssf.itinerario f join ssf.puntospatrullaje g on f.id_punto = g.id_punto
                             WHERE f.id_ruta = a.id_ruta),'') as itinerario,
                   COALESCE((SELECT top 1 f.inicio FROM ssf.programapatrullajes f
                             WHERE f.id_propuestapatrullaje = a.id_propuestapatrullaje
                             AND YEAR(f.fechapatrullaje) = YEAR(getutcdate()) 
                             AND MONTH(f.fechapatrullaje) = MONTH(getutcdate()) 
                             AND DAY(f.fechapatrullaje) = DAY(getutcdate())),'00:00:00') inicio,
                   COALESCE((SELECT top 1 g.descripcionestadopatrullaje 
                             FROM ssf.programapatrullajes f
                             JOIN ssf.estadopatrullaje g ON f.id_estadopatrullaje = g.id_estadopatrullaje
                             WHERE f.id_propuestapatrullaje = a.id_propuestapatrullaje
                             AND YEAR(f.fechapatrullaje) = YEAR(getutcdate()) 
                             AND MONTH(f.fechapatrullaje) = MONTH(getutcdate()) 
                             AND DAY(f.fechapatrullaje) = DAY(getutcdate())),c.descripcionestadopropuesta)	descripcionestadopatrullaje
            FROM ssf.propuestaspatrullajes a
            JOIN ssf.rutas b ON a.id_ruta = b.id_ruta            
            JOIN SSF.clasepatrullaje m ON a.id_clasepatrullaje = m.id_clasepatrullaje
            JOIN ssf.estadopropuesta c ON a.id_estadopropuesta = c.id_estadopropuesta
            JOIN ssf.niveles d ON a.riesgopatrullaje=d.id_nivel
            JOIN ssf.tipopatrullaje k ON b.id_tipopatrullaje = k.id_tipopatrullaje
            WHERE YEAR(a.fechapatrullaje)= @pAnio
            AND MONTH(a.fechapatrullaje)= @pMes
            AND k.descripcion = @pTipo
            AND b.regionssf = @pRegion
            AND m.descripcion = @pClase
            ORDER BY a.fechapatrullaje";

            object[] parametros = new object[]
            {
                new SqlParameter("@pAnio", anio),
                new SqlParameter("@pMes", mes),
                new SqlParameter("@pTipo", tipo),
                new SqlParameter("@pRegion", region),
                new SqlParameter("@pClase", clase)
            };

            return _programaContext.PatrullajesVista.FromSqlRaw(sqlQuery, parametros).ToList();
        }

        //Caso 5 ExtraordinarioOrdinario  - 
        public List<PatrullajeVista> ObtenerPropuestasExtraordinariasPorFiltro(string tipo, int region, int anio, int mes, string clase)
        {
            string sqlQuery = @"SELECT a.id_propuestapatrullaje id, a.id_ruta,a.fechapatrullaje, a.id_puntoresponsable, b.clave,
                   b.regionmilitarsdn,b.regionssf,b.observaciones observacionesruta, a.ultimaactualizacion,a.id_usuario,d.descripcionnivel,
                   e.fechatermino, 0 id_usuarioresponsablepatrullaje, 0 riesgopatrullaje, '' observaciones,
                   COALESCE(a.solicitudoficioautorizacion, '') solicitudoficiocomision,
                   COALESCE(a.oficioautorizacion, '') as oficiocomision,
                   COALESCE((SELECT STRING_AGG(CAST(ubicacion as nvarchar(MAX)), '-') WITHIN GROUP(ORDER BY f.posicion ASC)
                             FROM ssf.itinerario f join ssf.puntospatrullaje g on f.id_punto = g.id_punto
                             WHERE f.id_ruta = a.id_ruta),'') as itinerario,
                   COALESCE((SELECT top 1 f.inicio FROM ssf.programapatrullajes f
                             WHERE f.id_propuestapatrullaje = a.id_propuestapatrullaje
                             AND YEAR(f.fechapatrullaje) = YEAR(getutcdate()) 
                             AND MONTH(f.fechapatrullaje) = MONTH(getutcdate()) 
                             AND DAY(f.fechapatrullaje) = DAY(getutcdate())),'00:00:00') inicio,
                   COALESCE((SELECT top 1 g.descripcionestadopatrullaje 
                             FROM ssf.programapatrullajes f
                             JOIN ssf.estadopatrullaje g ON f.id_estadopatrullaje = g.id_estadopatrullaje
                             WHERE f.id_propuestapatrullaje = a.id_propuestapatrullaje
                             AND YEAR(f.fechapatrullaje) = YEAR(getutcdate()) 
                             AND MONTH(f.fechapatrullaje) = MONTH(getutcdate()) 
                             AND DAY(f.fechapatrullaje) = DAY(getutcdate())),c.descripcionestadopropuesta)	descripcionestadopatrullaje
            FROM ssf.propuestaspatrullajes a
            JOIN ssf.rutas b ON a.id_ruta = b.id_ruta                        
            JOIN ssf.estadopropuesta c ON a.id_estadopropuesta = c.id_estadopropuesta
            JOIN ssf.niveles d ON a.riesgopatrullaje=d.id_nivel
            JOIN ssf.propuestaspatrullajescomplementossf e ON a.id_propuestapatrullaje=e.id_propuestapatrullaje
            JOIN ssf.clasepatrullaje m ON a.id_clasepatrullaje=m.id_clasepatrullaje
            JOIN ssf.tipopatrullaje k ON b.id_tipopatrullaje = k.id_tipopatrullaje
            WHERE YEAR(a.fechapatrullaje)= @pAnio
            AND MONTH(a.fechapatrullaje)= @pMes
            AND k.descripcion = @pTipo
            AND b.regionssf = @pRegion
            AND m.descripcion= @pClase
            ORDER BY a.fechapatrullaje";

            object[] parametros = new object[]
            {
                new SqlParameter("@pAnio", anio),
                new SqlParameter("@pMes", mes),
                new SqlParameter("@pTipo", tipo),
                new SqlParameter("@pRegion", region),
                new SqlParameter("@pClase", clase)
            };

            return _programaContext.PatrullajesVista.FromSqlRaw(sqlQuery, parametros).ToList();
        }

        //Dan servicio a las opciones 6,7,8,9  Ordinario  -- Propuestas
        public List<PatrullajeVista> ObtenerPropuestasPorFiltroEstado(string tipo, int region, int anio, int mes, string clase, string estadoPropuesta)
        {
            // 6  - paramEstado='Pendiente de autorizacion por la SSF'
            // 7  - paramEstado='Autorizada'
            // 8  - paramEstado='Rechazada'
            // 9  - paramEstado='Aprobada por comandancia regional'
            string sqlQuery = @"SELECT a.id_propuestapatrullaje id,a.id_ruta,a.fechapatrullaje,a.fechapatrullaje fechatermino,a.id_puntoresponsable,b.clave,
                   b.regionmilitarsdn,b.regionssf,b.observaciones observacionesruta,a.ultimaactualizacion,a.id_usuario,d.descripcionnivel, 
                   0 id_usuarioresponsablepatrullaje, '' observaciones, 0 riesgopatrullaje,
                   COALESCE(a.solicitudoficioautorizacion, '') solicitudoficiocomision,
                   COALESCE(a.oficioautorizacion, '') as oficiocomision,
                   COALESCE((SELECT STRING_AGG(CAST(ubicacion as nvarchar(MAX)), '-') WITHIN GROUP(ORDER BY f.posicion ASC)
                             FROM ssf.itinerario f join ssf.puntospatrullaje g on f.id_punto = g.id_punto
                             WHERE f.id_ruta = a.id_ruta),'') as itinerario,
                   COALESCE((SELECT top 1 f.inicio FROM ssf.programapatrullajes f
                             WHERE f.id_propuestapatrullaje = a.id_propuestapatrullaje
                             AND YEAR(f.fechapatrullaje) = YEAR(getutcdate()) 
                             AND MONTH(f.fechapatrullaje) = MONTH(getutcdate()) 
                             AND DAY(f.fechapatrullaje) = DAY(getutcdate())),'00:00:00') inicio,
                   COALESCE((SELECT top 1 g.descripcionestadopatrullaje 
                             FROM ssf.programapatrullajes f
                             JOIN ssf.estadopatrullaje g ON f.id_estadopatrullaje = g.id_estadopatrullaje
                             WHERE f.id_propuestapatrullaje = a.id_propuestapatrullaje
                             AND YEAR(f.fechapatrullaje) = YEAR(getutcdate()) 
                             AND MONTH(f.fechapatrullaje) = MONTH(getutcdate()) 
                             AND DAY(f.fechapatrullaje) = DAY(getutcdate())),c.descripcionestadopropuesta)	descripcionestadopatrullaje
            FROM ssf.propuestaspatrullajes a
            JOIN ssf.rutas b ON a.id_ruta = b.id_ruta            
            JOIN SSF.clasepatrullaje m ON a.id_clasepatrullaje = m.id_clasepatrullaje
            JOIN ssf.estadopropuesta c ON a.id_estadopropuesta = c.id_estadopropuesta
            JOIN ssf.niveles d ON a.riesgopatrullaje=d.id_nivel
            JOIN ssf.tipopatrullaje k ON b.id_tipopatrullaje = k.id_tipopatrullaje
            WHERE YEAR(a.fechapatrullaje)= @paramAnio
            AND MONTH(a.fechapatrullaje)= @paramMes
            AND k.descripcion = @paramTipo
            AND b.regionssf = @paramRegion
            AND m.descripcion = @paramClase
            AND c.descripcionestadopropuesta = @paramEstado
            ORDER BY a.fechapatrullaje";

            object[] parametros = new object[]
            {
                new SqlParameter("@paramAnio", anio),
                new SqlParameter("@paramMes", mes),
                new SqlParameter("@paramTipo", tipo),
                new SqlParameter("@paramRegion", region),
                new SqlParameter("@paramClase", clase),
                new SqlParameter("@paramEstado", estadoPropuesta)
            };

            return _programaContext.PatrullajesVista.FromSqlRaw(sqlQuery, parametros).ToList();
        }

        //Caso 6,7,8,9 Extraordinario  - 
        public List<PatrullajeVista> ObtenerPropuestasExtraordinariasPorFiltroEstado(string tipo, int region, int anio, int mes, string clase, string estadoPropuesta)
        {
            // 6 -- 'Pendiente de autorizacion por la SSF'
            // 7 -- 'Autorizada'
            // 8 -- 'Rechazada'
            // 9 -- 'Aprobada por comandancia regional'
            string sqlQuery = @"SELECT a.id_propuestapatrullaje id, a.id_ruta,a.fechapatrullaje, a.id_puntoresponsable, b.clave,
                   b.regionmilitarsdn,b.regionssf,b.observaciones observacionesruta, a.ultimaactualizacion,a.id_usuario,d.descripcionnivel,
                   e.fechatermino,0 id_usuarioresponsablepatrullaje, '' observaciones, 0 riesgopatrullaje,
                   COALESCE(a.solicitudoficioautorizacion, '') solicitudoficiocomision,
                   COALESCE(a.oficioautorizacion, '') as oficiocomision,
                   COALESCE((SELECT STRING_AGG(CAST(ubicacion as nvarchar(MAX)), '-') WITHIN GROUP(ORDER BY f.posicion ASC)
                             FROM ssf.itinerario f join ssf.puntospatrullaje g on f.id_punto = g.id_punto
                             WHERE f.id_ruta = a.id_ruta),'') as itinerario,
                   COALESCE((SELECT top 1 f.inicio FROM ssf.programapatrullajes f
                             WHERE f.id_propuestapatrullaje = a.id_propuestapatrullaje
                             AND YEAR(f.fechapatrullaje) = YEAR(getutcdate()) 
                             AND MONTH(f.fechapatrullaje) = MONTH(getutcdate()) 
                             AND DAY(f.fechapatrullaje) = DAY(getutcdate())),'00:00:00') inicio,
                   COALESCE((SELECT top 1 g.descripcionestadopatrullaje 
                             FROM ssf.programapatrullajes f
                             JOIN ssf.estadopatrullaje g ON f.id_estadopatrullaje = g.id_estadopatrullaje
                             WHERE f.id_propuestapatrullaje = a.id_propuestapatrullaje
                             AND YEAR(f.fechapatrullaje) = YEAR(getutcdate()) 
                             AND MONTH(f.fechapatrullaje) = MONTH(getutcdate()) 
                             AND DAY(f.fechapatrullaje) = DAY(getutcdate())),c.descripcionestadopropuesta)	descripcionestadopatrullaje
            FROM ssf.propuestaspatrullajes a
            JOIN ssf.rutas b ON a.id_ruta = b.id_ruta                        
            JOIN ssf.estadopropuesta c ON a.id_estadopropuesta = c.id_estadopropuesta
            JOIN ssf.niveles d ON a.riesgopatrullaje=d.id_nivel
            JOIN ssf.propuestaspatrullajescomplementossf e ON a.id_propuestapatrullaje=e.id_propuestapatrullaje
            JOIN ssf.clasepatrullaje m ON a.id_clasepatrullaje=m.id_clasepatrullaje
            JOIN ssf.tipopatrullaje k ON b.id_tipopatrullaje = k.id_tipopatrullaje
            WHERE YEAR(a.fechapatrullaje)= @pAnio
            AND MONTH(a.fechapatrullaje)= @pMes
            AND k.descripcion = @pTipo
            AND b.regionssf = @pRegion
            AND m.descripcion= @pClase
            AND c.descripcionestadopropuesta= @pEstado
            ORDER BY a.fechapatrullaje";

            object[] parametros = new object[]
            {
                new SqlParameter("@pAnio", anio),
                new SqlParameter("@pMes", mes),
                new SqlParameter("@pTipo", tipo),
                new SqlParameter("@pRegion", region),
                new SqlParameter("@pClase", clase),
                new SqlParameter("@pEstado", estadoPropuesta)
            };

            return _programaContext.PatrullajesVista.FromSqlRaw(sqlQuery, parametros).ToList();
        }

        //Caso 1 Programas EN PROGRESO Periodo 1 - Un día
        public List<PatrullajeVista> ObtenerProgramasEnProgresoPorDia(string tipo, int region, int anio, int mes, int dia)
        {
            string sqlQuery = @"SELECT a.id_programa id, a.id_ruta, a.fechapatrullaje, a.fechapatrullaje fechatermino, a.inicio, a.id_puntoresponsable, b.clave, b.regionmilitarsdn,
                                       b.regionssf, b.observaciones observacionesruta, c.descripcionestadopatrullaje, a.observaciones, a.riesgopatrullaje,
                                       d.descripcionnivel, a.ultimaactualizacion, a.id_usuario, a.id_usuarioresponsablepatrullaje,
                                       COALESCE(a.solicitudoficiocomision,'') solicitudoficiocomision,
                                       COALESCE(a.oficiocomision,'') oficiocomision,
                                       COALESCE((SELECT STRING_AGG(CAST(ubicacion as nvarchar(MAX)), '-') WITHIN GROUP(ORDER BY f.posicion ASC)
                                                 FROM ssf.itinerario f join ssf.puntospatrullaje g on f.id_punto = g.id_punto
                                                 WHERE f.id_ruta = a.id_ruta),'') as itinerario
                                FROM ssf.programapatrullajes a
                                JOIN ssf.rutas b ON a.id_ruta=b.id_ruta
                                JOIN ssf.estadopatrullaje c ON a.id_estadopatrullaje=c.id_estadopatrullaje
                                JOIN ssf.niveles d ON a.riesgopatrullaje=d.id_nivel
                                JOIN ssf.tipopatrullaje e ON b.id_tipopatrullaje=e.id_tipopatrullaje
                                WHERE e.descripcion= @pTipo 
                                AND b.regionssf= @pRegion 
                                AND a.fechapatrullaje= DATEFROMPARTS (@pAnio, @pMes, @pDia)        
                                AND a.id_estadopatrullaje = (SELECT id_estadopatrullaje FROM ssf.estadopatrullaje WHERE descripcionestadopatrullaje='En progreso') 
                                ORDER BY a.fechapatrullaje,a.inicio";

            object[] parametros = new object[]
            {
                new SqlParameter("@pTipo", tipo),
                new SqlParameter("@pRegion", region),
                new SqlParameter("@pAnio", anio),
                new SqlParameter("@pMes", mes),
                new SqlParameter("@pDia", dia)
             };

            return _programaContext.PatrullajesVista.FromSqlRaw(sqlQuery, parametros).ToList();
        }

        //Caso 1 Programas EN PROGRESO Periodo 2 - Un mes
        public List<PatrullajeVista> ObtenerProgramasEnProgresoPorMes(string tipo, int region, int anio, int mes)
        {
            string sqlQuery = @"SELECT a.id_programa id, a.id_ruta, a.fechapatrullaje, a.fechapatrullaje fechatermino, a.inicio, a.id_puntoresponsable, b.clave, b.regionmilitarsdn,
                                       b.regionssf, b.observaciones observacionesruta, c.descripcionestadopatrullaje, a.observaciones, a.riesgopatrullaje,
                                       d.descripcionnivel, a.ultimaactualizacion, a.id_usuario, a.id_usuarioresponsablepatrullaje,
                                       COALESCE(a.solicitudoficiocomision,'') solicitudoficiocomision,
                                       COALESCE(a.oficiocomision,'') oficiocomision,
                                       COALESCE((SELECT STRING_AGG(CAST(ubicacion as nvarchar(MAX)), '-') WITHIN GROUP(ORDER BY f.posicion ASC)
                                                 FROM ssf.itinerario f join ssf.puntospatrullaje g on f.id_punto = g.id_punto
                                                 WHERE f.id_ruta = a.id_ruta),'') as itinerario
                                FROM ssf.programapatrullajes a
                                JOIN ssf.rutas b ON a.id_ruta=b.id_ruta
                                JOIN ssf.estadopatrullaje c ON a.id_estadopatrullaje=c.id_estadopatrullaje
                                JOIN ssf.niveles d ON a.riesgopatrullaje=d.id_nivel
                                JOIN ssf.tipopatrullaje e ON b.id_tipopatrullaje=e.id_tipopatrullaje
                                WHERE e.descripcion= @pTipo 
                                AND b.regionssf= @pRegion 
                                AND YEAR(a.fechapatrullaje) = @pAnio AND MONTH(a.fechapatrullaje) = @pMes
                                AND a.id_estadopatrullaje = (SELECT id_estadopatrullaje FROM ssf.estadopatrullaje WHERE descripcionestadopatrullaje='En progreso') 
                                ORDER BY a.fechapatrullaje,a.inicio";

            object[] parametros = new object[]
            {
                new SqlParameter("@pTipo", tipo),
                new SqlParameter("@pRegion", region),
                new SqlParameter("@pAnio", anio),
                new SqlParameter("@pMes", mes)
             };

            return _programaContext.PatrullajesVista.FromSqlRaw(sqlQuery, parametros).ToList();
        }

        //Caso 1 Programas EN PROGRESO Periodo 3 - todos
        public List<PatrullajeVista> ObtenerProgramasEnProgreso(string tipo, int region)
        {
            string sqlQuery = @"SELECT a.id_programa id, a.id_ruta, a.fechapatrullaje, a.fechapatrullaje fechatermino, a.inicio, a.id_puntoresponsable, b.clave, b.regionmilitarsdn,
                                       b.regionssf, b.observaciones observacionesruta, c.descripcionestadopatrullaje, a.observaciones, a.riesgopatrullaje,
                                       d.descripcionnivel, a.ultimaactualizacion, a.id_usuario, a.id_usuarioresponsablepatrullaje,
                                       COALESCE(a.solicitudoficiocomision,'') solicitudoficiocomision,
                                       COALESCE(a.oficiocomision,'') oficiocomision,
                                       COALESCE((SELECT STRING_AGG(CAST(ubicacion as nvarchar(MAX)), '-') WITHIN GROUP(ORDER BY f.posicion ASC)
                                                 FROM ssf.itinerario f join ssf.puntospatrullaje g on f.id_punto = g.id_punto
                                                 WHERE f.id_ruta = a.id_ruta),'') as itinerario
                                FROM ssf.programapatrullajes a
                                JOIN ssf.rutas b ON a.id_ruta=b.id_ruta
                                JOIN ssf.estadopatrullaje c ON a.id_estadopatrullaje=c.id_estadopatrullaje
                                JOIN ssf.niveles d ON a.riesgopatrullaje=d.id_nivel
                                JOIN ssf.tipopatrullaje e ON b.id_tipopatrullaje=e.id_tipopatrullaje
                                WHERE e.descripcion= @pTipo 
                                AND b.regionssf= @pRegion                                 
                                AND a.id_estadopatrullaje = (SELECT id_estadopatrullaje FROM ssf.estadopatrullaje WHERE descripcionestadopatrullaje='En progreso') 
                                ORDER BY a.fechapatrullaje,a.inicio";

            object[] parametros = new object[]
            {
                new SqlParameter("@pTipo", tipo),
                new SqlParameter("@pRegion", region),
             };

            return _programaContext.PatrullajesVista.FromSqlRaw(sqlQuery, parametros).ToList();
        }

        //Caso 2 Programas CONCLUIDOS Periodo 1 - Un día
        public List<PatrullajeVista> ObtenerProgramasConcluidosPorDia(string tipo, int region, int anio, int mes, int dia)
        {
            string sqlQuery = @"SELECT a.id_programa id, a.id_ruta, a.fechapatrullaje, a.fechapatrullaje fechatermino, a.inicio, a.id_puntoresponsable, b.clave, b.regionmilitarsdn,
                                       b.regionssf, b.observaciones observacionesruta, c.descripcionestadopatrullaje, a.observaciones, a.riesgopatrullaje,
                                       d.descripcionnivel, a.ultimaactualizacion, a.id_usuario, a.id_usuarioresponsablepatrullaje,
                                       COALESCE(a.solicitudoficiocomision,'') solicitudoficiocomision,
                                       COALESCE(a.oficiocomision,'') oficiocomision,
                                       COALESCE((SELECT STRING_AGG(CAST(ubicacion as nvarchar(MAX)), '-') WITHIN GROUP(ORDER BY f.posicion ASC)
                                                 FROM ssf.itinerario f join ssf.puntospatrullaje g on f.id_punto = g.id_punto
                                                 WHERE f.id_ruta = a.id_ruta),'') as itinerario
                                FROM ssf.programapatrullajes a
                                JOIN ssf.rutas b ON a.id_ruta=b.id_ruta
                                JOIN ssf.estadopatrullaje c ON a.id_estadopatrullaje=c.id_estadopatrullaje
                                JOIN ssf.niveles d ON a.riesgopatrullaje=d.id_nivel
                                JOIN ssf.tipopatrullaje e ON b.id_tipopatrullaje=e.id_tipopatrullaje
                                WHERE e.descripcion= @pTipo 
                                AND b.regionssf= @pRegion 
                                AND a.fechapatrullaje= DATEFROMPARTS(@pAnio, @pMes, @pDia)        
                                AND a.id_estadopatrullaje = (SELECT id_estadopatrullaje FROM ssf.estadopatrullaje WHERE descripcionestadopatrullaje='Concluido') 
                                ORDER BY a.fechapatrullaje,a.inicio";

            object[] parametros = new object[]
            {
                new SqlParameter("@pTipo", tipo),
                new SqlParameter("@pRegion", region),
                new SqlParameter("@pAnio", anio),
                new SqlParameter("@pMes", mes),
                new SqlParameter("@pDia", dia)
             };

            return _programaContext.PatrullajesVista.FromSqlRaw(sqlQuery, parametros).ToList();
        }

        //Caso 2 Programas CONCLUIDOS Periodo 2 - Un mes
        public List<PatrullajeVista> ObtenerProgramasConcluidosPorMes(string tipo, int region, int anio, int mes)
        {
            string sqlQuery = @"SELECT a.id_programa id, a.id_ruta, a.fechapatrullaje, a.fechapatrullaje fechatermino, a.inicio, a.id_puntoresponsable, b.clave, b.regionmilitarsdn,
                                       b.regionssf, b.observaciones observacionesruta, c.descripcionestadopatrullaje, a.observaciones, a.riesgopatrullaje,
                                       d.descripcionnivel, a.ultimaactualizacion, a.id_usuario, a.id_usuarioresponsablepatrullaje,
                                       COALESCE(a.solicitudoficiocomision,'') solicitudoficiocomision,
                                       COALESCE(a.oficiocomision,'') oficiocomision,
                                       COALESCE((SELECT STRING_AGG(CAST(ubicacion as nvarchar(MAX)), '-') WITHIN GROUP(ORDER BY f.posicion ASC)
                                                 FROM ssf.itinerario f join ssf.puntospatrullaje g on f.id_punto = g.id_punto
                                                 WHERE f.id_ruta = a.id_ruta),'') as itinerario
                                FROM ssf.programapatrullajes a
                                JOIN ssf.rutas b ON a.id_ruta=b.id_ruta
                                JOIN ssf.estadopatrullaje c ON a.id_estadopatrullaje=c.id_estadopatrullaje
                                JOIN ssf.niveles d ON a.riesgopatrullaje=d.id_nivel
                                JOIN ssf.tipopatrullaje e ON b.id_tipopatrullaje=e.id_tipopatrullaje
                                WHERE e.descripcion= @pTipo 
                                AND b.regionssf= @pRegion 
                                AND YEAR(a.fechapatrullaje) = @pAnio AND MONTH(a.fechapatrullaje) = @pMes
                                AND a.id_estadopatrullaje = (SELECT id_estadopatrullaje FROM ssf.estadopatrullaje WHERE descripcionestadopatrullaje='Concluido') 
                                ORDER BY a.fechapatrullaje,a.inicio";

            object[] parametros = new object[]
            {
                new SqlParameter("@pTipo", tipo),
                new SqlParameter("@pRegion", region),
                new SqlParameter("@pAnio", anio),
                new SqlParameter("@pMes", mes)
             };

            return _programaContext.PatrullajesVista.FromSqlRaw(sqlQuery, parametros).ToList();
        }

        //Caso 2 Programas CONCLUIDOS Periodo 3 - todos
        public List<PatrullajeVista> ObtenerProgramasConcluidos(string tipo, int region)
        {
            string sqlQuery = @"SELECT a.id_programa id, a.id_ruta, a.fechapatrullaje, a.fechapatrullaje fechatermino, a.inicio, a.id_puntoresponsable, b.clave, b.regionmilitarsdn,
                                       b.regionssf, b.observaciones observacionesruta, c.descripcionestadopatrullaje, a.observaciones, a.riesgopatrullaje,
                                       d.descripcionnivel, a.ultimaactualizacion, a.id_usuario, a.id_usuarioresponsablepatrullaje,
                                       COALESCE(a.solicitudoficiocomision,'') solicitudoficiocomision,
                                       COALESCE(a.oficiocomision,'') oficiocomision,
                                       COALESCE((SELECT STRING_AGG(CAST(ubicacion as nvarchar(MAX)), '-') WITHIN GROUP(ORDER BY f.posicion ASC)
                                                 FROM ssf.itinerario f join ssf.puntospatrullaje g on f.id_punto = g.id_punto
                                                 WHERE f.id_ruta = a.id_ruta),'') as itinerario
                                FROM ssf.programapatrullajes a
                                JOIN ssf.rutas b ON a.id_ruta=b.id_ruta
                                JOIN ssf.estadopatrullaje c ON a.id_estadopatrullaje=c.id_estadopatrullaje
                                JOIN ssf.niveles d ON a.riesgopatrullaje=d.id_nivel
                                JOIN ssf.tipopatrullaje e ON b.id_tipopatrullaje=e.id_tipopatrullaje
                                WHERE e.descripcion= @pTipo 
                                AND b.regionssf= @pRegion                                 
                                AND a.id_estadopatrullaje = (SELECT id_estadopatrullaje FROM ssf.estadopatrullaje WHERE descripcionestadopatrullaje='Concluido') 
                                ORDER BY a.fechapatrullaje,a.inicio";

            object[] parametros = new object[]
            {
                new SqlParameter("@pTipo", tipo),
                new SqlParameter("@pRegion", region),
             };

            return _programaContext.PatrullajesVista.FromSqlRaw(sqlQuery, parametros).ToList();
        }

        //Caso 3 Programas CANCELADOS Periodo 1 - Un día
        public List<PatrullajeVista> ObtenerProgramasCanceladosPorDia(string tipo, int region, int anio, int mes, int dia)
        {
            string sqlQuery = @"SELECT a.id_programa id, a.id_ruta, a.fechapatrullaje, a.fechapatrullaje fechatermino, a.inicio, a.id_puntoresponsable, b.clave, b.regionmilitarsdn,
                                       b.regionssf, b.observaciones observacionesruta, c.descripcionestadopatrullaje, a.observaciones, a.riesgopatrullaje,
                                       d.descripcionnivel, a.ultimaactualizacion, a.id_usuario, a.id_usuarioresponsablepatrullaje,
                                       COALESCE(a.solicitudoficiocomision,'') solicitudoficiocomision,
                                       COALESCE(a.oficiocomision,'') oficiocomision,
                                       COALESCE((SELECT STRING_AGG(CAST(ubicacion as nvarchar(MAX)), '-') WITHIN GROUP(ORDER BY f.posicion ASC)
                                                 FROM ssf.itinerario f join ssf.puntospatrullaje g on f.id_punto = g.id_punto
                                                 WHERE f.id_ruta = a.id_ruta),'') as itinerario
                                FROM ssf.programapatrullajes a
                                JOIN ssf.rutas b ON a.id_ruta=b.id_ruta
                                JOIN ssf.estadopatrullaje c ON a.id_estadopatrullaje=c.id_estadopatrullaje
                                JOIN ssf.niveles d ON a.riesgopatrullaje=d.id_nivel
                                JOIN ssf.tipopatrullaje e ON b.id_tipopatrullaje=e.id_tipopatrullaje
                                WHERE e.descripcion= @pTipo 
                                AND b.regionssf= @pRegion 
                                AND a.fechapatrullaje= DATEFROMPARTS (@pAnio, @pMes, @pDia)        
                                AND a.id_estadopatrullaje IN ((SELECT id_estadopatrullaje FROM ssf.estadopatrullaje WHERE descripcionestadopatrullaje like 'Cancel%') )
                                ORDER BY a.fechapatrullaje,a.inicio";

            object[] parametros = new object[]
            {
                new SqlParameter("@pTipo", tipo),
                new SqlParameter("@pRegion", region),
                new SqlParameter("@pAnio", anio),
                new SqlParameter("@pMes", mes),
                new SqlParameter("@pDia", dia)
             };

            return _programaContext.PatrullajesVista.FromSqlRaw(sqlQuery, parametros).ToList();
        }

        //Caso 3 Programas CANCELADOS Periodo 2 - Un mes
        public List<PatrullajeVista> ObtenerProgramasCanceladosPorMes(string tipo, int region, int anio, int mes)
        {
            string sqlQuery = @"SELECT a.id_programa id, a.id_ruta, a.fechapatrullaje, a.fechapatrullaje fechatermino, a.inicio, a.id_puntoresponsable, b.clave, b.regionmilitarsdn,
                                       b.regionssf, b.observaciones observacionesruta, c.descripcionestadopatrullaje, a.observaciones, a.riesgopatrullaje,
                                       d.descripcionnivel, a.ultimaactualizacion, a.id_usuario, a.id_usuarioresponsablepatrullaje,
                                       COALESCE(a.solicitudoficiocomision,'') solicitudoficiocomision,
                                       COALESCE(a.oficiocomision,'') oficiocomision,
                                       COALESCE((SELECT STRING_AGG(CAST(ubicacion as nvarchar(MAX)), '-') WITHIN GROUP(ORDER BY f.posicion ASC)
                                                 FROM ssf.itinerario f join ssf.puntospatrullaje g on f.id_punto = g.id_punto
                                                 WHERE f.id_ruta = a.id_ruta),'') as itinerario
                                FROM ssf.programapatrullajes a
                                JOIN ssf.rutas b ON a.id_ruta=b.id_ruta
                                JOIN ssf.estadopatrullaje c ON a.id_estadopatrullaje=c.id_estadopatrullaje
                                JOIN ssf.niveles d ON a.riesgopatrullaje=d.id_nivel
                                JOIN ssf.tipopatrullaje e ON b.id_tipopatrullaje=e.id_tipopatrullaje
                                WHERE e.descripcion= @pTipo 
                                AND b.regionssf= @pRegion 
                                AND YEAR(a.fechapatrullaje) = @pAnio AND MONTH(a.fechapatrullaje) = @pMes
                                AND a.id_estadopatrullaje IN ((SELECT id_estadopatrullaje FROM ssf.estadopatrullaje WHERE descripcionestadopatrullaje like 'Cancel%') )
                                ORDER BY a.fechapatrullaje,a.inicio";

            object[] parametros = new object[]
            {
                new SqlParameter("@pTipo", tipo),
                new SqlParameter("@pRegion", region),
                new SqlParameter("@pAnio", anio),
                new SqlParameter("@pMes", mes)
             };

            return _programaContext.PatrullajesVista.FromSqlRaw(sqlQuery, parametros).ToList();
        }

        //Caso 3 Programas CANCELADOS Periodo 3 - todos
        public List<PatrullajeVista> ObtenerProgramasCancelados(string tipo, int region)
        {
            string sqlQuery = @"SELECT a.id_programa id, a.id_ruta, a.fechapatrullaje, a.fechapatrullaje fechatermino, a.inicio, a.id_puntoresponsable, b.clave, b.regionmilitarsdn,
                                       b.regionssf, b.observaciones observacionesruta, c.descripcionestadopatrullaje, a.observaciones, a.riesgopatrullaje,
                                       d.descripcionnivel, a.ultimaactualizacion, a.id_usuario, a.id_usuarioresponsablepatrullaje,
                                       COALESCE(a.solicitudoficiocomision,'') solicitudoficiocomision,
                                       COALESCE(a.oficiocomision,'') oficiocomision,
                                       COALESCE((SELECT STRING_AGG(CAST(ubicacion as nvarchar(MAX)), '-') WITHIN GROUP(ORDER BY f.posicion ASC)
                                                 FROM ssf.itinerario f join ssf.puntospatrullaje g on f.id_punto = g.id_punto
                                                 WHERE f.id_ruta = a.id_ruta),'') as itinerario
                                FROM ssf.programapatrullajes a
                                JOIN ssf.rutas b ON a.id_ruta=b.id_ruta
                                JOIN ssf.estadopatrullaje c ON a.id_estadopatrullaje=c.id_estadopatrullaje
                                JOIN ssf.niveles d ON a.riesgopatrullaje=d.id_nivel
                                JOIN ssf.tipopatrullaje e ON b.id_tipopatrullaje=e.id_tipopatrullaje
                                WHERE e.descripcion= @pTipo 
                                AND b.regionssf= @pRegion                                 
                                AND a.id_estadopatrullaje IN ((SELECT id_estadopatrullaje FROM ssf.estadopatrullaje WHERE descripcionestadopatrullaje like 'Cancel%') )
                                ORDER BY a.fechapatrullaje,a.inicio";

            object[] parametros = new object[]
            {
                new SqlParameter("@pTipo", tipo),
                new SqlParameter("@pRegion", region),
             };

            return _programaContext.PatrullajesVista.FromSqlRaw(sqlQuery, parametros).ToList();
        }

        //Caso 4 Programas Periodo 1 - Un día
        public List<PatrullajeVista> ObtenerProgramasPorDia(string tipo, int region, int anio, int mes, int dia)
        {
            string sqlQuery = @"SELECT a.id_programa id, a.id_ruta, a.fechapatrullaje, a.fechapatrullaje fechatermino, a.inicio, a.id_puntoresponsable, b.clave, b.regionmilitarsdn,
                                       b.regionssf, b.observaciones observacionesruta, c.descripcionestadopatrullaje, a.observaciones, a.riesgopatrullaje,
                                       d.descripcionnivel, a.ultimaactualizacion, a.id_usuario, a.id_usuarioresponsablepatrullaje,
                                       COALESCE(a.solicitudoficiocomision,'') solicitudoficiocomision,
                                       COALESCE(a.oficiocomision,'') oficiocomision,
                                       COALESCE((SELECT STRING_AGG(CAST(ubicacion as nvarchar(MAX)), '-') WITHIN GROUP(ORDER BY f.posicion ASC)
                                                 FROM ssf.itinerario f join ssf.puntospatrullaje g on f.id_punto = g.id_punto
                                                 WHERE f.id_ruta = a.id_ruta),'') as itinerario
                                FROM ssf.programapatrullajes a
                                JOIN ssf.rutas b ON a.id_ruta=b.id_ruta
                                JOIN ssf.estadopatrullaje c ON a.id_estadopatrullaje=c.id_estadopatrullaje
                                JOIN ssf.niveles d ON a.riesgopatrullaje=d.id_nivel
                                JOIN ssf.tipopatrullaje e ON b.id_tipopatrullaje=e.id_tipopatrullaje
                                WHERE e.descripcion= @pTipo 
                                AND b.regionssf= @pRegion 
                                AND a.fechapatrullaje= DATEFROMPARTS (@pAnio, @pMes, @pDia)        
                                ORDER BY a.fechapatrullaje,a.inicio";

            object[] parametros = new object[]
            {
                new SqlParameter("@pTipo", tipo),
                new SqlParameter("@pRegion", region),
                new SqlParameter("@pAnio", anio),
                new SqlParameter("@pMes", mes),
                new SqlParameter("@pDia", dia)
             };

            return _programaContext.PatrullajesVista.FromSqlRaw(sqlQuery, parametros).ToList();
        }

        //Caso 4 Programas Periodo 2 - Un mes  --- Aplica también para el Caso 0 Ordinario
        public List<PatrullajeVista> ObtenerProgramasPorMes(string tipo, int region, int anio, int mes)
        {
            string sqlQuery = @"SELECT a.id_programa id, a.id_ruta, a.fechapatrullaje, a.fechapatrullaje fechatermino, a.inicio, a.id_puntoresponsable, b.clave, b.regionmilitarsdn,
                                       b.regionssf, b.observaciones observacionesruta, c.descripcionestadopatrullaje, a.observaciones, a.riesgopatrullaje,
                                       d.descripcionnivel, a.ultimaactualizacion, a.id_usuario, a.id_usuarioresponsablepatrullaje,
                                       COALESCE(a.solicitudoficiocomision,'') solicitudoficiocomision,
                                       COALESCE(a.oficiocomision,'') oficiocomision,
                                       COALESCE((SELECT STRING_AGG(CAST(ubicacion as nvarchar(MAX)), '-') WITHIN GROUP(ORDER BY f.posicion ASC)
                                                 FROM ssf.itinerario f join ssf.puntospatrullaje g on f.id_punto = g.id_punto
                                                 WHERE f.id_ruta = a.id_ruta),'') as itinerario
                                FROM ssf.programapatrullajes a
                                JOIN ssf.rutas b ON a.id_ruta=b.id_ruta
                                JOIN ssf.estadopatrullaje c ON a.id_estadopatrullaje=c.id_estadopatrullaje
                                JOIN ssf.niveles d ON a.riesgopatrullaje=d.id_nivel
                                JOIN ssf.tipopatrullaje e ON b.id_tipopatrullaje=e.id_tipopatrullaje
                                WHERE e.descripcion= @pTipo 
                                AND b.regionssf= @pRegion 
                                AND YEAR(a.fechapatrullaje) = @pAnio AND MONTH(a.fechapatrullaje) = @pMes        
                                ORDER BY a.fechapatrullaje,a.inicio";

            object[] parametros = new object[]
            {
                new SqlParameter("@pTipo", tipo),
                new SqlParameter("@pRegion", region),
                new SqlParameter("@pAnio", anio),
                new SqlParameter("@pMes", mes)
             };

            return _programaContext.PatrullajesVista.FromSqlRaw(sqlQuery, parametros).ToList();
        }

        //Caso 4 Programas Periodo 3 - Todos
        public List<PatrullajeVista> ObtenerProgramas(string tipo, int region)
        {
            string sqlQuery = @"SELECT a.id_programa id, a.id_ruta, a.fechapatrullaje,a.fechapatrullaje fechatermino, a.inicio, a.id_puntoresponsable, b.clave, b.regionmilitarsdn,
                                       b.regionssf, b.observaciones observacionesruta, c.descripcionestadopatrullaje, a.observaciones, a.riesgopatrullaje,
                                       d.descripcionnivel, a.ultimaactualizacion, a.id_usuario, a.id_usuarioresponsablepatrullaje,
                                       COALESCE(a.solicitudoficiocomision,'') solicitudoficiocomision,
                                       COALESCE(a.oficiocomision,'') oficiocomision,
                                       COALESCE((SELECT STRING_AGG(CAST(ubicacion as nvarchar(MAX)), '-') WITHIN GROUP(ORDER BY f.posicion ASC)
                                                 FROM ssf.itinerario f join ssf.puntospatrullaje g on f.id_punto = g.id_punto
                                                 WHERE f.id_ruta = a.id_ruta),'') as itinerario
                                FROM ssf.programapatrullajes a
                                JOIN ssf.rutas b ON a.id_ruta=b.id_ruta
                                JOIN ssf.estadopatrullaje c ON a.id_estadopatrullaje=c.id_estadopatrullaje
                                JOIN ssf.niveles d ON a.riesgopatrullaje=d.id_nivel
                                JOIN ssf.tipopatrullaje e ON b.id_tipopatrullaje=e.id_tipopatrullaje
                                WHERE e.descripcion= @pTipo 
                                AND b.regionssf= @pRegion      
                                ORDER BY a.fechapatrullaje,a.inicio";

            object[] parametros = new object[]
            {
                new SqlParameter("@pTipo", tipo),
                new SqlParameter("@pRegion", region)
             };

            return _programaContext.PatrullajesVista.FromSqlRaw(sqlQuery, parametros).ToList();
        }

        public void AgregaProgramaFechasMultiples(ProgramaPatrullaje pp, List<DateTime> fechas)
        {
            var lstProgramas = new List<ProgramaPatrullaje>();
            var lstPropuestas = new List<PropuestaPatrullaje>();

            string edoAutorizada = "Autorizada";
            var idEdoAutorizada = _programaContext.EstadosPropuesta.Where(x => x.DescripcionEstadoPropuesta == edoAutorizada).Select(x => x.IdEstadoPropuesta).ToList();

            var propuestas = _programaContext.PropuestasPatrullajes.Where(x => x.IdPropuestaPatrullaje == pp.IdPropuestaPatrullaje).ToList();

            foreach ( var prop in propuestas ) 
            {
                prop.IdEstadoPropuesta = idEdoAutorizada[0];
            }

            foreach (var fecha in fechas)
            {
                var programa = new ProgramaPatrullaje
                {
                    IdRuta = pp.IdRuta,
                    IdUsuario = pp.IdUsuario,
                    IdPuntoResponsable = pp.IdPuntoResponsable,
                    IdPropuestaPatrullaje = pp.IdPropuestaPatrullaje,
                    IdRutaOriginal = pp.IdRutaOriginal,
                    RiesgoPatrullaje = pp.RiesgoPatrullaje,
                    IdApoyoPatrullaje = pp.IdApoyoPatrullaje,
                    UltimaActualizacion = pp.UltimaActualizacion,
                    FechaPatrullaje = pp.FechaPatrullaje
                };

                lstProgramas.Add(programa);
            }

            _programaContext.ProgramasPatrullajes.AddRange(lstProgramas);
            _programaContext.PropuestasPatrullajes.UpdateRange(propuestas);
            _programaContext.SaveChanges();
        }

        public void AgregaPropuestasFechasMultiples(ProgramaPatrullaje pp, List<DateTime> fechas, string clase)
        {
            string edoCreada = "Creada";
            var idEdoCreada = _programaContext.EstadosPropuesta.Where(x => x.DescripcionEstadoPropuesta == edoCreada).Select(x => x.IdEstadoPropuesta).ToList();
            var idClasePatrullaje = _programaContext.ClasesPatrullaje.Where(x => x.Descripcion == clase).Select(x => x.IdClasePatrullaje).ToList();

            var propuestas = new List<PropuestaPatrullaje>();

            foreach (var fecha in fechas)
            {
                var numRutas =_programaContext.PropuestasPatrullajes.Where(x => x.IdRuta == pp.IdRuta && x.FechaPatrullaje == pp.FechaPatrullaje).Count();   

                if (numRutas == 0)
                {
                    var prop = new PropuestaPatrullaje() 
                    {
                    UltimaActualizacion = pp.UltimaActualizacion,
                    IdRuta = pp.IdRuta,
                    FechaPatrullaje = pp.FechaPatrullaje,
                    IdUsuario = pp.IdUsuario,
                    IdPuntoResponsable = pp.IdPuntoResponsable,
                    Observaciones = pp.Observaciones,
                    IdEstadoPropuesta = idEdoCreada[0],
                    RiesgoPatrullaje = pp.RiesgoPatrullaje,
                    IdClasePatrullaje = idClasePatrullaje[0],
                    IdApoyoPatrullaje = pp.IdApoyoPatrullaje
                    };

                    propuestas.Add(prop);
                }
            }

            if (propuestas.Count > 0) 
            {
                _programaContext.PropuestasPatrullajes.AddRange(propuestas);
                _programaContext.SaveChanges();
            }
        }

        //OJO Este está repetido en un update
        public void AgregaProgramasActualizaPropuestas(List<ProgramaPatrullaje> programas)
        {
            string edoAutorizada = "Autorizada";
            var idEdoAutorizada = _programaContext.EstadosPropuesta.Where(x => x.DescripcionEstadoPropuesta == edoAutorizada).Select(x => x.IdEstadoPropuesta).ToList();

            var idPropuestas = programas.Select(x => x.IdPropuestaPatrullaje).ToList();

            var aActualizar = (from c in _programaContext.PropuestasPatrullajes
                               where idPropuestas.Any(o => o == c.IdPropuestaPatrullaje) //IN
                               select c).ToList();

            foreach (var item in aActualizar)
            {
                item.IdEstadoPropuesta = idEdoAutorizada[0];
            }

            _programaContext.ProgramasPatrullajes.AddRange(programas);
            _programaContext.PropuestasPatrullajes.UpdateRange(aActualizar);
            _programaContext.SaveChanges();
        }



        public void AgregaPropuestaExtraordinaria(ProgramaPatrullaje pp)
        {

            /*strInstruccionSQL = "SELECT * from propuestaspatrullajes where id_ruta=" & ProgramaPatrullaje.intIdRuta & " AND fechapatrullaje='" & ProgramaPatrullaje.strFechaPatrullaje & "'"
                                    dataResultado = fnConsultaDatosMySQL(strInstruccionSQL)
                                    If dataResultado.Rows.Count = 0 Then
                                        'La ruta aún no está registrada en BD como propuesta de patrullaje
                                        strInstruccionSQL = "INSERT INTO propuestaspatrullajes (ultimaactualizacion, id_ruta,fechapatrullaje,id_usuario,id_puntoresponsable,observaciones
                                                                                        ,id_estadopropuesta,riesgopatrullaje,id_clasepatrullaje,id_apoyopatrullaje)
                                                    VALUES(NOW(), " & ProgramaPatrullaje.intIdRuta & ", '" & ProgramaPatrullaje.strFechaPatrullaje & "', " & intIdUsuario & "
                                                            , " & ProgramaPatrullaje.intIdPuntoResponsable & ", '" & ProgramaPatrullaje.strObservacionesPrograma & "'
                                                            , (SELECT id_estadopropuesta FROM estadopropuesta WHERE descripcionestadopropuesta = 'Creada')
                                                            ," & ProgramaPatrullaje.intIdRiesgoPatrullaje & "
                                                            ,(SELECT id_clasepatrullaje FROM clasepatrullaje WHERE descripcion = '" & clase & "')
                                                            ," & ProgramaPatrullaje.intApoyoPatrullaje & ")"
                                        If fnActualizaDatosMySQL(strInstruccionSQL) > 0 Then
                                            'Insertar el resto de datos en la tabla propuestaspatrullajescomplementosssf, propuestaspatrullajesvehiculos y propuestaspatrullajeslineas
                                            strInstruccionSQL = "INSERT INTO propuestaspatrullajescomplementossf (id_propuestapatrullaje,fechatermino)
                                                        VALUES((SELECT id_propuestapatrullaje FROM propuestaspatrullajes
                                                                    WHERE id_ruta = " & ProgramaPatrullaje.intIdRuta & " AND fechapatrullaje = '" & ProgramaPatrullaje.strFechaPatrullaje & "')
                                                                ,'" & ProgramaPatrullaje.strFechaTermino & "')"
                                            If fnActualizaDatosMySQL(strInstruccionSQL) > 0 Then
                                                'Insertar los vehículos y líneas
                                                For Each intIdVehiculo As Integer In ProgramaPatrullaje.lstPropuestasPatrullajesVehiculos
                                                    strInstruccionSQL = "INSERT INTO propuestaspatrullajesvehiculos (id_propuestapatrullaje,id_vehiculo)
                                                                VALUES((SELECT id_propuestapatrullaje FROM propuestaspatrullajes WHERE id_ruta = " & ProgramaPatrullaje.intIdRuta & "
                                                                        AND fechapatrullaje = '" & ProgramaPatrullaje.strFechaPatrullaje & "')
                                                                        ," & intIdVehiculo & ")"
                                                    fnActualizaDatosMySQL(strInstruccionSQL)
                                                Next
                                                For Each intIdLinea As Integer In ProgramaPatrullaje.lstPropuestasPatrullajesLineas
                                                    strInstruccionSQL = "INSERT INTO propuestaspatrullajeslineas (id_propuestapatrullaje,id_linea)
                                                                VALUES((SELECT id_propuestapatrullaje FROM propuestaspatrullajes WHERE id_ruta = " & ProgramaPatrullaje.intIdRuta & "
                                                                        AND fechapatrullaje = '" & ProgramaPatrullaje.strFechaPatrullaje & "')
                                                                        ," & intIdLinea & ")"
                                                    fnActualizaDatosMySQL(strInstruccionSQL)
                                                Next
                                            Else
                                                EscribeEnLog("No se insertaron los registros: " & strInstruccionSQL)
                                            End If
                                        Else
                                            EscribeEnLog("No se insertaron los registros: " & strInstruccionSQL)
                                        End If*/



            throw new NotImplementedException();
        }

        public void ActualizaPropuestaAutorizadaToRechazada(int idPropuesta, int idUsuario)
        {
            string edoAutorizada = "Autorizada";
            var idEdoAutorizada = _programaContext.EstadosPropuesta.Where(x => x.DescripcionEstadoPropuesta == edoAutorizada).Select(x => x.IdEstadoPropuesta).ToList();
            var propuestaActualizar = _programaContext.PropuestasPatrullajes.Where(x => x.IdPropuestaPatrullaje == idPropuesta && x.IdEstadoPropuesta < idEdoAutorizada[0]).ToList();

            if (propuestaActualizar.Count == 1)
            {
                var actualizar = propuestaActualizar[0];
                string edoRechazada = "Rechazada";
                var idEdoRechazada = _programaContext.EstadosPropuesta.Where(x => x.DescripcionEstadoPropuesta == edoRechazada).Select(x => x.IdEstadoPropuesta).ToList();

                actualizar.IdEstadoPropuesta = idEdoRechazada[0];
                actualizar.IdUsuario = idUsuario;

                _programaContext.PropuestasPatrullajes.Update(actualizar);
                _programaContext.SaveChanges();
            }
        }

        public void ActualizaPropuestaAprobadaPorComandanciaToPendientoDeAprobacionComandancia(int idPropuesta, int idUsuario)
        {
            string edoAprobada = "Aprobada por comandancia regional";
            var idEdoAprobada = _programaContext.EstadosPropuesta.Where(x => x.DescripcionEstadoPropuesta == edoAprobada).Select(x => x.IdEstadoPropuesta).ToList();
            var propuestaActualizar = _programaContext.PropuestasPatrullajes.Where(x => x.IdPropuestaPatrullaje == idPropuesta && x.IdEstadoPropuesta < idEdoAprobada[0]).ToList();

            if (propuestaActualizar.Count == 1) 
            {
                var actualizar = propuestaActualizar[0];
                string edoPendiente = "Pendiente de aprobacion por comandancia regional";
                var idEdoPendiente = _programaContext.EstadosPropuesta.Where(x => x.DescripcionEstadoPropuesta == edoPendiente).Select(x => x.IdEstadoPropuesta).ToList();

                actualizar.IdEstadoPropuesta = idEdoPendiente[0];
                actualizar.IdUsuario = idUsuario;

                _programaContext.PropuestasPatrullajes.Update(actualizar);
                _programaContext.SaveChanges();
            }
        }

        public void ActualizaPropuestaAutorizadaToPendientoDeAutorizacionSsf(int idPropuesta, int idUsuario)
        {
            string edoAutorizada = "Autorizada";
            var idEdoAutorizada = _programaContext.EstadosPropuesta.Where(x => x.DescripcionEstadoPropuesta == edoAutorizada).Select(x => x.IdEstadoPropuesta).ToList();
            var propuestaActualizar = _programaContext.PropuestasPatrullajes.Where(x => x.IdPropuestaPatrullaje == idPropuesta && x.IdEstadoPropuesta < idEdoAutorizada[0]).ToList();

            if (propuestaActualizar.Count == 1)
            {
                var actualizar = propuestaActualizar[0];
                string edoPendiente = "Pendiente de autorizacion por la SSF";
                var idEdoPendiente = _programaContext.EstadosPropuesta.Where(x => x.DescripcionEstadoPropuesta == edoPendiente).Select(x => x.IdEstadoPropuesta).ToList();

                actualizar.IdEstadoPropuesta = idEdoPendiente[0];
                actualizar.IdUsuario = idUsuario;

                _programaContext.PropuestasPatrullajes.Update(actualizar);
                _programaContext.SaveChanges();
            }
        }


        //OJO Este está repetido
        public void ActualizaProgramasConPropuestas(List<ProgramaPatrullaje> programas)
        {
            string edoAutorizada = "Autorizada";
            var idEdoAutorizada = _programaContext.EstadosPropuesta.Where(x => x.DescripcionEstadoPropuesta == edoAutorizada).Select(x => x.IdEstadoPropuesta).ToList();

            var idPropuestas = programas.Select(x => x.IdPropuestaPatrullaje).ToList();

            var aActualizar = (from c in _programaContext.PropuestasPatrullajes
                              where idPropuestas.Any(o => o == c.IdPropuestaPatrullaje) //IN
                              select c).ToList();

            foreach (var item in aActualizar)
            {
                item.IdEstadoPropuesta = idEdoAutorizada[0];
            }

            _programaContext.ProgramasPatrullajes.AddRange(programas);
            _programaContext.PropuestasPatrullajes.UpdateRange(aActualizar);
            _programaContext.SaveChanges();             
        }

        public void ActualizaRutayUsuarioDelPrograma(int idPrograma, int idRuta, int idUsuario)
        {
            var programa = _programaContext.ProgramasPatrullajes.Where(x => x.IdPrograma == idPrograma).ToList();

            if (programa.Count() == 1) 
            {
                var progamaActualizar = programa[0];
                progamaActualizar.IdRuta = idRuta;
                progamaActualizar.IdUsuario = idUsuario;

                _programaContext.ProgramasPatrullajes.Update(progamaActualizar);
                _programaContext.SaveChanges();
            }       
        }

        public void DeletePropuesta(int id)
        {
            string estado = "Autorizada";
            var edos = _programaContext.EstadosPropuesta.
                Where(x => x.DescripcionEstadoPropuesta == estado).
                Select(x => x.IdEstadoPropuesta);

            var pro =_programaContext.PropuestasPatrullajes.
                Where(x => x.IdPropuestaPatrullaje == id);

            var aBorrar = (from c in pro
                           where !edos.Any(o => o == c.IdEstadoPropuesta) //NOT IN
                           select c).ToList();
         
            if (aBorrar.Count > 0 )
            {
                _programaContext.PropuestasPatrullajes.Remove(aBorrar[0]);
                _programaContext.SaveChanges();
            }
        }
    }
}
