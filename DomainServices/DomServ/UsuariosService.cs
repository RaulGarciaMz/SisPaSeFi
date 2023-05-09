using Domain.Common;
using Domain.DTOs;
using Domain.Entities;
using Domain.Entities.Vistas;
using Domain.Ports.Driven.Repositories;
using Domain.Ports.Driving;
using System.Globalization;

namespace DomainServices.DomServ
{
    public class UsuariosService : IUsuariosService
    {
        private readonly IUsuariosRepo _repo;

        public UsuariosService(IUsuariosRepo repo)
        {
            _repo = repo;
        }

        public async Task ActualizaUsuariosPorOpcionAsync(string opcion, string usuario, List<UsuarioDto> users)
        {
            var user = await _repo.ObtenerUsuarioConfiguradorPorNombreAsync(usuario);
            if (user != null)
            {
                var idComandancia = -1;
                if (opcion.Contains("-"))
                {
                    var datos = opcion.Split("-");
                    opcion = datos[0];
                    idComandancia = Int32.Parse(datos[1]);
                }

                if (opcion == "CambiarClave")
                {
                    await CambiaClaveDeUsuario(users, usuario);
                }
                else 
                {
                    switch (opcion)
                    {
                        case "Desbloquear":
                            await DesbloqueaListaDeUsuariosAsync(users);
                            break;
                        case "Bloquear":
                            await BloqueaListaDeUsuariosAsync(users);
                            break;
                        case "ReiniciarClave":
                            await ReiniciarClaveDeListaUsuariosAsync(users);
                            break;
                        case "Actualizar":
                            await _repo.ActualizaListasDeUsuariosAsync(users);
                            break;
                        case "RegistrarComandancia":
                            await RegistraListaDeUsuariosComandanciaAsync(users, idComandancia);
                            break;
                        case "QuitarComandancia":
                            await BorraListaDeUsuariosComandanciaAsync(users, idComandancia);
                            break;
                        case "RegistrarRol":
                            await RegistraListaDeUsuariosRolAsync(users, idComandancia);
                            break;
                        case "QuitarRol":
                            await BorraListaDeUsuariosRolAsync(users, idComandancia);
                            break;
                        case "RegistrarGrupoCorreo":
                            await RegistraListaDeUsuarioGrupoCorreoAsync(users, idComandancia);
                            break;
                        case "QuitarGrupoCorreo":
                            await BorraListaDeUsuarioGrupoCorreoAsync(users, idComandancia);
                            break;
                    }
                }

            }
        }

