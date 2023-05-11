using Domain.Entities;
using Domain.Entities.Vistas;
using Domain.Ports.Driven.Repositories;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using SqlServerAdapter.Data;
using static System.Net.Mime.MediaTypeNames;

namespace SqlServerAdapter
{
    public class ProgramaPatrullajeRepository : IProgramaPatrullajeRepo
    {

        protected readonly ProgramaContext _programaContext;

        public ProgramaPatrullajeRepository(ProgramaContext programaContext)
        {
            _programaContext = programaContext ?? throw new ArgumentNullException(nameof(programaContext));
        }

        /// <summary>
        /// Método <c>ObtenerPropuestasExtraordinariasPorAnioMesDiaAsync</c> implementa la interface para obtener propuestas extraordinarias por fecha.
        /// Caso 0 Extraordinario  --Propuestas extraordinarias
        /// </summary>       
        public async Task<List<PatrullajeVista>> ObtenerPropuestasExtraordinariasPorAnioMesDiaAsync(string tipo, int region, int anio, int mes, int dia)
        {
            string sqlQuery = @"SELECT a.id_propuestapatrullaje id, a.id_ruta,a.fechapatrullaje, a.id_puntoresponsable, 
                   a.ultimaactualizacion,a.id_usuario, b.clave, b.regionmilitarsdn,b.regionssf, b.observaciones observacionesruta, 
                   d.descripcionnivel, e.fechatermino, 0 id_usuarioresponsablepatrullaje, '' observaciones, 0 riesgopatrullaje, a.id_apoyopatrullaje,
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

            return await _programaContext.PatrullajesVista.FromSqlRaw(sqlQuery, parametros).ToListAsync();
        }

        /// <summary>
        /// Método <c>ObtenerPropuestasPendientesPorAutorizarPorFiltro</c> implementa la interface para obtener propuestas pendientes por autorizar, acorde al filtro de los parámetros indicados.
        /// Caso 5 Ordinario  - Propuestas pendientes de autorizar
        /// </summary>
        public async Task<List<PatrullajeVista>> ObtenerPropuestasPendientesPorAutorizarPorFiltroAsync(string tipo, int region, int anio, int mes, string clase)
        {
            string sqlQuery = @"SELECT a.id_propuestapatrullaje id, a.id_ruta,a.fechapatrullaje, a.fechapatrullaje fechatermino, a.id_puntoresponsable, b.clave,
                   b.regionmilitarsdn,b.regionssf,b.observaciones observacionesruta, a.ultimaactualizacion,a.id_usuario,d.descripcionnivel,0 id_usuarioresponsablepatrullaje,
                   '' observaciones, 0 riesgopatrullaje, a.id_apoyopatrullaje,
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

            return await _programaContext.PatrullajesVista.FromSqlRaw(sqlQuery, parametros).ToListAsync();
        }

        /// <summary>
        /// Método <c>ObtenerPropuestasExtraordinariasPorFiltro</c> implementa la interface para obtere propuetsas extraordinarias acorde al filtro de los parámetros indicados.
        /// Caso 5 ExtraordinarioOrdinario
        /// </summary>
        public async Task<List<PatrullajeVista>> ObtenerPropuestasExtraordinariasPorFiltroAsync(string tipo, int region, int anio, int mes, string clase)
        {
            string sqlQuery = @"SELECT a.id_propuestapatrullaje id, a.id_ruta,a.fechapatrullaje, a.id_puntoresponsable, b.clave,
                   b.regionmilitarsdn,b.regionssf,b.observaciones observacionesruta, a.ultimaactualizacion,a.id_usuario,d.descripcionnivel,
                   e.fechatermino, 0 id_usuarioresponsablepatrullaje, 0 riesgopatrullaje, '' observaciones, a.id_apoyopatrullaje,
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

            return await _programaContext.PatrullajesVista.FromSqlRaw(sqlQuery, parametros).ToListAsync();
        }

        /// <summary>
        /// Método <c>ObtenerPropuestasPorFiltroEstado</c> implementa la interface para obtener propuestas acorde al filtro de los parámetros indicados.
        /// Dan servicio a las opciones 6,7,8,9  Ordinario  -- Propuestas
        /// </summary>
        public async Task<List<PatrullajeVista>> ObtenerPropuestasPorFiltroEstadoAsync(string tipo, int region, int anio, int mes, string clase, string estadoPropuesta)
        {
            // 6  - paramEstado='Pendiente de autorizacion por la SSF'
            // 7  - paramEstado='Autorizada'
            // 8  - paramEstado='Rechazada'
            // 9  - paramEstado='Aprobada por comandancia regional'
            string sqlQuery = @"SELECT a.id_propuestapatrullaje id,a.id_ruta,a.fechapatrullaje,a.fechapatrullaje fechatermino,a.id_puntoresponsable,b.clave,
                   b.regionmilitarsdn,b.regionssf,b.observaciones observacionesruta,a.ultimaactualizacion,a.id_usuario,d.descripcionnivel, 
                   0 id_usuarioresponsablepatrullaje, '' observaciones, 0 riesgopatrullaje, a.id_apoyopatrullaje,
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

            return await _programaContext.PatrullajesVista.FromSqlRaw(sqlQuery, parametros).ToListAsync();
        }

