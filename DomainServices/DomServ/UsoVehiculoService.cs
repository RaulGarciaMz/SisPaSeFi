using Domain.DTOs;
using Domain.Entities.Vistas;
using Domain.Ports.Driven;
using Domain.Ports.Driven.Repositories;
using Domain.Ports.Driving;

namespace DomainServices.DomServ
{
    public class UsoVehiculoService : IUsoVehiculoService
    {
        private readonly IUsoVehiculoRepo _repo;
        private readonly IUsuariosParaValidacionQuery _user;

        public UsoVehiculoService(IUsoVehiculoRepo repo, IUsuariosParaValidacionQuery uc)
        {
            _repo = repo;
            _user = uc;
        }

        public async Task AgregaAsync(UsoVehiculoDtoForCreateOrUpdate uv)
        {
            var user = await _user.ObtenerUsuarioConfiguradorPorNombreAsync(uv.Usuario);

            if (user != null)
            {
                await _repo.AgregaAsync(uv.IdPrograma, uv.IdVehiculo, user.IdUsuario, uv.KmInicio, uv.KmFin, uv.ConsumoCombustible, uv.EstadoVehiculo);
            }
        }

        public async Task ActualizaAsync(UsoVehiculoDtoForCreateOrUpdate uv)
        {
            var user = await _user.ObtenerUsuarioConfiguradorPorNombreAsync(uv.Usuario);

            if (user != null)
            {
                await _repo.ActualizaAsync(uv.IdPrograma, uv.IdVehiculo, uv.IdUsuarioVehiculo, uv.KmInicio, uv.KmFin, uv.ConsumoCombustible, uv.EstadoVehiculo);
            }
        }

        public async Task BorraAsync(int idPrograma, int idVehiculo, string usuario)
        {
            var user = await _user.ObtenerUsuarioConfiguradorPorNombreAsync(usuario);

            if (user != null)
            {
                await _repo.BorraAsync(idPrograma, idVehiculo);
            }
        }

        public async Task<List<UsoVehiculoVista>> ObtenerUsoVehiculosPorProgramaAsync(int idPrograma, string usuario)
        {
            var regreso = new List<UsoVehiculoVista>();
            var user = await _user.ObtenerUsuarioConfiguradorPorNombreAsync(usuario);

            if (user != null)
            {
                regreso = await _repo.ObtenerUsoVehiculosPorProgramaAsync(idPrograma);
            }

            return regreso;
        }
    }
}
