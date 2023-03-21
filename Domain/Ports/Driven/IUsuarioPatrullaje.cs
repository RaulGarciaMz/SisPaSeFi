using Domain.Entities;
using Domain.Entities.Vistas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Ports.Driven
{
    public interface IUsuarioPatrullajeCommand
    {
        Task AgregaUsuarioPatrullajeAsync(int idPrograma, int idUsuario);
        Task BorraUsuarioPatrullajeAsync(int idPrograma, int idUsuario);
    }

    public interface IUsuarioPatrullajeQuery
    {
        Task<List<UsuarioPatrullajeVista>> ObtenerPersonalAsignadoEnProgramaAsync(int idPrograma);
        Task<List<UsuarioPatrullajeVista>> ObtenerPersonalNoAsignadoEnProgramaAsync(int idPrograma, int region);
        Task<List<UsuarioPatrullaje>> ObtenerUsuarioPatrullajeAsignadoEnProgramaAsync(int idPrograma, int idUsuario);

    }
}