        /// <summary>
        /// Método <c>ObtenerPropuestasExtraordinariasPorFiltroEstado</c> implementa la interface para obtener propuestas extraordinarias por estado de la propuesta.
        /// Caso 6,7,8,9 Extraordinario
        /// </summary>
        public async Task<List<PatrullajeVista>> ObtenerPropuestasExtraordinariasPorFiltroEstadoAsync(string tipo, int region, int anio, int mes, string clase, string estadoPropuesta)
        {
            // 6 -- 'Pendiente de autorizacion por la SSF'
            // 7 -- 'Autorizada'
            // 8 -- 'Rechazada'
            // 9 -- 'Aprobada por comandancia regional'
            string sqlQuery = @"SELECT a.id_propuestapatrullaje id, a.id_ruta,a.fechapatrullaje, a.id_puntoresponsable, b.clave,
                   b.regionmilitarsdn,b.regionssf,b.observaciones observacionesruta, a.ultimaactualizacion,a.id_usuario,d.descripcionnivel,
                   e.fechatermino,0 id_usuarioresponsablepatrullaje, '' observaciones, 0 riesgopatrullaje, a.id_apoyopatrullaje,
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

            return await _programaContext.PatrullajesVista.FromSqlRaw(sqlQuery, parametros).ToListAsync();
        }

        /// <summary>
        /// Método <c>ObtenerPatrullajesEnRutaAndFechaEspecificaAsync</c> implementa la interface para obtener patrullajes de una ruta y fecha específica
        /// </summary>
        public async Task<List<PatrullajeVista>> ObtenerPatrullajesEnRutaAndFechaEspecificaAsync(int ruta, int anio, int mes, int dia)
        {

            string sqlQuery = @"SELECT a.id_programa, a.id_ruta, a.fechapatrullaje, a.inicio, a.id_puntoresponsable
                                   ,a.ultimaactualizacion, a.id_usuario, a.id_usuarioresponsablepatrullaje
                                   ,a.observaciones, a.riesgopatrullaje, -1 id_apoyopatrullaje
                                   ,COALESCE(a.solicitudoficiocomision,'') solicitudoficiocomision
                                   ,COALESCE(a.oficiocomision,'') oficiocomision   
                                   ,b.clave, b.regionmilitarsdn, b.regionssf, b.observaciones observacionesruta
                                   ,c.descripcionestadopatrullaje, d.descripcionnivel
                                   ,COALESCE((SELECT STRING_AGG(CAST(g.ubicacion as nvarchar(MAX)), '-') WITHIN GROUP(ORDER BY f.posicion ASC) as recorrido 
                                       FROM ssf.itinerario f
                                	   JOIN ssf.puntospatrullaje g ON f.id_ruta=a.id_ruta AND f.id_punto=g.id_punto
                                        ),'') as itinerario
                                FROM ssf.programapatrullajes a
                                JOIN ssf.rutas b ON a.id_ruta=b.id_ruta
                                JOIN ssf.estadopatrullaje c on a.id_estadopatrullaje=c.id_estadopatrullaje
                                JOIN ssf.niveles d ON a.riesgopatrullaje=d.id_nivel
                                JOIN ssf.tipopatrullaje e ON b.id_tipopatrullaje=e.id_tipopatrullaje
                                WHERE a.id_ruta=@pRuta AND YEAR(a.fechapatrullaje)=@pAnio
                                AND MONTH(a.fechapatrullaje)=@pMes AND DAY(a.fechapatrullaje)=@pDia     
                                ORDER BY a.ultimaActualizacion DESC, a.fechapatrullaje,a.inicio";

            object[] parametros = new object[]
            {
                new SqlParameter("@pAnio", anio),
                new SqlParameter("@pMes", mes),
                new SqlParameter("@pDia", dia),
                new SqlParameter("@pRuta", ruta)
            };

            return await _programaContext.PatrullajesVista.FromSqlRaw(sqlQuery, parametros).ToListAsync();
        }

        /// <summary>
        /// Método <c>ObtenerProgramasEnProgresoPorDia</c> implementa la interface para obtener programas en progreso por fecha.
        /// Caso 1 Programas EN PROGRESO Periodo 1 - Un día
        /// </summary>
        public async Task<List<PatrullajeVista>> ObtenerProgramasEnProgresoPorDiaAsync(string tipo, int region, int anio, int mes, int dia)
        {
            string sqlQuery = @"SELECT a.id_programa id, a.id_ruta, a.fechapatrullaje, a.fechapatrullaje fechatermino, a.inicio, a.id_puntoresponsable, b.clave, b.regionmilitarsdn,
                                       b.regionssf, b.observaciones observacionesruta, c.descripcionestadopatrullaje, a.observaciones, a.riesgopatrullaje,
                                       d.descripcionnivel, a.ultimaactualizacion, a.id_usuario, a.id_usuarioresponsablepatrullaje, -1 id_apoyopatrullaje,
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

            return await _programaContext.PatrullajesVista.FromSqlRaw(sqlQuery, parametros).ToListAsync();
        }

        /// <summary>
        /// Método <c>ObtenerProgramasEnProgresoPorMes</c> implementa la interface para obtener programas en progreso por mes.
        /// Caso 1 Programas EN PROGRESO Periodo 2 - Un mes
        /// </summary>
        public async Task<List<PatrullajeVista>> ObtenerProgramasEnProgresoPorMesAsync(string tipo, int region, int anio, int mes)
        {
            string sqlQuery = @"SELECT a.id_programa id, a.id_ruta, a.fechapatrullaje, a.fechapatrullaje fechatermino, a.inicio, a.id_puntoresponsable, b.clave, b.regionmilitarsdn,
                                       b.regionssf, b.observaciones observacionesruta, c.descripcionestadopatrullaje, a.observaciones, a.riesgopatrullaje,
                                       d.descripcionnivel, a.ultimaactualizacion, a.id_usuario, a.id_usuarioresponsablepatrullaje, -1 id_apoyopatrullaje,
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

            return await _programaContext.PatrullajesVista.FromSqlRaw(sqlQuery, parametros).ToListAsync();
        }

