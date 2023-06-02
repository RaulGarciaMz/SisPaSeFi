using Domain.DTOs;
using Domain.Entities;
using Domain.Entities.Vistas;
using Domain.Ports.Driven.Repositories;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using SqlServerAdapter.Data;
using System.Data;
using System.Security.Cryptography;
using System.Text;

namespace SqlServerAdapter
{
    public class UsuariosRepository : IUsuariosRepo
    {
        protected readonly UsuariosContext _userContext;

        public UsuariosRepository(UsuariosContext userContext)
        {
            _userContext = userContext ?? throw new ArgumentNullException(nameof(userContext));
        }

        public async Task ActualizaListasDeUsuariosDesbloquearAsync(List<string> desbloquear)
        {
            if (desbloquear.Count > 0)
            {
                var pDesbloq = await _userContext.Usuarios.Where(x => desbloquear.Contains(x.UsuarioNom)).ToListAsync();

                foreach (var de in pDesbloq)
                {
                    de.Bloqueado = 0;
                    de.Intentos = 0;
                }
                _userContext.Usuarios.AddRange(pDesbloq);
                await _userContext.SaveChangesAsync();
            }
        }

        public async Task ActualizaListasDeUsuariosBloquearAsync(List<string> bloquear)
        {
            if (bloquear.Count > 0)
            {
                var pBloq = await _userContext.Usuarios.Where(x => bloquear.Contains(x.UsuarioNom)).ToListAsync();
                foreach (var bl in pBloq)
                {
                    bl.Bloqueado = 1;
                }

                _userContext.Usuarios.AddRange(pBloq);
                await _userContext.SaveChangesAsync();
            }
        }

        public async Task ActualizaListasDeUsuariosReiniciarClaveAsync(List<string> reiniciar)
        {
            if (reiniciar.Count > 0)
            {
                var pReini = await _userContext.Usuarios.Where(x => reiniciar.Contains(x.UsuarioNom)).ToListAsync();
                foreach (var re in pReini)
                {
                    re.Pass = ComputeMD5(re.UsuarioNom);
                }

                _userContext.Usuarios.AddRange(pReini);
                await _userContext.SaveChangesAsync();
            }
        }

        public async Task ActualizaListasDeUsuariosAsync(List<UsuarioDtoForUpdate> lstUsuarios)
        {
            if (lstUsuarios.Count > 0)
            {
                var lstUsuariosUpdate = new List<Usuario>();

                foreach (var user in lstUsuarios)
                {
                    var nombreRepetido = await _userContext.Usuarios.Where(x => x.UsuarioNom == user.strNombreDeUsuario && x.IdUsuario != user.intIdUsuario).ToListAsync();

                    if (nombreRepetido == null || nombreRepetido.Count == 0)
                    { 
                        var utu = await _userContext.Usuarios.Where(x => x.IdUsuario == user.intIdUsuario).SingleOrDefaultAsync();
                        if (utu != null)
                        { 
                            utu.UsuarioNom = user.strNombreDeUsuario;
                            utu.Nombre = user.strNombre;
                            utu.Apellido1 = user.strApellido1;
                            utu.Apellido2 = user.strApellido2;
                            utu.CorreoElectronico = user.strCorreoElectronico;
                            utu.Cel = user.strCel;
                            utu.RegionSsf = user.intRegionSSF;
                            utu.Configurador = user.intConfigurador;
                            utu.DesbloquearRegistros = user.intDesbloquearRegistros;
                            utu.TiempoEspera = user.intTiempoEspera;
                            
                            lstUsuariosUpdate.Add(utu);
                        }
                    }
                }

                if (lstUsuariosUpdate.Count > 0)
                {
                    _userContext.Usuarios.AddRange(lstUsuariosUpdate);
                    await _userContext.SaveChangesAsync();
                }
            }
        }      

        public async Task AgregaUsuarioAsync(UsuarioDto user)
        {
            var u = new Usuario() 
            { 
                UsuarioNom =user.strNombreDeUsuario,
                Pass = ComputeMD5(user.strNombreDeUsuario),
                Nombre = user.strNombre,
                Apellido1 = user.strApellido1,
                Apellido2 = user.strApellido2,
                CorreoElectronico = user.strCorreoElectronico,
                Cel = user.strCel,
                RegionSsf = user.intRegionSSF,
                Configurador    = user.intConfigurador,
                DesbloquearRegistros = user.intDesbloquearRegistros,
                TiempoEspera = user.intTiempoEspera,
                //Campos no nulos
                EstampaTiempoAceptacionUso = DateTime.UtcNow,
                EstampaTiempoUltimoAcceso = DateTime.UtcNow
            };

            _userContext.Usuarios.Add(u);
            await _userContext.SaveChangesAsync();
        }

