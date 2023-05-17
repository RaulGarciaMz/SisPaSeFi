using Domain.Entities;
using Domain.Ports.Driven;
using Domain.Ports.Driven.Repositories;
using Domain.Ports.Driving;

namespace DomainServices.DomServ
{
    public class PermisosEdicionConduccionService : IPermisoEdicionConduccionService
    {
        private readonly IPermisosConduccionRepo _repo;
        private readonly IUsuariosParaValidacionQuery _user;

        public PermisosEdicionConduccionService(IPermisosConduccionRepo repo, IUsuariosParaValidacionQuery u)
        {
            _repo = repo;
            _user = u;
        }

        public async Task AgregarPorOpcionAsync(int region, int anio, int mes, string usuario)
        {
            var user = await _user.ObtenerUsuarioConfiguradorPorNombreAsync(usuario);

            if (user != null)
            {
                await _repo.AgregarPorOpcionAsync(region, anio, mes);
            }                
        }

        public async Task BorraPorOpcionAsync(int region, int anio, int mes, string usuario)
        {
            var user = await _user.ObtenerUsuarioConfiguradorPorNombreAsync(usuario);

            if (user != null)
            {
                await _repo.BorraPorOpcionAsync(region, anio, mes);
            }            
        }

        public async Task<List<PermisoEdicionProcesoConduccion>> ObtenerPermisosAsync(string usuario)
        {
            var l = new List<PermisoEdicionProcesoConduccion>();
            var user = await _user.ObtenerUsuarioConfiguradorPorNombreAsync(usuario);

            if (user != null)
            {
                l = await _repo.ObtenerPermisosAsync();
            }

            return l;
        }

        public async Task<PermisoEdicionProcesoConduccion?> ObtenerPermisosPorOpcionAsync(int region, int anio, int mes, string usuario)
        {
            var user = await _user.ObtenerUsuarioConfiguradorPorNombreAsync(usuario);

            if (user != null)
            {
                return await _repo.ObtenerPermisosPorOpcionAsync(region, anio, mes);
            }

            return null;
        }
    }
}
