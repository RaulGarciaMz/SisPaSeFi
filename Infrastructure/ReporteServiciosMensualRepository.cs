using Domain.Entities.Vistas;
using Domain.Ports.Driven.Repositories;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using SqlServerAdapter.Data;

namespace SqlServerAdapter
{
    public class ReporteServiciosMensualRepository : IReporteServicioMensualRepo
    {
        protected readonly ReporteServiciosMensualContext _rpteMensualContext;

        public ReporteServiciosMensualRepository(ReporteServiciosMensualContext rpteMensualContext)
        {
            _rpteMensualContext = rpteMensualContext ?? throw new ArgumentNullException(nameof(rpteMensualContext));
        }

        public async Task<List<ReporteServicioMensualVista>> ObtenerReporteAereoPorOpcionAsync(int Anio, int Mes, string tipo)
        {
            string sqlQuery = @"SELECT a.id_nota,a.id_programa,b.fechaPatrullaje,b.id_ruta,c.regionSSF,c.id_tipoPatrullaje,a.ultimaActualizacion,
                                       a.id_usuario,a.inicio,a.termino,a.tiempoVuelo,a.calzoAcalzo,a.observaciones,d.descripcionEstadoPatrullaje, 
                                       a.kmRecorrido,a.comandantesInstalacionSSF,a.personalMilitarSEDENAOficial,a.id_estadoTarjetaInformativa,
                                       a.personalMilitarSEDENATropa,a.linieros,a.comandantesTurnoSSF,a.oficialesSSF,a.personalNavalSEMAROficial,
                                       a.personalNavalSEMARTropa,
                                       (SELECT STRING_AGG(CAST(g.ubicacion as nvarchar(MAX)),'-') WITHIN GROUP (ORDER BY f.posicion ASC) as ruta 
                                        FROM ssf.itinerario f
                                       	JOIN ssf.puntospatrullaje g ON f.id_punto=g.id_punto
                                        WHERE f.id_ruta=b.id_ruta ) as itinerariorutas,
                                       (SELECT STRING_AGG(CAST(CONCAT('Linea',h.clave,'Estructura',g.nombre,'Incidencia',f.incidencia,'SSFPATRULLAJECR') as nvarchar(MAX)),':') 
                                                    WITHIN GROUP (ORDER BY h.clave, g.nombre ASC) as incidenciaEstructura 
                                        FROM ssf.reporteEstructuras f
                                	    JOIN ssf.estructura g ON f.id_estructura=g.id_estructura
                                	    JOIN ssf.linea h ON g.id_linea=h.id_linea
                                        WHERE f.id_nota=a.id_nota) as incidenciaenestructura,
                                       (SELECT STRING_AGG(CAST(CONCAT('Instalacion',f.ubicacion,'Incidencia',g.incidencia,'SSFPATRULLAJECR') as nvarchar(MAX)),':') 
                                                           WITHIN GROUP (ORDER BY f.ubicacion ASC) as incidenciaInstalacion 
                                        FROM ssf.puntosPatrullaje f
                                        JOIN ssf.reportePunto g ON f.id_punto = g.id_punto
                                        WHERE g.id_nota=a.id_nota ) as incidenciaeninstalacion, 
                                       (SELECT STRING_AGG(CAST(CONCAT(g.matricula,'SSFPATRULLAJESCR')  as nvarchar(MAX)),' ') 
                                                    WITHIN GROUP (ORDER BY g.matricula ASC)  as vehiculo  
                                        FROM ssf.usovehiculo f
                                        JOIN ssf.vehiculos g ON f.id_vehiculo=g.id_vehiculo
                                        WHERE f.id_programa=a.id_programa ) as matriculas, 
                                       (SELECT STRING_AGG(CAST(CONCAT(CAST(f.kminicio AS VARCHAR(10)), ', ',CAST(f.kmFin AS VARCHAR(10)),'SSFPATRULLAJESCR')  as nvarchar(MAX)),' ') 
                                                    WITHIN GROUP (ORDER BY g.matricula ASC) as odometro 
                                        FROM ssf.usovehiculo f
                                        JOIN ssf.vehiculos g ON f.id_vehiculo=g.id_vehiculo
                                        WHERE f.id_programa=a.id_programa ) as odometros, 
                                       (SELECT STRING_AGG(CAST(CONCAT(CAST(f.kmfin-f.kminicio AS VARCHAR(10)), 'SSFPATRULLAJESCR')  as nvarchar(MAX)),' ') 
                                                    WITHIN GROUP (ORDER BY g.matricula ASC)   as kmTotal 
                                        FROM ssf.usovehiculo f
                                        JOIN ssf.vehiculos g ON f.id_vehiculo=g.id_vehiculo
                                        WHERE f.id_programa=a.id_programa ) as kmrecorridos 
                                FROM ssf.tarjetaInformativa a
                                JOIN ssf.programaPatrullajes b ON a.id_programa=b.id_programa
                                JOIN ssf.rutas c ON b.id_ruta=c.id_ruta
                                JOIN ssf.estadoPatrullaje d ON b.id_estadoPatrullaje=d.id_estadoPatrullaje
                                JOIN ssf.tipoPatrullaje e ON c.id_tipoPatrullaje=e.id_tipoPatrullaje
                                WHERE MONTH(b.fechaPatrullaje)=@pMes and YEAR(b.fechaPatrullaje)=@pAnio 
                                AND DATEDIFF(second,0,cast(a.tiempoVuelo as datetime))>0 
                                AND  e.descripcion= @pTipo 
                                ORDER by b.fechaPatrullaje,c.regionSSF, a.inicio";

            object[] parametros = new object[]
            {
                new SqlParameter("@pAnio", Anio),
                new SqlParameter("@pMes", Mes),
                new SqlParameter("@pTipo", tipo)
            };

            return await _rpteMensualContext.ReporteServiciosMensual.FromSqlRaw(sqlQuery, parametros).ToListAsync();
        }

