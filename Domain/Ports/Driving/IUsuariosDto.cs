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
        Task BloqueaUsuario(string usuario);
        Task DesbloqueaUsuario(string usuario);
        Task ReiniciaClaveUsuario(string usuario);
    }

    public interface IUsuariosDtoQuery
    {
        Task<UsuarioDto?> ObtenerUsuarioConfiguradorPorId(int idUsuario);

        Task<UsuarioDto?> ObtenerUsuarioConfiguradorPorNombre(string usuario);
        Task<UsuarioDto?> ObtenerUsuarioPorCriterio(string criterio);
    }
}
