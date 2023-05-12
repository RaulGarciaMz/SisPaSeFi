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
        private readonly IUsuariosParaValidacionQuery _user;

        public MenuService(IMenuRepo repo, IUsuariosParaValidacionQuery u)
        {
            _repo = repo;
            _user = u;
        }

        public async Task<List<MenuDto>> ObtenerMenuPorOpcionAsync(string opcion, string criterio, string usuario)
        {
            var menus = new List<MenuDto>();
            var user = await _user.ObtenerUsuarioConfiguradorPorNombreAsync(usuario);

            if (user != null)
            {
                var ms = new List<MenuVista>();
                var padre = Int32.Parse(criterio);

                switch (opcion)
                {
                    case "Submenu":
                        ms = await _repo.ObtenerMenuPorCriterioPadreAsync(padre);
                        break;
                    case "MenuUsuario":
                        ms = await _repo.ObtenerMenuPorUsuarioAndCriterioPadreAsync(user.IdUsuario, padre);
                        break;
                }

                if (ms != null && ms.Count > 0)
                {
                    menus = ConvierteListMenuToDto(ms);
                }
            }

            return menus;
        }

        public async Task AgregaMenuAsync(MenuDto menu, string usuario)
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
                intIdMenu = m.idmenu,
                intIdPadre = m.padre,
                intPosicion = m.posicion,
                strDescripcion = m.descripcion,
                strDesplegado = m.desplegado,
                strLiga = m.liga,
                intNavegar = m.navegar
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
