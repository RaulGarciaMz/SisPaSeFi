using Domain.DTOs;
using Domain.Entities.Vistas;
using Domain.Ports.Driven;
using Domain.Ports.Driven.Repositories;
using Domain.Ports.Driving;

namespace DomainServices.DomServ
{
    public class MenuService : IMenuService
    {
        private readonly IMenuRepo _repo;
        private readonly IUsuariosConfiguradorQuery _user;

        public MenuService(IMenuRepo repo, IUsuariosConfiguradorQuery u)
        {
            _repo = repo;
            _user = u;
        }

        public async Task<List<MenuDto>> ObtenerMenuPorOpcionAsync(string opcion, int padre, string usuario)
        {
            var menus = new List<MenuDto>();
            var user = await _user.ObtenerUsuarioConfiguradorPorNombreAsync(usuario);

            if (user != null)
            {
                var ms = new List<MenuVista>();
                switch (opcion)
                {
                    case "Submenu":
                        ms = await _repo.ObtenerMenuPorCriterioPadreAsync(padre);
                        break;
                }

                if (ms != null && ms.Count > 0)
                {
                    menus = ConvierteListMenuToDto(ms);
                }
            }

            return menus;
        }

        public async Task AgregaMenuAsync(string opcion, MenuDto menu, string usuario)
        {
            var user = await _user.ObtenerUsuarioConfiguradorPorNombreAsync(usuario);

            if (user != null)
            {

                var nombreRepetido = await _repo.ObtenerMenuPorDesplegadoAsync(menu.strDesplegado);
                if (nombreRepetido.Count == 0)
                {
                    await _repo.AgregarMenuAsync(menu.strDesplegado, menu.strLiga, menu.strDescripcion, menu.intIdPadre.Value, menu.intPosicion.Value);
                }
            }
        }

        public async Task ActualizaMenuAsync(MenuDto menu, string usuario)
        {
            var user = await _user.ObtenerUsuarioConfiguradorPorNombreAsync(usuario);

            if (user != null)
            {

                var nombreRepetido = await _repo.ObtenerMenusConNombreIgualAlDelMenuIdAsync(menu.strDesplegado, menu.intIdMenu);
                if (nombreRepetido.Count == 0)
                {
                    await _repo.ActualizarMenuAsync(menu.intIdMenu, menu.strDesplegado, menu.strLiga, menu.strDescripcion, menu.intIdPadre.Value, menu.intPosicion.Value);
                }
            }
        }

        private MenuDto ConvierteMenuToDto(MenuVista m) 
        {
            return new MenuDto() 
            {
                intIdMenu = m.IdMenu,
                intIdPadre = m.Padre,
                intPosicion = m.Posicion,
                strDescripcion = m.Descripcion,
                strDesplegado = m.Desplegado,
                strLiga = m.Liga
            };
        }

        private List<MenuDto> ConvierteListMenuToDto(List<MenuVista> menus)
        {
            var list = new List<MenuDto>();

            foreach (var m in menus) 
            {
                list.Add(ConvierteMenuToDto(m));
            }

            return list;
        }
    }
}
