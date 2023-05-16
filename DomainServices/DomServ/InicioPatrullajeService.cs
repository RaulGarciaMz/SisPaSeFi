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

                await _repo.AgregaInicioPatrullajeTransaccionalAsync(a, user.IdUsuario, programas);
            }
        }
    }
}