        /// <summary>
        /// Método <c>ObtenerProgramasEnProgreso</c> implementa la interface para obtener programas en progreso para una región.
        /// Caso 1 Programas EN PROGRESO Periodo 3 - todos
        /// </summary>
        public async Task<List<PatrullajeVista>> ObtenerProgramasEnProgresoAsync(string tipo, int region)
        {
            string sqlQuery = @"SELECT a.id_programa id, a.id_ruta, a.fechapatrullaje, a.fechapatrullaje fechatermino, a.inicio, a.id_puntoresponsable, b.clave, b.regionmilitarsdn,
                                       b.regionssf, b.observaciones observacionesruta, c.descripcionestadopatrullaje, a.observaciones, a.riesgopatrullaje,
                                       d.descripcionnivel, a.ultimaactualizacion, a.id_usuario, a.id_usuarioresponsablepatrullaje, -1 id_apoyopatrullaje,
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

            return await _programaContext.PatrullajesVista.FromSqlRaw(sqlQuery, parametros).ToListAsync();
        }

        /// <summary>
        /// Método <c>ObtenerProgramasConcluidosPorDia</c> implementa la interface para obtener programas concluidos por fecha (día).
        /// Caso 2 Programas CONCLUIDOS Periodo 1 - Un día
        /// </summary>
        public async Task<List<PatrullajeVista>> ObtenerProgramasConcluidosPorDiaAsync(string tipo, int region, int anio, int mes, int dia)
        {
            string sqlQuery = @"SELECT a.id_programa id, a.id_ruta, a.fechapatrullaje, a.fechapatrullaje fechatermino, a.inicio, a.id_puntoresponsable, b.clave, b.regionmilitarsdn,
                                       b.regionssf, b.observaciones observacionesruta, c.descripcionestadopatrullaje, a.observaciones, a.riesgopatrullaje,
                                       d.descripcionnivel, a.ultimaactualizacion, a.id_usuario, a.id_usuarioresponsablepatrullaje, -1 id_apoyopatrullaje,
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

            return await _programaContext.PatrullajesVista.FromSqlRaw(sqlQuery, parametros).ToListAsync();
        }

        /// <summary>
        /// Método <c>ObtenerProgramasConcluidosPorMes</c> implementa la interface para obtener programas concluidos por mes.
        /// Caso 2 Programas CONCLUIDOS Periodo 2 - Un mes
        /// </summary>
        public async Task<List<PatrullajeVista>> ObtenerProgramasConcluidosPorMesAsync(string tipo, int region, int anio, int mes)
        {
            string sqlQuery = @"SELECT a.id_programa id, a.id_ruta, a.fechapatrullaje, a.fechapatrullaje fechatermino, a.inicio, a.id_puntoresponsable, b.clave, b.regionmilitarsdn,
                                       b.regionssf, b.observaciones observacionesruta, c.descripcionestadopatrullaje, a.observaciones, a.riesgopatrullaje,
                                       d.descripcionnivel, a.ultimaactualizacion, a.id_usuario, a.id_usuarioresponsablepatrullaje, -1 id_apoyopatrullaje,
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

            return await _programaContext.PatrullajesVista.FromSqlRaw(sqlQuery, parametros).ToListAsync();
        }

        /// <summary>
        /// Método <c>ObtenerProgramasConcluidos</c> implementa la interface para aobtener programas concluidos por region.
        /// Caso 2 Programas CONCLUIDOS Periodo 3 - todos
        /// </summary>
        public async Task<List<PatrullajeVista>> ObtenerProgramasConcluidosAsync(string tipo, int region)
        {
            string sqlQuery = @"SELECT a.id_programa id, a.id_ruta, a.fechapatrullaje, a.fechapatrullaje fechatermino, a.inicio, a.id_puntoresponsable, b.clave, b.regionmilitarsdn,
                                       b.regionssf, b.observaciones observacionesruta, c.descripcionestadopatrullaje, a.observaciones, a.riesgopatrullaje,
                                       d.descripcionnivel, a.ultimaactualizacion, a.id_usuario, a.id_usuarioresponsablepatrullaje, -1 id_apoyopatrullaje,
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

            return await _programaContext.PatrullajesVista.FromSqlRaw(sqlQuery, parametros).ToListAsync();
        }

        /// <summary>
        /// Método <c>ObtenerProgramasCanceladosPorDia</c> implementa la interface para obtener programas cancelados por fecha (día).
        /// Caso 3 Programas CANCELADOS Periodo 1 - Un día
        /// </summary>
        //
        public async Task<List<PatrullajeVista>> ObtenerProgramasCanceladosPorDiaAsync(string tipo, int region, int anio, int mes, int dia)
        {
            string sqlQuery = @"SELECT a.id_programa id, a.id_ruta, a.fechapatrullaje, a.fechapatrullaje fechatermino, a.inicio, a.id_puntoresponsable, b.clave, b.regionmilitarsdn,
                                       b.regionssf, b.observaciones observacionesruta, c.descripcionestadopatrullaje, a.observaciones, a.riesgopatrullaje,
                                       d.descripcionnivel, a.ultimaactualizacion, a.id_usuario, a.id_usuarioresponsablepatrullaje, -1 id_apoyopatrullaje,
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

            return await _programaContext.PatrullajesVista.FromSqlRaw(sqlQuery, parametros).ToListAsync();
        }