        public async Task<DetalleReporteServicioMensualVista> ObtenerResumenParaReporteAereoPorOpcionAsync(int Anio, int Mes, string tipo)
        {
            string sqlQuery = @"SELECT count(a.id_nota) total,
                                       concat('', cast(sum(DATEDIFF(second,0,cast(a.tiempoVuelo as datetime))) / 3600 AS varchar) + ':' +
                                                  RIGHT('0' + CAST((sum(DATEDIFF(second,0,cast(a.tiempoVuelo as datetime))) / 60) % 60 AS VARCHAR),2) + ':' +
                                                  RIGHT('0' + CAST(sum(DATEDIFF(second,0,cast(a.tiempoVuelo as datetime))) % 60 AS VARCHAR),2)) dato1,
                                       sum(a.comandantesInstalacionSSF) dato2, sum(a.personalMilitarSEDENAOficial) dato3,
                                       sum(a.personalMilitarSEDENATropa) dato4, sum(a.linieros) dato5, 
                                       sum(a.comandantesTurnoSSF) dato6, sum(a.oficialesSSF) dato7 
                                FROM ssf.tarjetaInformativa a
                                JOIN ssf.programaPatrullajes b ON a.id_programa=b.id_programa
                                JOIN ssf.rutas c ON  b.id_ruta=c.id_ruta
                                JOIN ssf.estadoPatrullaje d ON b.id_estadoPatrullaje=d.id_estadoPatrullaje
                                JOIN ssf.tipoPatrullaje e ON c.id_tipoPatrullaje=e.id_tipoPatrullaje
                                WHERE MONTH(b.fechaPatrullaje)= @pMes AND YEAR(b.fechaPatrullaje)= @pAnio 
                                AND DATEDIFF(second,0,cast(a.tiempoVuelo as datetime))>0 
                                AND e.descripcion= @pTipo";

            object[] parametros = new object[]
            {
                new SqlParameter("@pAnio", Anio),
                new SqlParameter("@pMes", Mes),
                new SqlParameter("@pTipo", tipo)
            };

            return await _rpteMensualContext.DetallesReporteServicioMensualVista.FromSqlRaw(sqlQuery, parametros).SingleAsync();
        }

