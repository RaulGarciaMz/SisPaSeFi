using Domain.DTOs;
using Domain.Entities;
using Domain.Ports.Driven;
using Domain.Ports.Driven.Repositories;
using Domain.Ports.Driving;

namespace DomainServices.DomServ
{
    public class RolesService : IRolesService
    {
        private readonly IRolesRepo _repo;
        private readonly IUsuariosParaValidacionQuery _user;

        public RolesService(IRolesRepo repo, IUsuariosParaValidacionQuery uc)
        {
            _repo = repo;
            _user = uc;
        }

        public async Task<List<RolDto>> ObtenerRolesAsync(string usuario) 
        {
            var dtos = new List<RolDto>();

            var user = await _user.ObtenerUsuarioConfiguradorPorNombreAsync(usuario);

            if (user != null)
            {
                var lstRoles = await _repo.ObtenerRolesAsync();

                if (lstRoles != null && lstRoles.Count > 0)
                {
                    dtos = ConvierteListaRolesToDto(lstRoles);
                }
            }

            return dtos;
        }

        public async Task AgregaRolAsync(string nombre, string descripcion, int idMenu, string usuario)
        {
            var user = await _user.ObtenerUsuarioConfiguradorPorNombreAsync(usuario);

            if (user != null)
            {
                var rol = await _repo.ObtenerRolPorNombreAsync(nombre);
                if (rol == null)
                {
                    await _repo.AgregaRolAsync(nombre, descripcion, idMenu);
                }
            }
        }

        public async Task ActualizaRolPorOpcionAsync(int idRol, string nombre, string descripcion, int idMenu, string dato, string opcion, string usuario)
        {
            var user = await _user.ObtenerUsuarioConfiguradorPorNombreAsync(usuario);

            if (user != null)
            {

                switch (opcion)
                {
                    case "ActualizarRol":
                        var rol = await _repo.ObtenerRolPorNombreAndIdAsync(nombre, idRol);
                        if (rol == null)
                        {
                            await _repo.ActualizaRolAsync(idRol, nombre, descripcion, idMenu);
                        }
                        break;
                    case "QuitarMenuRol":
                        await _repo.EliminarRolMenuAsync(idRol, idMenu);
                        break;
                    case "AgregarMenuRol":
                        var d = Int32.Parse(dato);
                        await _repo.AgregaRolMenuAsync(idRol, idMenu, d);
                        break;
                }

            }
        }

        private RolDto ConvierteRolToDto(Rol rol) 
        {
            return new RolDto 
            { 
                intIdRol = rol.IdRol,
                intIdMenu = rol.IdMenu,
                strDescripcion = rol.Descripcion,
                strNombre = rol.Nombre
            };
        
        }

        private List<RolDto> ConvierteListaRolesToDto(List<Rol> lista)
        {
            var l = new List<RolDto>();

            foreach (var r in lista)
            {
                l.Add(ConvierteRolToDto(r));
            }

            return l;
        }

    }
}
