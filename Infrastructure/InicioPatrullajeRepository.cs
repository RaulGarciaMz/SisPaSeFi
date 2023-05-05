using Domain.Entities;
using Domain.Entities.Vistas;
using Domain.Ports.Driven.Repositories;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using SqlServerAdapter.Data;

namespace SqlServerAdapter
{
    public class InicioPatrullajeRepository : IInicioPatrullajeRepo
    {
     
        protected readonly InicioPatrullajeContext _inicioPatrullajeContext;

        public InicioPatrullajeRepository(InicioPatrullajeContext inicioContext)
        {
            _inicioPatrullajeContext = inicioContext ?? throw new ArgumentNullException(nameof(inicioContext));
        }

        public async Task<List<InicioPatrullajeProgramaVista>> ObtenerProgramaPorRutaAndFechaAsync(int idRuta, string fecha)
        {
            string sqlQuery = @"SELECT a.id_programa, a.riesgopatrullaje, b.regionSSF 
                                FROM ssf.programapatrullajes a
                                JOIN ssf.rutas b ON a.id_ruta=b.id_ruta
                                WHERE a.id_ruta= @pIdRuta
                                AND a.fechapatrullaje=@pFechaPatrullaje
                                AND a.id_estadopatrullaje<(SELECT id_estadopatrullaje FROM ssf.estadopatrullaje WHERE descripcionestadopatrullaje='Concluido')";

            object[] parametros = new object[]
            {
                new SqlParameter("@pIdRuta", idRuta),
                new SqlParameter("@pFechaPatrullaje", fecha),
            };

            return await _inicioPatrullajeContext.IniciosPatrullajesVista.FromSqlRaw(sqlQuery, parametros).ToListAsync();
        }

        public async Task<List<InicioPatrullajePuntosVista>> ObtenerPuntosEnRutaDelItinerarioAsync(int idRuta)
        {
            string sqlQuery = @"SELECT b.id_punto, b.ubicacion, b.coordenadas, b.id_municipio, b.id_nivelriesgo
                                FROM ssf.itinerario a
                                JOIN ssf.puntospatrullaje b ON a.id_punto= b.id_punto
                                WHERE b.esinstalacion= 1
                                AND a.id_ruta = @pIdRuta";

            object[] parametros = new object[]
            {
                new SqlParameter("@pIdRuta", idRuta)
            };

            return await _inicioPatrullajeContext.IniciosPatrullajePuntosVista.FromSqlRaw(sqlQuery, parametros).ToListAsync();
        }

        public async Task<List<TarjetaInformativa>> ObtenerTarjetaInformativaPorProgramaAsync(int idPrograma)
        {
            return await _inicioPatrullajeContext.TarjetasInformativas.Where(x => x.IdPrograma == idPrograma).ToListAsync();
        }

        public async Task<List<int>> ObtenerUsoVehiculoPorProgramaAndIdVehiculoAsync(int idPrograma, int idVehiculo)
        {
            return await _inicioPatrullajeContext.UsosVehiculos.Where(x => x.IdPrograma == idPrograma && x.IdVehiculo == idVehiculo).Select(x => x.IdUsoVehiculo).ToListAsync();
        }

        public async Task AgregaProgramaPatrullajeAsync(int idRuta, DateTime fechaPatrullaje, int idUsuario, int idPuntoResponsable, int idRutaOriginal)
        {
            var p = new ProgramaPatrullaje() 
            {
                IdRuta = idRuta,
                IdUsuario = idUsuario,
                IdPuntoResponsable = idPuntoResponsable,
                IdPropuestaPatrullaje = 0,
                RiesgoPatrullaje = 1,
                IdRutaOriginal = idRutaOriginal,
                FechaPatrullaje = fechaPatrullaje,
                UltimaActualizacion = DateTime.UtcNow
            };

            _inicioPatrullajeContext.ProgramasPatrullaje.Add(p);

            await _inicioPatrullajeContext.SaveChangesAsync();
        }

        public async Task ActualizaProgramaPatrullajeAsync(int idPrograma, TimeSpan inicio, int idUsuario, int riesgo)
        {
            var p = await _inicioPatrullajeContext.ProgramasPatrullaje.Where(x => x.IdPrograma == idPrograma).SingleOrDefaultAsync();

            if (p != null)
            {
                p.UltimaActualizacion = DateTime.UtcNow;
                p.Inicio = inicio;
                p.IdEstadoPatrullaje = 2;
                p.IdUsuario = idUsuario;
                p.RiesgoPatrullaje = riesgo;

                _inicioPatrullajeContext.ProgramasPatrullaje.Update(p);
                await _inicioPatrullajeContext.SaveChangesAsync();
            }
        }

