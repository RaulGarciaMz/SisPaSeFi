using Domain.DTOs;
using Domain.Entities;
using Domain.Entities.Vistas;
using Domain.Ports.Driven;
using Domain.Ports.Driven.Repositories;
using Domain.Ports.Driving;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainServices.DomServ
{
    public class ItinerariosService : IItinerariosService
    {
        private readonly IItinerariosRepo _repo;
        private readonly IUsuariosConfiguradorQuery _user;

        public ItinerariosService(IItinerariosRepo repo, IUsuariosConfiguradorQuery uc)
        {
            _repo = repo;
            _user = uc;
        }

        public async Task<List<ItinerarioVista>> ObtenerItinerariosporRutaAsync(int idRuta, string usuario)
        {
            var itinerarios = new List<ItinerarioVista>();
            var user = await _user.ObtenerUsuarioConfiguradorPorNombreAsync(usuario);

            if (user != null)
            {
                itinerarios = await _repo.ObtenerItinerariosporRutaAsync(idRuta);
            }

            return itinerarios;
        }

        public async Task AgregaItinerarioAsync(ItinerarioDtoForCreate it)
        {
            var user = await _user.ObtenerUsuarioConfiguradorPorNombreAsync(it.Usuario);

            if (user != null)
            {
                var i = new Itinerario()
                {
                    IdRuta = it.IdRuta,
                    IdPunto = it.IdPunto,
                    Posicion = it.Posicion
                };

                var existe = await ExistePosicionEnRuta(it.IdRuta, it.Posicion);
                if (!existe)
                {
                    await _repo.AgregaItinerarioAsync(it.IdRuta, it.IdPunto, it.Posicion);
                }
            }
        }

        public async Task ActualizaItinerarioAsync(ItinerarioDtoForCreate it)
        {
            var user = await _user.ObtenerUsuarioConfiguradorPorNombreAsync(it.Usuario);

            if (user != null)
            {                   
                var existe = await ExistePuntoEnMismaPosicionEnRuta(it.IdRuta, it.Posicion, it.IdPunto);
                if (!existe)
                {
                    await _repo.ActualizaItinerarioAsync(it.IdRuta, it.IdPunto, it.Posicion);
                }
            }
        }

        public async Task BorraItinerarioAsync(int id, string usuario)
        {
            var user = await _user.ObtenerUsuarioConfiguradorPorNombreAsync(usuario);

            if (user != null)
            {
                await _repo.BorraItinerarioAsync(id);
            }
        }

        private async Task<bool> ExistePosicionEnRuta(int idRuta, int posicion) 
        {
           var l = await _repo.ObtenerItinerariosporRutaAndPosicionAsync(idRuta, posicion);

            if (l != null) 
            { 
                return true; 
            } else 
            { 
                return false; 
            }
        }

        private async Task<bool> ExistePuntoEnMismaPosicionEnRuta(int idRuta, int posicion, int IdPunto)
        {
            var l = await _repo.ObtenerItinerariosporRutaAndPosicionAndPuntoAsync(idRuta, posicion, IdPunto);

            if (l != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