        public async Task AgregaUsuarioDeDocumentoAsync(int idDocumento, int idUsuario)
        {
            var u = new UsuarioDocumento()
            {
                IdDocumentoPatrullaje = idDocumento,
                IdUsuario = idUsuario,
            };
            
            _userContext.UsuarioDocumentos.Add(u);
            await _userContext.SaveChangesAsync();
        }

        public async Task BorraUsuarioDeDocumentoAsync(int idDocumento, int idUsuario)
        {
            var u = await _userContext.UsuarioDocumentos.Where(x => x.IdDocumentoPatrullaje == idDocumento && x.IdUsuario == idUsuario).FirstOrDefaultAsync();
            if (u != null)
            {
                _userContext.UsuarioDocumentos.Remove(u);
                await _userContext.SaveChangesAsync();
            }
        }

        public async Task BorraUsuarioDePatrullajeAsync(int idPrograma, int idUsuario)
        {
            var u = await _userContext.UsuariosPatrullaje.Where(x => x.IdPrograma == idPrograma && x.IdUsuario == idUsuario).FirstOrDefaultAsync();
            if (u != null)
            {
                _userContext.UsuariosPatrullaje.Remove(u);
                await _userContext.SaveChangesAsync();
            }
        }

        public async Task ActualizaClaveDeUsuario(string usuario, string cveNueva, string cveAnterior)
        {
            var md5PassNuevo = ComputeMD5(cveNueva);
            var md5PassAnterior = ComputeMD5(cveAnterior);

            var u = await _userContext.Usuarios.Where(x => x.UsuarioNom == usuario && x.Pass == md5PassAnterior).SingleOrDefaultAsync();

            if (u != null)
            {
                u.Pass = md5PassNuevo;
                _userContext.Update(u);

                await _userContext.SaveChangesAsync();
            }
        }

        public async Task<Usuario?> ObtenerUsuarioConfiguradorPorIdAsync(int idUsuario)
        {
            return await _userContext.Usuarios.Where(x => x.IdUsuario == idUsuario && x.Configurador == 1).AsNoTracking().FirstOrDefaultAsync();
        }

        public async Task<Usuario?> ObtenerUsuarioConfiguradorPorNombreAsync(string usuario)
        {
            return await _userContext.Usuarios.Where(x => x.UsuarioNom == usuario && x.Configurador == 1).AsNoTracking().FirstOrDefaultAsync();
        }

        public async Task<Usuario?> ObtenerUsuarioConfiguradorVerificaMd5Async(string usuario, string pass)
        {
            var md5 = ComputeMD5(pass);

            return await _userContext.Usuarios.Where(x => x.UsuarioNom == usuario && x.Configurador == 1 && (x.Pass == md5 || x.PassTemp == md5)).AsNoTracking().FirstOrDefaultAsync();
        }

        public async Task<Usuario?> ObtenerUsuarioPorUsuarioNomAsync(string usuario)
        {
            return await _userContext.Usuarios.Where(x => x.UsuarioNom == usuario).AsNoTracking().FirstOrDefaultAsync();
        }

        public async Task<int?> ObtenerIdUsuarioPorUsuarioNomAsync(string usuario)
        {
            return await _userContext.Usuarios.Where(x => x.UsuarioNom == usuario).Select(x => x.IdUsuario).FirstOrDefaultAsync();
        }

        public async Task<Usuario?> ObtenerUsuarioPorNombreConDiferenteIdAsync(string usuario, int idUsuario)
        {
            return await _userContext.Usuarios.Where(x => x.UsuarioNom == usuario && x.IdUsuario != idUsuario).FirstOrDefaultAsync();
        }

        public async Task<List<UsuarioVista>> ObtenerUsuariosPorCriterioAsync(string criterio)
        {
            criterio = "%" + criterio + "%";

            string sqlQuery = @"SELECT id_usuario, usuario_nom, nombre, apellido1, apellido2, correoelectronico, cel, 
                                       configurador, regionSSF, desbloquearregistros, tiempoespera
                                FROM ssf.usuarios 
                                WHERE nombre like @pCriterio OR apellido1 LIKE @pCriterio OR apellido2 LIKE @pCriterio 
                                OR usuario_nom LIKE @pCriterio";

            object[] parametros = new object[]
            {
                new SqlParameter("@pCriterio", criterio)
            };

            return await _userContext.UsuariosVista.FromSqlRaw(sqlQuery, parametros).ToListAsync();
        }

        public async Task<List<UsuarioVista>> ObtenerUsuariosDeDocumentoAsync(int id)
        {
            string sqlQuery = @"SELECT a.id_usuario, a.usuario_nom, a.nombre, a.apellido1, a.apellido2, a.correoelectronico, a.cel, a.configurador, a.regionSSF, a.desbloquearregistros, a.tiempoespera
                                FROM ssf.usuarios a
                                JOIN ssf.usuariodocumento b ON a.id_usuario=b.id_usuario
                                WHERE b.id_documentopatrullaje=@pIdDocumento";

            object[] parametros = new object[]
            {
                new SqlParameter("@pIdDocumento", id)
            };

            return await _userContext.UsuariosVista.FromSqlRaw(sqlQuery, parametros).ToListAsync();
        }

