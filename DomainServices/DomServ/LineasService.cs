using Domain.DTOs;
using Domain.Entities.Vistas;
using Domain.Enums;
using Domain.Ports.Driven;
using Domain.Ports.Driven.Repositories;
using Domain.Ports.Driving;

namespace DomainServices.DomServ
{
    public class LineasService : ILineasService
    {
        private readonly ILineasRepo _repo;
        private readonly IUsuariosParaValidacionQuery _user;

        public LineasService(ILineasRepo repo, IUsuariosParaValidacionQuery u)
        {
            _repo = repo;
            _user = u;
        }

        public async Task ActualizaAsync(LineaDtoForUpdate linea)
        {
            var user = await _user.ObtenerUsuarioConfiguradorPorNombreAsync(linea.strUsuario);

            if (user != null)
            {
                await _repo.ActualizaAsync(linea.intIdLinea, linea.strClave, linea.strDescripcion, linea.intIdPuntoInicio, linea.intIdPuntoFin, user.IdUsuario, linea.intBloqueado);
            }
        }

        public async Task AgregarAsync(LineaDtoForCreate linea)
        {
            var user = await _user.ObtenerUsuarioConfiguradorPorNombreAsync(linea.strUsuario);

            if (user != null)
            {
                await _repo.AgregarAsync(linea.strClave, linea.strDescripcion, linea.intIdPuntoInicio, linea.intIdPuntoFin);
            }
        }
        public async Task BorraAsync(int idLinea, string usuario)
        {
            var user = await _user.ObtenerUsuarioConfiguradorPorNombreAsync(usuario);

            if (user != null)
            {
                await _repo.BorraAsync(idLinea);
            }
        }

        public async Task<List<LineaVista>> ObtenerLineasAsync(int opcion, string criterio, string usuario)
        {
            var lineas = new List<LineaVista>();
            var user = await _user.ObtenerUsuarioConfiguradorPorNombreAsync(usuario);
            var laOpcion = (LineasOpcion)opcion;

            if (user != null)
            {
                switch (laOpcion) 
                {
                    case LineasOpcion.PorClave:
                        lineas = await _repo.ObtenerLineaPorClaveAsync(criterio);
                        break;
                    case LineasOpcion.PorPuntoInicial:
                        lineas = await _repo.ObtenerLineaPorUbicacionDePuntoInicioAsync(criterio);
                        break;
                    case LineasOpcion.PorPuntoFinal:
                        lineas = await _repo.ObtenerLineaPorUbicacionDePuntoFinalAsync(criterio);
                        break;
                    case LineasOpcion.PorRecorrido:
                        lineas = await _repo.ObtenerLineaDentroDeRecorridoDeRutaAsync(criterio);
                        break;
                    case LineasOpcion.PorRadioEnPunto:
                        var idPunto = Int32.Parse(criterio);
                        lineas = await _repo.ObtenerLineasDentroDeRadio5KmDeUnPuntoAsync(idPunto);
                        break;
                    case LineasOpcion.PorRadioEnCoordenada:
                        var coordenada = criterio.Split(",");
                        lineas = await _repo.ObtenerLineasDentroDeRadio5KmDeUnasCoordenadasAsync(coordenada[0], coordenada[1]);
                        break;
                }
            }

            return lineas;
        }
    }
}
