using Domain.Entities;
using Microsoft.Data.SqlClient;
using SqlServerAdapter.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities.Vistas;
using Domain.Ports.Driven.Repositories;
using System.Runtime.CompilerServices;

namespace SqlServerAdapter
{
    public class TarjetaInformativaRepository : ITarjetaInformativaRepo
    {
        protected readonly TarjetaInformativaContext _tarjetaContext;

        public TarjetaInformativaRepository(TarjetaInformativaContext tarjetaContext)
        {
            _tarjetaContext = tarjetaContext;
        }

        public async Task AgregaAsync(TarjetaInformativa tarjeta, int idEstadoPatrullaje , int usuarioId) 
        { 
            tarjeta.IdUsuario= usuarioId;
            _tarjetaContext.TarjetasInformativas.Add(tarjeta);

            var programa = await ObtenerProgramaPatrullajePorIdAsync(tarjeta.IdPrograma);

            if (programa != null)
            {
                programa.IdUsuario = usuarioId;
                programa.UltimaActualizacion = tarjeta.UltimaActualizacion;
                programa.Termino = tarjeta.Termino;
                programa.IdEstadoPatrullaje = idEstadoPatrullaje;
                programa.Observaciones = tarjeta.Observaciones;

                _tarjetaContext.Programas.Update(programa);
            }

            await _tarjetaContext.SaveChangesAsync();
        }

        public async Task UpdateAsync(TarjetaInformativa tarjeta, int idEstadoPatrullaje, int usuarioId) 
        {
            tarjeta.IdUsuario= usuarioId;
            _tarjetaContext.TarjetasInformativas.Update(tarjeta);

            var programa = await ObtenerProgramaPatrullajePorIdAsync(tarjeta.IdPrograma);

            if (programa != null ) 
            {
                programa.IdUsuario= usuarioId;
                programa.IdEstadoPatrullaje = idEstadoPatrullaje;
                programa.Termino = tarjeta.Termino;
                programa.Observaciones = tarjeta.Observaciones;
                programa.UltimaActualizacion = tarjeta.UltimaActualizacion;

                _tarjetaContext.Programas.Update(programa);
            }           
            
            await _tarjetaContext.SaveChangesAsync();
        }

