using Domain.Common;
using Domain.DTOs;
using Domain.Ports.Driven;
using Domain.Ports.Driving;
using Microsoft.IdentityModel.Tokens;
using System.DirectoryServices;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace DomainServices.DomServ
{
    public class RegistroService : IRegistroService
    {
        private readonly IUsuariosRegistroQuery _repo;

        public RegistroService(IUsuariosRegistroQuery repo)
        {
            _repo = repo;
        }

        public async Task<UsuarioRegistradoDto> ObtenerUsuarioRegistradoAsync(UsuarioDtoForGet u, string pathLdap)
        {    
            var enc = new Encriptador();
            var user = new UsuarioRegistradoDto() 
            { 
                strNombreDeUsuario = u.strNombreDeUsuario,
                strResultado = "Usuario no encontrado."
            };

            var cveDesencriptada = enc.Desencriptar(u.strClaveEncriptada);
            var datosClave = cveDesencriptada.Split("!#");

            if (datosClave.Length > 0) 
            {
                var strClave = enc.GeneraCadenaClave(datosClave);
                var fecha = enc.GeneraFecha(datosClave);

                var ahora = DateTime.Now;
                if (fecha > ahora.AddMinutes(-10))
                {
                    var esUsuarioLocal = await EsUsuarioLocalValido(u.strNombreDeUsuario, strClave);
                    var esUsuarioEnDirectorioActivo = EsUsuarioEnDirectorioActivo(pathLdap,u.strNombreDeUsuario, strClave);

                    if (esUsuarioLocal || esUsuarioEnDirectorioActivo)
                    {
                        var userV = await _repo.ObtenerUsuarioRegistradoAsync(u.strNombreDeUsuario);

                        if (userV != null && userV.Bloqueado == 0)
                        {
                            user.strNombre = userV.Nombre;
                            user.strApellido1 = userV.Apellido1;
                            user.strApellido2 = userV.Apellido2;
                            user.strCel = userV.Cel;
                            user.intConfigurador = userV.Configurador;
                            user.intBloqueado = userV.Bloqueado.Value;
                            user.intAceptacionAvisoLegal = userV.AceptacionAvisoLegal.Value;
                            user.intIntentos = userV.Intentos.Value;
                            user.intNotificarAccesos = userV.NotificarAcceso.Value;
                            user.strUltimoAcceso = userV.EstampaTiempoUltimoAcceso.ToString("yyyy-MM-dd HH:mm:ss");
                            user.strCorreoElectronico = userV.CorreoElectronico;
                            user.intRegionSSF = userV.RegionSsf;
                            user.intTiempoEspera = userV.TiempoEspera;
                            user.intDesbloquearRegistros = userV.DesbloquearRegistros;
                            user.strResultado = "Usuario válido.";
                            user.strToken = GeneraToken(u.strNombreDeUsuario, strClave);
                        } else {
                            user.strResultado = "Usuario bloqueado.";
                        }
                    }
                }
            }

            return user;
        }

        private async Task<bool> EsUsuarioLocalValido(string usuario, string clave) 
        {
            var cuantos = await _repo.IdentificaUsuarioLocalAsync(usuario, clave);
            return cuantos == 1;
        }

        /// <summary>
        /// Valida si el usuario es válido en el directorio activo
        /// </summary>
        /// <param name="path">Un path de LDAP para el FQDN del directorio activo. Por ejemplo:  LDAP://jasl.com </param>
        /// <param name="user">Nombre de la cuenta del usuario. Puede contener prefijo el dominio, por ej. jasl\AlfredoSL ó sólo AlfredoSL</param>
        /// <param name="pass">Password del usuario</param>
        /// <returns></returns>
        private bool EsUsuarioEnDirectorioActivo(string path, string user, string pass)
        {
            var de = new DirectoryEntry(path, user, pass, AuthenticationTypes.Secure);
            try
            {
                var ds = new DirectorySearcher(de);
                ds.FindOne();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        private string GeneraToken(string user, string pass)
        {
            string secretKey = "DwkdopIDAISOPDQWD59AS8D9AWD2ASD9sd59qwd";
            byte[] key = Encoding.ASCII.GetBytes(secretKey);
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user),
                new Claim(ClaimTypes.Surname, pass)
            };

            var tokenDescriptor = new SecurityTokenDescriptor();
            tokenDescriptor.Subject = new ClaimsIdentity(claims);
            tokenDescriptor.Expires = DateTime.UtcNow.AddDays(1d);
            tokenDescriptor.SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature);
           
            var tokenHandler = new JwtSecurityTokenHandler();
            var createdToken = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(createdToken);
        }

    }
}
