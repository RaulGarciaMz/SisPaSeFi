using Domain.Entities;
using Domain.Entities.Vistas;
using Domain.Ports.Driven.Repositories;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using SqlServerAdapter.Data;
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

        public async Task BloqueaUsuarioAsync(string usuario)
        {
            var record = await _userContext.Usuarios.Where(x => x.UsuarioNom == usuario).FirstOrDefaultAsync();

            if (record != null) 
            {
                record.Bloqueado = 1;
                _userContext.Usuarios.Update(record);
                await _userContext.SaveChangesAsync();
            }
        }

        public async Task DesbloqueaUsuarioAsync(string usuario)
        {
            var record = await _userContext.Usuarios.Where(x => x.UsuarioNom == usuario).FirstOrDefaultAsync();

            if (record != null)
            {
                record.Bloqueado = 0;
                record.Intentos = 0;
                _userContext.Usuarios.Update(record);
                await _userContext.SaveChangesAsync();
            }
        }

        public async Task ReiniciaClaveUsuarioAsync(string usuario)
        {
            var record = await _userContext.Usuarios.Where(x => x.UsuarioNom == usuario).FirstOrDefaultAsync();

            if (record != null)
            {
                record.Pass = ComputeMD5(usuario);
                _userContext.Usuarios.Update(record);
                await _userContext.SaveChangesAsync();
            }
        }

        public async Task AgregaUsuarioDeDocumentoAsync(int idDocumento, int idUsuario)
        {
            var u = new UsuarioDocumento()
            {
                IdDocumentoPatrullaje = idDocumento,
                IdUsuario = idUsuario
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

        public async Task<Usuario?> ObtenerUsuarioConfiguradorPorIdAsync(int idUsuario)
        {
            return await _userContext.Usuarios.Where(x => x.IdUsuario == idUsuario && x.Configurador == 1).FirstOrDefaultAsync();
        }

        public async Task<Usuario?> ObtenerUsuarioConfiguradorPorNombreAsync(string usuario)
        {
            return await _userContext.Usuarios.Where(x => x.UsuarioNom == usuario && x.Configurador == 1).FirstOrDefaultAsync();
        }

        public async Task<List<UsuarioVista>> ObtenerUsuariosPorCriterioAsync(string criterio)
        {
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


        //Métodos para el controlador de registro
        public async Task<UsuarioRegistroVista?> ObtenerUsuarioParaRegistroAsync(string usuario)
        {
            UsuarioRegistroVista? ur = null;
            var u = await _userContext.Usuarios.Where(x => x.UsuarioNom == usuario).SingleOrDefaultAsync();

            if (u != null)
            {
                ur = new UsuarioRegistroVista() 
                { 
                    nombre = u.Nombre,
                    apellido1 = u.Apellido1,
                    apellido2 = u.Apellido2,
                    cel = u.Cel,
                    configurador = u.Configurador,
                    bloqueado = u.Bloqueado,
                    AceptacionAvisoLegal = u.AceptacionAvisoLegal,
                    intentos = u.Intentos,
                    NotificarAcceso = u.NotificarAcceso,
                    EstampaTiempoUltimoAcceso = u.EstampaTiempoUltimoAcceso,
                    correoelectronico = u.CorreoElectronico,
                    regionSSF = u. RegionSsf,
                    tiempoEspera = u.TiempoEspera,
                    desbloquearregistros = u.DesbloquearRegistros
                };
            }

            return ur;
        }

        public async Task<bool> SaveChangesAsync()
        {
            return (await _userContext.SaveChangesAsync() >= 0);
        }

        private string ComputeMD5(string s)
        {
            using (MD5 md5 = MD5.Create())
            {
                return BitConverter.ToString(md5.ComputeHash(Encoding.UTF8.GetBytes(s)))
                            .Replace("-", "");
            }
        }


    }
}
