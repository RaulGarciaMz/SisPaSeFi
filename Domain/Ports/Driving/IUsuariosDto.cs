using Domain.DTOs;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Ports.Driving
{
    public interface IUsuariosDtoCommand
    {
        Task BloqueaUsuarioAsync(string usuario);
        Task DesbloqueaUsuarioAsync(string usuario);
        Task ReiniciaClaveUsuarioAsync(string usuario);
    }

    public interface IUsuariosDtoQuery
    {
        Task<UsuarioDto?> ObtenerUsuarioConfiguradorPorIdAsync(int idUsuario);
        Task<UsuarioDto?> ObtenerUsuarioConfiguradorPorNombreAsync(string usuario);
        Task<UsuarioDto?> ObtenerUsuarioPorCriterioAsync(string criterio);
    }
}
