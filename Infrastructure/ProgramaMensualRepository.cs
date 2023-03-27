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
    public class ProgramaMensualRepository : IProgramaMensualRepo
    {
        protected readonly ProgramaMensualContext _programaContext;

        public ProgramaMensualRepository(ProgramaMensualContext programaContext)
        {
            _programaContext = programaContext ?? throw new ArgumentNullException(nameof(programaContext));
        }

        public async Task<List<ResponsableRegionesVista>> ObtenerRegionesMilitaresProgramaMensualAsync(int anio, int mes, string region, string tipo)
        {
            string sqlQuery = @"SELECT DISTINCT e.nombre municipio, f.nombre estado, g.nombre, g.apellido1, g.apellido2, 
                                       COALESCE((
                                       SELECT STRING_AGG(CAST(u.regionmilitarsdn as nvarchar(MAX)), ',') WITHIN GROUP(ORDER BY u.regionmilitarsdn ASC)
                                       FROM (
                                       		select distinct CAST(i.regionmilitarsdn as int) regionmilitarsdn
                                       		FROM ssf.programapatrullajes h
                                       		JOIN ssf.rutas i ON h.id_ruta=i.id_ruta
                                       		JOIN ssf.tipopatrullaje j ON i.id_tipoPatrullaje = j.id_tipoPatrullaje
                                       		WHERE  MONTH(h.fechapatrullaje) = @pMes AND YEAR(h.fechapatrullaje) = @pAnio
                                       		AND j.descripcion = @pTipoPatrullaje
                                       		and i.regionSSF = @pRegion
                                       	 ) u
                                       ), '') regionesmilitares
                                FROM ssf.programapatrullajes a
                                JOIN ssf.rutas b ON a.id_ruta = b.id_ruta
                                JOIN ssf.comandanciasregionales c ON  b.regionSSF = c.numero
                                JOIN ssf.puntospatrullaje d ON c.id_punto = d.id_punto
                                JOIN ssf.municipios e ON d.id_municipio=e.id_municipio
                                JOIN ssf.estadospais f ON e.id_estado=f.id_estado
                                JOIN ssf.usuarios g ON c.id_usuario=g.id_usuario
                                JOIN ssf.tipopatrullaje h ON b.id_tipoPatrullaje = h.id_tipoPatrullaje
                                WHERE  MONTH(a.fechaPatrullaje) = @pMes AND YEAR(a.fechapatrullaje) = @pAnio
                                AND h.descripcion = @pTipoPatrullaje
                                AND b.regionssf = @pRegion";

            object[] parametros = new object[]
            {
                new SqlParameter("@pAnio", anio),
                new SqlParameter("@pMes", mes),
                new SqlParameter("@pRegion", region),
                new SqlParameter("@pTipoPatrullaje", tipo)
            };

            return await _programaContext.RegionesMilitares.FromSqlRaw(sqlQuery, parametros).ToListAsync();
        }

        public async Task<List<ProgramaItinerarioVista>> ObtenerProgramaMensualAsync(int anio, int mes, string region, string tipo)
        {
            string sqlQuery = @"SELECT distinct a.id_ruta, b.clave, b.regionmilitarsdn, b.regionssf, b.zonamilitarsdn
                                       ,COALESCE((SELECT STRING_AGG(CAST(CONCAT(g.ubicacion,' (',REPLACE(g.coordenadas,',',''),')')  as nvarchar(MAX)),' - ') WITHIN GROUP (ORDER BY f.posicion ASC) 
                                          FROM ssf.itinerario f
                                          JOIN ssf.puntospatrullaje g ON f.id_punto=g.id_punto
                                          WHERE f.id_ruta=a.id_ruta ),'') as itinerarioruta
                                       ,COALESCE((SELECT  STRING_AGG(u.dia,  ', ')  WITHIN GROUP (ORDER BY u.dia ASC) 
                                                  FROM (SELECT DISTINCT DAY(h.fechapatrullaje) dia
                                                        FROM ssf.programapatrullajes h 
                                                        WHERE h.id_ruta=a.id_ruta
                                       	 	         AND MONTH(h.fechapatrullaje) = @pMes 
                                       				 AND YEAR(h.fechapatrullaje) = @pAnio
                                                 )u
                                           ),'') as fechas
                                       ,b.observaciones AS observacionesruta
                                       ,(SELECT MIN(c.fechapatrullaje) 
                                         FROM ssf.programapatrullajes c 
                                         WHERE c.id_ruta=a.id_ruta
                                         AND MONTH(c.fechapatrullaje) =  @pMes 
                                         AND YEAR(c.fechapatrullaje) = @pAnio 
                                          ) AS fecha
                                       ,COALESCE((SELECT STRING_AGG(CAST(CONCAT(f.posicion,'[!:!]',g.ubicacion,'[!:!]',g.coordenadas) as nvarchar(MAX)), '¦') WITHIN GROUP  (ORDER BY f.posicion ASC) 
                                           FROM ssf.itinerario f
                                       	JOIN ssf.puntospatrullaje g ON f.id_punto=g.id_punto
                                           WHERE f.id_ruta=b.id_ruta  ),'') as itinerariorutapatrullaje
                                FROM ssf.programapatrullajes a
                                JOIN ssf.rutas b ON a.id_ruta=b.id_ruta
                                JOIN ssf.tipopatrullaje c ON b.id_tipoPatrullaje = c.id_tipoPatrullaje
                                WHERE MONTH(a.fechapatrullaje) = @pMes 
                                AND YEAR(a.fechapatrullaje) = @pAnio 
                                AND c.descripcion = @pTipoPatrullaje
                                AND b.regionssf = @pRegion
                                ORDER BY fecha";

            object[] parametros = new object[]
            {
                new SqlParameter("@pAnio", anio),
                new SqlParameter("@pMes", mes),
                new SqlParameter("@pRegion", region),
                new SqlParameter("@pTipoPatrullaje", tipo)
            };

            return await _programaContext.ProgramasItinerarios.FromSqlRaw(sqlQuery, parametros).ToListAsync();
        }
    }
}
