using Domain.DTOs;
using Domain.Entities;
using Domain.Ports.Driven.Repositories;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using SqlServerAdapter.Data;

namespace SqlServerAdapter
{
    public class TerminacionPatrullajeRepository : ITerminacionPatrullajeRepo
    {
        protected readonly TerminacionPatrullajeContext _terminacionContext;

        public TerminacionPatrullajeRepository(TerminacionPatrullajeContext terminacionContext)
        {
            _terminacionContext = terminacionContext ?? throw new ArgumentNullException(nameof(terminacionContext));
        }

        public async Task<int> ObtenerIdProgramaPorRutaAndFechaAsync(int idRuta, DateTime fecha)
        {
            var res = -1;
            string sqlQuery = @"SELECT a.id_programa, a.riesgopatrullaje, b.regionSSF 
                                FROM ssf.programapatrullajes a
                                JOIN ssf.rutas b ON a.id_ruta=b.id_ruta
                                WHERE a.id_ruta= @pIdRuta
                                AND a.fechapatrullaje= @pFecha
                                AND a.id_estadopatrullaje < (SELECT id_estadopatrullaje FROM ssf.estadopatrullaje WHERE descripcionestadopatrullaje='Concluido')";

            object[] parametros = new object[]
            {
                new SqlParameter("@pIdRuta", idRuta),
                new SqlParameter("@pFecha", fecha),

             };

            var idsProgramas = await _terminacionContext.ProgramasPatrullaje.FromSqlRaw(sqlQuery, parametros).ToListAsync();

            if (idsProgramas != null && idsProgramas.Count > 0)
            {
                res = idsProgramas[0].IdPrograma;
            }

            return res;
        }

        public async Task<IEnumerable<UsoVehiculo>> ObtenerUsoVehiculoPorProgramaAndIdVehiculoAsync(int idPrograma, int idVehiculo)
        {
            return await _terminacionContext.UsosVehiculo.Where(x => x.IdPrograma == idPrograma && x.IdVehiculo == idVehiculo ).ToListAsync();
        }

        public async Task ActualizaProgramaEnMemoriaAsync(int idPrograma, int idUsuario, TimeSpan termino)
        {
            var prog = await _terminacionContext.ProgramasPatrullaje.Where(x => x.IdPrograma == idPrograma).SingleOrDefaultAsync();

            if (prog != null) 
            {
                prog.IdEstadoPatrullaje = 4;
                prog.IdUsuario = idUsuario;
                prog.Termino = termino;

                _terminacionContext.ProgramasPatrullaje.Update(prog);
            }
        }

        public async Task ActualizaTarjetaInformativaEnMemoriaAsync(int idPrograma, int idUsuario, TerminacionPatrullajeDto t)
        {
            var tarjeta = await _terminacionContext.TarjetasInformativas.Where(x => x.IdPrograma == idPrograma ).SingleOrDefaultAsync();
            if (tarjeta != null) 
            {
                tarjeta.IdUsuario = idUsuario;
                var ini = TimeSpan.Parse(t.HoraInicio);
                var fin = TimeSpan.Parse(t.HoraTermino);
                tarjeta.Inicio = new TimeSpan(ini.Hours,ini.Minutes, 0);
                tarjeta.Termino = new TimeSpan(fin.Hours,fin.Minutes, 0);
                var tv = t.TiempoVuelo.Replace("N/A", "00:00");
                var tvSpan = TimeSpan.Parse(tv);
                tarjeta.TiempoVuelo = new TimeSpan(tvSpan.Hours, tvSpan.Minutes, 0);
                var cc = t.CalzoCalzo.Replace("N/A", "00:00");
                var ccSpan = TimeSpan.Parse(cc);
                tarjeta.CalzoAcalzo = new TimeSpan(ccSpan.Hours, ccSpan.Minutes, 0);
                tarjeta.Observaciones = t.ObservacionesPatrullaje;
                tarjeta.ComandantesInstalacionSsf = t.ComandanteInstalacion;
                tarjeta.PersonalMilitarSedenaoficial = t.OficialSDN;
                tarjeta.KmRecorrido = t.KmRecorrido;
                tarjeta.PersonalMilitarSedenatropa = t.TropaSDN;
                tarjeta.Linieros = t.Conductores;
                tarjeta.ComandantesTurnoSsf = t.ComandanteTurno;
                tarjeta.OficialesSsf = t.OficialSSF;
                tarjeta.PersonalNavalSemaroficial = t.OficialSEMAR;
                tarjeta.PersonalNavalSemartropa = t.TropaSEMAR;

                _terminacionContext.TarjetasInformativas.Update(tarjeta);
            }
        }

        public async Task ActualizaOrAgregaUsosVehiculoEnMemoriaAsync(int idPrograma, int idUsuario, TerminacionPatrullajeDto t)
        {
            var lstActualizar = new List<UsoVehiculo>();
            var lstCrear = new List<UsoVehiculo>();
            foreach (var item in t.objTerminoPatrullajeVehiculo)
            {
                var v = await _terminacionContext.UsosVehiculo.Where(x => x.IdPrograma == idPrograma && x.IdVehiculo == t.objTerminoPatrullajeVehiculo[0].IdVehiculo).SingleOrDefaultAsync();

                if (v != null)
                {
                    v.KmInicio = item.KmInicio;
                    v.KmFin = item.KmFin;
                    v.ConsumoCombustible = item.Combustible;
                    v.IdUsuarioVehiculo = idUsuario;
                    v.EstadoVehiculo = item.EstadoVehiculo;

                    lstActualizar.Add(v);
                }
                else 
                {
                    var nvoUso = new UsoVehiculo() 
                    {
                        IdPrograma = idPrograma,
                        IdVehiculo = item.IdVehiculo,
                        KmInicio = item.KmInicio,
                        KmFin = item.KmFin,
                        ConsumoCombustible= item.Combustible,
                        IdUsuarioVehiculo = idUsuario,
                        EstadoVehiculo= item.EstadoVehiculo
                    };

                    lstCrear.Add(nvoUso);
                }
            }
        
            _terminacionContext.UsosVehiculo.UpdateRange(lstActualizar);
            _terminacionContext.UsosVehiculo.AddRange(lstCrear);
        }

        public async Task SaveTransactionAsync()
        {
            await _terminacionContext.SaveChangesAsync();
        }
    }
}
