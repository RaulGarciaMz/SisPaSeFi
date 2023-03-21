using Domain.Entities;
using Domain.Entities.Vistas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Ports.Driven
{
    public interface IPersonalParticipanteCommand
    {
        Task AgregaUsuarioPatrullajeAsync(int idPrograma, int idUsuario);
        Task BorraUsuarioPatrullajeAsync(int idPrograma, int idUsuario);
    }

    public interface IPersonalParticipanteQuery
    {
        Task<List<PersonalParticipanteVista>> ObtenerPersonalAsignadoEnProgramaAsync(int idPrograma);
        Task<List<PersonalParticipanteVista>> ObtenerPersonalNoAsignadoEnProgramaAsync(int idPrograma, int region);
        Task<List<UsuarioPatrullaje>> ObtenerUsuarioPatrullajeAsignadoEnProgramaAsync(int idPrograma, int idUsuario);

    }
}
