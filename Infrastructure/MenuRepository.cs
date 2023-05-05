using Domain.Entities;
using Domain.Entities.Vistas;
using Domain.Ports.Driven.Repositories;
using Microsoft.EntityFrameworkCore;
using SqlServerAdapter.Data;

namespace SqlServerAdapter
{
    public class MenuRepository : IMenuRepo
    {
        protected readonly MenuContext _menuContext;

        public MenuRepository(MenuContext menuContext)
        {
            _menuContext = menuContext ?? throw new ArgumentNullException(nameof(menuContext));
        }

        public async Task<List<MenuVista>> ObtenerMenuPorCriterioPadreAsync(int padre)
        {
            return await _menuContext.Menus.Where(x => x.Padre == padre)
                .Select(s => new MenuVista { 
                    IdMenu = s.IdMenu,
                    Padre = s.Padre,
                    Descripcion = s.Descripcion,
                    Desplegado = s.Desplegado,
                    Liga = s.Liga,
                    Posicion = s.Posicion
                })
                .Distinct().OrderBy(x => x.Posicion).ToListAsync();
        }

        public async Task<List<Menu>> ObtenerMenuPorDesplegadoAsync(string desplegado)
        {
            return await _menuContext.Menus.Where(x => x.Desplegado == desplegado).ToListAsync();
        }

        public async Task<List<Menu>> ObtenerMenusConNombreIgualAlDelMenuIdAsync(string desplegado, int idMenu)
        {
            return await _menuContext.Menus.Where(x => x.Desplegado == desplegado && x.IdMenu != idMenu).ToListAsync();
        }

        public async Task AgregarMenuAsync(string desplegado, string liga, string descripcion, int idPadre, int posicion)
        {
            var m = new Menu() 
            {
                Desplegado = desplegado,
                Liga = liga,
                Descripcion = descripcion,
                Padre=idPadre,
                Posicion=posicion
            };

            _menuContext.Menus.Add(m);

            await _menuContext.SaveChangesAsync();
        }

        public async Task ActualizarMenuAsync(int idMenu, string desplegado, string liga, string descripcion, int idPadre, int posicion)
        {
            var menu = await _menuContext.Menus.Where(m => m.IdMenu == idMenu).SingleOrDefaultAsync();

            if (menu != null)
            {
                menu.Desplegado = desplegado;
                menu.Liga = liga;
                menu.Descripcion = descripcion;
                menu.Padre = idPadre;
                menu.Posicion = posicion;

                _menuContext.Menus.Update(menu);

               await _menuContext.SaveChangesAsync();
            }
        }
    }
}