        public async Task<List<ReporteServicioMensualVista>> ObtenerReporteTerrestreSedenaPorAnioAnMesAsync(int Anio, int Mes)
        {
            string sqlQuery = @"SELECT a.id_nota,a.id_programa,b.fechaPatrullaje,b.id_ruta,c.regionSSF,c.id_tipoPatrullaje,a.ultimaActualizacion,
                                       a.id_usuario,a.inicio,a.termino,a.tiempoVuelo,a.calzoAcalzo,a.observaciones,d.descripcionEstadoPatrullaje, 
                                       a.kmRecorrido,a.comandantesInstalacionSSF,a.personalMilitarSEDENAOficial,a.id_estadoTarjetaInformativa,
                                       a.personalMilitarSEDENATropa,a.linieros,a.comandantesTurnoSSF,a.oficialesSSF,a.personalNavalSEMAROficial,
                                       a.personalNavalSEMARTropa
                                      ,(SELECT STRING_AGG(CAST(g.ubicacion as nvarchar(MAX)),'-') WITHIN GROUP (ORDER BY f.posicion ASC) as ruta 
                                          FROM ssf.itinerario f
                                      	JOIN ssf.puntospatrullaje g ON f.id_punto=g.id_punto
                                      	WHERE f.id_ruta=b.id_ruta ) as itinerariorutas 
                                      ,(SELECT STRING_AGG(CAST(CONCAT('Linea',h.clave,'Estructura',g.nombre,'Incidencia',f.incidencia,'SSFPATRULLAJECR') as nvarchar(MAX)),':') 
                                                          WITHIN GROUP (ORDER BY h.clave, g.nombre ASC) as incidenciaEstructura 
                                          FROM ssf.reporteEstructuras f
                                      	JOIN ssf.estructura g ON f.id_estructura=g.id_estructura
                                      	JOIN ssf.linea h ON g.id_linea=h.id_linea
                                          WHERE f.id_nota=a.id_nota) as incidenciaenestructura
                                      ,(SELECT STRING_AGG(CAST(CONCAT('Instalacion',f.ubicacion,'Incidencia',g.incidencia,'SSFPATRULLAJECR') as nvarchar(MAX)),':') 
                                                          WITHIN GROUP (ORDER BY f.ubicacion ASC) as incidenciaInstalacion 
                                      	FROM ssf.puntosPatrullaje f
                                      	JOIN ssf.reportePunto g ON f.id_punto = g.id_punto
                                          WHERE g.id_nota=a.id_nota) as incidenciaeninstalacion 
                                      ,(SELECT STRING_AGG(CAST(CONCAT(g.matricula,'SSFPATRULLAJESCR') as nvarchar(MAX)),' ') 
                                                          WITHIN GROUP (ORDER BY g.matricula ASC) as vehiculo 
                                          FROM ssf.usovehiculo f
                                      	JOIN ssf.vehiculos g ON f.id_vehiculo=g.id_vehiculo
                                          WHERE f.id_programa=a.id_programa) as matriculas 
                                      ,(SELECT STRING_AGG(CAST(CONCAT(CAST(f.kminicio AS VARCHAR(10)), ', ',CAST(f.kmFin AS VARCHAR(10)),'SSFPATRULLAJESCR') as nvarchar(MAX)),' ') 
                                                          WITHIN GROUP (ORDER BY g.matricula ASC) as odometro 
                                          FROM ssf.usovehiculo f
                                      	JOIN ssf.vehiculos g ON f.id_vehiculo=g.id_vehiculo
                                          WHERE f.id_programa=a.id_programa) as odometros 
                                      ,(SELECT STRING_AGG(CAST(CONCAT(CAST(f.kmfin-f.kminicio AS VARCHAR(10)), 'SSFPATRULLAJESCR') as nvarchar(MAX)),' ') 
                                                          WITHIN GROUP (ORDER BY g.matricula ASC) as kmTotal 
                                          FROM ssf.usovehiculo f
                                      	JOIN ssf.vehiculos g ON f.id_vehiculo=g.id_vehiculo
                                          WHERE f.id_programa=a.id_programa) as kmrecorridos 
                                      FROM ssf.tarjetaInformativa a
                                      JOIN ssf.programaPatrullajes b ON a.id_programa=b.id_programa
                                      JOIN ssf.rutas c ON b.id_ruta=c.id_ruta
                                      JOIN ssf.estadoPatrullaje d ON b.id_estadoPatrullaje=d.id_estadoPatrullaje
                                      JOIN ssf.tipoPatrullaje e ON c.id_tipoPatrullaje=e.id_tipoPatrullaje
                                      WHERE MONTH(b.fechaPatrullaje)=@pMes and YEAR(b.fechaPatrullaje)=@pAnio 
                                      AND e.descripcion='TERRESTRE' 
                                      AND (a.personalMilitarSEDENAOficial+a.personalMilitarSEDENATropa)>0 
                                      order by b.fechaPatrullaje, c.regionSSF, a.inicio";

            object[] parametros = new object[]
            {
                new SqlParameter("@pAnio", Anio),
                new SqlParameter("@pMes", Mes)
            };

            return await _rpteMensualContext.ReporteServiciosMensual.FromSqlRaw(sqlQuery, parametros).ToListAsync();

        }

