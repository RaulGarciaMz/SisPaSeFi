using Domain.DTOs;
using Domain.Ports.Driven;
using Domain.Ports.Driven.Repositories;

namespace DomainServices.DomServ
{
    public class RegistroIncidenciasService
    {
        private readonly IRegistroIncidenteRepo _repo;
        private readonly IUsuariosParaValidacionQuery _user;

        public RegistroIncidenciasService(IRegistroIncidenteRepo repo, IUsuariosParaValidacionQuery u)
        {
            _repo = repo;
            _user = u;
        }

        public async Task AgregaIncidencia(RegistrarIncidenciaDto i)
        {
            
            var user = await _user.ObtenerUsuarioPorUsuarioNomAsync(i.usuario);

            if (user != null)
            {
                await _repo.AgregaIncidenciaTransaccionalAsync(i, user.IdUsuario);
            }
        }

    }
}