        public async Task AgregaTarjetaInformativaAsync(int idPrograma, int idUsuario, TimeSpan inicio, int comandantesInstalacionSsf, int sedenaOficial, DateTime fechaPatrullaje, int tropaSdn, int linieros ,int comandantesTurnos, int oficialSsf, DateTime fechaTermino)
        {
            var timeSpanDefault = TimeSpan.Parse("00:00:00");

            var p = new TarjetaInformativa()
            {
                IdPrograma = idPrograma,
                IdUsuario = idUsuario,
                Inicio = inicio,
                Termino = timeSpanDefault,
                TiempoVuelo = timeSpanDefault,
                CalzoAcalzo = timeSpanDefault,
                UltimaActualizacion = DateTime.UtcNow,
                Observaciones = "",
                ComandantesInstalacionSsf = comandantesInstalacionSsf,
                PersonalMilitarSedenaoficial = sedenaOficial,
                KmRecorrido = 0,
                FechaPatrullaje = fechaPatrullaje,
                PersonalMilitarSedenatropa = tropaSdn,
                Linieros = linieros,
                ComandantesTurnoSsf = comandantesTurnos,
                OficialesSsf = oficialSsf,
                PersonalNavalSemaroficial = 0,
                PersonalNavalSemartropa = 0,
                FechaTermino = fechaTermino
            };

            _inicioPatrullajeContext.TarjetasInformativas.Add(p);

            await _inicioPatrullajeContext.SaveChangesAsync();
        }

        public async Task ActualizaTarjetaInformativaAsync(int idPrograma, int idUsuario, TimeSpan inicio, int comandantesInstalacionSsf, int sedenaOficial, DateTime fechaPatrullaje, int tropaSdn, int linieros, int comandantesTurnos, int oficialSsf, DateTime fechaTermino)
        {
            var p = await _inicioPatrullajeContext.TarjetasInformativas.Where(x => x.IdPrograma == idPrograma).SingleOrDefaultAsync();

            if (p != null)
            {
                p.UltimaActualizacion = DateTime.UtcNow;
                p.IdUsuario = idUsuario;
                p.ComandantesInstalacionSsf = comandantesInstalacionSsf;
                p.PersonalMilitarSedenaoficial = sedenaOficial;
                p.PersonalMilitarSedenatropa = tropaSdn;
                p.Linieros = linieros;
                p.ComandantesTurnoSsf = comandantesTurnos;
                p.OficialesSsf = oficialSsf;
                p.Inicio = inicio;

                _inicioPatrullajeContext.TarjetasInformativas.Update(p);
                await _inicioPatrullajeContext.SaveChangesAsync();
            }
        }

        public async Task AgregaUsoVehiculoAsync(int idPrograma, int idVehiculo,  int kminicio, int idUsuarioVehiculo, string estadoVehiculo)
        {
            var p = new UsoVehiculo()
            {
                IdPrograma = idPrograma,
                IdVehiculo = idVehiculo,
                KmInicio = kminicio,
                IdUsuarioVehiculo = idUsuarioVehiculo,
                EstadoVehiculo = estadoVehiculo
            };

            _inicioPatrullajeContext.UsosVehiculos.Add(p);

            await _inicioPatrullajeContext.SaveChangesAsync();
        }

        public async Task ActualizaUsoVehiculoAsync(int idPrograma, int idVehiculo, int kminicio, int idUsuarioVehiculo, string estadoVehiculo)
        {
            var p = await _inicioPatrullajeContext.UsosVehiculos.Where(x => x.IdPrograma == idPrograma && x.IdVehiculo == idVehiculo).SingleOrDefaultAsync();

            if (p != null)
            {
                p.KmInicio = kminicio;
                p.IdUsuarioVehiculo = idUsuarioVehiculo;
                p.EstadoVehiculo = estadoVehiculo;
            }

            _inicioPatrullajeContext.UsosVehiculos.Update(p);

            await _inicioPatrullajeContext.SaveChangesAsync();
        }
    }
}
