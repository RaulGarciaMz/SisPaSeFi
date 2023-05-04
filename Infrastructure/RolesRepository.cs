using Domain.Entities;
using Domain.Ports.Driven.Repositories;
using Microsoft.EntityFrameworkCore;
using SqlServerAdapter.Data;

namespace SqlServerAdapter
{
    public class RolesRepository : IRolesRepo
    {
        protected readonly RolesContext _rolesContext;

        public RolesRepository(RolesContext rolesContext)
        {
            _rolesContext = rolesContext ?? throw new ArgumentNullException(nameof(rolesContext));
        }

        public async Task<List<Rol>> ObtenerRolesAsync()
        {
            return await _rolesContext.Roles.ToListAsync();
        }

        public async Task<Rol?> ObtenerRolPorNombreAsync(string nombre)
        {
            return await _rolesContext.Roles.Where(x => x.Nombre == nombre). SingleOrDefaultAsync();
        }

        public async Task<Rol?> ObtenerRolPorNombreAndIdAsync(string nombre, int idRol)
        {
            return await _rolesContext.Roles.Where(x => x.Nombre == nombre && x.IdRol != idRol).SingleOrDefaultAsync();
        }

        public async Task AgregaRolAsync(string nombre, string descripcion, int idMenu)
        {
            var rol = new Rol() 
            {
                Nombre = nombre,
                Descripcion = descripcion,  
                IdMenu = idMenu
            };

            _rolesContext.Roles.Add(rol);

            await _rolesContext.SaveChangesAsync();
        }

        public async Task ActualizaRolAsync(int idRol, string nombre, string descripcion, int idMenu)
        {

            var r = await _rolesContext.Roles.Where(x => x.IdRol == idRol).SingleOrDefaultAsync();
            if (r != null)
            {
                r.IdMenu = idMenu;
                r.Descripcion = descripcion;
                r.Nombre = nombre;

                _rolesContext.Roles.Update(r);

                await _rolesContext.SaveChangesAsync();
            }
        }
    }
}
