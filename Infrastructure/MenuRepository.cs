using Domain.Entities;
using Domain.Entities.Vistas;
using Domain.Ports.Driven.Repositories;
using Microsoft.Data.SqlClient;
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
                    idmenu = s.IdMenu,
                    padre = s.Padre.Value,
                    descripcion = s.Descripcion,
                    desplegado = s.Desplegado,
                    liga = s.Liga,
                    posicion = s.Posicion.Value,
                    navegar =0
                })
                .Distinct().OrderBy(x => x.posicion).ToListAsync();
        }

        public async Task<List<MenuVista>> ObtenerMenuPorUsuarioAndCriterioPadreAsync(int idUsuario, int padre)
        {
            string sqlQuery = @"SELECT DISTINCT c.idmenu, c.desplegado, c.liga, c.descripcion, c.posicion, c.padre, b.navegar
                                FROM ssf.usuariorol a
                                JOIN ssf.rolmenu b ON a.id_rol=b.id_rol
                                JOIN ssf.menus c ON b.idmenu=c.idmenu
                                WHERE a.id_usuario= @pIdUsuario
                                AND c.padre = @pCriterio
                                ORDER BY c.posicion";

            object[] parametros = new object[]
            {
                new SqlParameter("@pIdUsuario", idUsuario),
                new SqlParameter("@pCriterio", padre)
             };

            return await _menuContext.MenusVista.FromSqlRaw(sqlQuery, parametros).ToListAsync();
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
                Posicion=posicion,
                //Campos no nulos
                IdGrupo = 1
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

