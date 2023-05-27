using Domain.DTOs;
using Domain.Entities.Vistas;
using Domain.Ports.Driven;
using Domain.Ports.Driven.Repositories;
using Domain.Ports.Driving;

namespace DomainServices.DomServ
{
    public class EstadisticasService : IEstadisticasService
    {
        private readonly IEstadisticasRepo _repo;
        private readonly IUsuariosParaValidacionQuery _user;

        public EstadisticasService(IEstadisticasRepo repo, IUsuariosParaValidacionQuery u)
        {
            _repo = repo;
            _user = u;
        }

        public async Task<List<EstadisticasSistemaDto>> ObtenerEstadisticasDeSistemaAsync(string usuario)
        { 
            var ret = new List<EstadisticasSistemaDto>();
            var afect = new List<EstadisticaSistemaVista>();
            var user = await _user.ObtenerUsuarioConfiguradorPorNombreAsync(usuario);

            if (user != null)
            {
                afect = await _repo.ObtenerEstadisticasDeSistemaAsync();
                ret = ConvierteListaEstadisticasSistemaToDto(afect);
            }

            return ret;
        }

        private List<EstadisticasSistemaDto> ConvierteListaEstadisticasSistemaToDto(List<EstadisticaSistemaVista> e)
        {

            var ret = new List<EstadisticasSistemaDto>();

            foreach (var estadistica in e)
            {
                var est = new EstadisticasSistemaDto()
                {
                    strDescripcion = estadistica.concepto,
                    strValor = estadistica.total.ToString()
                };

                ret.Add(est);
            }
            return ret;
        }
    }
}