        /// <summary>
        /// Método <c>ObtenerProgramasCanceladosPorMes</c> implementa la interface para obtener programas cancelados por mes.
        /// Caso 3 Programas CANCELADOS Periodo 2 - Un mes
        /// </summary>
        public async Task<List<PatrullajeVista>> ObtenerProgramasCanceladosPorMesAsync(string tipo, int region, int anio, int mes)
        {
            string sqlQuery = @"SELECT a.id_programa id, a.id_ruta, a.fechapatrullaje, a.fechapatrullaje fechatermino, a.inicio, a.id_puntoresponsable, b.clave, b.regionmilitarsdn,
                                       b.regionssf, b.observaciones observacionesruta, c.descripcionestadopatrullaje, a.observaciones, a.riesgopatrullaje,
                                       d.descripcionnivel, a.ultimaactualizacion, a.id_usuario, a.id_usuarioresponsablepatrullaje, -1 id_apoyopatrullaje,
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

            return await _programaContext.PatrullajesVista.FromSqlRaw(sqlQuery, parametros).ToListAsync();
        }

        /// <summary>
        /// Método <c>ObtenerProgramasCancelados</c> implementa la interface para obtener programas cancelados por región.
        /// Caso 3 Programas CANCELADOS Periodo 3 - todos
        /// </summary>
        public async Task<List<PatrullajeVista>> ObtenerProgramasCanceladosAsync(string tipo, int region)
        {
            string sqlQuery = @"SELECT a.id_programa id, a.id_ruta, a.fechapatrullaje, a.fechapatrullaje fechatermino, a.inicio, a.id_puntoresponsable, b.clave, b.regionmilitarsdn,
                                       b.regionssf, b.observaciones observacionesruta, c.descripcionestadopatrullaje, a.observaciones, a.riesgopatrullaje,
                                       d.descripcionnivel, a.ultimaactualizacion, a.id_usuario, a.id_usuarioresponsablepatrullaje, -1 id_apoyopatrullaje,
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

            return await _programaContext.PatrullajesVista.FromSqlRaw(sqlQuery, parametros).ToListAsync();
        }

        /// <summary>
        /// Método <c>ObtenerProgramasPorDia</c> implementa la interface para obtener programas por fecha (día).
        /// Caso 4 Programas Periodo 1 - Un día
        /// </summary>
        public async Task<List<PatrullajeVista>> ObtenerProgramasPorDiaAsync(string tipo, int region, int anio, int mes, int dia)
        {
            string sqlQuery = @"SELECT a.id_programa id, a.id_ruta, a.fechapatrullaje, a.fechapatrullaje fechatermino, a.inicio, a.id_puntoresponsable, b.clave, b.regionmilitarsdn,
                                       b.regionssf, b.observaciones observacionesruta, c.descripcionestadopatrullaje, a.observaciones, a.riesgopatrullaje,
                                       d.descripcionnivel, a.ultimaactualizacion, a.id_usuario, a.id_usuarioresponsablepatrullaje, -1 id_apoyopatrullaje,
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

            return await _programaContext.PatrullajesVista.FromSqlRaw(sqlQuery, parametros).ToListAsync();
        }

        /// <summary>
        /// Método <c>ObtenerProgramasPorMes</c> implementa la interface para obtener programas por mes.
        /// Caso 4 Programas Periodo 2 - Un mes  --- Aplica también para el Caso 0 Ordinario
        /// </summary>
        public async Task<List<PatrullajeVista>> ObtenerProgramasPorMesAsync(string tipo, int region, int anio, int mes)
        {
            string sqlQuery = @"SELECT a.id_programa id, a.id_ruta, a.fechapatrullaje, a.fechapatrullaje fechatermino, a.inicio, a.id_puntoresponsable, b.clave, b.regionmilitarsdn,
                                       b.regionssf, b.observaciones observacionesruta, c.descripcionestadopatrullaje, a.observaciones, a.riesgopatrullaje,
                                       d.descripcionnivel, a.ultimaactualizacion, a.id_usuario, a.id_usuarioresponsablepatrullaje, -1 id_apoyopatrullaje,
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

            return await _programaContext.PatrullajesVista.FromSqlRaw(sqlQuery, parametros).ToListAsync();
        }

        /// <summary>
        /// Método <c>ObtenerProgramas</c> implementa la interface para obtener todos los programas por región.
        /// Caso 4 Programas Periodo 3 - Todos
        /// </summary>
        public async Task<List<PatrullajeVista>> ObtenerProgramasAsync(string tipo, int region)
        {
            string sqlQuery = @"SELECT a.id_programa id, a.id_ruta, a.fechapatrullaje,a.fechapatrullaje fechatermino, a.inicio, a.id_puntoresponsable, b.clave, b.regionmilitarsdn,
                                       b.regionssf, b.observaciones observacionesruta, c.descripcionestadopatrullaje, a.observaciones, a.riesgopatrullaje,
                                       d.descripcionnivel, a.ultimaactualizacion, a.id_usuario, a.id_usuarioresponsablepatrullaje, -1 id_apoyopatrullaje,
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

            return await _programaContext.PatrullajesVista.FromSqlRaw(sqlQuery, parametros).ToListAsync();
        }

