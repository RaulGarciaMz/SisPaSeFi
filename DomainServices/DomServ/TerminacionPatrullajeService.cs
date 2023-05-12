using Domain.DTOs;
using Domain.Ports.Driven;
using Domain.Ports.Driven.Repositories;
using Domain.Ports.Driving;

namespace DomainServices.DomServ
{
    public class TerminacionPatrullajeService : ITerminacionPatrullajeService
    {
        private readonly ITerminacionPatrullajeRepo _repo;
        private readonly IUsuariosParaValidacionQuery _user;

        public TerminacionPatrullajeService(ITerminacionPatrullajeRepo repo, IUsuariosParaValidacionQuery u)
        {
            _repo = repo;
            _user = u;
        }

      public async Task RegistraTerminacionAsync(TerminacionPatrullajeDto p)
        {
            var userId = await _user.ObtenerIdUsuarioPorUsuarioNomAsync(p.usuario);

            if (userId != null)
            {
                var fecha = DateTime.Parse(p.FechaPatrullaje);
                var idPrograma = await _repo.ObtenerIdProgramaPorRutaAndFechaAsync(p.IdRuta, fecha);
                
                if (idPrograma > -1) 
                {
                    var termino = TimeSpan.Parse(p.HoraTermino);
                    var tNoSegs = new TimeSpan(termino.Hours, termino.Minutes, 0);

                    await _repo.ActualizaProgramaEnMemoriaAsync(idPrograma, userId.Value, tNoSegs);
                    await _repo.ActualizaTarjetaInformativaEnMemoriaAsync(idPrograma, userId.Value, p);
                    await _repo.ActualizaOrAgregaUsosVehiculoEnMemoriaAsync(idPrograma, userId.Value, p);

                    await _repo.SaveTransactionAsync();
                }
            }
        }
    }
}
