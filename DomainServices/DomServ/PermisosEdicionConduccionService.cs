using Domain.Ports.Driven.Repositories;
using Domain.Ports.Driven;
using Domain.Ports.Driving;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;

namespace DomainServices.DomServ
{
    public class PermisosEdicionConduccionService : IPermisoEdicionConduccionService
    {
        private readonly IPermisosConduccionRepo _repo;
        private readonly IUsuariosConfiguradorQuery _user;

        public PermisosEdicionConduccionService(IPermisosConduccionRepo repo, IUsuariosConfiguradorQuery u)
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

        public async Task<List<Permisosedicionprocesoconduccion>> ObtenerPermisosAsync(string usuario)
        {
            var l = new List<Permisosedicionprocesoconduccion>();
            var user = await _user.ObtenerUsuarioConfiguradorPorNombreAsync(usuario);

            if (user != null)
            {
                l = await _repo.ObtenerPermisosAsync();
            }

            return l;
        }

        public async Task<Permisosedicionprocesoconduccion?> ObtenerPermisosPorOpcionAsync(int region, int anio, int mes, string usuario)
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