        public async Task<List<UsuarioVista>> ObtenerUsuariosNoIncluidosEnDocumentoAsync(string criterio, int idDocumento)
        {
            string sqlQuery = @"SELECT id_usuario, usuario_nom,nombre,apellido1,apellido2,correoelectronico,cel,configurador,regionSSF,desbloquearregistros,tiempoespera
                                FROM ssf.usuarios 
                                WHERE id_usuario NOT IN (SELECT id_usuario FROM ssf.usuariodocumento WHERE id_documentopatrullaje=@pIdDocumento)
                                AND (nombre like @pCriterio OR apellido1 LIKE @pCriterio OR apellido2 LIKE @pCriterio 
                                OR usuario_nom LIKE @pCriterio)";

            object[] parametros = new object[]
            {
                new SqlParameter("@pIdDocumento", criterio),
                new SqlParameter("@pIdDocumento", idDocumento)
            };

            return await _userContext.UsuariosVista.FromSqlRaw(sqlQuery, parametros).ToListAsync();

        }

        //Métodos de Usuarios comandacia
        public async Task<List<UsuarioComandancia>> ObtenerUsuariosComandanciaPorIdUsuarioAndIdComandanciaAsync(int idUsuario, int idComandancia)
        {
            return await _userContext.UsuariosComandancia.Where(x => x.IdUsuario == idUsuario && x.IdComandancia == idComandancia).ToListAsync();          
        }

        public async Task AgregaListaDeUsuariosComandanciaAsync(List<UsuarioComandancia> usuarios)
        {
            _userContext.UsuariosComandancia.AddRange(usuarios);
            await _userContext.SaveChangesAsync();
        }

        public async Task BorraListaDeUsuariosComandanciaAsync(List<UsuarioComandancia> usuarios)
        {
                _userContext.UsuariosComandancia.RemoveRange(usuarios);
                await _userContext.SaveChangesAsync();
        }

        //Métodos de Usuarios Rol
        public async Task<List<UsuarioRol>> ObtenerUsuariosRolPorIdUsuarioAndIdRolAsync(int idUsuario, int idRol)
        {
            return await _userContext.UsuariosRol.Where(x => x.IdUsuario == idUsuario && x.IdRol == idRol).ToListAsync();
        }

        public async Task AgregaListaDeUsuariosRolAsync(List<UsuarioRol> usuarios)
        {
            _userContext.UsuariosRol.AddRange(usuarios);
            await _userContext.SaveChangesAsync();
        }

        public async Task BorraListaDeUsuariosRolAsync(List<UsuarioRol> usuarios)
        {
            _userContext.UsuariosRol.RemoveRange(usuarios);
            await _userContext.SaveChangesAsync();
        }

        //Métodos de UsuarioGrupoCorreoElectronico
        public async Task<List<UsuarioGrupoCorreoElectronico>> ObtenerUsuariosGrupoCorreoElectronicoPorIdUsuarioAndIdGrupoAsync(int idUsuario, int idGrupo)
        {
            return await _userContext.UsuariosGrupoCorreoElectronico.Where(x => x.IdUsuario == idUsuario && x.IdGrupoCorreo == idGrupo).ToListAsync();
        }

        public async Task AgregaListaDeUsuariosGrupoCorreoElectronicoAsync(List<UsuarioGrupoCorreoElectronico> usuarios)
        {
            _userContext.UsuariosGrupoCorreoElectronico.AddRange(usuarios);
            await _userContext.SaveChangesAsync();
        }

        public async Task BorraListaDeUsuariosGrupoCorreoElectronicoAsync(List<UsuarioGrupoCorreoElectronico> usuarios)
        {
            _userContext.UsuariosGrupoCorreoElectronico.RemoveRange(usuarios);
            await _userContext.SaveChangesAsync();
        }


/*        public async Task<Usuario?> ObtenerUsuarioRegistradoAsync(string usuario)
        {
            return await _userContext.Usuarios.Where(x => x.UsuarioNom == usuario).SingleOrDefaultAsync();
        }*/

        public async Task<int> IdentificaUsuarioLocalAsync(string usuario, string clave)
        {
            var md5Pass = ComputeMD5(clave);

            var u = await _userContext.Usuarios.Where(x => x.UsuarioNom == usuario && x.Pass == md5Pass).ToListAsync();
            return u.Count;
        }

        private string ComputeMD5(string s)
        {
            using (MD5 md5 = MD5.Create())
            {
                return BitConverter.ToString(md5.ComputeHash(Encoding.UTF8.GetBytes(s)))
                            .Replace("-", "").ToLower();
            }
        }

    }
}
