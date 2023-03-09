using Domain.Entities;
using Domain.Ports.Driven;
using Domain.Ports.Driven.Repositories;
using Microsoft.EntityFrameworkCore;
using SqlServerAdapter.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

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

        public async Task<Usuario?> ObtenerUsuarioConfiguradorPorIdAsync(int idUsuario)
        {
            return await _userContext.Usuarios.Where(x => x.IdUsuario == idUsuario && x.Configurador == 1).FirstOrDefaultAsync();
        }

        public async Task<Usuario?> ObtenerUsuarioConfiguradorPorNombreAsync(string usuario)
        {
            return await _userContext.Usuarios.Where(x => x.UsuarioNom == usuario && x.Configurador == 1).FirstOrDefaultAsync();
        }

        public async Task<Usuario?> ObtenerUsuarioPorCriterioAsync(string criterio)
        {
            return await _userContext.Usuarios.Where(x => x.Nombre.Contains(criterio) || x.Apellido1.Contains(criterio) || x.Apellido2.Contains(criterio) || x.UsuarioNom.Contains(criterio)).FirstOrDefaultAsync(); 
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
