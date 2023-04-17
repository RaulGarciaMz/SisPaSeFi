using Domain.Entities.Vistas;
using Domain.Ports.Driven;
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
    public class EstadisticasRepository : IEstadisticasQuery
    {
        public EstadisticasRepository()
        {
            
        }
        protected readonly EstadisticasContext _estadisticasContext;

        public EstadisticasRepository(EstadisticasContext estadisticaContext)
        {
            _estadisticasContext = estadisticaContext ?? throw new ArgumentNullException(nameof(estadisticaContext));
        }

        public async Task<List<EstadisticaSistemaVista>> ObtenerEstadisticasDeSistemaAsync()
        {
            string sqlQuery = @"SELECT 'PuntosPatrullaje' concepto, COUNT(id_punto) total FROM ssf.puntospatrullaje
                                UNION
                                SELECT 'Instalaciones' concepto, COUNT(id_punto) total FROM ssf.puntospatrullaje WHERE esinstalacion=1
                                UNION
                                SELECT 'Lineas' concepto, COUNT(id_linea) total FROM ssf.linea
                                UNION
                                SELECT 'Estructuras' concepto, COUNT(id_estructura) total FROM ssf.estructura
                                UNION 
                                SELECT 'Rutas' concepto, COUNT(id_ruta) total FROM ssf.rutas
                                UNION
                                SELECT 'Usuarios' concepto, COUNT(id_usuario) total FROM ssf.usuarios
                                UNION
                                SELECT 'AccesosMes' concepto, totalaccesos total FROM ssf.accesos WHERE MONTH(fecha)= MONTH(GETDATE()) AND YEAR(fecha)= YEAR(GETDATE())
                                UNION
                                SELECT 'AccesosAnio' concepto, ISNULL(sum(totalaccesos),0)  total FROM ssf.accesos WHERE YEAR(fecha)= YEAR(GETDATE())
                                UNION
                                SELECT 'PatrullajesEnProgreso' concepto, COUNT(a.id_programa)  total
                                FROM ssf.programapatrullajes a
                                JOIN ssf.estadopatrullaje b ON a.id_estadoPatrullaje = b.id_estadoPatrullaje
                                WHERE MONTH(fechapatrullaje)= MONTH(GETDATE()) AND YEAR(fechapatrullaje)= YEAR(GETDATE())
                                AND b.descripcionestadopatrullaje NOT IN ('Programado','Concluido')
                                UNION
                                SELECT 'PatrullajesAereosEnProgreso' concepto, COUNT(a.id_programa) total
                                FROM ssf.programapatrullajes a
                                JOIN ssf.rutas b ON a.id_ruta=b.id_ruta
                                JOIN ssf.tipopatrullaje c ON b.id_tipoPatrullaje = b.id_tipoPatrullaje 
                                JOIN  ssf.estadopatrullaje d ON a.id_estadoPatrullaje = d.id_estadoPatrullaje
                                WHERE  c.descripcion = 'AEREO'
                                AND MONTH(a.fechapatrullaje)= MONTH(GETDATE()) AND YEAR(a.fechaPatrullaje)= YEAR(GETDATE())
                                AND d.descripcionestadopatrullaje NOT IN ('Programado','Concluido')
                                UNION
                                SELECT 'PatrullajesTerrestresEnProgreso' concepto, COUNT(a.id_programa)  total
                                FROM ssf.programapatrullajes a
                                JOIN ssf.rutas b ON  a.id_ruta=b.id_ruta
                                JOIN ssf.tipopatrullaje c ON b.id_tipoPatrullaje = c.id_tipoPatrullaje
                                JOIN ssf.estadopatrullaje d ON a.id_estadoPatrullaje = d.id_estadoPatrullaje
                                WHERE c.descripcion='TERRESTRE' AND d.descripcionestadopatrullaje NOT IN ('Programado','Concluido')
                                AND MONTH(a.fechaPatrullaje)= MONTH(GETDATE()) AND YEAR(a.fechaPatrullaje)= YEAR(GETDATE())
                                UNION
                                SELECT 'PatrullajesProgramados' concepto, COUNT(a.id_programa)  total
                                FROM ssf.programapatrullajes a
                                JOIN ssf.estadopatrullaje b ON a.id_estadoPatrullaje = b.id_estadoPatrullaje
                                WHERE b.descripcionestadopatrullaje = 'Programado'
                                AND MONTH(fechapatrullaje)= MONTH(GETDATE()) AND YEAR(fechapatrullaje)= YEAR(GETDATE())
                                UNION
                                SELECT 'PatrullajesAereosProgramados' concepto, COUNT(a.id_programa)  total
                                FROM ssf.programapatrullajes a
                                JOIN ssf.rutas b ON a.id_ruta=b.id_ruta
                                JOIN ssf.tipopatrullaje c ON b.id_tipoPatrullaje = b.id_tipoPatrullaje 
                                JOIN ssf.estadopatrullaje d ON a.id_estadoPatrullaje = d.id_estadoPatrullaje
                                WHERE  c.descripcion='AEREO' AND d.descripcionestadopatrullaje = 'Programado'
                                AND MONTH(a.fechapatrullaje)= MONTH(GETDATE()) AND YEAR(a.fechaPatrullaje)= YEAR(GETDATE())
                                UNION
                                SELECT 'PatrullajesTerrestresProgramados' concepto, COUNT(id_programa)  total
                                FROM ssf.programapatrullajes a
                                JOIN ssf.rutas b ON a.id_ruta=b.id_ruta
                                JOIN ssf.tipopatrullaje c ON b.id_tipoPatrullaje = b.id_tipoPatrullaje 
                                JOIN ssf.estadopatrullaje d ON a.id_estadoPatrullaje = d.id_estadoPatrullaje
                                WHERE c.descripcion='TERRESTRE' AND d.descripcionestadopatrullaje = 'Programado'
                                AND MONTH(a.fechaPatrullaje)= MONTH(GETDATE()) AND YEAR(a.fechaPatrullaje)= YEAR(GETDATE()) ";

            object[] parametros = new object[]
            {
            };

            return await _estadisticasContext.EstadisticasSistemaVista.FromSqlRaw(sqlQuery, parametros).ToListAsync();
        }

    }
}


