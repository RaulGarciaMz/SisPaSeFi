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
    public class ProgramaPatrullajeRepository 
    {

        protected readonly ProgramaContext _programaContext;
        private string _extraordinarias;

        public ProgramaPatrullajeRepository(ProgramaContext programaContext)
        {
            _programaContext = programaContext;



            _extraordinarias = @"SELECT a.id_propuestapatrullaje id_programa,a.id_ruta,a.fechapatrullaje,e.fechatermino,a.id_puntoresponsable,b.clave,
                   b.regionmilitarsdn,b.regionssf,b.observaciones,a.ultimaactualizacion,a.id_usuario,d.descripcionnivel descripcionnivelriesgo,
                   COALESCE(a.solicitudoficioautorizacion, '') SolicitudOficioComision,
                   COALESCE(a.oficioautorizacion, '') as OficioComision,
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
            JOIN ssf.propuestaspatrullajescomplementossf e ON a.id_propuestapatrullaje=e.id_propuestapatrullaje
            JOIN ssf.tipopatrullaje k ON b.id_tipopatrullaje = k.id_tipopatrullaje";
        }

        //Dan servicio a las opciones 6,7,8,9  Ordinario
        public List<PropuestaVista> ObtenerPropuestasPorFiltro(string tipo, int mes, int anio, int region, string clase, string estadoPropuesta)
        {
            string sqlQuery = @"SELECT a.id_propuestapatrullaje id_programa,a.id_ruta,a.fechapatrullaje,a.fechapatrullaje a.fechapatrullaje fechatermino, a.id_puntoresponsable,b.clave,
                   b.regionmilitarsdn,b.regionssf,b.observaciones,a.ultimaactualizacion,a.id_usuario,d.descripcionnivel descripcionnivelriesgo,
                   COALESCE(a.solicitudoficioautorizacion, '') SolicitudOficioComision,
                   COALESCE(a.oficioautorizacion, '') as OficioComision,
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

            return _programaContext.PropuestasVistas.FromSqlRaw(sqlQuery, parametros).ToList();
        }
        //Dan servicio a las opciones 6,7,8,9 Extraordinario
        public List<ProgramaVista> ObtenerPropuestasExtraordinariasPorFiltro(string tipo, int mes, int anio, int region, string clase, string estadoPropuesta)
        {
            string sqlQuery = @"SELECT a.id_propuestapatrullaje id_programa,a.id_ruta,a.fechapatrullaje,e.fechatermino,a.id_puntoresponsable,b.clave,
                   b.regionmilitarsdn,b.regionssf,b.observaciones,a.ultimaactualizacion,a.id_usuario,d.descripcionnivel descripcionnivelriesgo,
                   COALESCE(a.solicitudoficioautorizacion, '') SolicitudOficioComision,
                   COALESCE(a.oficioautorizacion, '') as OficioComision,
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
            JOIN ssf.propuestaspatrullajescomplementossf e ON a.id_propuestapatrullaje=e.id_propuestapatrullaje
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

            return _programaContext.ProgramasVistas.FromSqlRaw(sqlQuery, parametros).ToList();
        }

        //Dan servicio a la opcion 5 Caso 5 Ordinario
        public List<PropuestaVista> ObtenerPropuestasTodasPorFiltro(string tipo, int mes, int anio, int region, string clase)
        {
            string sqlQuery = @"SELECT a.id_propuestapatrullaje id_programa,a.id_ruta,a.fechapatrullaje,a.fechapatrullaje a.fechapatrullaje fechatermino, a.id_puntoresponsable,b.clave,
                   b.regionmilitarsdn,b.regionssf,b.observaciones,a.ultimaactualizacion,a.id_usuario,d.descripcionnivel descripcionnivelriesgo,
                   COALESCE(a.solicitudoficioautorizacion, '') SolicitudOficioComision,
                   COALESCE(a.oficioautorizacion, '') as OficioComision,
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
            ORDER BY a.fechapatrullaje";

            object[] parametros = new object[]
            {
                new SqlParameter("@paramAnio", anio),
                new SqlParameter("@paramMes", mes),
                new SqlParameter("@paramTipo", tipo),
                new SqlParameter("@paramRegion", region),
                new SqlParameter("@paramClase", clase)               
            };

            return _programaContext.PropuestasVistas.FromSqlRaw(sqlQuery, parametros).ToList();
        }

        //Dan servicio a la opcion 5  Caso 5  - Extraordinario
        public List<ProgramaVista> ObtenerPropuestasExtraordinariasTodasPorFiltro(string tipo, int mes, int anio, int region, string clase, string estadoPropuesta)
        {
            string sqlQuery = @"SELECT a.id_propuestapatrullaje id_programa,a.id_ruta,a.fechapatrullaje,e.fechatermino,a.id_puntoresponsable,b.clave,
                   b.regionmilitarsdn,b.regionssf,b.observaciones,a.ultimaactualizacion,a.id_usuario,d.descripcionnivel descripcionnivelriesgo,
                   COALESCE(a.solicitudoficioautorizacion, '') SolicitudOficioComision,
                   COALESCE(a.oficioautorizacion, '') as OficioComision,
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
            JOIN ssf.propuestaspatrullajescomplementossf e ON a.id_propuestapatrullaje=e.id_propuestapatrullaje
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
                new SqlParameter("@paramEstado", tipo)
            };

            return _programaContext.ProgramasVistas.FromSqlRaw(sqlQuery, parametros).ToList();
        }

        public List<ProgramaVista> ObtenerPatrullajesPorRegion(string tipo, int region, string clase, string estadoPropuesta)
        {
            string sqlQuery = @"SELECT a.id_propuestapatrullaje,a.id_ruta,a.fechapatrullaje,a.id_puntoresponsable,b.clave,
                   b.regionmilitarsdn,b.regionssf,b.observaciones,a.ultimaactualizacion,a.id_usuario,d.descripcionnivel,
                   COALESCE(a.solicitudoficioautorizacion, '') as solicitudoficioautorizacion,
                   COALESCE(a.oficioautorizacion, '') as oficioautorizacion,
                   COALESCE((SELECT STRING_AGG(CAST(ubicacion as nvarchar(MAX)), '-') WITHIN GROUP(ORDER BY f.posicion ASC)
                             FROM ssf.itinerario f join ssf.puntospatrullaje g on f.id_punto = g.id_punto
                             WHERE f.id_ruta = a.id_ruta),'') as itinerarioruta,
                   COALESCE((SELECT top 1 f.inicio FROM ssf.programapatrullajes f
                             WHERE f.id_propuestapatrullaje = a.id_propuestapatrullaje
                             AND YEAR(f.fechapatrullaje) = YEAR(getutcdate()) 
                             AND MONTH(f.fechapatrullaje) = MONTH(getutcdate()) 
                             AND DAY(f.fechapatrullaje) = DAY(getutcdate())),'00:00:00'),
                   COALESCE((SELECT g.descripcionestadopatrullaje 
                             FROM ssf.programapatrullajes f
                             JOIN ssf.estadopatrullaje g ON f.id_estadopatrullaje = g.id_estadopatrullaje
                             WHERE f.id_propuestapatrullaje = a.id_propuestapatrullaje
                             AND YEAR(f.fechapatrullaje) = YEAR(getutcdate()) 
                             AND MONTH(f.fechapatrullaje) = MONTH(getutcdate()) 
                             AND DAY(f.fechapatrullaje) = DAY(getutcdate())),c.descripcionestadopropuesta)	
            FROM ssf.propuestaspatrullajes a
            JOIN ssf.rutas b ON a.id_ruta = b.id_ruta
            JOIN ssf.niveles d ON a.riesgopatrullaje = d.id_nivel
            JOIN SSF.clasepatrullaje m ON a.id_clasepatrullaje = m.id_clasepatrullaje
            JOIN ssf.estadopropuesta c ON a.id_estadopropuesta = c.id_estadopropuesta
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

                new SqlParameter("@paramTipo", tipo),
                new SqlParameter("@paramRegion", region),
                new SqlParameter("@paramClase", clase),
                new SqlParameter("@paramEstado", tipo)
            };

            return _programaContext.ProgramasVistas.FromSqlRaw(sqlQuery, parametros).ToList();
        }
        public List<ProgramaVista> ObtenerPatrullajesPorMes(string tipo, int mes, int anio, int region, string clase, string estadoPropuesta)
        {
            string sqlQuery = @"SELECT a.id_propuestapatrullaje,a.id_ruta,a.fechapatrullaje,a.id_puntoresponsable,b.clave,
                   b.regionmilitarsdn,b.regionssf,b.observaciones,a.ultimaactualizacion,a.id_usuario,d.descripcionnivel,
                   COALESCE(a.solicitudoficioautorizacion, '') as solicitudoficioautorizacion,
                   COALESCE(a.oficioautorizacion, '') as oficioautorizacion,
                   COALESCE((SELECT STRING_AGG(CAST(ubicacion as nvarchar(MAX)), '-') WITHIN GROUP(ORDER BY f.posicion ASC)
                             FROM ssf.itinerario f join ssf.puntospatrullaje g on f.id_punto = g.id_punto
                             WHERE f.id_ruta = a.id_ruta),'') as itinerarioruta,
                   COALESCE((SELECT top 1 f.inicio FROM ssf.programapatrullajes f
                             WHERE f.id_propuestapatrullaje = a.id_propuestapatrullaje
                             AND YEAR(f.fechapatrullaje) = YEAR(getutcdate()) 
                             AND MONTH(f.fechapatrullaje) = MONTH(getutcdate()) 
                             AND DAY(f.fechapatrullaje) = DAY(getutcdate())),'00:00:00'),
                   COALESCE((SELECT g.descripcionestadopatrullaje 
                             FROM ssf.programapatrullajes f
                             JOIN ssf.estadopatrullaje g ON f.id_estadopatrullaje = g.id_estadopatrullaje
                             WHERE f.id_propuestapatrullaje = a.id_propuestapatrullaje
                             AND YEAR(f.fechapatrullaje) = YEAR(getutcdate()) 
                             AND MONTH(f.fechapatrullaje) = MONTH(getutcdate()) 
                             AND DAY(f.fechapatrullaje) = DAY(getutcdate())),c.descripcionestadopropuesta)	
            FROM ssf.propuestaspatrullajes a
            JOIN ssf.rutas b ON a.id_ruta = b.id_ruta
            JOIN ssf.niveles d ON a.riesgopatrullaje = d.id_nivel
            JOIN SSF.clasepatrullaje m ON a.id_clasepatrullaje = m.id_clasepatrullaje
            JOIN ssf.estadopropuesta c ON a.id_estadopropuesta = c.id_estadopropuesta
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
                new SqlParameter("@paramEstado", tipo)
            };

            return _programaContext.ProgramasVistas.FromSqlRaw(sqlQuery, parametros).ToList();
        }
        public List<ProgramaVista> ObtenerPatrullajesPorDia(string tipo, int dia, int mes, int anio, int region, string clase, string estadoPropuesta)
        {
            string sqlQuery = @"SELECT a.id_propuestapatrullaje,a.id_ruta,a.fechapatrullaje,a.id_puntoresponsable,b.clave,
                   b.regionmilitarsdn,b.regionssf,b.observaciones,a.ultimaactualizacion,a.id_usuario,d.descripcionnivel,
                   COALESCE(a.solicitudoficioautorizacion, '') as solicitudoficioautorizacion,
                   COALESCE(a.oficioautorizacion, '') as oficioautorizacion,
                   COALESCE((SELECT STRING_AGG(CAST(ubicacion as nvarchar(MAX)), '-') WITHIN GROUP(ORDER BY f.posicion ASC)
                             FROM ssf.itinerario f join ssf.puntospatrullaje g on f.id_punto = g.id_punto
                             WHERE f.id_ruta = a.id_ruta),'') as itinerarioruta,
                   COALESCE((SELECT top 1 f.inicio FROM ssf.programapatrullajes f
                             WHERE f.id_propuestapatrullaje = a.id_propuestapatrullaje
                             AND YEAR(f.fechapatrullaje) = YEAR(getutcdate()) 
                             AND MONTH(f.fechapatrullaje) = MONTH(getutcdate()) 
                             AND DAY(f.fechapatrullaje) = DAY(getutcdate())),'00:00:00'),
                   COALESCE((SELECT g.descripcionestadopatrullaje 
                             FROM ssf.programapatrullajes f
                             JOIN ssf.estadopatrullaje g ON f.id_estadopatrullaje = g.id_estadopatrullaje
                             WHERE f.id_propuestapatrullaje = a.id_propuestapatrullaje
                             AND YEAR(f.fechapatrullaje) = YEAR(getutcdate()) 
                             AND MONTH(f.fechapatrullaje) = MONTH(getutcdate()) 
                             AND DAY(f.fechapatrullaje) = DAY(getutcdate())),c.descripcionestadopropuesta)	
            FROM ssf.propuestaspatrullajes a
            JOIN ssf.rutas b ON a.id_ruta = b.id_ruta
            JOIN ssf.niveles d ON a.riesgopatrullaje = d.id_nivel
            JOIN SSF.clasepatrullaje m ON a.id_clasepatrullaje = m.id_clasepatrullaje
            JOIN ssf.estadopropuesta c ON a.id_estadopropuesta = c.id_estadopropuesta
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
                new SqlParameter("@paramEstado", tipo)
            };

            return _programaContext.ProgramasVistas.FromSqlRaw(sqlQuery, parametros).ToList();
        }

        public List<ProgramaVista> ObtenerPatrullajesExtraordinariasPorRegion(string tipo,int region, string clase, string estadoPropuesta)
        {
            string sqlQuery = @"SELECT a.id_propuestapatrullaje,a.id_ruta,a.fechapatrullaje,a.id_puntoresponsable,b.clave,
                   b.regionmilitarsdn,b.regionssf,b.observaciones,a.ultimaactualizacion,a.id_usuario,d.descripcionnivel,
                   COALESCE(a.solicitudoficioautorizacion, '') as solicitudoficioautorizacion,
                   COALESCE(a.oficioautorizacion, '') as oficioautorizacion,
                   COALESCE((SELECT STRING_AGG(CAST(ubicacion as nvarchar(MAX)), '-') WITHIN GROUP(ORDER BY f.posicion ASC)
                             FROM ssf.itinerario f join ssf.puntospatrullaje g on f.id_punto = g.id_punto
                             WHERE f.id_ruta = a.id_ruta),'') as itinerarioruta,
                   COALESCE((SELECT top 1 f.inicio FROM ssf.programapatrullajes f
                             WHERE f.id_propuestapatrullaje = a.id_propuestapatrullaje
                             AND YEAR(f.fechapatrullaje) = YEAR(getutcdate()) 
                             AND MONTH(f.fechapatrullaje) = MONTH(getutcdate()) 
                             AND DAY(f.fechapatrullaje) = DAY(getutcdate())),'00:00:00'),
                   COALESCE((SELECT g.descripcionestadopatrullaje 
                             FROM ssf.programapatrullajes f
                             JOIN ssf.estadopatrullaje g ON f.id_estadopatrullaje = g.id_estadopatrullaje
                             WHERE f.id_propuestapatrullaje = a.id_propuestapatrullaje
                             AND YEAR(f.fechapatrullaje) = YEAR(getutcdate()) 
                             AND MONTH(f.fechapatrullaje) = MONTH(getutcdate()) 
                             AND DAY(f.fechapatrullaje) = DAY(getutcdate())),c.descripcionestadopropuesta)	
            FROM ssf.propuestaspatrullajes a
            JOIN ssf.rutas b ON a.id_ruta = b.id_ruta
            JOIN ssf.estadopropuesta c ON a.id_estadopropuesta=c.id_estadopropuesta
            JOIN ssf.niveles d ON a.riesgopatrullaje = d.id_nivel
            JOIN ssf.propuestaspatrullajescomplementossf e ON a.id_propuestapatrullaje=e.id_propuestapatrullaje
            JOIN SSF.clasepatrullaje m ON a.id_clasepatrullaje = m.id_clasepatrullaje
            JOIN ssf.estadopropuesta c ON a.id_estadopropuesta = c.id_estadopropuesta
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

                new SqlParameter("@paramTipo", tipo),
                new SqlParameter("@paramRegion", region),
                new SqlParameter("@paramClase", clase),
                new SqlParameter("@paramEstado", tipo)
            };

            return _programaContext.ProgramasVistas.FromSqlRaw(sqlQuery, parametros).ToList();
        }
        public List<ProgramaVista> ObtenerPatrullajesExtraordinariasPorMes(string tipo, int mes, int anio, int region, string clase, string estadoPropuesta)
        {
            string sqlQuery = @"SELECT a.id_propuestapatrullaje,a.id_ruta,a.fechapatrullaje,a.id_puntoresponsable,b.clave,
                   b.regionmilitarsdn,b.regionssf,b.observaciones,a.ultimaactualizacion,a.id_usuario,d.descripcionnivel,
                   COALESCE(a.solicitudoficioautorizacion, '') as solicitudoficioautorizacion,
                   COALESCE(a.oficioautorizacion, '') as oficioautorizacion,
                   COALESCE((SELECT STRING_AGG(CAST(ubicacion as nvarchar(MAX)), '-') WITHIN GROUP(ORDER BY f.posicion ASC)
                             FROM ssf.itinerario f join ssf.puntospatrullaje g on f.id_punto = g.id_punto
                             WHERE f.id_ruta = a.id_ruta),'') as itinerarioruta,
                   COALESCE((SELECT top 1 f.inicio FROM ssf.programapatrullajes f
                             WHERE f.id_propuestapatrullaje = a.id_propuestapatrullaje
                             AND YEAR(f.fechapatrullaje) = YEAR(getutcdate()) 
                             AND MONTH(f.fechapatrullaje) = MONTH(getutcdate()) 
                             AND DAY(f.fechapatrullaje) = DAY(getutcdate())),'00:00:00'),
                   COALESCE((SELECT g.descripcionestadopatrullaje 
                             FROM ssf.programapatrullajes f
                             JOIN ssf.estadopatrullaje g ON f.id_estadopatrullaje = g.id_estadopatrullaje
                             WHERE f.id_propuestapatrullaje = a.id_propuestapatrullaje
                             AND YEAR(f.fechapatrullaje) = YEAR(getutcdate()) 
                             AND MONTH(f.fechapatrullaje) = MONTH(getutcdate()) 
                             AND DAY(f.fechapatrullaje) = DAY(getutcdate())),c.descripcionestadopropuesta)	
            FROM ssf.propuestaspatrullajes a
            JOIN ssf.rutas b ON a.id_ruta = b.id_ruta
            JOIN ssf.estadopropuesta c ON a.id_estadopropuesta=c.id_estadopropuesta
            JOIN ssf.niveles d ON a.riesgopatrullaje = d.id_nivel
            JOIN ssf.propuestaspatrullajescomplementossf e ON a.id_propuestapatrullaje=e.id_propuestapatrullaje
            JOIN SSF.clasepatrullaje m ON a.id_clasepatrullaje = m.id_clasepatrullaje
            JOIN ssf.estadopropuesta c ON a.id_estadopropuesta = c.id_estadopropuesta
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
                new SqlParameter("@paramEstado", tipo)
            };

            return _programaContext.ProgramasVistas.FromSqlRaw(sqlQuery, parametros).ToList();
        }

        public List<ProgramaVista> ObtenerPatrullajesExtraordinariasPorDia(string tipo, int dia, int mes, int anio, int region, string clase, string estadoPropuesta)
        {
            string sqlQuery = @"SELECT a.id_propuestapatrullaje,a.id_ruta,a.fechapatrullaje,a.id_puntoresponsable,b.clave,
                   b.regionmilitarsdn,b.regionssf,b.observaciones,a.ultimaactualizacion,a.id_usuario,d.descripcionnivel,
                   COALESCE(a.solicitudoficioautorizacion, '') as solicitudoficioautorizacion,
                   COALESCE(a.oficioautorizacion, '') as oficioautorizacion,
                   COALESCE((SELECT STRING_AGG(CAST(ubicacion as nvarchar(MAX)), '-') WITHIN GROUP(ORDER BY f.posicion ASC)
                             FROM ssf.itinerario f join ssf.puntospatrullaje g on f.id_punto = g.id_punto
                             WHERE f.id_ruta = a.id_ruta),'') as itinerarioruta,
                   COALESCE((SELECT top 1 f.inicio FROM ssf.programapatrullajes f
                             WHERE f.id_propuestapatrullaje = a.id_propuestapatrullaje
                             AND YEAR(f.fechapatrullaje) = YEAR(getutcdate()) 
                             AND MONTH(f.fechapatrullaje) = MONTH(getutcdate()) 
                             AND DAY(f.fechapatrullaje) = DAY(getutcdate())),'00:00:00'),
                   COALESCE((SELECT g.descripcionestadopatrullaje 
                             FROM ssf.programapatrullajes f
                             JOIN ssf.estadopatrullaje g ON f.id_estadopatrullaje = g.id_estadopatrullaje
                             WHERE f.id_propuestapatrullaje = a.id_propuestapatrullaje
                             AND YEAR(f.fechapatrullaje) = YEAR(getutcdate()) 
                             AND MONTH(f.fechapatrullaje) = MONTH(getutcdate()) 
                             AND DAY(f.fechapatrullaje) = DAY(getutcdate())),c.descripcionestadopropuesta)	
            FROM ssf.propuestaspatrullajes a
            JOIN ssf.rutas b ON a.id_ruta = b.id_ruta
            JOIN ssf.estadopropuesta c ON a.id_estadopropuesta=c.id_estadopropuesta
            JOIN ssf.niveles d ON a.riesgopatrullaje = d.id_nivel
            JOIN ssf.propuestaspatrullajescomplementossf e ON a.id_propuestapatrullaje=e.id_propuestapatrullaje
            JOIN SSF.clasepatrullaje m ON a.id_clasepatrullaje = m.id_clasepatrullaje
            JOIN ssf.estadopropuesta c ON a.id_estadopropuesta = c.id_estadopropuesta
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
                new SqlParameter("@paramEstado", tipo)
            };

            return _programaContext.ProgramasVistas.FromSqlRaw(sqlQuery, parametros).ToList();
        }


        /*     public List<PatrullajesProgYExtRegistradosVista> ObtenerCaso0Ordinario(string tipo, int region,  int mes, int anio) 
             {
                 string sqlQuery = @"SELECT a.id_programa IdPrograma, a.id_ruta IdRuta, a.fechapatrullaje FechaPatrullaje, a.inicio Inicio
                                           ,a.id_puntoresponsable IdPuntoResponsable, b.clave Clave, b.regionmilitarsdn RegionMilitarSDN
                                           ,b.regionssf RegionSSF, b.observaciones ObservacionesRuta, c.descripcionestadopatrullaje DescripcionEstadoPatrullaje
                                           ,a.observaciones ObservacionesPrograma, a.riesgopatrullaje IdRiesgoPatrullaje
                                           ,COALESCE(a.solicitudoficiocomision, '') SolicitudOficioComision
                                           ,COALESCE(a.oficiocomision, '') OficioComision
                                           ,d.descripcionnivel DescripcionNivelRiesgo
                                           ,COALESCE((SELECT STRING_AGG(CAST(ubicacion as nvarchar(MAX)), '-') WITHIN GROUP(ORDER BY f.posicion ASC)
                                                      FROM ssf.itinerario f join ssf.puntospatrullaje g on f.id_punto = g.id_punto
                                                      WHERE f.id_ruta = a.id_ruta),'') as Itinerario
                                           ,a.ultimaactualizacion UltimaActualizacion
                                           ,a.id_usuario IdUsuario
                                           ,a.id_usuarioresponsablepatrullaje UsuarioResponsablePatrullaje
                                      FROM ssf.programapatrullajes a
                                      JOIN ssf.rutas b ON a.id_ruta = b.id_ruta
                                      JOIN ssf.estadopatrullaje c ON a.id_estadopatrullaje = c.id_estadopatrullaje
                                      JOIN ssf.niveles d ON a.riesgopatrullaje = d.id_nivel
                                      JOIN ssf.tipopatrullaje e ON b.id_tipopatrullaje = e.id_tipopatrullaje
                                      WHERE e.descripcion = @pTipo AND b.regionssf = @pRegion AND MONTH(a.fechapatrullaje)=@pMes AND YEAR(a.fechapatrullaje)= @pAnio 
                                      ORDER BY a.fechapatrullaje";

                 object[] parametros = new object[]
                 {              
                     new SqlParameter("@pTipo", tipo),
                     new SqlParameter("@pRegion", region),
                     new SqlParameter("@pAnio", anio),
                     new SqlParameter("@pMes", mes)
                  };

                 return _programaContext.ProgramasVistas.FromSqlRaw(sqlQuery, parametros).ToList();

             }*/



        public List<PropuestaVista> Caso0Ordinario(string tipo, int mes, int anio, int region)
        {
            string sqlQuery = @"SELECT p.id, p.fecha,  p.id_punto_responsable, p.id_ruta, r.clave, r.id_comandancia_regional_SSF, 
                                       r.region_militar_SDN, r.observaciones, p.id_nivel_riesgo, p.id_usuario, n.descripcion,
                                       COALESCE((SELECT STRING_AGG(CAST(p.ubicacion as nvarchar(MAX)), '-') WITHIN GROUP(ORDER BY i.posicion ASC)
                                                FROM dmn.Itinerario i JOIN dmn.Punto_Patrullaje p ON i.id_punto = p.id
                                                WHERE i.id_ruta = r.id),'') as itinerario
                                FROM dmn.Programa_Patrullaje p
                                JOIN dmn.Ruta r ON p.id_ruta = r.id
                                JOIN cat.Estado_Patrullaje e ON p.id_estado_patrullaje = e.id
                                JOIN cat.Tipo_Patrullaje t ON r.id_tipo_patrullaje = t.id
                                JOIN cat.Nivel_Riesgo n ON p.id_nivel_riesgo = n.id
                                WHERE YEAR(p.fecha) = @pAnio AND MONTH(p.fecha) = @pMes
                                AND r.id_comandancia_regional_SSF = @pRegion
                                AND t.nombre = @pTipo
                                ORDER BY p.fecha";

            object[] parametros = new object[]
            {
                new SqlParameter("@pAnio", anio),
                new SqlParameter("@pMes", mes),
                new SqlParameter("@pRegion", region),
                new SqlParameter("@pTipo", tipo)
            };

            return _programaContext.PropuestasVistas.FromSqlRaw(sqlQuery, parametros).ToList();
        }
        public List<PropuestaVista> Caso5Ordinario(string tipo, int mes, int anio, int region, string clase)
        {
            string sqlQuery = @"SELECT p.id, p.id_usuario, pf.fecha, pc.id_punto_responsable, 
                                       r.id id_ruta, r.clave, r.id_comandancia_regional_SSF, r.region_militar_SDN, r.observaciones, n.descripcion,
                                       COALESCE((SELECT STRING_AGG(CAST(p.ubicacion as nvarchar(MAX)),'-') WITHIN GROUP (ORDER BY i.posicion ASC) 
                                                FROM dmn.Itinerario i JOIN dmn.Punto_Patrullaje p ON i.id_punto = p.id
                                                WHERE i.id_ruta=r.id),'') as itinerario,
                                       COALESCE((SELECT MIN(pg.fecha) FROM dmn.Programa_Patrullaje pg
                                                 WHERE pg.id_propuesta_patrullaje = p.id
                                                AND YEAR(pg.fecha) = YEAR(getutcdate()) 
                                                AND MONTH(pg.fecha) = MONTH(getutcdate()) 
                                                AND DAY(pg.fecha) = DAY(getutcdate()))
                                	            ,DATEADD(hh, 0, DATEADD(dd, DATEDIFF(dd, 0, GETDATE()), 0))) inicio,
                                       COALESCE((SELECT TOP 1 e.estado FROM dmn.Programa_Patrullaje pg
                                                 JOIN cat.Estado_Patrullaje e ON pg.id_estado_patrullaje = e.id
                                                 WHERE pg.id_propuesta_patrullaje = p.id
                                                 AND YEAR(pg.fecha) = YEAR(getutcdate()) 
                                                 AND MONTH(pg.fecha) = MONTH(getutcdate()) 
                                                 AND DAY(pg.fecha) = DAY(getutcdate()) )
                                                 ,e.estado) estadopatrullaje
                                FROM dmn.Propuesta_patrullaje p
                                JOIN dmn.Propuesta_Patrullaje_Ruta_Contenedor pc ON p.id = pc.id_propuesta_patrullaje
                                JOIN dmn.Propuesta_Patrullaje_Fecha pf ON pc.id_propuesta_patrullaje = pf.id_propuesta_patrullaje AND pc.id_ruta = pf.id_ruta
                                JOIN dmn.Ruta r ON pc.id_ruta = r.id
                                JOIN cat.Clase_Patrullaje c ON p.id_clase_patrullaje = c.id
                                JOIN cat.Estado_Propuesta e ON pf.id_estado_propuesta = e.id
                                JOIN cat.Tipo_Patrullaje t ON r.id_tipo_patrullaje = t.id
                                JOIN cat.Nivel_Riesgo n ON pc.id_nivel_riesgo = n.id
                                WHERE YEAR(PF.fecha) = @pAnio AND MONTH(PF.fecha) = @pMes
                                AND r.id_comandancia_regional_SSF = @pRegion
                                AND t.nombre = @pTipo
                                AND c.nombre = @pClase
                                ORDER BY pf.fecha";

            object[] parametros = new object[]
            {
                new SqlParameter("@pAnio", anio),
                new SqlParameter("@pMes", mes),
                new SqlParameter("@pRegion", region),
                new SqlParameter("@pTipo", tipo),
                new SqlParameter("@pClase", clase)
            };

            return _programaContext.PropuestasVistas.FromSqlRaw(sqlQuery, parametros).ToList();
        }


       // Del 6 al 9 sn lo mismo en ordinario
        public List<PropuestaVista> Caso6Ordinario(string tipo, int mes, int anio, int region, string clase, string estado)
        {
            // estado = "Pendiente de autorizacion por la SSF"
            string sqlQuery = @"SELECT p.id, p.id_usuario, pf.fecha, pc.id_punto_responsable, 
                                       r.id id_ruta, r.clave, r.id_comandancia_regional_SSF, r.region_militar_SDN, r.observaciones, d.descripcion,
                                       COALESCE((SELECT STRING_AGG(CAST(p.ubicacion as nvarchar(MAX)),'-') WITHIN GROUP (ORDER BY i.posicion ASC) 
                                                FROM dmn.Itinerario i JOIN dmn.Punto_Patrullaje p ON i.id_punto = p.id
                                                WHERE i.id_ruta=r.id),'') as itinerario,
                                       COALESCE((SELECT MIN(pg.fecha) FROM dmn.Programa_Patrullaje pg
                                                 WHERE pg.id_propuesta_patrullaje = p.id
                                                AND YEAR(pg.fecha) = YEAR(getutcdate()) 
                                                AND MONTH(pg.fecha) = MONTH(getutcdate()) 
                                                AND DAY(pg.fecha) = DAY(getutcdate()))
                                	            ,DATEADD(hh, 0, DATEADD(dd, DATEDIFF(dd, 0, GETDATE()), 0))) inicio,
                                       COALESCE((SELECT TOP 1 e.estado FROM dmn.Programa_Patrullaje pg
                                                 JOIN cat.Estado_Patrullaje e ON pg.id_estado_patrullaje = e.id
                                                 WHERE pg.id_propuesta_patrullaje = p.id
                                                 AND YEAR(pg.fecha) = YEAR(getutcdate()) 
                                                 AND MONTH(pg.fecha) = MONTH(getutcdate()) 
                                                 AND DAY(pg.fecha) = DAY(getutcdate()) )
                                                 ,e.estado) estadopatrullaje
                                FROM dmn.Propuesta_patrullaje p
                                JOIN dmn.Propuesta_Patrullaje_Ruta_Contenedor pc ON p.id = pc.id_propuesta_patrullaje
                                JOIN dmn.Propuesta_Patrullaje_Fecha pf ON pc.id_propuesta_patrullaje = pf.id_propuesta_patrullaje AND pc.id_ruta = pf.id_ruta
                                JOIN dmn.Ruta r ON pc.id_ruta = r.id
                                JOIN cat.Clase_Patrullaje c ON p.id_clase_patrullaje = c.id
                                JOIN cat.Estado_Propuesta e ON pf.id_estado_propuesta = e.id
                                JOIN cat.Tipo_Patrullaje t ON r.id_tipo_patrullaje = t.id
                                JOIN cat.Nivel_Riesgo n ON pc.id_nivel_riesgo = n.id
                                WHERE YEAR(PF.fecha) = @pAnio AND MONTH(PF.fecha) = @pMes
                                AND r.id_comandancia_regional_SSF = @pRegion
                                AND t.nombre = @pTipo
                                AND c.nombre = @pClase
                                AND e.estado = @pEstado
                                ORDER BY pf.fecha";

            object[] parametros = new object[]
            {
                new SqlParameter("@pAnio", anio),               
                new SqlParameter("@pMes", mes),
                new SqlParameter("@pRegion", region),
                new SqlParameter("@pTipo", tipo),
                new SqlParameter("@pClase", clase),
                new SqlParameter("@pEstado", estado),
            };

            return _programaContext.PropuestasVistas.FromSqlRaw(sqlQuery, parametros).ToList();
        }

         public List<PropuestaVista> Caso7Ordinario(string tipo, int mes, int anio, int region, string clase, string estado)
        {
            // estado = "Autorizada"
            string sqlQuery = @"SELECT p.id, p.id_usuario, pf.fecha, pc.id_punto_responsable, 
                                       r.id id_ruta, r.clave, r.id_comandancia_regional_SSF, r.region_militar_SDN, r.observaciones, n.descripcion,
                                       COALESCE((SELECT STRING_AGG(CAST(p.ubicacion as nvarchar(MAX)),'-') WITHIN GROUP (ORDER BY i.posicion ASC) 
                                                FROM dmn.Itinerario i JOIN dmn.Punto_Patrullaje p ON i.id_punto = p.id
                                                WHERE i.id_ruta=r.id),'') as itinerario,
                                       COALESCE((SELECT MIN(pg.fecha) FROM dmn.Programa_Patrullaje pg
                                                 WHERE pg.id_propuesta_patrullaje = p.id
                                                AND YEAR(pg.fecha) = YEAR(getutcdate()) 
                                                AND MONTH(pg.fecha) = MONTH(getutcdate()) 
                                                AND DAY(pg.fecha) = DAY(getutcdate()))
                                	            ,DATEADD(hh, 0, DATEADD(dd, DATEDIFF(dd, 0, GETDATE()), 0))) inicio,
                                       COALESCE((SELECT TOP 1 e.estado FROM dmn.Programa_Patrullaje pg
                                                 JOIN cat.Estado_Patrullaje e ON pg.id_estado_patrullaje = e.id
                                                 WHERE pg.id_propuesta_patrullaje = p.id
                                                 AND YEAR(pg.fecha) = YEAR(getutcdate()) 
                                                 AND MONTH(pg.fecha) = MONTH(getutcdate()) 
                                                 AND DAY(pg.fecha) = DAY(getutcdate()) )
                                                 ,e.estado) estadopatrullaje
                                FROM dmn.Propuesta_patrullaje p
                                JOIN dmn.Propuesta_Patrullaje_Ruta_Contenedor pc ON p.id = pc.id_propuesta_patrullaje
                                JOIN dmn.Propuesta_Patrullaje_Fecha pf ON pc.id_propuesta_patrullaje = pf.id_propuesta_patrullaje AND pc.id_ruta = pf.id_ruta
                                JOIN dmn.Ruta r ON pc.id_ruta = r.id
                                JOIN cat.Clase_Patrullaje c ON p.id_clase_patrullaje = c.id
                                JOIN cat.Estado_Propuesta e ON pf.id_estado_propuesta = e.id
                                JOIN cat.Tipo_Patrullaje t ON r.id_tipo_patrullaje = t.id
                                JOIN cat.Nivel_Riesgo n ON pc.id_nivel_riesgo = n.id
                                WHERE YEAR(PF.fecha) = @pAnio AND MONTH(PF.fecha) = @pMes
                                AND r.id_comandancia_regional_SSF = @pRegion
                                AND t.nombre = @pTipo
                                AND c.nombre = @pClase
                                AND e.estado = @pEstado
                                ORDER BY pf.fecha";

            object[] parametros = new object[]
            {
                new SqlParameter("@pAnio", anio),
                new SqlParameter("@pMes", mes),
                new SqlParameter("@pRegion", region),
                new SqlParameter("@pTipo", tipo),
                new SqlParameter("@pClase", clase),
                new SqlParameter("@pEstado", estado),
            };

            return _programaContext.PropuestasVistas.FromSqlRaw(sqlQuery, parametros).ToList();
        }

        public List<PropuestaVista> Caso8Ordinario(string tipo, int mes, int anio, int region, string clase, string estado)
        {
            // estado = "Rechazada"
            string sqlQuery = @"SELECT p.id, p.id_usuario, pf.fecha, pc.id_punto_responsable, 
                                       r.id id_ruta, r.clave, r.id_comandancia_regional_SSF, r.region_militar_SDN, r.observaciones, n.descripcion,
                                       COALESCE((SELECT STRING_AGG(CAST(p.ubicacion as nvarchar(MAX)),'-') WITHIN GROUP (ORDER BY i.posicion ASC) 
                                                FROM dmn.Itinerario i JOIN dmn.Punto_Patrullaje p ON i.id_punto = p.id
                                                WHERE i.id_ruta=r.id),'') as itinerario,
                                       COALESCE((SELECT MIN(pg.fecha) FROM dmn.Programa_Patrullaje pg
                                                 WHERE pg.id_propuesta_patrullaje = p.id
                                                AND YEAR(pg.fecha) = YEAR(getutcdate()) 
                                                AND MONTH(pg.fecha) = MONTH(getutcdate()) 
                                                AND DAY(pg.fecha) = DAY(getutcdate()))
                                	            ,DATEADD(hh, 0, DATEADD(dd, DATEDIFF(dd, 0, GETDATE()), 0))) inicio,
                                       COALESCE((SELECT TOP 1 e.estado FROM dmn.Programa_Patrullaje pg
                                                 JOIN cat.Estado_Patrullaje e ON pg.id_estado_patrullaje = e.id
                                                 WHERE pg.id_propuesta_patrullaje = p.id
                                                 AND YEAR(pg.fecha) = YEAR(getutcdate()) 
                                                 AND MONTH(pg.fecha) = MONTH(getutcdate()) 
                                                 AND DAY(pg.fecha) = DAY(getutcdate()) )
                                                 ,e.estado) estadopatrullaje
                                FROM dmn.Propuesta_patrullaje p
                                JOIN dmn.Propuesta_Patrullaje_Ruta_Contenedor pc ON p.id = pc.id_propuesta_patrullaje
                                JOIN dmn.Propuesta_Patrullaje_Fecha pf ON pc.id_propuesta_patrullaje = pf.id_propuesta_patrullaje AND pc.id_ruta = pf.id_ruta
                                JOIN dmn.Ruta r ON pc.id_ruta = r.id
                                JOIN cat.Clase_Patrullaje c ON p.id_clase_patrullaje = c.id
                                JOIN cat.Estado_Propuesta e ON pf.id_estado_propuesta = e.id
                                JOIN cat.Tipo_Patrullaje t ON r.id_tipo_patrullaje = t.id
                                JOIN cat.Nivel_Riesgo n ON pc.id_nivel_riesgo = n.id
                                WHERE YEAR(PF.fecha) = @pAnio AND MONTH(PF.fecha) = @pMes
                                AND r.id_comandancia_regional_SSF = @pRegion
                                AND t.nombre = @pTipo
                                AND c.nombre = @pClase
                                AND e.estado = @pEstado
                                ORDER BY pf.fecha";

            object[] parametros = new object[]
            {
                new SqlParameter("@pAnio", anio),
                new SqlParameter("@pMes", mes),
                new SqlParameter("@pRegion", region),
                new SqlParameter("@pTipo", tipo),
                new SqlParameter("@pClase", clase),
                new SqlParameter("@pEstado", estado),
            };

            return _programaContext.PropuestasVistas.FromSqlRaw(sqlQuery, parametros).ToList();
        }

        public List<PropuestaVista> Caso9Ordinario(string tipo, int mes, int anio, int region, string clase, string estado)
        {
            // estado = "Aprobada por comandancia regional"
            string sqlQuery = @"SELECT p.id, p.id_usuario, pf.fecha, pc.id_punto_responsable, 
                                       r.id id_ruta, r.clave, r.id_comandancia_regional_SSF, r.region_militar_SDN, r.observaciones, n.descripcion
                                       COALESCE((SELECT STRING_AGG(CAST(p.ubicacion as nvarchar(MAX)),'-') WITHIN GROUP (ORDER BY i.posicion ASC) 
                                                FROM dmn.Itinerario i JOIN dmn.Punto_Patrullaje p ON i.id_punto = p.id
                                                WHERE i.id_ruta=r.id),'') as itinerario,
                                       COALESCE((SELECT MIN(pg.fecha) FROM dmn.Programa_Patrullaje pg
                                                 WHERE pg.id_propuesta_patrullaje = p.id
                                                AND YEAR(pg.fecha) = YEAR(getutcdate()) 
                                                AND MONTH(pg.fecha) = MONTH(getutcdate()) 
                                                AND DAY(pg.fecha) = DAY(getutcdate()))
                                	            ,DATEADD(hh, 0, DATEADD(dd, DATEDIFF(dd, 0, GETDATE()), 0))) inicio,
                                       COALESCE((SELECT TOP 1 e.estado FROM dmn.Programa_Patrullaje pg
                                                 JOIN cat.Estado_Patrullaje e ON pg.id_estado_patrullaje = e.id
                                                 WHERE pg.id_propuesta_patrullaje = p.id
                                                 AND YEAR(pg.fecha) = YEAR(getutcdate()) 
                                                 AND MONTH(pg.fecha) = MONTH(getutcdate()) 
                                                 AND DAY(pg.fecha) = DAY(getutcdate()) )
                                                 ,e.estado) estadopatrullaje
                                FROM dmn.Propuesta_patrullaje p
                                JOIN dmn.Propuesta_Patrullaje_Ruta_Contenedor pc ON p.id = pc.id_propuesta_patrullaje
                                JOIN dmn.Propuesta_Patrullaje_Fecha pf ON pc.id_propuesta_patrullaje = pf.id_propuesta_patrullaje AND pc.id_ruta = pf.id_ruta
                                JOIN dmn.Ruta r ON pc.id_ruta = r.id
                                JOIN cat.Clase_Patrullaje c ON p.id_clase_patrullaje = c.id
                                JOIN cat.Estado_Propuesta e ON pf.id_estado_propuesta = e.id
                                JOIN cat.Tipo_Patrullaje t ON r.id_tipo_patrullaje = t.id
                                JOIN cat.Nivel_Riesgo n ON pc.id_nivel_riesgo = n.id
                                WHERE YEAR(PF.fecha) = @pAnio AND MONTH(PF.fecha) = @pMes
                                AND r.id_comandancia_regional_SSF = @pRegion
                                AND t.nombre = @pTipo
                                AND c.nombre = @pClase
                                AND e.estado = @pEstado
                                ORDER BY pf.fecha";

            object[] parametros = new object[]
            {
                new SqlParameter("@pAnio", anio),
                new SqlParameter("@pMes", mes),
                new SqlParameter("@pRegion", region),
                new SqlParameter("@pTipo", tipo),
                new SqlParameter("@pClase", clase),
                new SqlParameter("@pEstado", estado),
            };

            return _programaContext.PropuestasVistas.FromSqlRaw(sqlQuery, parametros).ToList();
        }
    }
}