        /// <summary>
        /// Método <c>AgregaPropuestasComoProgramasActualizaPropuestas</c> implementa la interface para registrar programas referidas a las propuestas indicadas (convierte propuestas en programas).
        /// </summary>
        public async Task AgregaPropuestasComoProgramasActualizaPropuestasAsync(List<ProgramaPatrullaje> programas, int usuarioId)
        {
            string edoAutorizada = "Autorizada";
            var idEdoAutorizada = await _programaContext.EstadosPropuesta.Where(x => x.DescripcionEstadoPropuesta == edoAutorizada).Select(x => x.IdEstadoPropuesta).ToListAsync();
            var lstPropuestasActualizar = new List<PropuestaPatrullaje>();

            foreach (var p in programas)
            {
                p.IdUsuario = usuarioId;
                var aActualizar = await _programaContext.PropuestasPatrullajes.Where(x => x.IdPropuestaPatrullaje == p.IdPropuestaPatrullaje).ToListAsync();

                foreach (var item in aActualizar)
                {
                    item.IdEstadoPropuesta = idEdoAutorizada[0];
                }

                if (aActualizar.Count > 0)
                {
                    lstPropuestasActualizar.Add(aActualizar[0]);
                }
            }

            _programaContext.ProgramasPatrullajes.AddRange(programas);
            _programaContext.PropuestasPatrullajes.UpdateRange(lstPropuestasActualizar);
            await _programaContext.SaveChangesAsync();
        }

        /// <summary>
        /// Método <c>AgregaPropuestaExtraordinaria</c> implementa la interface para agregar propuestas extraordinaria.
        /// </summary>
        public async Task AgregaPropuestaExtraordinariaAsync(PropuestaExtraordinariaAdd pp, string clase, int usuarioId)
        {
            var rutaNoExisteEnPropuesta = await _programaContext.PropuestasPatrullajes.
                                Where(x => x.IdRuta == pp.Propuesta.IdRuta && x.FechaPatrullaje == pp.Propuesta.FechaPatrullaje).
                                CountAsync() == 0;

            string edoCreada = "Creada";
            var idEdoCreada = await _programaContext.EstadosPropuesta
                .Where(x => x.DescripcionEstadoPropuesta == edoCreada)
                .Select(x => x.IdEstadoPropuesta).ToListAsync();

            if (rutaNoExisteEnPropuesta)
            {
                var idClase = await _programaContext.ClasesPatrullaje
                    .Where(x => x.Descripcion == clase)
                    .Select(x => x.IdClasePatrullaje).ToListAsync();

                pp.Propuesta.IdClasePatrullaje = idClase[0];
                pp.Propuesta.IdEstadoPropuesta = idEdoCreada[0];
                pp.Propuesta.IdUsuario = usuarioId;
                pp.Propuesta.Observaciones = "";


                using (var transaction = _programaContext.Database.BeginTransaction())
                {
                    try
                    {
                        _programaContext.PropuestasPatrullajes.Add(pp.Propuesta);
                        var insertados = await _programaContext.SaveChangesAsync();

                        if (insertados > 0)
                        {
                            var propuesta = pp.Propuesta.IdPropuestaPatrullaje;

                            var complemento = new PropuestaPatrullajeComplementossf()
                            {
                                IdPropuestaPatrullaje = propuesta,
                                FechaTermino = pp.Propuesta.FechaPatrullaje
                            };

                            _programaContext.PropuestasComplementosSsf.Add(complemento);
                            var complementosInsertados = await _programaContext.SaveChangesAsync();

                            if (complementosInsertados > 0)
                            {
                                foreach (var v in pp.Vehiculos)
                                {
                                    //v.IdPropuestaPatrullaje = propuesta;
                                    _programaContext.Database.ExecuteSql($"INSERT INTO ssf.propuestaspatrullajesvehiculos (id_propuestapatrullaje,id_vehiculo) VALUES({propuesta},{v.IdVehiculo})");
                                }

                                foreach (var l in pp.Lineas)
                                {
                                    //l.IdPropuestaPatrullaje = propuesta;
                                    _programaContext.Database.ExecuteSql($"INSERT INTO ssf.propuestaspatrullajeslineas (id_propuestapatrullaje,id_linea) VALUES({propuesta},{l.IdLinea})");
                                }
              /*                  _programaContext.PropuestasVehiculos.AddRange(pp.Vehiculos);
                                _programaContext.PropuestasLineas.AddRange(pp.Lineas);*/               

                                await _programaContext.SaveChangesAsync();
                            }
                        }

                        transaction.Commit();
                    }
                    catch (Exception)
                    {
                        transaction.Rollback();
                        throw;
                    }
                }
            }
        }

