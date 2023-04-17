﻿using Domain.DTOs;
using Domain.Entities.Vistas;
using Domain.Ports.Driven.Repositories;
using Domain.Ports.Driving;

namespace DomainServices.DomServ
{
    public class UsuariosService : IUsuariosService
    {
        private readonly IUsuariosRepo _repo;

        public UsuariosService(IUsuariosRepo repo)
        {
            _repo = repo;
        }

        public async Task ActualizaUsuarioPorOpcionAsync(string opcion, string usuario)
        {
            var user = await _repo.ObtenerUsuarioConfiguradorPorNombreAsync(usuario);
            if (user != null)
            {
                switch (opcion) 
                {
                    case "Desbloquear":
                        await _repo.DesbloqueaUsuarioAsync(usuario);
                        break;
                    case "Bloquear":
                        await _repo.BloqueaUsuarioAsync(usuario);
                        break;
                    case "ReiniciarClave":
                        await _repo.ReiniciaClaveUsuarioAsync(usuario);
                        break;
                }                
            }
        }

        public async Task AgregaPorOpcionAsync(string opcion, string dato, string usuario)
        {
            var user = await _repo.ObtenerUsuarioConfiguradorPorNombreAsync(usuario);
            if (user != null)
            {
                switch (opcion)
                {
                    case "AsignaUsuarioDeDocumento":

                        if (!dato.Contains("-")) return;

                        var datos = dato.Split("-");
                        var idDocumento = Int32.Parse(datos[0]);
                        var idUsuario = Int32.Parse(datos[1]);
                        await _repo.AgregaUsuarioDeDocumentoAsync(idDocumento, idUsuario);
                        break;

                }
            }
        }

        public async Task BorraPorOpcionAsync(string opcion, string dato, string usuario)
        {
            var user = await _repo.ObtenerUsuarioConfiguradorPorNombreAsync(usuario);
            if (user != null)
            {
                switch (opcion)
                {
                    case "EliminaUsuarioDeDocumento":

                        if (!dato.Contains("-")) return;

                        var datos = dato.Split("-");
                        var idDocumento = Int32.Parse(datos[0]);
                        var idUsuario = Int32.Parse(datos[1]);
                        await _repo.BorraUsuarioDeDocumentoAsync(idDocumento, idUsuario);
                        break;

                }
            }
        }

        public async Task BloqueaUsuarioAsync(string usuario)
        {
            var user = await _repo.ObtenerUsuarioConfiguradorPorNombreAsync(usuario);
            if (user != null) 
            {
                await _repo.BloqueaUsuarioAsync(usuario);
            }                     
        }

        public async Task DesbloqueaUsuarioAsync(string usuario)
        {
            var user = await _repo.ObtenerUsuarioConfiguradorPorNombreAsync(usuario);
            if (user != null)
            {
                await _repo.DesbloqueaUsuarioAsync(usuario);
            }
        }

        public async Task ReiniciaClaveUsuarioAsync(string usuario)
        {
            var user = await _repo.ObtenerUsuarioConfiguradorPorNombreAsync(usuario);
            if (user != null)
            {
                await _repo.ReiniciaClaveUsuarioAsync(usuario);
            }
        }

        public async Task<UsuarioDto?> ObtenerUsuarioConfiguradorPorIdAsync(int idUsuario)
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

        public async Task<UsuarioDto?> ObtenerUsuarioConfiguradorPorNombreAsync(string usuario)
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

        public async Task<List<UsuarioDto>> ObtenerUsuarioPorOpcionAsync(string opcion, string criterio, string usuario)
        {
            var l = new List<UsuarioDto>();
            var usDto = new UsuarioDto();
            int idDocumento = 0;

            var user = await _repo.ObtenerUsuarioConfiguradorPorNombreAsync(usuario);
            
            if (user != null)
            {
                if (opcion.Contains("-"))
                {
                    var adicionales = opcion.Split('-');
                    opcion = adicionales[0];
                    idDocumento = Int32.Parse(adicionales[1]);
                }               

                switch (opcion) 
                {
                    case "BuscarUsuarios":
                        l = await ObtenerUsuariosPorCriterioAsync(criterio);
                        break;
                    case "UsuariosDeDocumento":
                        l = await ObtenerUsuariosDeDocumentoAsync(idDocumento);
                        break;
                    case "UsuariosNoIncluidosEnDocumento":
                        l = await ObtenerUsuariosNoIncluidosEnDocumentoAsync(criterio, idDocumento);
                        break;
                }
            }
            
            return l;
        }

        private async Task<List<UsuarioDto>> ObtenerUsuariosPorCriterioAsync(string criterio)
        {
            var nvoCrit = "%" + criterio + "%";
            var users = await _repo.ObtenerUsuariosPorCriterioAsync(nvoCrit);
            return ConvierteListaUsuarioToDto(users);
        }

        private async Task<List<UsuarioDto>> ObtenerUsuariosDeDocumentoAsync(int idDocumento)
        {
            var users = await _repo.ObtenerUsuariosDeDocumentoAsync(idDocumento);
            return ConvierteListaUsuarioToDto(users);
        }

        private async Task<List<UsuarioDto>> ObtenerUsuariosNoIncluidosEnDocumentoAsync(string criterio, int idDocumento)
        {
            var nvoCrit = "%" + criterio + "%";
            var users = await _repo.ObtenerUsuariosNoIncluidosEnDocumentoAsync(nvoCrit, idDocumento);
            return ConvierteListaUsuarioToDto(users);
        }

        private UsuarioDto ConvierteUsuarioToDto(UsuarioVista user)
        {
            return new UsuarioDto()
            {
                apellido1 = user.apellido1,
                apellido2 = user.apellido2,
                configurador = user.configurador,
                id_usuario = user.id_usuario,
                usuario_nom = user.usuario_nom,
                desbloquearregistros = user.desbloquearregistros,
                cel = user.cel,
                nombre = user.nombre,
                regionSSF = user.regionSSF,
                correoelectronico = user.correoelectronico,
                tiempoespera = user.tiempoEspera
            };
        }

        private List<UsuarioDto> ConvierteListaUsuarioToDto(List<UsuarioVista> users)
        {
            var usersDto = new List<UsuarioDto>();

            foreach (var user in users)
            {
                var usDto = ConvierteUsuarioToDto(user);

                usersDto.Add(usDto);
            }

            return usersDto;
        }
    }
}
