using Domain.Entities;
using Domain.Entities.Vistas;
using Domain.Ports.Driven.Repositories;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using SqlServerAdapter.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SqlServerAdapter
{
    public class UsuarioPatrullajeRepository : IUsuarioPatrullajeRepo
    {
        protected readonly UsuarioPatrullajeContext _userPatrullajeContext;

        public UsuarioPatrullajeRepository(UsuarioPatrullajeContext userpatrullajeContext)
        {
            _userPatrullajeContext = userpatrullajeContext ?? throw new ArgumentNullException(nameof(userpatrullajeContext));
        }

        public async Task<List<UsuarioPatrullajeVista>> ObtenerPersonalAsignadoEnProgramaAsync(int idPrograma) 
        {
            string sqlQuery = @"SELECT a.id_usuario, a.usuario_nom, a.nombre, a.apellido1, a.apellido2, a.correoelectronico, a.cel, a.configurador
                                FROM ssf.usuarios a
                                JOIN ssf.usuariopatrullaje b ON a.id_usuario=b.id_usuario
                                WHERE b.id_programa = @pIdPrograma";

            object[] parametros = new object[]
            {
                new SqlParameter("@pIdPrograma", idPrograma)

            };

            return await _userPatrullajeContext.UsuariosPatrullajeVista.FromSqlRaw(sqlQuery, parametros).ToListAsync();
        }

        public async Task<List<UsuarioPatrullajeVista>> ObtenerPersonalNoAsignadoEnProgramaAsync(int idPrograma, int region)
        {
            string sqlQuery = @"SELECT a.id_usuario, a.usuario_nom, a.nombre, a.apellido1, a.apellido2, a.correoelectronico, a.cel, a.configurador
                                FROM ssf.usuarios a
                                WHERE a.regionssf=@pRegion
                                AND a.id_usuario NOT IN (SELECT id_usuario FROM ssf.usuariopatrullaje WHERE id_programa=@pIdPrograma)";

            object[] parametros = new object[]
            {
                new SqlParameter("@pRegion", region),
                new SqlParameter("@pIdPrograma", idPrograma)

            };

            return await _userPatrullajeContext.UsuariosPatrullajeVista.FromSqlRaw(sqlQuery, parametros).ToListAsync();
        }

        public async Task<List<UsuarioPatrullaje>> ObtenerUsuarioPatrullajeAsignadoEnProgramaAsync(int idPrograma, int idUsuario)
        {
            return await _userPatrullajeContext.UsuariosPatrullaje.Where(x => x.IdPrograma == idPrograma && x.IdUsuario == idUsuario).ToListAsync();  
        }

        public async Task AgregaUsuarioPatrullajeAsync(int idPrograma, int idUsuario)
        {
            var u = new UsuarioPatrullaje() 
            {
                IdPrograma = idPrograma,
                IdUsuario = idUsuario
            };


            _userPatrullajeContext.UsuariosPatrullaje.Add(u);
            await _userPatrullajeContext.SaveChangesAsync();
        }

        public async Task BorraUsuarioPatrullajeAsync(int idPrograma, int idUsuario)
        {
            var u = await _userPatrullajeContext.UsuariosPatrullaje.Where(x => x.IdPrograma == idPrograma && x.IdUsuario == idUsuario).SingleOrDefaultAsync();
            if (u != null)
            {
                _userPatrullajeContext.UsuariosPatrullaje.Remove(u);
                await _userPatrullajeContext.SaveChangesAsync();
            }
        }

    }
}