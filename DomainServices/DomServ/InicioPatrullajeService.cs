using Domain.DTOs;
using Domain.Ports.Driven;
using Domain.Ports.Driven.Repositories;
using Domain.Ports.Driving;

namespace DomainServices.DomServ
{
    public class InicioPatrullajeService : IInicioPatrullajeService
    {

        private readonly IInicioPatrullajeRepo _repo;
        private readonly IUsuariosParaValidacionQuery _user;

        public InicioPatrullajeService(IInicioPatrullajeRepo repo, IUsuariosParaValidacionQuery u)
        {
            _repo = repo;
            _user = u;
        }

        public async Task AgregaInicioPatrullajeAsync(InicioPatrullajeDto a)
        {
            var user = await _user.ObtenerUsuarioPorUsuarioNomAsync(a.usuario);

            if (user != null)
            {
                var programas  = await _repo.ObtenerProgramaPorRutaAndFechaAsync(a.IdRuta, a.FechaPatrullaje);

                switch (programas.Count)
                {
                    case 0:
                        await CreaProgramaPatrullajeEnMemoriaAsync(a, user.IdUsuario);
                        break;
                    case > 0:
                        await ActualizaProgramaPatrullajeConTarjetaInformativaEnMemoriaAsync(programas[0].id_programa, programas[0].riesgopatrullaje, user.IdUsuario, a);
                        break;
                }

                if (a.objInicioPatrullajeVehiculo != null && a.objInicioPatrullajeVehiculo.Count > 0)
                {
                    await _repo.CreaOrActualizaUsosVehiculoEnMemoria(a.objInicioPatrullajeVehiculo, programas[0].id_programa, user.IdUsuario);
                }            

                await _repo.SaveTransactionAsync();
            }
        }

        private async Task CreaProgramaPatrullajeEnMemoriaAsync(InicioPatrullajeDto a, int idUsuario)
        {
            var p = await _repo.ObtenerPuntosEnRutaDelItinerarioAsync(a.IdRuta);

            if (p != null && p.Count > 0)
            {
                var fecha = DateTime.Parse(a.FechaPatrullaje);
                _repo.AgregaProgramaPatrullajeEnMemoria(a.IdRuta, fecha, idUsuario, p[0].id_punto, a.IdRuta);
            }
        }

        private async Task ActualizaProgramaPatrullajeConTarjetaInformativaEnMemoriaAsync(int idPrograma, int riesgo, int idUsuario, InicioPatrullajeDto a)
        {
            var horaInicio = new TimeSpan(int.Parse(a.HoraInicio), 0, 0);

            _repo.ActualizaProgramaPatrullajeEnMemoria(idPrograma, horaInicio, idUsuario, riesgo);

            var tarjeta = await _repo.ObtenerTarjetaInformativaPorProgramaAsync(idPrograma);

            if(tarjeta != null) 
            {
                var fecha = DateTime.Parse(a.FechaPatrullaje);

                switch (tarjeta.Count) 
                {
                    case 0:
                        var horaNvaTarjeta = TimeSpan.Parse(a.HoraInicio);
                        _repo.AgregaTarjetaInformativaEnMemoria(idPrograma, idUsuario, horaNvaTarjeta, a.ComandanteInstalacion, a.OficialSDN, fecha, a.TropaSDN, a.Conductores, a.ComandanteTurno, a.OficialSSF, fecha);
                        break;
                    case > 0:
                        var horaActInicio = new TimeSpan(int.Parse(a.HoraInicio), 0, 0);
                        await _repo.ActualizaTarjetaInformativaEnMemoria(idPrograma, idUsuario, horaActInicio, a.ComandanteInstalacion, a.OficialSDN, fecha, a.TropaSDN, a.Conductores, a.ComandanteTurno, a.OficialSSF, fecha);
                        break;
                }
            }

        }
    }
}
