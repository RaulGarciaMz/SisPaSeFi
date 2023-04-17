using Domain.Entities.Vistas;
using Domain.Ports.Driven;
using Domain.Ports.Driven.Repositories;
using Domain.Ports.Driving;

namespace DomainServices.DomServ
{
    public class EstadisticasService : IEstadisticasService
    {
        private readonly IEstadisticasRepo _repo;
        private readonly IUsuariosConfiguradorQuery _user;

        public EstadisticasService(IEstadisticasRepo repo, IUsuariosConfiguradorQuery u)
        {
            _repo = repo;
            _user = u;
        }

        public async Task<List<EstadisticaSistemaVista>> ObtenerEstadisticasDeSistemaAsync(string usuario)
        {
            var afect = new List<EstadisticaSistemaVista>();
            var user = await _user.ObtenerUsuarioConfiguradorPorNombreAsync(usuario);

            if (user != null)
            {
                afect = await _repo.ObtenerEstadisticasDeSistemaAsync();
            }

            return afect;
        }
    }
}