        public async Task< List<TarjetaInformativaVista>> ObtenerPorAnioMesAsync(string tipo, string region, int anio, int mes) 
        {
            string sqlQuery = @"SELECT a.id_nota, a.id_programa, b.fechapatrullaje, b.id_ruta, c.regionssf, c.id_tipopatrullaje, a.ultimaactualizacion,
                                       a.id_usuario, a.inicio, a.termino, a.tiempovuelo, a.calzoacalzo, a.observaciones, d.id_estadopatrullaje,
                                       d.descripcionestadopatrullaje, a.kmrecorrido, a.comandantesinstalacionssf, a.personalmilitarsedenaoficial,
                                       a.id_estadotarjetainformativa, a.personalmilitarsedenatropa, a.linieros, a.comandantesturnossf, a.oficialesssf,
                                       a.personalnavalsemaroficial,a.personalnavalsemartropa,
                                       COALESCE((SELECT STRING_AGG(CAST(g.ubicacion as nvarchar(MAX)),'-') WITHIN GROUP (ORDER BY f.posicion ASC) 
                                                FROM ssf.itinerario f join ssf.puntospatrullaje g on f.id_punto=g.id_punto
                                                WHERE f.id_ruta=b.id_ruta),'') as itinerario,
                                       COALESCE((SELECT STRING_AGG(CAST(CONCAT('Linea',h.clave,'Estructura',g.nombre,'Incidencia',f.incidencia,'SSFPATRULLAJECR') as nvarchar(MAX)),':') 
                                                        WITHIN GROUP (ORDER BY h.clave, g.nombre ASC) 
                                                FROM ssf.reporteestructuras f
                                               JOIN ssf.estructura g ON f.id_estructura=g.id_estructura
                                               JOIN ssf.linea h ON g.id_linea=h.id_linea
                                                WHERE f.id_nota=a.id_nota),'') as incidenciaenestructura,
                                       COALESCE((SELECT STRING_AGG(CAST(CONCAT('Instalacion',f.ubicacion,'Incidencia',g.incidencia,'SSFPATRULLAJECR') as nvarchar(MAX)),':') 
                                                        WITHIN GROUP (ORDER BY f.ubicacion ASC) 
                                                FROM ssf.puntospatrullaje f
                                               JOIN ssf.reportepunto g ON f.id_punto = g.id_punto
                                                WHERE g.id_nota=a.id_nota),'') as incidenciaeninstalacion,
                                       COALESCE((SELECT STRING_AGG(CAST(CONCAT(g.matricula,'SSFPATRULLAJECR') as nvarchar(MAX)),' ') WITHIN GROUP (ORDER BY g.matricula ASC) 
                                                FROM ssf.usovehiculo f
                                               JOIN ssf.vehiculos g ON f.id_vehiculo=g.id_vehiculo
                                                WHERE f.id_programa=a.id_programa),'') as matriculas,
                                       COALESCE((SELECT STRING_AGG(CAST(CONCAT(f.kminicio,', ',f.kmfin,'SSFPATRULLAJECR') as nvarchar(MAX)),' ') WITHIN GROUP (ORDER BY g.matricula ASC) 
                                                FROM ssf.usovehiculo f
                                               JOIN ssf.vehiculos g ON f.id_vehiculo=g.id_vehiculo
                                                WHERE f.id_programa=a.id_programa),'') as odometros,
                                       COALESCE((SELECT STRING_AGG(CAST(CONCAT(f.kmfin-f.kminicio,'SSFPATRULLAJECR') as nvarchar(MAX)),' ') WITHIN GROUP (ORDER BY g.matricula ASC) 
                                                FROM ssf.usovehiculo f
                                               JOIN ssf.vehiculos g ON f.id_vehiculo=g.id_vehiculo
                                                WHERE f.id_programa=a.id_programa),'') as kmrecorridos
                                FROM ssf.tarjetainformativa a
                                JOIN ssf.programapatrullajes b ON a.id_programa=b.id_programa
                                JOIN ssf.rutas c ON b.id_ruta=c.id_ruta 
                                JOIN ssf.estadopatrullaje d ON b.id_estadopatrullaje=d.id_estadopatrullaje
                                JOIN ssf.tipopatrullaje e ON c.id_tipopatrullaje=e.id_tipopatrullaje
                                WHERE e.descripcion=@pTipo AND AND c.regionssf= @pRegion
                                AND MONTH(b.fechapatrullaje)=@pMes AND YEAR(b.fechapatrullaje)=@pAnio 
                                ORDER BY b.fechapatrullaje, a.inicio";

            object[] parametros = new object[]
            {
                new SqlParameter("@pTipo", tipo),
                new SqlParameter("@pRegion", region),
                new SqlParameter("@pAnio", anio),
                new SqlParameter("@pMes", mes)
             };

            return await _tarjetaContext.TarjetasInformativasVista.FromSqlRaw(sqlQuery, parametros).ToListAsync();
        }

        public async Task<int> ObtenerIdUsuarioRegistradoAsync(string usuario)
        {
            var user = await _tarjetaContext.Usuarios.Where(x => x.UsuarioNom == usuario).Select(x => x.IdUsuario).ToListAsync();

            if (user.Count == 0)
            {
                return -1;
            }
            else
            {
                return user[0];
            }
        }

        public async Task<int> ObtenerIdUsuarioConfiguradorAsync(string usuario)
        {
            var user = await _tarjetaContext.Usuarios.Where(x => x.UsuarioNom == usuario && x.Configurador == 1).Select(x => x.IdUsuario).ToListAsync();

            if (user.Count == 0)
            {
                return -1;
            }
            else
            {
                return user[0];
            }
        }

        public async Task<TarjetaInformativa?> ObtenerTarjetaPorIdNotaAsync(int idNota) 
        { 
            return await _tarjetaContext.TarjetasInformativas.Where(x => x.IdNota== idNota).FirstOrDefaultAsync();
        }

        public async Task<int> NumeroDeTarjetasPorProgamaAsync(int idPrograma)
        {
            return await _tarjetaContext.TarjetasInformativas.Where(x => x.IdPrograma == idPrograma).CountAsync();
        }

        public async Task<bool> SaveChangesAsync()
        {
            return (await _tarjetaContext.SaveChangesAsync() >= 0);
        }

        private async Task<ProgramaPatrullaje?> ObtenerProgramaPatrullajePorIdAsync(int id)
        {
            return await _tarjetaContext.Programas.Where(x => x.IdPrograma == id).FirstOrDefaultAsync();
        }

    }
}
