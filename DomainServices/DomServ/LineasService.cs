using Domain.DTOs;
using Domain.Entities;
using Domain.Entities.Vistas;
using Domain.Enums;
using Domain.Ports.Driven;
using Domain.Ports.Driven.Repositories;
using Domain.Ports.Driving;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace DomainServices.DomServ
{
    public class LineasService : ILineasService
    {
        private readonly ILineasRepo _repo;
        private readonly IUsuariosConfiguradorQuery _user;

        public LineasService(ILineasRepo repo, IUsuariosConfiguradorQuery u)
        {
            _repo = repo;
            _user = u;
        }

        public async Task ActualizaAsync(LineaDtoForUpdate linea)
        {
            var user = await _user.ObtenerUsuarioConfiguradorPorNombreAsync(linea.Usuario);

            if (user != null)
            {
                await _repo.ActualizaAsync(linea.IdLinea, linea.Clave, linea.Descripcion, linea.IdPuntoInicio, linea.IdPuntoFin, user.IdUsuario, linea.Bloqueado);
            }
        }
        public async Task AgregarAsync(LineaDtoForCreate linea)
        {
            var user = await _user.ObtenerUsuarioConfiguradorPorNombreAsync(linea.Usuario);

            if (user != null)
            {
                await _repo.AgregarAsync(linea.Clave, linea.Descripcion, linea.IdPuntoInicio, linea.IdPuntoFin);
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