        public async Task<DetalleReporteServicioMensualVista> ObtenerResumenParaReporteTerrestreSedenaPorAnioAnMesAsync(int Anio, int Mes)
        {
            string sqlQuery = @"SELECT count(a.id_nota) total, 
                                       sum(a.kmRecorrido) dato1,sum(a.comandantesInstalacionSSF) dato2, sum(a.personalMilitarSEDENAOficial) dato3,
                                       sum(a.personalMilitarSEDENATropa) dato4, sum(a.linieros) dato5, sum(a.comandantesTurnoSSF) dato6,
                                       sum(a.oficialesSSF) dato7 
                                FROM ssf.tarjetaInformativa a
                                JOIN ssf.programaPatrullajes b ON a.id_programa=b.id_programa
                                JOIN ssf.rutas c ON b.id_ruta=c.id_ruta
                                JOIN ssf.estadoPatrullaje d ON b.id_estadoPatrullaje=d.id_estadoPatrullaje
                                JOIN ssf.tipoPatrullaje e ON c.id_tipoPatrullaje=e.id_tipoPatrullaje
                                WHERE MONTH(b.fechaPatrullaje)= @pMes and YEAR(b.fechaPatrullaje)= @pAnio 
                                AND e.descripcion='TERRESTRE' 
                                AND (a.personalMilitarSEDENAOficial + a.personalMilitarSEDENATropa) > 0";

            object[] parametros = new object[]
            {
                new SqlParameter("@pAnio", Anio),
                new SqlParameter("@pMes", Mes)
            };

            return await _rpteMensualContext.DetallesReporteServicioMensualVista.FromSqlRaw(sqlQuery, parametros).SingleAsync();
        }

        public async Task<List<ReporteServicioMensualVista>> ObtenerReporteTerrestreRegionPorAnioAnMesAsync(int Anio, int Mes)
        {
            string sqlQuery = @"SELECT a.id_nota,a.id_programa,b.fechaPatrullaje,b.id_ruta,c.regionSSF,c.id_tipoPatrullaje,a.ultimaActualizacion,
                                       a.id_usuario,a.inicio,a.termino,a.tiempoVuelo,a.calzoAcalzo,a.observaciones,d.descripcionEstadoPatrullaje, 
                                       a.kmRecorrido,a.comandantesInstalacionSSF,a.personalMilitarSEDENAOficial,a.id_estadoTarjetaInformativa,
                                       a.personalMilitarSEDENATropa,a.linieros,a.comandantesTurnoSSF,a.oficialesSSF,a.personalNavalSEMAROficial,
                                       a.personalNavalSEMARTropa
                                      ,(SELECT STRING_AGG(CAST(g.ubicacion as nvarchar(MAX)),'-') WITHIN GROUP (ORDER BY f.posicion ASC) as ruta 
                                          FROM ssf.itinerario f
                                      	JOIN ssf.puntospatrullaje g ON f.id_punto=g.id_punto
                                      	WHERE f.id_ruta=b.id_ruta ) as itinerariorutas 
                                      ,(SELECT STRING_AGG(CAST(CONCAT('Linea',h.clave,'Estructura',g.nombre,'Incidencia',f.incidencia,'SSFPATRULLAJECR') as nvarchar(MAX)),':') 
                                                          WITHIN GROUP (ORDER BY h.clave, g.nombre ASC) as incidenciaEstructura 
                                          FROM ssf.reporteEstructuras f
                                      	JOIN ssf.estructura g ON f.id_estructura=g.id_estructura
                                      	JOIN ssf.linea h ON g.id_linea=h.id_linea
                                          WHERE f.id_nota=a.id_nota) as incidenciaenestructura
                                      ,(SELECT STRING_AGG(CAST(CONCAT('Instalacion',f.ubicacion,'Incidencia',g.incidencia,'SSFPATRULLAJECR') as nvarchar(MAX)),':') 
                                                          WITHIN GROUP (ORDER BY f.ubicacion ASC) as incidenciaInstalacion 
                                      	FROM ssf.puntosPatrullaje f
                                      	JOIN ssf.reportePunto g ON f.id_punto = g.id_punto
                                          WHERE g.id_nota=a.id_nota) as incidenciaeninstalacion 
                                      ,(SELECT STRING_AGG(CAST(CONCAT(g.matricula,'SSFPATRULLAJESCR') as nvarchar(MAX)),' ') 
                                                          WITHIN GROUP (ORDER BY g.matricula ASC) as vehiculo 
                                          FROM ssf.usovehiculo f
                                      	JOIN ssf.vehiculos g ON f.id_vehiculo=g.id_vehiculo
                                          WHERE f.id_programa=a.id_programa) as matriculas 
                                      ,(SELECT STRING_AGG(CAST(CONCAT(CAST(f.kminicio AS VARCHAR(10)), ', ',CAST(f.kmFin AS VARCHAR(10)),'SSFPATRULLAJESCR') as nvarchar(MAX)),' ') 
                                                          WITHIN GROUP (ORDER BY g.matricula ASC) as odometro 
                                          FROM ssf.usovehiculo f
                                      	JOIN ssf.vehiculos g ON f.id_vehiculo=g.id_vehiculo
                                          WHERE f.id_programa=a.id_programa) as odometros 
                                      ,(SELECT STRING_AGG(CAST(CONCAT(CAST(f.kmfin-f.kminicio AS VARCHAR(10)), 'SSFPATRULLAJESCR') as nvarchar(MAX)),' ') 
                                                          WITHIN GROUP (ORDER BY g.matricula ASC) as kmTotal 
                                          FROM ssf.usovehiculo f
                                      	JOIN ssf.vehiculos g ON f.id_vehiculo=g.id_vehiculo
                                          WHERE f.id_programa=a.id_programa) as kmrecorridos 
                              FROM ssf.tarjetaInformativa a
                              JOIN ssf.programaPatrullajes b ON a.id_programa=b.id_programa
                              JOIN ssf.rutas c ON b.id_ruta=c.id_ruta
                              JOIN ssf.estadoPatrullaje d ON b.id_estadoPatrullaje=d.id_estadoPatrullaje
                              JOIN ssf.tipoPatrullaje e ON c.id_tipoPatrullaje=e.id_tipoPatrullaje
                              WHERE MONTH(b.fechaPatrullaje)= @pMes and YEAR(b.fechaPatrullaje)= @pAnio 
                              AND e.descripcion='TERRESTRE' 
                              AND (a.personalMilitarSEDENAOficial+a.personalMilitarSEDENATropa) = 0 
                              AND (a.comandantesInstalacionSSF+a.comandantesTurnoSSF+a.oficialesSSF+a.linieros) > 0 
                              order by b.fechaPatrullaje, c.regionSSF, a.inicio";

            object[] parametros = new object[]
            {
                new SqlParameter("@pAnio", Anio),
                new SqlParameter("@pMes", Mes)
            };

            return await _rpteMensualContext.ReporteServiciosMensual.FromSqlRaw(sqlQuery, parametros).ToListAsync();

        }

