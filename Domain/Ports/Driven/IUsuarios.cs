using Domain.Entities;
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
    }

    public interface IUsuariosQuery
    {
        Task<Usuario?> ObtenerUsuarioPorCriterioAsync(string criterio);
    }

    public interface IUsuariosConfiguradorQuery
    {
        Task<Usuario?> ObtenerUsuarioConfiguradorPorIdAsync(int idUsuario);
        Task<Usuario?> ObtenerUsuarioConfiguradorPorNombreAsync(string usuario);
    }
}
