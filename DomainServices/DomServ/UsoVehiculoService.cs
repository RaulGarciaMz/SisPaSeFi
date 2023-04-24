using Domain.DTOs;
using Domain.Entities;
using Domain.Entities.Vistas;
using Domain.Ports.Driven;
using Domain.Ports.Driven.Repositories;
using Domain.Ports.Driving;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainServices.DomServ
{
    public class UsoVehiculoService : IUsoVehiculoService
    {
        private readonly IUsoVehiculoRepo _repo;
        private readonly IUsuariosConfiguradorQuery _user;

        public UsoVehiculoService(IUsoVehiculoRepo repo, IUsuariosConfiguradorQuery uc)
        {
            _repo = repo;
            _user = uc;
        }

        public async Task AgregaAsync(UsoVehiculoDtoForCreateOrUpdate uv)
        {
            var user = await _user.ObtenerUsuarioConfiguradorPorNombreAsync(uv.Usuario);

            if (user != null)
            {
                await _repo.AgregaAsync(uv.IdPrograma, uv.IdVehiculo, uv.IdUsuarioVehiculo, uv.KmInicio, uv.KmFin, uv.ConsumoCombustible, uv.EstadoVehiculo);
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