        public async Task<DetalleReporteServicioMensualVista> ObtenerResumenParaReporteTerrestreRegionPorAnioAnMesAsync(int Anio, int Mes)
        {
            string sqlQuery = @"SELECT count(a.id_nota) total, 
                                       sum(a.kmRecorrido) dato1, sum(a.comandantesInstalacionSSF) dato2,sum(a.personalMilitarSEDENAOficial) dato3,
                                       sum(a.personalMilitarSEDENATropa) dato4, sum(a.linieros) dato5, sum(a.comandantesTurnoSSF) dato6,
                                       sum(a.oficialesSSF) dato7 
                                FROM ssf.tarjetaInformativa a
                                JOIN ssf.programaPatrullajes b ON a.id_programa=b.id_programa
                                JOIN ssf.rutas c ON b.id_ruta=c.id_ruta
                                JOIN ssf.estadoPatrullaje d ON b.id_estadoPatrullaje=d.id_estadoPatrullaje
                                JOIN ssf.tipoPatrullaje e ON c.id_tipoPatrullaje=e.id_tipoPatrullaje
                                WHERE MONTH(b.fechaPatrullaje)= @pMes and YEAR(b.fechaPatrullaje)= @pAnio 
                                AND e.descripcion='TERRESTRE' 
                                AND (a.personalMilitarSEDENAOficial + a.personalMilitarSEDENATropa) = 0 
                                AND (a.comandantesInstalacionSSF + a.comandantesTurnoSSF + a.oficialesSSF + a.linieros) > 0";

            object[] parametros = new object[]
            {
                new SqlParameter("@pAnio", Anio),
                new SqlParameter("@pMes", Mes)
            };

            return await _rpteMensualContext.DetallesReporteServicioMensualVista.FromSqlRaw(sqlQuery, parametros).SingleAsync();
        }
    }
}
