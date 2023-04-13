using Domain.Entities;
using Domain.Entities.Vistas;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Ports.Driven
{
    public interface IUsuariosCommand
    {
        Task DesbloqueaUsuarioAsync(string usuario);
        Task ReiniciaClaveUsuarioAsync(string usuario);
        Task BloqueaUsuarioAsync(string usuario);
        Task AgregaUsuarioDeDocumentoAsync(int idDocumento, int idUsuario);
        Task BorraUsuarioDeDocumentoAsync(int idDocumento, int idUsuario);
    }

    public interface IUsuariosQuery
    {
        //Task<Usuario?> ObtenerUsuarioPorCriterioAsync(string criterio);
        Task<List<UsuarioVista>> ObtenerUsuariosPorCriterioAsync(string criterio);
        Task<List<UsuarioVista>> ObtenerUsuariosDeDocumentoAsync(int id);
        Task<List<UsuarioVista>> ObtenerUsuariosNoIncluidosEnDocumentoAsync(string criterio, int idDocumento);
    }

    public interface IUsuariosConfiguradorQuery
    {
        Task<Usuario?> ObtenerUsuarioConfiguradorPorIdAsync(int idUsuario);
        Task<Usuario?> ObtenerUsuarioConfiguradorPorNombreAsync(string usuario);
    }
}
