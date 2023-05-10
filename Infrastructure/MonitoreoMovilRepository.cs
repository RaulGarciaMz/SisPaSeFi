using Domain.Entities;
using Domain.Entities.Vistas;
using Domain.Ports.Driven.Repositories;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using SqlServerAdapter.Data;

namespace SqlServerAdapter
{
    public class MonitoreoMovilRepository : IMonitoreoMovilRepo
    {
        protected readonly MonitoreoContext _monitoreoContext;

        public MonitoreoMovilRepository(MonitoreoContext monitoreoContext)
        {
            _monitoreoContext = monitoreoContext ?? throw new ArgumentNullException(nameof(monitoreoContext));
        }

        public async Task<List<MonitoreoVista>> ObtenerProgramasEnEstadoPreConcluidoPorTipoAsync(string tipo, int idUsuario)
        {

            string sqlQuery = @"SELECT a.id_programa, a.id_ruta, a.fechapatrullaje, a.inicio, b.clave, b.regionmilitarsdn, 
                                       b.regionssf, b.observaciones, d.descripcionestadopatrullaje, a.riesgopatrullaje, 
                                       c.descripcionnivel, a.id_puntoresponsable, a.id_ruta_original, e.descripcion
                                FROM ssf.programapatrullajes a
                                JOIN ssf.rutas b ON a.id_ruta=b.id_ruta
                                JOIN ssf.niveles c ON a.riesgopatrullaje=c.id_nivel
                                JOIN ssf.estadopatrullaje d ON a.id_estadopatrullaje=d.id_estadopatrullaje
                                JOIN ssf.apoyopatrullaje e ON a.id_apoyopatrullaje=e.id_apoyopatrullaje
                                JOIN  ssf.tipopatrullaje f ON b.id_tipopatrullaje = f.id_tipopatrullaje
                                WHERE DAY(a.fechapatrullaje)=DAY(GETDATE()) AND MONTH(a.fechapatrullaje)=MONTH(GETDATE()) AND YEAR(a.fechapatrullaje)=YEAR(GETDATE())
                                AND f.descripcion=@pTipo
                                AND b.regionssf in (SELECT id_comandancia FROM ssf.usuariocomandancia WHERE id_usuario=@pIdUsuario)
                                AND a.id_estadopatrullaje between 1 and 3
                                ORDER BY b.regionssf, a.inicio";

            object[] parametros = new object[]
            {
                new SqlParameter("@pTipo", tipo),
                new SqlParameter("@pIdUsuario", idUsuario)
            };

            return await _monitoreoContext.MonitoreosVista.FromSqlRaw(sqlQuery, parametros).ToListAsync();
        }

        public async Task<List<MonitoreoVista>> ObtenerProgramasConcluidosPorTipoAsync(string tipo, int idUsuario)
        {

            string sqlQuery = @"SELECT a.id_programa, a.id_ruta, a.fechapatrullaje, a.inicio, b.clave, b.regionmilitarsdn, 
                                       b.regionssf, b.observaciones, d.descripcionestadopatrullaje, a.riesgopatrullaje, 
                                       c.descripcionnivel, a.id_puntoresponsable, a.id_ruta_original, e.descripcion
                                FROM ssf.programapatrullajes a
                                JOIN ssf.rutas b ON a.id_ruta=b.id_ruta
                                JOIN ssf.niveles c ON a.riesgopatrullaje=c.id_nivel
                                JOIN ssf.estadopatrullaje d ON a.id_estadopatrullaje=d.id_estadopatrullaje
                                JOIN ssf.apoyopatrullaje e ON a.id_apoyopatrullaje=e.id_apoyopatrullaje
                                JOIN  ssf.tipopatrullaje f ON b.id_tipopatrullaje = f.id_tipopatrullaje
                                WHERE DAY(a.fechapatrullaje)=DAY(GETDATE()) AND MONTH(a.fechapatrullaje)=MONTH(GETDATE()) AND YEAR(a.fechapatrullaje)=YEAR(GETDATE())
                                AND f.descripcion=@pTipo
                                AND b.regionssf in (SELECT id_comandancia FROM ssf.usuariocomandancia WHERE id_usuario=@pIdUsuario)
                                AND d.descripcionEstadoPatrullaje = 'Concluido'
                                ORDER BY b.regionssf, a.inicio";

            object[] parametros = new object[]
            {
                new SqlParameter("@pTipo", tipo),
                new SqlParameter("@pIdUsuario", idUsuario)
            };

            return await _monitoreoContext.MonitoreosVista.FromSqlRaw(sqlQuery, parametros).ToListAsync();
        }

        public async Task<List<MonitoreoVista>> ObtenerProgramasEnEstadoPostConclucionPorTipoAsync(string tipo, int idUsuario)
        {

            string sqlQuery = @"SELECT a.id_programa, a.id_ruta, a.fechapatrullaje, a.inicio, b.clave, b.regionmilitarsdn, 
                                       b.regionssf, b.observaciones, d.descripcionestadopatrullaje, a.riesgopatrullaje, 
                                       c.descripcionnivel, a.id_puntoresponsable, a.id_ruta_original, e.descripcion
                                FROM ssf.programapatrullajes a
                                JOIN ssf.rutas b ON a.id_ruta=b.id_ruta
                                JOIN ssf.niveles c ON a.riesgopatrullaje=c.id_nivel
                                JOIN ssf.estadopatrullaje d ON a.id_estadopatrullaje=d.id_estadopatrullaje
                                JOIN ssf.apoyopatrullaje e ON a.id_apoyopatrullaje=e.id_apoyopatrullaje
                                JOIN  ssf.tipopatrullaje f ON b.id_tipopatrullaje = f.id_tipopatrullaje
                                WHERE DAY(a.fechapatrullaje)=DAY(GETDATE()) AND MONTH(a.fechapatrullaje)=MONTH(GETDATE()) AND YEAR(a.fechapatrullaje)=YEAR(GETDATE())
                                AND f.descripcion=@pTipo
                                AND b.regionssf in (SELECT id_comandancia FROM ssf.usuariocomandancia WHERE id_usuario=@pIdUsuario)
                                AND a.id_estadopatrullaje > (SELECT id_estadopatrullaje FROM ssf.estadopatrullaje WHERE descripcionestadopatrullaje='Concluido')
                                ORDER BY b.regionssf, a.inicio";

            object[] parametros = new object[]
            {
                new SqlParameter("@pTipo", tipo),
                new SqlParameter("@pIdUsuario", idUsuario)
            };

            return await _monitoreoContext.MonitoreosVista.FromSqlRaw(sqlQuery, parametros).ToListAsync();
        }

