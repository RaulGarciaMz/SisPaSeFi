using Domain.DTOs;
using Domain.Entities;
using Domain.Ports.Driven.Repositories;
using Domain.Ports.Driving;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainServices.DomServ
{
    public class UsuariosService : IUsuariosService
    {
        private readonly IUsuariosRepo _repo;

        public UsuariosService(IUsuariosRepo repo)
        {
            _repo = repo;
        }

        public async Task BloqueaUsuario(string usuario)
        {
            await _repo.BloqueaUsuarioAsync(usuario);            
        }

        public async Task DesbloqueaUsuario(string usuario)
        {
            await _repo.DesbloqueaUsuarioAsync(usuario);
        }

        public async Task ReiniciaClaveUsuario(string usuario)
        {
            await _repo.ReiniciaClaveUsuarioAsync(usuario);
        }

        public async Task<UsuarioDto?> ObtenerUsuarioConfiguradorPorId(int idUsuario)
        {
            var usDto = new UsuarioDto();
            var user = await _repo.ObtenerUsuarioConfiguradorPorIdAsync(idUsuario);

            if (user != null) 
            {
                usDto.apellido1 = user.Apellido1;
                usDto.apellido2 = user.Apellido2;
                usDto.configurador = user.Configurador;
                usDto.id_usuario = user.IdUsuario;
                usDto.usuario_nom = user.UsuarioNom;
                usDto.desbloquearregistros = user.DesbloquearRegistros;
                usDto.cel = user.Cel;
                usDto.nombre = user.Nombre;
                usDto.regionSSF = user.RegionSsf;
                usDto.correoelectronico = user.CorreoElectronico;
                usDto.tiempoespera = user.TiempoEspera;
            }

            return usDto;
        }

        public async Task<UsuarioDto?> ObtenerUsuarioConfiguradorPorNombre(string usuario)
        {
            var usDto = new UsuarioDto();
            var user = await _repo.ObtenerUsuarioConfiguradorPorNombreAsync(usuario);

            if (user != null)
            {
                usDto.apellido1 = user.Apellido1;
                usDto.apellido2 = user.Apellido2;
                usDto.configurador = user.Configurador;
                usDto.id_usuario = user.IdUsuario;
                usDto.usuario_nom = user.UsuarioNom;
                usDto.desbloquearregistros = user.DesbloquearRegistros;
                usDto.cel = user.Cel;
                usDto.nombre = user.Nombre;
                usDto.regionSSF = user.RegionSsf;
                usDto.correoelectronico = user.CorreoElectronico;
                usDto.tiempoespera = user.TiempoEspera;
            }

            return usDto;
        }

        public async Task<UsuarioDto?> ObtenerUsuarioPorCriterio(string criterio)
        {
            var usDto = new UsuarioDto();
            var user = await _repo.ObtenerUsuarioPorCriterioAsync(criterio);

            if (user != null)
            {
                usDto.apellido1 = user.Apellido1;
                usDto.apellido2 = user.Apellido2;
                usDto.configurador = user.Configurador;
                usDto.id_usuario = user.IdUsuario;
                usDto.usuario_nom = user.UsuarioNom;
                usDto.desbloquearregistros = user.DesbloquearRegistros;
                usDto.cel = user.Cel;
                usDto.nombre = user.Nombre;
                usDto.regionSSF = user.RegionSsf;
                usDto.correoelectronico = user.CorreoElectronico;
                usDto.tiempoespera = user.TiempoEspera;
            }

            return usDto;
        }
    }
}
