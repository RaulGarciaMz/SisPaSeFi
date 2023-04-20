using Domain.Ports.Driven.Repositories;
using Domain.Ports.Driven;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.DTOs;
using System.DirectoryServices;
using System.Security.Cryptography;
using System.Globalization;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Domain.Ports.Driving;

namespace DomainServices.DomServ
{
    public class RegistroService : IRegistroService
    {
        private readonly IUsuariosRegistroQuery _repo;

        public RegistroService(IUsuariosRegistroQuery repo)
        {
            _repo = repo;
        }

        public async Task<UsuarioDtoRegistro> ObtenerUsuarioRegistradoAsync(UsuarioDtoForAutentication u, string pathLdap)
        {
            var user = new UsuarioDtoRegistro();

            var cveDesencriptada = Desencriptar(u.Clave);
            var datosClave = cveDesencriptada.Split("!#");
            string strClave = "";

            var longitud = datosClave.Length;

            if (longitud > 0) 
            {
                for (int j = 0; j <= longitud - 3; j++)
                    strClave = strClave + datosClave[j];

                var fechahora = datosClave[longitud - 2] + " " + datosClave[longitud - 1];
                var fecha = DateTime.ParseExact(fechahora, "d", new CultureInfo("es-MX"));

                var ahora = DateTime.Now;
                if (fecha > ahora.AddMinutes(-10))
                {
                    var esLocal = await EsUsuarioLocalValido(u.Nombre, strClave);
                    var esDirectorioActivo = EsUsuarioEnDirectorioActivo(pathLdap,u.Nombre, strClave);

                    if (esLocal || esDirectorioActivo)
                    {
                        var userV = await _repo.ObtenerUsuarioParaRegistroAsync(u.Nombre);

                        if (userV != null && userV.bloqueado == 0)
                        {
                            user.Nombre = userV.nombre;
                            user.Apellido1 = userV.apellido1;
                            user.Apellido2 = userV.apellido2;
                            user.Cel = userV.cel;
                            user.Configurador = userV.configurador;
                            user.Bloqueado = userV.bloqueado;
                            user.AceptacionAvisoLegal = userV.AceptacionAvisoLegal;
                            user.Intentos = userV.intentos;
                            user.NotificarAcceso = userV.NotificarAcceso;
                            user.UltimoAcceso = userV.EstampaTiempoUltimoAcceso;
                            user.CorreoElectronico = userV.correoelectronico;
                            user.RegionSSF = userV.regionSSF;
                            user.TiempoEspera = userV.tiempoEspera;
                            user.DesbloquearRegistros = userV.desbloquearregistros;
                            user.Resultado = "Usuario válido.";
                            user.Token = GeneraToken(u.Nombre, strClave);
                        } else {
                            user.Resultado = "Usuario bloqueado.";
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

        private string Desencriptar(string texto)
        {
            string resultado = "";

            if (!string.IsNullOrEmpty(texto.Trim()))
            {
                var des = new TripleDESCryptoServiceProvider();   // Algoritmo TripleDES
                var hashmd5 = new MD5CryptoServiceProvider();     // objeto md5
                string myKey = "#SistemaPatrullajeSSF.2022!";    // Clave secreta

                des.Key = hashmd5.ComputeHash(new UnicodeEncoding().GetBytes(myKey));
                des.Mode = CipherMode.ECB;
                var desencrypta = des.CreateDecryptor();
                byte[] buff = Convert.FromBase64String(texto);
                resultado = Encoding.ASCII.GetString(desencrypta.TransformFinalBlock(buff, 0, buff.Length));
            }

            return resultado;
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