        public async Task<List<PuntoEnRutaVista>> ObtenerPuntosEnRutaAsync(int idRuta)
        {
            string sqlQuery = @"SELECT b.ubicacion, b.latitud, b.longitud 
                                FROM ssf.itinerario a
                                JOIN ssf.puntospatrullaje b ON a.id_punto=b.id_punto
                                WHERE a.id_ruta= @pIdRuta
                                ORDER BY a.posicion";

            object[] parametros = new object[]
            {
                new SqlParameter("@pIdRuta", idRuta)
            };

            return await _monitoreoContext.PuntosEnRutaVista.FromSqlRaw(sqlQuery, parametros).ToListAsync();

        }

        public async Task<List<TarjetaInformativa>> ObtenerTarjetasInformativasPorProgramaAsync(int idPrograma)
        {
            return await _monitoreoContext.TarjetasInformativas.Where(x => x.IdPrograma == idPrograma).AsNoTracking().ToListAsync();
        }

        public async Task<List<IncidenciaTarjetaVista>> ObtenerIncidenciasEnEstructuraPorTarjetaAsync(int idTarjeta)
        {
            string sqlQuery = @"SELECT DISTINCT * 
                                FROM (
                                    SELECT a.incidencia, b.nombre, c.clave 
                                	FROM ssf.reporteestructuras a
                                	JOIN ssf.estructura b ON a.id_estructura=b.id_estructura
                                	JOIN ssf.linea c ON b.id_linea=c.id_linea
                                    WHERE a.id_nota= @pIdTarjeta
                                    UNION 
                                    SELECT a.incidencia, b.nombre, c.clave 
                                	FROM ssf.reporteestructuras a
                                	JOIN ssf.estructura b ON a.id_estructura=b.id_estructura
                                	JOIN ssf.linea c ON b.id_linea=c.id_linea
                                	JOIN ssf.tarjetainformativareporte d ON a.id_reporte=d.idreporte
                                	WHERE d.idtarjeta=@pIdTarjeta
                                	AND d.idtiporeporte=(SELECT idtiporeporte FROM ssf.tiporeporte WHERE descripcion='ESTRUCTURA')
                                ) AS reportes
                                ORDER BY reportes.clave, reportes.nombre ";

            object[] parametros = new object[]
            {
                new SqlParameter("@pIdTarjeta", idTarjeta)
            };

            return await _monitoreoContext.IncidenciasTarjetasVista.FromSqlRaw(sqlQuery, parametros).ToListAsync();

        }

        public async Task<List<IncidenciaTarjetaVista>> ObtenerIncidenciasEnInstalacionPorTarjetaAsync(int idTarjeta)
        {
            string sqlQuery = @"SELECT DISTINCT * 
                                FROM (
                                        SELECT a.incidencia, b.ubicacion 
                                		FROM ssf.reportepunto a
                                		JOIN ssf.puntospatrullaje b ON a.id_punto=b.id_punto
                                        WHERE a.id_nota= @pIdTarjeta
                                        UNION 
                                        SELECT a.incidencia, b.ubicacion 
                                		FROM ssf.reportepunto a
                                		JOIN ssf.puntospatrullaje b ON a.id_punto=b.id_punto
                                		JOIN ssf.tarjetainformativareporte d ON a.id_reportepunto=d.idreporte
                                       WHERE d.idtarjeta= @pIdTarjeta
                                       AND d.idtiporeporte=(SELECT idtiporeporte FROM ssf.tiporeporte WHERE descripcion='INSTALACION')
                                     ) AS reportes
                                    ORDER BY reportes.ubicacion  ";

            object[] parametros = new object[]
            {
                new SqlParameter("@pIdTarjeta", idTarjeta)
            };

            return await _monitoreoContext.IncidenciasTarjetasVista.FromSqlRaw(sqlQuery, parametros).ToListAsync();
        }

        public async Task<List<UsoVehiculoMonitoreoVista>> ObtenerUsoVehiculoPorProgramaAsync(int idPrograma)
        {
            string sqlQuery = @"SELECT a.id_vehiculo, a.kminicio, a.kmfin, a.consumocombustible, 
                                       a.estadovehiculo, b.numeroeconomico, b.matricula
                                FROM ssf.usovehiculo a
                                JOIN ssf.vehiculos b ON a.id_vehiculo=b.id_vehiculo
                                WHERE a.id_programa= @pIdPrograma";

            object[] parametros = new object[]
            {
                new SqlParameter("@pIdPrograma", idPrograma)
            };

            return await _monitoreoContext.UsosVehiculosMonitoreoVista.FromSqlRaw(sqlQuery, parametros).ToListAsync();
        }

    }
}