        /// <summary>
        /// Método <c>AgregaPropuestasFechasMultiples</c> implementa la interface para agregar propuestas de múltiples fechas.
        /// </summary>
        public async Task AgregaPropuestasFechasMultiplesAsync(PropuestaPatrullaje pp, List<DateTime> fechas, string clase, int usuarioId)
        {
            string edoCreada = "Creada";
            var idEdoCreada = await _programaContext.EstadosPropuesta.Where(x => x.DescripcionEstadoPropuesta == edoCreada).Select(x => x.IdEstadoPropuesta).ToListAsync();
            var idClasePatrullaje = await _programaContext.ClasesPatrullaje.Where(x => x.Descripcion == clase).Select(x => x.IdClasePatrullaje).ToListAsync();

            var propuestas = new List<PropuestaPatrullaje>();

            string observacion = "";
            if (pp.Observaciones != null) observacion = pp.Observaciones;

            foreach (var fecha in fechas)
            {
                var numRutas = await _programaContext.PropuestasPatrullajes.Where(x => x.IdRuta == pp.IdRuta && x.FechaPatrullaje == pp.FechaPatrullaje).CountAsync();

                if (numRutas == 0)
                {
                    var prop = new PropuestaPatrullaje()
                    {
                        UltimaActualizacion = pp.UltimaActualizacion,
                        IdRuta = pp.IdRuta,
                        FechaPatrullaje = fecha,
                        IdUsuario = usuarioId,
                        IdPuntoResponsable = pp.IdPuntoResponsable,
                        Observaciones = observacion,
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
                await _programaContext.SaveChangesAsync();
            }
        }

        /// <summary>
        /// Método <c>AgregaProgramaFechasMultiples</c> implementa la interface para agregar programas para múltiples fechas.
        /// </summary>
        public async Task AgregaProgramaFechasMultiplesAsync(ProgramaPatrullaje pp, List<DateTime> fechas, int usuarioId)
        {
            var lstProgramas = new List<ProgramaPatrullaje>();
            var lstPropuestas = new List<PropuestaPatrullaje>();

            string edoAutorizada = "Autorizada";
            var idEdoAutorizada = await _programaContext.EstadosPropuesta.Where(x => x.DescripcionEstadoPropuesta == edoAutorizada).Select(x => x.IdEstadoPropuesta).ToListAsync();

            var propuestas = await _programaContext.PropuestasPatrullajes.Where(x => x.IdPropuestaPatrullaje == pp.IdPropuestaPatrullaje).ToListAsync();

            foreach (var prop in propuestas)
            {
                prop.IdEstadoPropuesta = idEdoAutorizada[0];
            }

            foreach (var fecha in fechas)
            {
                var programa = new ProgramaPatrullaje
                {
                    IdRuta = pp.IdRuta,
                    IdUsuario = usuarioId,
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
            await _programaContext.SaveChangesAsync();
        }

        /// <summary>
        /// Método <c>ActualizaProgramaPorCambioDeRuta</c> implementa la interface para actualizar programas debido a cambio de ruta.
        /// </summary>
        public async Task ActualizaProgramaPorCambioDeRutaAsync(int idPrograma, int idRuta, int usuarioId)
        {
            var programa = await _programaContext.ProgramasPatrullajes.Where(x => x.IdPrograma == idPrograma).ToListAsync();

            if (programa.Count() == 1)
            {
                var progamaActualizar = programa[0];
                progamaActualizar.IdRuta = idRuta;
                progamaActualizar.IdUsuario = usuarioId;

                _programaContext.ProgramasPatrullajes.Update(progamaActualizar);
                await _programaContext.SaveChangesAsync();
            }
        }

        /// <summary>
        /// Método <c>ActualizaProgramasConPropuestas</c> implementa la interface para actualizar programas con propuestas.
        /// </summary>
        public async Task ActualizaPropuestasAgregaProgramasAsync(List<ProgramaPatrullaje> programas)
        {
            string edoAutorizada = "Autorizada";
            var idEdoAutorizada = await _programaContext.EstadosPropuesta.Where(x => x.DescripcionEstadoPropuesta == edoAutorizada).Select(x => x.IdEstadoPropuesta).ToListAsync();

            var idPropuestas = programas.Select(x => x.IdPropuestaPatrullaje).ToList();

            var aActualizar = await (from c in _programaContext.PropuestasPatrullajes
                                     where idPropuestas.Any(o => o == c.IdPropuestaPatrullaje) //IN
                                     select c).ToListAsync();

            foreach (var item in aActualizar)
            {
                item.IdEstadoPropuesta = idEdoAutorizada[0];
            }

            _programaContext.ProgramasPatrullajes.AddRange(programas);
            _programaContext.PropuestasPatrullajes.UpdateRange(aActualizar);
            await _programaContext.SaveChangesAsync();
        }

        /// <summary>
        /// Método <c>ActualizaProgramasPorInicioPatrullajeAsync</c> implementa la interface para actualizar programas por inicio de patrullaje.
        /// </summary>
        public async Task ActualizaProgramasPorInicioPatrullajeAsync(int idPrograma, int idRiesgo, int idUsuario, int idEstadoPatrullaje, TimeSpan inicio)
        {
            var pa = await _programaContext.ProgramasPatrullajes.Where(x => x.IdRuta == idPrograma).SingleOrDefaultAsync();
            if (pa != null)
            {
                pa.UltimaActualizacion = DateTime.UtcNow;
                pa.Inicio = inicio;
                pa.IdEstadoPatrullaje = idEstadoPatrullaje;
                pa.IdUsuario = idUsuario;
                pa.RiesgoPatrullaje = idRiesgo;

                _programaContext.ProgramasPatrullajes.Update(pa);
                await _programaContext.SaveChangesAsync();
            }
        }

        /// <summary>
        /// Método <c>ActualizaPropuestaToAutorizadaAsync</c> implementa la interface para actualizar propuestas (autorizar propuesta)
        /// </summary>
        public async Task ActualizaPropuestaToAutorizadaAsync(int idPropuesta)
        {
            var pa = await _programaContext.PropuestasPatrullajes.Where(x => x.IdPropuestaPatrullaje == idPropuesta).SingleOrDefaultAsync();
            if (pa != null)
            {
                string decEdo = "Autorizada";
                var edo = await _programaContext.EstadosPropuesta.Where(x => x.DescripcionEstadoPropuesta == decEdo).SingleOrDefaultAsync();
                if (edo != null)
                {
                    pa.IdEstadoPropuesta = edo.IdEstadoPropuesta;
                    _programaContext.PropuestasPatrullajes.Update(pa);
                    await _programaContext.SaveChangesAsync();
                }
            }
        }

        /// <summary>
        /// Método <c>ActualizaPropuestaToAprobadaComandanciaRegionalAsync</c> implementa la interface para actualizar propuestas (Aprobada por comandancia regional)
        /// </summary>
        public async Task ActualizaPropuestaToAprobadaComandanciaRegionalAsync(int idPropuesta)
        {
            string decEdo = "Aprobada por comandancia regional";
            var edo = await _programaContext.EstadosPropuesta.Where(x => x.DescripcionEstadoPropuesta == decEdo).SingleOrDefaultAsync();

            if (edo != null)
            {
                var pa = await _programaContext.PropuestasPatrullajes.Where(x => x.IdPropuestaPatrullaje == idPropuesta && x.IdEstadoPropuesta < edo.IdEstadoPropuesta).SingleOrDefaultAsync();

                if (pa != null)
                {

                    pa.IdEstadoPropuesta = edo.IdEstadoPropuesta;

                    _programaContext.PropuestasPatrullajes.Update(pa);
                    await _programaContext.SaveChangesAsync();
                }
            }
        }

        /// <summary>
        /// Método <c>ActualizaProgramaRegistraSolicitudOficioComisionAsync</c> implementa la interface para actualizar programas (solicitud de oficio de comisión del programa)
        /// </summary>
        public async Task ActualizaProgramaRegistraSolicitudOficioComisionAsync(int idPrograma, string oficio)
        {
            var programa = await _programaContext.ProgramasPatrullajes.Where(x => x.IdPrograma == idPrograma).SingleOrDefaultAsync();

            if (programa != null)
            {
                programa.SolicitudOficioComision = oficio;
                programa.UltimaActualizacion = DateTime.UtcNow;

                _programaContext.ProgramasPatrullajes.Update(programa);
                await _programaContext.SaveChangesAsync();
            }
        }

        /// <summary>
        /// Método <c>ActualizaPropuestaRegistraSolicitudOficioAutorizacionAsync</c> implementa la interface para actualizar programas (solicitud de oficio de comisión del programa)
        /// </summary>
        public async Task ActualizaPropuestaRegistraSolicitudOficioAutorizacionAsync(int idPropuesta, string oficio)
        {
            var propuesta = await _programaContext.PropuestasPatrullajes.Where(x => x.IdPropuestaPatrullaje == idPropuesta).SingleOrDefaultAsync();

            if (propuesta != null)
            {
                propuesta.SolicitudOficioAutorizacion = oficio;
                propuesta.UltimaActualizacion = DateTime.UtcNow;

                _programaContext.PropuestasPatrullajes.Update(propuesta);
                await _programaContext.SaveChangesAsync();
            }
        }

        /// <summary>
        /// Método <c>ActualizaProgramaRegistraOficioComisionAsync</c> implementa la interface para actualizar programas (solicitud de oficio de comisión del programa)
        /// </summary>
        public async Task ActualizaProgramaRegistraOficioComisionAsync(int idPrograma, string oficio)
        {
            var programa = await _programaContext.ProgramasPatrullajes.Where(x => x.IdPrograma == idPrograma).SingleOrDefaultAsync();

            if (programa != null)
            {
                programa.OficioComision = oficio;
                programa.UltimaActualizacion = DateTime.UtcNow;

                _programaContext.ProgramasPatrullajes.Update(programa);
                await _programaContext.SaveChangesAsync();
            }
        }

        /// <summary>
        /// Método <c>ActualizaPropuestaRegistraOficioAutorizacionAsync</c> implementa la interface para actualizar programas (solicitud de oficio de comisión del programa)
        /// </summary>
        public async Task ActualizaPropuestaRegistraOficioAutorizacionAsync(int idPropuesta, string oficio)
        {
            var propuesta = await _programaContext.PropuestasPatrullajes.Where(x => x.IdPropuestaPatrullaje == idPropuesta).SingleOrDefaultAsync();

            if (propuesta != null)
            {
                propuesta.OficioAutorizacion = oficio;
                propuesta.UltimaActualizacion = DateTime.UtcNow;

                _programaContext.PropuestasPatrullajes.Update(propuesta);
                await _programaContext.SaveChangesAsync();
            }
        }

        public async Task ActualizaFechaPatrullajeEnProgramaAndTarjetaAsync(int idPrograma, DateTime fechaPatrullaje)
        {
            var programas = await _programaContext.ProgramasPatrullajes.Where(x => x.IdPrograma == idPrograma).ToListAsync();
            var tarjetas = await _programaContext.TarjetasInformativas.Where(x => x.IdPrograma == idPrograma).ToListAsync();

            if (programas != null && programas.Count > 0)
            {
                foreach (var prog in programas)
                {
                    prog.FechaPatrullaje = fechaPatrullaje;

                }
                _programaContext.ProgramasPatrullajes.UpdateRange(programas);
            }

            if (tarjetas != null && tarjetas.Count > 0)
            {
                foreach (var tarj in tarjetas)
                {
                    tarj.FechaPatrullaje = fechaPatrullaje;
                }
                _programaContext.TarjetasInformativas.UpdateRange(tarjetas);
            }

            await _programaContext.SaveChangesAsync();
        }



        /// <summary>
        /// Método <c>ActualizaPropuestasAutorizadaToRechazada</c> implementa la interface para actualizar el estado de propuestas autorizadas hacia propuestas rechazadas.
        /// </summary>
        public async Task ActualizaPropuestasAutorizadaToRechazadaAsync(List<int> idPropuestas, int usuarioId)
        {
            string edoAutorizada = "Autorizada";
            var idEdoAutorizada = await _programaContext.EstadosPropuesta.Where(x => x.DescripcionEstadoPropuesta == edoAutorizada).Select(x => x.IdEstadoPropuesta).ToListAsync();
            string edoRechazada = "Rechazada";
            var idEdoRechazada = await _programaContext.EstadosPropuesta.Where(x => x.DescripcionEstadoPropuesta == edoRechazada).Select(x => x.IdEstadoPropuesta).ToListAsync();

            var propuestasActualizar = await (from c in _programaContext.PropuestasPatrullajes
                                              where idPropuestas.Any(o => o == c.IdPropuestaPatrullaje) //IN
                                              && c.IdEstadoPropuesta != idEdoAutorizada[0]
                                              select c).ToListAsync();

            if (propuestasActualizar.Count > 0)
            {
                foreach (var pa in propuestasActualizar)
                {
                    pa.IdUsuario = usuarioId;
                    pa.IdEstadoPropuesta = edoRechazada[0];
                }

                _programaContext.PropuestasPatrullajes.UpdateRange(propuestasActualizar);
                await _programaContext.SaveChangesAsync();
            }
        }

        /// <summary>
        /// Método <c>ActualizaPropuestasAprobadaPorComandanciaToPendientoDeAprobacionComandancia</c> implementa la interface para actualizar el estado d elas propuestas de aprobada por comandancia hacia pendiente de aprobación.
        /// </summary>
        public async Task ActualizaPropuestasAprobadaPorComandanciaToPendientoDeAprobacionComandanciaAsync(List<int> idPropuestas, int usuarioId)
        {
            string edoAprobada = "Aprobada por comandancia regional";
            var idEdoAprobada = await _programaContext.EstadosPropuesta.Where(x => x.DescripcionEstadoPropuesta == edoAprobada).Select(x => x.IdEstadoPropuesta).ToListAsync();
            string edoPendiente = "Pendiente de aprobacion por comandancia regional";
            var idEdoPendiente = await _programaContext.EstadosPropuesta.Where(x => x.DescripcionEstadoPropuesta == edoPendiente).Select(x => x.IdEstadoPropuesta).ToListAsync();

            var propuestasActualizar = await (from c in _programaContext.PropuestasPatrullajes
                                              where idPropuestas.Any(o => o == c.IdPropuestaPatrullaje) //IN
                                              && c.IdEstadoPropuesta < idEdoAprobada[0]
                                              select c).ToListAsync();

            if (propuestasActualizar.Count > 0)
            {
                foreach (var pa in propuestasActualizar)
                {
                    pa.IdUsuario = usuarioId;
                    pa.IdEstadoPropuesta = edoPendiente[0];
                }

                _programaContext.PropuestasPatrullajes.UpdateRange(propuestasActualizar);
                await _programaContext.SaveChangesAsync();
            }
        }

        /// <summary>
        /// Método <c>ActualizaPropuestasAutorizadaToPendientoDeAutorizacionSsf</c> implementa la interface para actualizar el estado de las propuestas de autorizadas hacia pendiente de autorización SSF.
        /// </summary>
        public async Task ActualizaPropuestasAutorizadaToPendientoDeAutorizacionSsfAsync(List<int> idPropuestas, int usuarioId)
        {
            string edoAutorizada = "Autorizada";
            var idEdoAutorizada = await _programaContext.EstadosPropuesta.Where(x => x.DescripcionEstadoPropuesta == edoAutorizada).Select(x => x.IdEstadoPropuesta).ToListAsync();
            string edoPendiente = "Pendiente de autorizacion por la SSF";
            var idEdoPendiente = await _programaContext.EstadosPropuesta.Where(x => x.DescripcionEstadoPropuesta == edoPendiente).Select(x => x.IdEstadoPropuesta).ToListAsync();

            var propuestasActualizar = await (from c in _programaContext.PropuestasPatrullajes
                                              where idPropuestas.Any(o => o == c.IdPropuestaPatrullaje) //IN
                                              && c.IdEstadoPropuesta < idEdoAutorizada[0]
                                              select c).ToListAsync();

            if (propuestasActualizar.Count > 0)
            {
                foreach (var pa in propuestasActualizar)
                {
                    pa.IdUsuario = usuarioId;
                    pa.IdEstadoPropuesta = edoPendiente[0];
                }

                _programaContext.PropuestasPatrullajes.UpdateRange(propuestasActualizar);
                await _programaContext.SaveChangesAsync();
            }
        }

        /// <summary>
        /// Método <c>DeletePropuesta</c> implementa la interface para eliminar una propuesta.
        /// </summary>
        public async Task DeletePropuestaAsync(int id)
        {
            string estado = "Autorizada";
            var edos = _programaContext.EstadosPropuesta.
                Where(x => x.DescripcionEstadoPropuesta == estado).
                Select(x => x.IdEstadoPropuesta);

            var pro = _programaContext.PropuestasPatrullajes.
                Where(x => x.IdPropuestaPatrullaje == id);

            var aBorrar = await (from c in pro
                                 where !edos.Any(o => o == c.IdEstadoPropuesta) //NOT IN
                                 select c).ToListAsync();

            if (aBorrar.Count > 0)
            {
                _programaContext.PropuestasPatrullajes.Remove(aBorrar[0]);
                await _programaContext.SaveChangesAsync();
            }
        }

        public async Task DeleteProgramaAsync(int id)
        {
            var aBorrar = await _programaContext.ProgramasPatrullajes.Where(x => x.IdPrograma == id).SingleAsync();

            _programaContext.ProgramasPatrullajes.Remove(aBorrar);
            await _programaContext.SaveChangesAsync();
        }

        public async Task<bool> SaveChangesAsync()
        {
            return (await _programaContext.SaveChangesAsync() >= 0);
        }
    }
}
