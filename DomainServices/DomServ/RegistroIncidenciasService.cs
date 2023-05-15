using Domain.DTOs;
using Domain.Ports.Driven;
using Domain.Ports.Driven.Repositories;
using Domain.Ports.Driving;

namespace DomainServices.DomServ
{
    public class RegistroIncidenciasService : IRegistroIncidenciaService
    {
        private readonly IRegistroIncidenciaRepo _repo;
        private readonly IUsuariosParaValidacionQuery _user;

        public RegistroIncidenciasService(IRegistroIncidenciaRepo repo, IUsuariosParaValidacionQuery u)
        {
            _repo = repo;
            _user = u;
        }

        public async Task AgregaIncidenciaTransaccionalAsync(RegistrarIncidenciaDto i)
        {
            
            var user = await _user.ObtenerUsuarioPorUsuarioNomAsync(i.usuario);

            if (user != null)
            {
                await _repo.AgregaIncidenciaTransaccionalAsync(i, user.IdUsuario);
            }
        }

    }
}