        public async Task AgregaPorOpcionAsync(string opcion, string dato, string usuario, UsuarioDto userDto)
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
                    case "CrearUsuario":
                        await _repo.AgregaUsuarioAsync(userDto);
                        break;

                }
            }
        }

        public async Task BorraPorOpcionAsync(string opcion, string dato, string usuario)
        {
            var user = await _repo.ObtenerUsuarioConfiguradorPorNombreAsync(usuario);
            if (user != null)
            {
                if (!dato.Contains("-")) return;
                var datos = dato.Split("-");
                var idElemento = Int32.Parse(datos[0]);
                var idUsuario = Int32.Parse(datos[1]);

                switch (opcion)
                {
                    case "EliminaUsuarioDeDocumento":

                        await _repo.BorraUsuarioDeDocumentoAsync(idElemento, idUsuario);
                        break;

                    case "EliminaUsuarioDePatrullaje":
                        await _repo.BorraUsuarioDePatrullajeAsync(idElemento, idUsuario);
                        break;
                }
            }
        }

        public async Task<UsuarioDto?> ObtenerUsuarioConfiguradorPorIdAsync(int idUsuario)
        {
            var usDto = new UsuarioDto();
            var user = await _repo.ObtenerUsuarioConfiguradorPorIdAsync(idUsuario);

            if (user != null) 
            {
                usDto.strApellido1 = user.Apellido1;
                usDto.strApellido2 = user.Apellido2;
                usDto.intConfigurador = user.Configurador;
                usDto.intIdUsuario = user.IdUsuario;
                usDto.strNombreDeUsuario = user.UsuarioNom;
                usDto.intDesbloquearRegistros = user.DesbloquearRegistros;
                usDto.strCel = user.Cel;
                usDto.strNombre = user.Nombre;
                usDto.intRegionSSF = user.RegionSsf;
                usDto.strCorreoElectronico = user.CorreoElectronico;
                usDto.intTiempoEspera = user.TiempoEspera;
            }

            return usDto;
        }

        public async Task<UsuarioDto?> ObtenerUsuarioConfiguradorPorNombreAsync(string usuario)
        {
            var usDto = new UsuarioDto();
            var user = await _repo.ObtenerUsuarioConfiguradorPorNombreAsync(usuario);

            if (user != null)
            {
                usDto.strApellido1 = user.Apellido1;
                usDto.strApellido2 = user.Apellido2;
                usDto.intConfigurador = user.Configurador;
                usDto.intIdUsuario = user.IdUsuario;
                usDto.strNombreDeUsuario = user.UsuarioNom;
                usDto.intDesbloquearRegistros = user.DesbloquearRegistros;
                usDto.strCel = user.Cel;
                usDto.strNombre = user.Nombre;
                usDto.intRegionSSF = user.RegionSsf;
                usDto.strCorreoElectronico = user.CorreoElectronico;
                usDto.intTiempoEspera = user.TiempoEspera;
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

        private async Task CambiaClaveDeUsuario(List<UsuarioDto> users, string usuario)
        { 
            if(users.Count() > 1) 
            {
                var encr = new Encriptador();

                var cveDesencriptada1 = encr.Desencriptar(users[0].strClave);
                var cveDesencriptada2 = encr.Desencriptar(users[1].strClave);

                var datosClave1 = cveDesencriptada1.Split("!#");
                var datosClave2 = cveDesencriptada2.Split("!#");

                if (datosClave1.Length > 0 && datosClave2.Length > 0)
                {
                    var cveAnterior = encr.GeneraCadenaClave(datosClave1);
                    var fecha1 = encr.GeneraFecha(datosClave1);

                    var cveNueva = encr.GeneraCadenaClave(datosClave2);
                    var fecha2 = encr.GeneraFecha(datosClave2);

                    var hace10Minutos = DateTime.Now.AddMinutes(-10);

                    if (fecha1 > hace10Minutos && fecha2 > hace10Minutos)
                    {
                        await _repo.ActualizaClaveDeUsuario(usuario, cveNueva, cveAnterior);
                    }
                }
            }
        }

        private async Task DesbloqueaListaDeUsuariosAsync(List<UsuarioDto> users)
        {
            var desbloquear = new List<string>();
            foreach (var u in users)
            {
                desbloquear.Add(u.strNombreDeUsuario);
            }

            await _repo.ActualizaListasDeUsuariosDesbloquearAsync(desbloquear);
        }

        private async Task BloqueaListaDeUsuariosAsync(List<UsuarioDto> users)
        {
            var bloquear = new List<string>();
            foreach (var u in users)
            {
                bloquear.Add(u.strNombreDeUsuario);
            }

            await _repo.ActualizaListasDeUsuariosBloquearAsync(bloquear);
        }

        private async Task ReiniciarClaveDeListaUsuariosAsync(List<UsuarioDto> users)
        {
            var bloquear = new List<string>();
            foreach (var u in users)
            {
                bloquear.Add(u.strNombreDeUsuario);
            }

            await _repo.ActualizaListasDeUsuariosReiniciarClaveAsync(bloquear);
        }

        private async Task RegistraListaDeUsuariosComandanciaAsync(List<UsuarioDto> users, int idComandancia)
        {
            var usToRegistrar = new List<UsuarioComandancia>();
            foreach (var u in users)
            {
                var uc = await _repo.ObtenerUsuariosComandanciaPorIdUsuarioAndIdComandanciaAsync(u.intIdUsuario, idComandancia);

                if (uc == null || uc.Count == 0)
                {
                    var nu = new UsuarioComandancia()
                    {
                        IdComandancia = idComandancia,
                        IdUsuario = u.intIdUsuario,
                    };

                    usToRegistrar.Add(nu);
                }
            }

            await _repo.AgregaListaDeUsuariosComandanciaAsync(usToRegistrar);
        }

        private async Task BorraListaDeUsuariosComandanciaAsync(List<UsuarioDto> users, int idComandancia)
        {
            var usToBorrar = new List<UsuarioComandancia>();
            foreach (var u in users)
            {
                var uc = await _repo.ObtenerUsuariosComandanciaPorIdUsuarioAndIdComandanciaAsync(u.intIdUsuario, idComandancia);

                if (uc != null && uc.Count > 0)
                {
                    usToBorrar.AddRange(uc);
                }
            }

            if (usToBorrar.Count > 0)
            {
                await _repo.BorraListaDeUsuariosComandanciaAsync(usToBorrar);
            }
        }

        private async Task RegistraListaDeUsuariosRolAsync(List<UsuarioDto> users, int idRol)
        {
            var usToRegistrar = new List<UsuarioRol>();
            foreach (var u in users)
            {
                var uc = await _repo.ObtenerUsuariosRolPorIdUsuarioAndIdRolAsync(u.intIdUsuario, idRol);

                if (uc == null || uc.Count == 0)
                {
                    var nu = new UsuarioRol()
                    {
                        IdRol = idRol,
                        IdUsuario = u.intIdUsuario,
                    };

                    usToRegistrar.Add(nu);
                }
            }

            await _repo.AgregaListaDeUsuariosRolAsync(usToRegistrar);
        }

        private async Task BorraListaDeUsuariosRolAsync(List<UsuarioDto> users, int idComandancia)
        {
            var usToBorrar = new List<UsuarioRol>();
            foreach (var u in users)
            {
                var uc = await _repo.ObtenerUsuariosRolPorIdUsuarioAndIdRolAsync(u.intIdUsuario, idComandancia);

                if (uc != null && uc.Count > 0)
                {
                    usToBorrar.AddRange(uc);
                }
            }

            if (usToBorrar.Count > 0)
            {
                await _repo.BorraListaDeUsuariosRolAsync(usToBorrar);
            }
        }

        private async Task RegistraListaDeUsuarioGrupoCorreoAsync(List<UsuarioDto> users, int idGrupo)
        {
            var usToRegistrar = new List<UsuarioGrupoCorreoElectronico>();
            foreach (var u in users)
            {
                var uc = await _repo.ObtenerUsuariosGrupoCorreoElectronicoPorIdUsuarioAndIdGrupoAsync(u.intIdUsuario, idGrupo);

                if (uc == null || uc.Count == 0)
                {
                    var nu = new UsuarioGrupoCorreoElectronico()
                    {
                        IdGrupoCorreo = idGrupo,
                        IdUsuario = u.intIdUsuario,
                    };

                    usToRegistrar.Add(nu);
                }
            }

            await _repo.AgregaListaDeUsuariosGrupoCorreoElectronicoAsync(usToRegistrar);
        }

        private async Task BorraListaDeUsuarioGrupoCorreoAsync(List<UsuarioDto> users, int idGrupo)
        {
            var usToBorrar = new List<UsuarioGrupoCorreoElectronico>();
            foreach (var u in users)
            {
                var uc = await _repo.ObtenerUsuariosGrupoCorreoElectronicoPorIdUsuarioAndIdGrupoAsync(u.intIdUsuario, idGrupo);

                if (uc != null && uc.Count > 0)
                {
                    usToBorrar.AddRange(uc);
                }
            }

            if (usToBorrar.Count > 0)
            {
                await _repo.BorraListaDeUsuariosGrupoCorreoElectronicoAsync(usToBorrar);
            }
        }

        private UsuarioDto ConvierteUsuarioToDto(UsuarioVista user)
        {
            return new UsuarioDto()
            {
                strApellido1 = user.apellido1,
                strApellido2 = user.apellido2,
                intConfigurador = user.configurador,
                intIdUsuario = user.id_usuario,
                strNombreDeUsuario = user.usuario_nom,
                intDesbloquearRegistros = user.desbloquearregistros,
                strCel = user.cel,
                strNombre = user.nombre,
                intRegionSSF = user.regionSSF,
                strCorreoElectronico = user.correoelectronico,
                intTiempoEspera = user.tiempoEspera
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
