using Domain.DTOs;
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

        public async Task AgregaInicioPatrullajeTransaccionalAsync(InicioPatrullajeDto a, int idUsuario, List<InicioPatrullajeProgramaVista> programas)
        {
            using (var transaction = _inicioPatrullajeContext.Database.BeginTransaction())
            {
                try
                {
                    switch (programas.Count)
                    {
                        case 0:
                            await CreaProgramaPatrullajeEnMemoriaAsync(a, idUsuario);
                            break;
                        case > 0:
                            await ActualizaProgramaPatrullajeConTarjetaInformativaEnMemoriaAsync(programas[0].id_programa, programas[0].riesgopatrullaje, idUsuario, a);
                            break;
                    }

                    await _inicioPatrullajeContext.SaveChangesAsync();

                    if (a.objInicioPatrullajeVehiculo != null && a.objInicioPatrullajeVehiculo.Count > 0)
                    {
                        programas = await ObtenerProgramaPorRutaAndFechaAsync(a.IdRuta, a.FechaPatrullaje);
                        await CreaOrActualizaUsosVehiculoEnMemoria(a.objInicioPatrullajeVehiculo, programas[0].id_programa, idUsuario);
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

        private async Task<List<InicioPatrullajePuntosVista>> ObtenerPuntosEnRutaDelItinerarioAsync(int idRuta)
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

        private async Task<List<TarjetaInformativa>> ObtenerTarjetaInformativaPorProgramaAsync(int idPrograma)
        {
            return await _inicioPatrullajeContext.TarjetasInformativas.Where(x => x.IdPrograma == idPrograma).ToListAsync();
        }

        private async Task<List<int>> ObtenerUsoVehiculoPorProgramaAndIdVehiculoAsync(int idPrograma, int idVehiculo)
        {
            return await _inicioPatrullajeContext.UsosVehiculos.Where(x => x.IdPrograma == idPrograma && x.IdVehiculo == idVehiculo).Select(x => x.IdUsoVehiculo).ToListAsync();
        }

        private void AgregaProgramaPatrullajeEnMemoria(int idRuta, DateTime fechaPatrullaje, int idUsuario, int idPuntoResponsable, int idRutaOriginal)
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
                UltimaActualizacion = DateTime.UtcNow,
                IdApoyoPatrullaje = 1,
                Observaciones = "",
                IdUsuarioResponsablePatrullaje =0,
                IdEstadoPatrullaje=0
            };

            _inicioPatrullajeContext.ProgramasPatrullaje.Add(p);
        }

        private void ActualizaProgramaPatrullajeEnMemoria(int idPrograma, TimeSpan inicio, int idUsuario, int riesgo)
        {
            var p = _inicioPatrullajeContext.ProgramasPatrullaje.Where(x => x.IdPrograma == idPrograma).SingleOrDefault();

            if (p != null)
            {
                p.UltimaActualizacion = DateTime.UtcNow;
                p.Inicio = inicio;
                p.IdEstadoPatrullaje = 2;
                p.IdUsuario = idUsuario;
                p.RiesgoPatrullaje = riesgo;

                _inicioPatrullajeContext.ProgramasPatrullaje.Update(p);
            }
        }

        private void AgregaTarjetaInformativaEnMemoria(int idPrograma, int idUsuario, TimeSpan inicio, int comandantesInstalacionSsf, int sedenaOficial, DateTime fechaPatrullaje, int tropaSdn, int linieros, int comandantesTurnos, int oficialSsf, DateTime fechaTermino)
        {
            var timeSpanDefault = new TimeSpan(0, 0, 0);

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
                FechaTermino = fechaTermino,
                //Valores no nulos
                Lineaestructurainstalacion = "",
                Responsablevuelo = "",
                Fuerzareaccion =0,
                Idresultadopatrullaje = 0,
                IdEstadoTarjetaInformativa = 1,
            };

            _inicioPatrullajeContext.TarjetasInformativas.Add(p);
        }

        private async Task ActualizaTarjetaInformativaEnMemoria(int idPrograma, int idUsuario, TimeSpan inicio, int comandantesInstalacionSsf, int sedenaOficial, DateTime fechaPatrullaje, int tropaSdn, int linieros, int comandantesTurnos, int oficialSsf, DateTime fechaTermino)
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
            }
        }

        private async Task CreaOrActualizaUsosVehiculoEnMemoria(List<InicioPatrullajeVehiculoDto> usosVehiculo, int idPrograma, int idUsuario)
        {
            foreach (var v in usosVehiculo)
            {
                var vehiculo = await ObtenerUsoVehiculoPorProgramaAndIdVehiculoAsync(idPrograma, v.IdVehiculo);

                if (vehiculo != null)
                {
                    switch (vehiculo.Count)
                    {
                        case 0:
                            var uv = new UsoVehiculo()
                            {
                                IdPrograma = idPrograma,
                                IdUsuarioVehiculo = idUsuario,
                                IdVehiculo = v.IdVehiculo,
                                KmInicio = v.KmInicio,
                                EstadoVehiculo = v.EstadoVehiculo,
                                //Valores por default
                                ConsumoCombustible = 0,
                                KmFin = 0
                            };

                            _inicioPatrullajeContext.UsosVehiculos.Add(uv);

                            break;

                        case > 0:
                            var usov = await _inicioPatrullajeContext.UsosVehiculos.Where(x => x.IdPrograma == idPrograma && x.IdVehiculo == v.IdVehiculo).SingleOrDefaultAsync();

                            if (usov != null)
                            {
                                usov.IdUsuarioVehiculo = idUsuario;
                                usov.KmInicio = v.KmInicio;
                                usov.EstadoVehiculo = v.EstadoVehiculo;

                                _inicioPatrullajeContext.UsosVehiculos.Update(usov);
                            }

                            break;
                    }
                }
            }



        }

        private async Task CreaProgramaPatrullajeEnMemoriaAsync(InicioPatrullajeDto a, int idUsuario)
        {
            var p = await ObtenerPuntosEnRutaDelItinerarioAsync(a.IdRuta);

            if (p != null && p.Count > 0)
            {
                var fecha = DateTime.Parse(a.FechaPatrullaje);
                AgregaProgramaPatrullajeEnMemoria(a.IdRuta, fecha, idUsuario, p[0].id_punto, a.IdRuta);
            }
        }

        private async Task ActualizaProgramaPatrullajeConTarjetaInformativaEnMemoriaAsync(int idPrograma, int riesgo, int idUsuario, InicioPatrullajeDto a)
        {
            var horaInicio = TimeSpan.Parse(a.HoraInicio);

            ActualizaProgramaPatrullajeEnMemoria(idPrograma, horaInicio, idUsuario, riesgo);

            var tarjeta = await ObtenerTarjetaInformativaPorProgramaAsync(idPrograma);

            if (tarjeta != null)
            {
                var fecha = DateTime.Parse(a.FechaPatrullaje);

                switch (tarjeta.Count)
                {
                    case 0:
                        var horaNvaTarjeta = TimeSpan.Parse(a.HoraInicio);
                        AgregaTarjetaInformativaEnMemoria(idPrograma, idUsuario, horaNvaTarjeta, a.ComandanteInstalacion, a.OficialSDN, fecha, a.TropaSDN, a.Conductores, a.ComandanteTurno, a.OficialSSF, fecha);
                        break;
                    case > 0:
                        var horaActInicio = new TimeSpan(int.Parse(a.HoraInicio), 0, 0);
                        ActualizaTarjetaInformativaEnMemoria(idPrograma, idUsuario, horaActInicio, a.ComandanteInstalacion, a.OficialSDN, fecha, a.TropaSDN, a.Conductores, a.ComandanteTurno, a.OficialSSF, fecha);
                        break;
                }
            }

        }

        /*    public async Task ActualizaProgramaPatrullajeAsync(int idPrograma, TimeSpan inicio, int idUsuario, int riesgo)
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

            public async Task ActualizaProgramaPatrullajeEnMemoria(InicioPatrullajeDto ip, int idPrograma, int idUsuario, int riesgo)
            {
                var p = await _inicioPatrullajeContext.ProgramasPatrullaje.Where(x => x.IdPrograma == idPrograma).SingleOrDefaultAsync();

                var inicio = new TimeSpan(int.Parse(ip.HoraInicio), 0, 0);

                if (p != null)
                {
                    p.UltimaActualizacion = DateTime.UtcNow;
                    p.Inicio = inicio;
                    p.IdEstadoPatrullaje = 2;
                    p.IdUsuario = idUsuario;
                    p.RiesgoPatrullaje = riesgo;

                    _inicioPatrullajeContext.ProgramasPatrullaje.Update(p);
                }
            }

            public async Task AgregaTarjetaInformativaAsync(int idPrograma, int idUsuario, TimeSpan inicio, int comandantesInstalacionSsf, int sedenaOficial, DateTime fechaPatrullaje, int tropaSdn, int linieros, int comandantesTurnos, int oficialSsf, DateTime fechaTermino)
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

            public async Task AgregaUsoVehiculoAsync(int idPrograma, int idVehiculo, int kminicio, int idUsuarioVehiculo, string estadoVehiculo)
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
    */

    }
}

